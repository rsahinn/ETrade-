using ETrade.Application.Abstractions;
using ETrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ETrade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly ICustomerReadRepository _customerReadRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _customerWriteRepository = customerWriteRepository;
            _customerReadRepository = customerReadRepository;
        }
        [HttpGet]
        public async Task Get()
        {

            //await _customerWriteRepository.AddAsync(new Customer() { Name = "Muhiddin"});
            //await _customerWriteRepository.SaveAsync();

            //await _orderWriteRepository.AddAsync(new Order { Address = "Türkiye,İstanbul", Description = "BlaBlaBla", CustomerId = 4 });
            //await _orderWriteRepository.SaveAsync();

            Order order= await _orderReadRepository.GetByIdAsync(3);
            order.Address = "İstanbul,Esenler";
            await _orderWriteRepository.SaveAsync();

        }


    }
}
