using AutoMapper;
using ECommerce.Core;
using ECommerce.Core.model.OrderAggrgate;
using ECommerce.Core.Service;
using ECommerce.Service;
using ECommerceApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService , IMapper mapper , IUnitOfWork unitOfWork)
        {
            this.orderService = orderService;
            this.mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder (OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var Order = await orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId,orderDto.DeliverymethodId, MappedAddress);
            if (Order == null) return BadRequest("There is a propblem with your ordeer");
            return Ok(Order);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await orderService.GetOrderForSpecificUse(BuyerEmail);
            if (Orders == null) return NotFound("There is no Orders for This User");
            var MappedOrders = mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);
            return Ok(MappedOrders);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByIdForUser(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await orderService.GetOrderByIdForSpecificUserAsync(BuyerEmail, id);
            if (Orders == null) return NotFound("There is no Orders for This User");
            var MappedOrder = mapper.Map<Order, OrderToReturnDto>(Orders);
            return Ok(MappedOrder);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<Deliverymethod>>> GetDeliverymethods()
        {
            var DeliveryMethod = await _unitOfWork.Repository<Deliverymethod>().GetAllAsync();
            return Ok(DeliveryMethod);
        }
    }
}
