using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrderManage.Infrastructure1.Data;
using OrderManager.Application.Mapping;
using OrderManager.Application.UserEceptions;
using OrderManager.Application.UserExceptions;
using OrderManager.Domain.Data_Transfer_Objects;
using OrderManager.Domain.Enums;
using OrderManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services
{
	public class OrderManagerService : IOrderManagerService
	{
		private readonly DataContext _repository;
		private readonly OrderMapping _mapping;
        public OrderManagerService(DataContext dataContext, OrderMapping orderMapping)
        {
            _repository = dataContext;
			_mapping = orderMapping;
        }
		
        public async Task AddOrderAsync(OrderDTO orderDTO)
		{
			if (orderDTO is null)
				throw new CustomException("Order is not added.");
			var order = _mapping.MapToEntity(orderDTO);
			await _repository.Orders.AddAsync(order);
			await _repository.SaveChangesAsync();
		}

		public async Task DeleteOrderAsync(int id)
		{
			var order = await _repository.DbFindAsync(id);
			if(order is null)
				throw new ObjectNotFoundException($"Order with that ID({id}) is not found!");
			if (order.Status is not Status.New)
				throw new InvalidStatusException("Sorry, this order is not new");
			await _repository.DbDeleteAsync(order);
		}

		public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
		{
			var orders = await _repository.Orders.Include(o => o.OrderLines).ToListAsync();
			return orders.Select(o => _mapping.MapToDTO(o)).ToList();
		}

		public async Task<OrderDTO?> GetSingleOrderAsync(int id)
		{
			var order = await _repository.DbFindAsync(id);
			if(order is null)
				throw new ObjectNotFoundException($"Sorry, the order with Id({id}) doesn't exist");
			return _mapping.MapToDTO(order);
		}

		public async Task UpdateOrderAsync(OrderDTO orderDTO)
		{
			var order = _mapping.MapToEntity(orderDTO);
			var existingOrder = await _repository.DbFindAsync(order);
			if (existingOrder is null)
				throw new ObjectNotFoundException("Sorry, that order is not exist.");
			if (existingOrder.Status is not Status.New)
				throw new InvalidStatusException("Sorry, this order is not new");
			existingOrder.Status = order.Status;
			existingOrder.ClientName = order.ClientName;
			existingOrder.AdditionalInfo = order.AdditionalInfo;
			existingOrder.OrderPrice = order.OrderPrice;
			await _repository.SaveChangesAsync();
			
		}
		public async Task RemoveOrderLineAsync(int orderId, int orderLineId)
		{
			var order = await _repository.DbFindAsync(orderId);
			if (order is null)
				throw new Exception($"Order with ID {orderId} not found.");
			var orderLineToRemove = order.OrderLines.FirstOrDefault(ol => ol.Id == orderLineId);
			if (orderLineToRemove == null)
				throw new Exception($"OrderLine with ID {orderLineId} not found in the order.");
			
			order.OrderLines.Remove(orderLineToRemove);
			if (order.OrderLines.Count is 0)
				await _repository.DbDeleteAsync(order);
			else
				await _repository.SaveChangesAsync();
		}
		public async Task AddOrderLineAsync(int orderId, OrderLineDTO orderLineDTO)
		{
			var order = await _repository.DbFindAsync(orderId);
			if (order is null)
				throw new Exception($"Order with ID {orderId} not found.");

			var existingOrder = await _repository.DbFindAsync(orderLineDTO.OrderId);
			if (existingOrder is null)
				throw new Exception($"Order with ID {orderLineDTO.OrderId} not found.");

			var newOrderLine = new OrderLine
			{
				Product = orderLineDTO.Product,
				OrderId = orderLineDTO.OrderId
			};

			order.OrderLines.Add(newOrderLine);

			await _repository.SaveChangesAsync();
		}
	}
}
