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
		public ChinaContext(DbContextOptions options):base(options)
		{
		}

		public DbSet<Order> Orders { get; set; }

	}
}
