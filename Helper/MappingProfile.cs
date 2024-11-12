using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Models;

namespace Ecommerce.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>();
                
        }
    }
}
