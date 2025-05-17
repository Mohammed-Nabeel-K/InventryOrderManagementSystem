using AutoMapper;
using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.Models.Common;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using System.Net;

namespace InventryOrderManagementSystem.BLL.Services
{
    public class OrderServices(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper,
        ICustomerRepository customerRepository) : IOrderServices
    {
        public async Task<Response<OrderGetDto>> AddOrderAsync(OrderDto orderDto, Guid UserId)
        {
            var customerExists = await customerRepository.IsCustomerExists(orderDto.CustomerId);
            if (!customerExists)
            {
                return new Response<OrderGetDto>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Customer Not Found"
                };
            }
            var order = mapper.Map<Order>(orderDto);
            foreach (var items in order.Items)
            {
                var product = await productRepository.ProductById(items.ProductId);
                if (product == null )
                {
                    return new Response<OrderGetDto>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Product with id {items.ProductId} not found"
                    };
                }else if (!product.IsActive || product.QuantityInStock <= items.Quantity)
                {
                    return new Response<OrderGetDto>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Product not available now"
                    };
                }
                    items.UnitPrice = product.Price;
                items.Total = items.Quantity * items.UnitPrice;
            }
            order.Status = OrderStatus.Pending;
            order.CreatedBy = UserId;
            order.TotelAmount = order.Items.Sum(i => i.Total);
            order.CreatedAt = DateTime.UtcNow;
            order.LastUpdatedAt = DateTime.UtcNow;
            var Createdorder = await orderRepository.AddorderAsync(order);
            return new Response<OrderGetDto>
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Order created successfully",
                Data = mapper.Map<OrderGetDto>(Createdorder)
            };
        }
        public async Task<Response<OrderDto>> GetOrderByIdAsync(Guid id)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return new Response<OrderDto>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Order not found"
                };
            }
            var orderDto = mapper.Map<OrderDto>(order);
            return new Response<OrderDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Order retrieved successfully",
                Data = orderDto
            };
        }
    }
}
