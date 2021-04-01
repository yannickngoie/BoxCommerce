using AutoMapper;
using Basket.API.GrpcServices;
using Basket.API.Models;
using Basket.API.Repositories.Interfaces;
using EventBus.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        // private readonly DiscountGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            // Communicate with Discount.Grpc and calculate lastest prices of products into sc
            //foreach (var item in basket.Items)
            //{
            //    var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            //    item.Price -= coupon.Amount;
            //}

            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {

            var basketCheckoutItems = basketCheckout.Items;
            var basketComponents = basketCheckout.Components;

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            var components = new List<Models.Component>();

            eventMessage.BasketCheckoutItemEvents = new List<BasketCheckoutItemEvent>();
            eventMessage.BasketEventComponents = new List<BasketEventComponent>();

            if (basketCheckoutItems != null)
            {
                foreach (var item in basketCheckoutItems)
                {
                    var result = _mapper.Map<BasketCheckoutItemEvent>(item);
                    eventMessage.BasketCheckoutItemEvents.Add(result);
               }

            }

            if (basketComponents != null)
            {
                foreach (var item in basketComponents)
                {
                    var result = _mapper.Map<BasketEventComponent>(item);
                    eventMessage.BasketEventComponents.Add(result);
                }
            }

            eventMessage.TotalPrice = eventMessage.TotalPrice;
            await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);

            // remove the basket
            await _repository.DeleteBasket(basketCheckout.UserName);

            return Accepted();

        }
    }
}
