using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebUI.Models;
using WebUI.Controllers;
using WebUI.Infrastructure;
using WebUI.Extension;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Business.Abstract;
using Business.Entities;
using WebUI.Models.Contractor;
using System.Data.Entity;

namespace WebUI.Controllers
{

    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .Map(m =>
            {
                m.ToTable("UserRoles");
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
            });
        }
        public DbSet<user_role> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
    }
}

