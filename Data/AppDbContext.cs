using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Management.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bank.Management.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        //it is just for creating roles to user
        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     base.OnModelCreating(builder);
        //     SeedRoles(builder);
        // }
        // private void SeedRoles(ModelBuilder builder){
        //     builder.Entity<IdentityRole>().HasData(
        //         new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
        //         new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
        //     );
        // }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions{get; set;}
    }
}