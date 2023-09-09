using ETrade.Application.Abstractions;
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

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        [HttpGet]
        public async Task Get()
        {
           await _productWriteRepository.AddRangeAsync(new()
            {
                new(){Name="Product1",Stock=20,CreateDate=DateTime.Now,Price=200},
                new(){Name="Product2",Stock=30,CreateDate=DateTime.Now,Price=300},
                new(){Name="Product3",Stock=70,CreateDate=DateTime.Now,Price=500},
                new(){Name="Product4",Stock=10,CreateDate=DateTime.Now,Price=100},
                new(){Name="Product5",Stock=90,CreateDate=DateTime.Now,Price=700},
                new(){Name="Product6",Stock=120,CreateDate=DateTime.Now,Price=600}
            });
            await _productWriteRepository.SaveAsync();
        }
        //[HttpGet]

        //public IActionResult GetAll()
        //{
        //    return Ok(_productReadRepository.GetAll());
        //}
        
    }
}
