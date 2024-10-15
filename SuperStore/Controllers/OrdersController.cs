using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core;
using SuperStore.Core.Entities.Order_Aggregate;
using SuperStore.Core.Services.Contracts;
using SuperStore.DTOs;
using SuperStore.Errors;

namespace SuperStore.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IMapper mapper,IOrderService orderService,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto?>> CreateOrder(OrderDto orderDto)
        {
            var Address = _mapper.Map<Address>(orderDto.ShippingAddress);
            var Order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, Address);
            if (Order is null) return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<OrderToReturnDto>(Order));
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>?>> GetAllOrdersForUser(string buyerEmail)
        {
            var Orders =await _orderService.GetAllOrdersForUserAsync(buyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(Orders));
        }
        [HttpGet("GetOrder")]
        public async Task<ActionResult<OrderToReturnDto?>> GetOrderForUser(string buyerEmail,int OrderId)
        {
            var Order =await _orderService.GetOrderByIdForUserAsync(buyerEmail, OrderId);
            if (Order is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<OrderToReturnDto>(Order));
        }

        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethods()
        {
            var DeliveryMethods =await _unitOfWork.Repositery<DeliveryMethod>().GetAllAsync();
            return Ok(DeliveryMethods);
        }
    }
}
