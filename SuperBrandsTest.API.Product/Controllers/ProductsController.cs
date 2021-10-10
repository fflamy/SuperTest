using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SuperBrandsTest.API.Product.Models;
using SuperBrandsTest.Entities;
using SuperBrandsTest.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SuperBrandsTest.API.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly BrandDBContext _context;

        public ProductsController(BrandDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ProductDto> Get(int productId)
        {
            return await _context.Products.Include(p => p.Brand).Include(p => p.Size).Where(p => p.Id == productId).Select(p => new ProductDto { Brand = p.Brand.Name, Name = p.Name, RussianSize = p.Size.RussianSize }).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post(Entities.Models.Product productModel)
        {
            if (!await _context.Brands.AnyAsync(b => b.Id == productModel.BrandId))
            {
                return StatusCode(404, "BrandNotFound");
            }
            if (!productModel.Validate())
            {
                return StatusCode(400, "Product validation failed");
            }
            int sizeId = -1;
            using (HttpClient client = new HttpClient())
            {
                productModel.Size.BrandId = productModel.BrandId;
                var message = new HttpRequestMessage();
                message.Method = HttpMethod.Post;
                message.Content = new StringContent(JsonConvert.SerializeObject(productModel.Size), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:8087/api/Sizes/CheckSize");
                var response = await client.SendAsync(message);
                string responseString =await response.Content.ReadAsStringAsync();
                if(!int.TryParse(responseString, out sizeId))
                {
                    return StatusCode(500);
                }
                if (sizeId <= 0)
                {
                    return StatusCode(400, "Size not found");
                }
            }

          
            productModel.Size = null;
            productModel.SizeId = sizeId;
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return Ok(productModel);
        }
    }
}
