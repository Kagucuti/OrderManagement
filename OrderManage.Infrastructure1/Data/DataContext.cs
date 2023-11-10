using Microsoft.EntityFrameworkCore;
using OrderManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManage.Infrastructure1.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TEST_DB;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("OrderManager.API"));
			}
		}
		public DbSet<OrderLine> OrderLines { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>()
				.HasMany(o => o.OrderLines)
				.WithOne(ol => ol.Order)
				.HasForeignKey(ol => ol.OrderId);
		}
		public async Task<Order?> DbFindAsync(int id)
			=> await Orders.FirstOrDefaultAsync(o => o.Id == id);
		public async Task<Order?> DbFindAsync(Order order)
			=> await Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
		public async Task DbAddAsync(Order order)
		{
			Orders.AddAsync(order);
			await SaveChangesAsync();
		}
		public async Task DbDeleteAsync(Order order)
		{
			Orders.Remove(order);
		    await SaveChangesAsync();
		}
		public async Task<List<Order>> DbGetAllAsync()
			 => await Orders.ToListAsync();
		


	}
}

