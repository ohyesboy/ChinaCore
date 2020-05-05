using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChinaCore.Controllers;
using Microsoft.EntityFrameworkCore;

namespace ChinaCore
{
	public interface IMyDataService
	{
		void SaveOrder(Order order);
	}

	public class MyDataService : IMyDataService
	{
		public void SaveOrder(Order order)
		{

		}

	}

	public class ChinaContext:DbContext
	{
		public ChinaContext()
		{
			
		}
		public DbSet<Order> Orders { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{       
          
			var cs = "Server=localhost;Database=China;uid=sa;pwd=password*8;Connection Timeout=10";
			optionsBuilder.UseSqlServer(cs);
			base.OnConfiguring(optionsBuilder);
		}
	}
}
