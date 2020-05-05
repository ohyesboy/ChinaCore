using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChinaCore.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[EnableCors("MyPolicy")]
	public class OrdersController : ControllerBase
	{
		private IMyDataService _dataService;
		private ILogger<OrdersController> _logger;
		private IConfiguration _config;

		public OrdersController(
			IMyDataService dataService,
			ILogger<OrdersController> logger,
			IConfiguration config)
		{
			_config = config;
			_logger = logger;
			_dataService = dataService;

		}
		[HttpPost]
		public IActionResult Post(Order order)
		{
			var db = new ChinaContext();
			db.Orders.Add(order);
			db.SaveChanges();
			return Ok();
		}

		[HttpGet]
		public IActionResult Get()
		{
			var db = new ChinaContext();
			db.Database.EnsureCreated();
			var orders = db.Orders
				.Include(x=>x.Items)
				.OrderBy(x=>x.Done)
				.ThenByDescending(x=>x.Time)
				.ToList();

			return Ok(orders);
		}

		[HttpGet("clearDone")]
		public IActionResult ClearDone()
		{
			var db = new ChinaContext();

			var orders = db.Orders
				.Where(x=>x.Done)
				.ToList();
			db.RemoveRange(orders);
			db.SaveChanges();
			return Ok();
		}

		[HttpGet("done/{id}")]
		public IActionResult Done(int id)
		{
			var db = new ChinaContext();

			var order = db.Orders
				.First(x => x.Id == id);
			order.Done = true;

			db.SaveChanges();
			return Ok();
		}
	}

	public class Order
	{
		[Key]
		public int Id { get; set; }
		public string CustomerName { get; set; }
		public DateTime Time { get; set; }

		public IList<OrderItem> Items { get; set; }

		public bool Done { get; set; }
	}

	public class OrderItem
	{

		[Key]
		public int Id { get; set; }

		public int OrderId { get; set; }

		public int Count { get; set; }
		public string Title { get; set; }
		public string Label { get; set; }

		public float Price { get; set; }


	}
}