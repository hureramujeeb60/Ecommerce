using Microsoft.AspNetCore.Mvc;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.DTO;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("Customer Data is null");
            }

            var customer = new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
            };

            var success = await _unitOfWork.Customers.AddAsync(customer);

            if(!success || await _unitOfWork.CompleteAsync() == 0)
            {
                return StatusCode(500, "Failed to Add Customer");
            }

            return Ok("Customer Added Successfully");

        }

    }
}
