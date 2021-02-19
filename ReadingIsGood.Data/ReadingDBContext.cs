using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using ReadingIsGood.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Data
{
    public partial class ReadingDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReadingDBContext()
        {
        }

        public ReadingDBContext(DbContextOptions<ReadingDBContext> options)
            : base(options)
        {
        }

        public ReadingDBContext(DbContextOptions<ReadingDBContext> options,
            IHttpContextAccessor httpContextAccessor)
             : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.;Database=ReadingDB;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit");

                entity.Property(e => e.Action).HasMaxLength(50);

                entity.Property(e => e.KeyValues).HasMaxLength(500);

                entity.Property(e => e.NewValues).HasMaxLength(500);

                entity.Property(e => e.OldValues).HasMaxLength(500);

                entity.Property(e => e.TableName).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Name).HasMaxLength(350);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Token).HasMaxLength(500);

                entity.Property(e => e.Username).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var temoraryAuditEntities = await AuditNonTemporaryProperties();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await AuditTemporaryProperties(temoraryAuditEntities);
            return result;
        }

        int CurrentUserId => int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        async Task<IEnumerable<Tuple<EntityEntry, Audit>>> AuditNonTemporaryProperties()
        {
            ChangeTracker.DetectChanges();

            var entitiesToTrack = ChangeTracker.Entries().Where(e => !(e.Entity is Audit) && e.State != EntityState.Detached && e.State != EntityState.Unchanged);

            if(!entitiesToTrack.Any())
            {
                return null;
            }

            var userId = CurrentUserId;

            await Audits.AddRangeAsync(
                entitiesToTrack.Where(e => !e.Properties.Any(p => p.IsTemporary)).Select(e => new Audit()
                {
                    TableName = e.Metadata.GetTableName(),
                    Action = Enum.GetName(typeof(EntityState), e.State),
                    CreateDate = DateTime.UtcNow,
                    UserId = userId,
                    KeyValues = JsonConvert.SerializeObject(e.Properties.Where(p => p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()),
                    NewValues = JsonConvert.SerializeObject(e.Properties.Where(p => e.State == EntityState.Added || e.State == EntityState.Modified).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()),
                    OldValues = JsonConvert.SerializeObject(e.Properties.Where(p => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToDictionary(p => p.Metadata.Name, p => p.OriginalValue).NullIfEmpty())
                }).ToList()
            );

            return entitiesToTrack.Where(e => e.Properties.Any(p => p.IsTemporary))
                 .Select(e => new Tuple<EntityEntry, Audit>(e,
                 new Audit()
                 {
                     TableName = e.Metadata.GetTableName(),
                     Action = Enum.GetName(typeof(EntityState), e.State),
                     CreateDate = DateTime.UtcNow,
                     UserId = userId,
                     NewValues = JsonConvert.SerializeObject(e.Properties.Where(p => !p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty())
                 }
                 )).ToList();
        }

        async Task AuditTemporaryProperties(IEnumerable<Tuple<EntityEntry, Audit>> temporatyEntities)
        {
            if (temporatyEntities != null && temporatyEntities.Any())
            {
                await Audits.AddRangeAsync(temporatyEntities.ForEach(t => t.Item2.KeyValues = JsonConvert.SerializeObject(t.Item1.Properties.Where(p => p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()))
                    .Select(t => t.Item2)
                );

                await SaveChangesAsync();
            }

            await Task.CompletedTask;
        }
    }
}