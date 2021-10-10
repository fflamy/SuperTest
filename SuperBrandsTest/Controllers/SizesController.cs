using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperBrandsTest.API.Brand.Models;
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
    public class SizesController : ControllerBase
    {
        readonly BrandDBContext _context;
        public SizesController(BrandDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Size>> Get(int brandId)
        {
            return await _context.Sizes.Where(s => s.BrandId == brandId).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(SizeModel sizeModel)
        {
            List<Size> result = new List<Size>();
            if (!await _context.Brands.AnyAsync(b => b.Id == sizeModel.BrandId))
            {
                return StatusCode(404, "Brand not found");
            }
            foreach (var sizeDto in sizeModel.Sizes)
            {
                if (sizeDto != null)
                {
                    Size size = new() { BrandId = sizeModel.BrandId, BrandSize = sizeDto.BrandSize, RussianSize = sizeDto.RussianSize };

                    try
                    {
                        await _context.Sizes.AddAsync(size);
                        await _context.SaveChangesAsync();
                        result.Add(size);
                    }
                    catch (DbUpdateException)
                    {
                        return StatusCode(400, $"Could not add size {sizeDto.RussianSize}, 'cause it is probably already presented. Operation aborted.");
                    }
                    catch (Exception e)
                    {
                        return StatusCode(500, e.Message);
                    }
                    
                }
            }
            return Ok(result);

        }
        [HttpPost("CheckSize")]
        public async Task<int> CheckSize(SizeDto sizeDto)
        {
            if (sizeDto is null)
            {
                return 0;
            }
            int result = await _context.Sizes.Where(s => s.BrandId == sizeDto.BrandId && (s.BrandSize == sizeDto.BrandSize || s.RussianSize == sizeDto.RussianSize)).Select(s => s.Id).FirstOrDefaultAsync();
            return result;
        }
    }
}
