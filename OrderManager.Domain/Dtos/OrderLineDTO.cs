using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Data_Transfer_Objects
{
	public class OrderLineDTO
	{
		public int Id { get; set; }
		public string Product { get; set; }
	}
}
