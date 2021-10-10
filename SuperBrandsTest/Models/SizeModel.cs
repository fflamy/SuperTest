using SuperBrandsTest.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBrandsTest.API.Brand.Models
{
    public class SizeModel
    {
        public List<SizeDto> Sizes { get; set; }
        public int BrandId { get; set; }
    }
}
