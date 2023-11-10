using OrderManager.Domain.Data_Transfer_Objects;
using OrderManager.Domain.Models;
namespace OrderManager.Application.Mapping
{
	public class OrderMapping
	{
		public OrderDTO MapToDTO(Order order)
		{
			if (order is null)
				return null;
			
			var orderDTO = new OrderDTO
			{
				Id = order.Id,
				Status = order.Status,
				ClientName = order.ClientName,
				OrderPrice = order.OrderPrice,
				OrderLines = order.OrderLines.AsParallel().Select(ol => MapToDTO(ol)).ToList()
			};
			return orderDTO;
		}

		public OrderLineDTO MapToDTO(OrderLine orderLine)
		{
			if (orderLine == null)
				return null;

			var orderLineDTO = new OrderLineDTO
			{
				Id = orderLine.Id,
				Product = orderLine.Product,
			};

			return orderLineDTO;
		}
		public Order MapToEntity(OrderDTO orderDTO)
		{
			if (orderDTO is null)
				return null;

			var order = new Order
			{
				Id = orderDTO.Id,
				Status = orderDTO.Status,
				ClientName = orderDTO.ClientName,
				OrderPrice = orderDTO.OrderPrice,
				OrderLines = orderDTO.OrderLines.AsParallel().Select(olDTO => MapToEntity(olDTO)).ToList()
			};

			return order;
		}

		public OrderLine MapToEntity(OrderLineDTO orderLineDTO)
		{
			if (orderLineDTO is null)
				return null;
			

			var orderLine = new OrderLine
			{
				Id = orderLineDTO.Id,
				Product = orderLineDTO.Product,
			};

			return orderLine;
		}


	}
}
