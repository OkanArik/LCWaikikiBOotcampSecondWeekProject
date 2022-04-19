using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.ProductOperations.Queries.GetProductDetail
{
    public class GetProductDetailQuery
    {
        public int ProductId { get; set; }
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext ;
        private readonly IMapper _mapper ;

        public GetProductDetailQuery(LCWaikikiSecondWeekProjectDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ProductDetailViewModel Handle()
        {
            var product=_dbContext.Products.Include(x=> x.Category ).SingleOrDefault(x=>x.Id==ProductId);

            if(product is null)
               throw new InvalidOperationException($"{ProductId} ' li ürün mevcut değil!");
            
            return _mapper.Map<ProductDetailViewModel>(product);
        }
    }

    public class ProductDetailViewModel
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
    }
}