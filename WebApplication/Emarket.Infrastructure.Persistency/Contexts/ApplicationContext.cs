using Emarket.Core.Domain.Commons;
using Emarket.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emarket.Infrastructure.Persistency.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions) {  }

        public DbSet<User> Users { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Category> Categories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach( var entry in ChangeTracker.Entries<AuditableProperties>() )
            {

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "USER SESSION";
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "DefaulUserSystem";
                        entry.Entity.LastModifiedAt = DateTime.Now;
                        break;
                }

            }

            return base.SaveChangesAsync(cancellationToken); //Still has it parent behavior and order added by the child.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //USE THE FLUENT API

            #region Region_Names

                modelBuilder.Entity<User>()
                .ToTable("Users");

                modelBuilder.Entity<Category>()
               .ToTable("Categories");

                modelBuilder.Entity<Advertisement>()
                   .ToTable("Advertisement");

            #endregion


            #region PK
                modelBuilder.Entity<User>().HasKey(user => user.UserId);
                modelBuilder.Entity<Category>().HasKey(category => category.CategoryId);
                modelBuilder.Entity<Advertisement>().HasKey(ads => ads.AdvertisementId);
            #endregion

            #region RelationShips

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Advertisements)
                .WithOne(ads => ads.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Advertisements)
                .WithOne(ads => ads.User)
                .HasForeignKey(ads => ads.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Constraints

            modelBuilder.Entity<User>()
            .Property(user => user.UserName)
            .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.UserName).HasMaxLength(30);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName).IsUnique();

            modelBuilder.Entity<User>()
            .Property(user => user.Password)
            .IsRequired();


        }


        #endregion


    }


}
