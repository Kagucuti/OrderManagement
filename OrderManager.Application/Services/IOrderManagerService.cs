using OrderManager.Domain.Data_Transfer_Objects;
using OrderManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services
{
    public interface IOrderManagerService
    {
		Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
		Task<OrderDTO?> GetSingleOrderAsync(int id);
		Task UpdateOrderAsync(OrderDTO orderDTO);
		Task DeleteOrderAsync(int id);
		Task AddOrderAsync(OrderDTO orderDTO);
		Task RemoveOrderLineAsync(int orderId, int orderLineId);
		Task AddOrderLineAsync(int orderId, OrderLineDTO orderLineDTO);
	}
}
