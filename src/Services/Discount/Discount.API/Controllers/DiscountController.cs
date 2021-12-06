using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/[Controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository? repository)
        {
            _repository = repository;
        }

        [HttpGet(template:"{productName}", Name = "GetDiscount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _repository.GetDiscount(productName);
            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            await _repository.CreateDiscount(coupon);

            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return Ok(await _repository.DeleteDiscount(productName));
        }
    }
}
