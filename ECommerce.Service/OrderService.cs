using ECommerce.Core;
using ECommerce.Core.model;
using ECommerce.Core.model.OrderAggrgate;
using ECommerce.Core.Repositories;
using ECommerce.Core.Service;
using ECommerce.Core.Specifications.OrderSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IBasketRepository basketRepository ,IUnitOfWork unitOfWork)
        {
            this.basketRepository = basketRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int DeliveryMethodId, Address ShippingAddress)
        {
            var Basket = await basketRepository.GetBasketAsync(basketId);
            if (Basket == null)
            {
                throw new Exception("Basket not found.");
            }
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count() > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.id);
                    var pictureUrl = string.IsNullOrEmpty(product.img) ? "mouse.jpg" : product.img;

                    var productItemOrdered = new ProductItemOrder(item.id, product.name, pictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.price,item.Quantity);
                    OrderItems.Add(orderItem);
                }
            }
            var subTotal = OrderItems.Sum(item => item.Price*item.Quantity);
            var DeliveryMethod = await _unitOfWork.Repository<Deliverymethod>().GetByIdAsync(DeliveryMethodId);
            var Order = new Order(buyerEmail, ShippingAddress, DeliveryMethod, OrderItems, subTotal);
            await _unitOfWork.Repository<Order>().AddAsync(Order);
        var Result =    await _unitOfWork.CompleteAsync();
            if (Result <= 0) return null;
            return Order;
        }

        public async Task<Order> GetOrderByIdForSpecificUserAsync(string buyerEmail ,int OrderId)
        {
            var Spec = new OrderSpecification(buyerEmail, OrderId);
            var Order = await _unitOfWork.Repository<Order>().GetByIdWithSpenAsync(Spec);
            return Order;
        }

        public Task<IReadOnlyList<Order>> GetOrderForSpecificUse(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var Orders = _unitOfWork.Repository<Order>().GetAllWithSpenAsync(spec);
            return Orders;
        }
    }
}
