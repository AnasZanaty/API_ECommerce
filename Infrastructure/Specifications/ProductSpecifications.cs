using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    //Specification Pattern - Third Step-
    public class ProductSpecifications
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public string? Sort { get; set; }

        private const int maxpagesize = 50;
        public int PageIndex { get; set; } = 1;

        private int pageSize =6;

        public int PageSize
        {

            get => pageSize;
            set => pageSize = (value > maxpagesize) ? maxpagesize : value;
        
        }

        
        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = value.Trim().ToLower();
        }

    }
}
