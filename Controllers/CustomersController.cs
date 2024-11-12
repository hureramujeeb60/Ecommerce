using Microsoft.AspNetCore.Mvc;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.DTO;
using Ecommerce.Service;
using AutoMapper;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : GenericController<Customer, CustomerDto>
    {
        public CustomersController(IGenericService<Customer> genericService, IMapper mapper) : base(genericService, mapper)
        {
        }
    }
}
