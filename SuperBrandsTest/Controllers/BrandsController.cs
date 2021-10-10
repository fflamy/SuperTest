using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperBrandsTest.Entities;
using SuperBrandsTest.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBrandsTest.API.Brand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BrandDBContext _context;

        public BrandsController(BrandDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<BrandDto> Get(int brandId)
        {
            BrandDto brand = await _context.Brands.Where(b => b.Id == brandId).Select(b => new BrandDto { Name = b.Name }).FirstOrDefaultAsync();
            if (brand != null)
            {
                brand.Sizes = await _context.Sizes.Where(s => s.BrandId == brandId).Select(s => new SizeDto { BrandSize = s.BrandSize, RussianSize = s.RussianSize, BrandId=s.BrandId }).ToListAsync();
            }
            else
            {
                brand = new BrandDto();
            }
            return brand;
        }

 
        [HttpPost]
        public async Task<IActionResult> Post([FromHeader]string brandName)
        {
            if (string.IsNullOrWhiteSpace(brandName))
            {
                return StatusCode(400, "Bad name");
            }
            if (await _context.Brands.AnyAsync(b => b.Name.ToLower() == brandName.ToLower()))
            {
                return StatusCode(400, "Bad already exists");
            }
            Entities.Models.Brand brand = new() { Name = brandName };

            try
            {
                await _context.AddAsync(brand);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(new { brand.Name, brand.Id });
        }
    }
}
