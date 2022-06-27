using Emarket.Core.Application.ViewModels.User;
using Emarket.Core.Domain.Commons;
using Emarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Emarket.Core.Application.Helpers;

namespace Emarket.Infrastructure.Persistency.Contexts
{
    public class ApplicationContext : DbContext
    {

        private readonly IHttpContextAccessor _httpContext;
        private UserViewModel _userView;

        public ApplicationContext(DbContextOptions dbContextOptions, IHttpContextAccessor httpContext)
        :base(dbContextOptions) 
        {
           _httpContext = httpContext;
           _userView = _httpContext.HttpContext.Session.Get<UserViewModel>("user_session");
        }

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
                        entry.Entity.CreatedBy = _userView == null? "System.Default": _userView.Email;
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _userView.Email;
                        entry.Entity.LastModifiedAt = DateTime.Now;
                        break;
                }

            }

            return base.SaveChangesAsync(cancellationToken); //Still it has its parent behavior and order added by the child. //SOLID
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

            modelBuilder.Entity<Advertisement>()
                .Property(ad => ad.Price)
                .HasColumnType(typeName: "decimal")
                .HasPrecision(6,2);

        }


        #endregion


    }


}
