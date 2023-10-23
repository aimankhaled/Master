using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Xml.Linq;
using POS.Server.Domain.Entities;

namespace POS.Server.InfaStructure
{
    public class DBContext : DbContext
    {
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Products> Products { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>().HasKey(x => x.OrderId);
            modelBuilder.Entity<OrderItems>().HasKey(x => x.OrderItemId);
            modelBuilder.Entity<Products>().HasKey(x => x.ProductID);
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {


        }
    }
}

