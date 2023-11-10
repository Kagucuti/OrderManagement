using OrderManager.Domain.Enums;
using OrderManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Data_Transfer_Objects
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public Status Status { get; set; }
		public string ClientName { get; set; }
		public double OrderPrice { get; set; }
		public List<OrderLineDTO> OrderLines { get; set; } 
	}


}
