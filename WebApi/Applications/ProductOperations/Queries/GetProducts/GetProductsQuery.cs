using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.ProductOperations.Queries.GetProducts
{
    public class GetProductsQuery
    {
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;
        
        private readonly IMapper _mapper;

        public GetProductsQuery(LCWaikikiSecondWeekProjectDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ProductsViewModel> Handle()
        {
            var productList= _dbContext.Products.Include(x=> x.Category).OrderBy(x=> x.Id).ToList();
            
            return _mapper.Map<List<ProductsViewModel>>(productList);// Map<TargetObject>(SourceObject);
        }
    }
    public class ProductsViewModel 
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
    }
}