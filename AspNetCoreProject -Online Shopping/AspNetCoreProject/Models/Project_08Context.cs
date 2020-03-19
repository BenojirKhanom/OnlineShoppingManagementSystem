using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNetCoreProject.Models;

namespace AspNetCoreProject.Models
{
    //public class Project_08Context : DbContext
    //{
    //    public Project_08Context (DbContextOptions<Project_08Context> options)
    //        : base(options)
    //    {
    //    }

    //    public DbSet<Project_08.Models.StudentModel> StudentModel { get; set; }
    //}


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }


        public DbSet<AspNetCoreProject.Models.StudentModel> StudentModel { get; set; }


        public DbSet<AspNetCoreProject.Models.MenuHelperModel> MenuHelperModel { get; set; }


        public DbSet<AspNetCoreProject.Models.MenuModel> MenuModel { get; set; }


        public DbSet<AspNetCoreProject.Models.MenuModelManage> MenuModelManage { get; set; }


        public DbSet<AspNetCoreProject.Models.Student_Result> Student_Result { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerReview> CustomerReviews { get; set; }
        public virtual DbSet<DeliveryBoy> DeliveryBoys { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<OrderDtl> OrderDtls { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShippingInfo> ShippingInfoes { get; set; }
    }
    public class SelectedInfoModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<ShippingInfo> ShippingInfos { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }


}
