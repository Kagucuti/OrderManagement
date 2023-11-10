using OrderManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrderManager.Domain.Models
{
    
	public class Order
	{
		public int Id { get; set; }

		public DateTime CreateDate { get; set; }
		public Status Status { get; set; }
		public string ClientName { get; set; } = string.Empty;
		public string AdditionalInfo { get; set; } = string.Empty;
		public double OrderPrice { get; set; } = AllPrice();

		public List<OrderLine> OrderLines { get; set; } = orderLines;
		private static List<OrderLine> orderLines = new List<OrderLine>();
		public void AddOrderLine(string _product, double _price)
		{
			OrderLines.Add(new OrderLine() { Product = _product, Price = _price });
		}

		public static double AllPrice()
		{
			double price = 0;
			foreach (var item in orderLines)
			price += item.Price;
			return price;
		}
	}
}