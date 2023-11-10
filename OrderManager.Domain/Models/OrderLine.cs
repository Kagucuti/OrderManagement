using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Models
{
	public class OrderLine
	{
		public int Id { get; set; }
		public string Product { get; set; } 
		public double Price { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; }
	}
}
