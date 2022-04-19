using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.ProductOperations.Queries.SearchProduct
{
    public class SearchProductQuery
    {
        public  SearchModel Model { get; set; }
        private readonly IMapper _mapper;
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;

        public SearchProductQuery(LCWaikikiSecondWeekProjectDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<SearchViewModel> Handle()
        {
            IQueryable<Product> query= _dbContext.Products.Include(x=> x.Category);
            bool isSearch=false;
            if(Model.MaxPrice is not null)
               {
                    query = query.Where(x=>  x.Price<Model.MaxPrice);
                    isSearch=true;
               }
            if(Model.CategoryId is not null)
               {
                    query = query.Where(x=>  x.CategoryId==Model.CategoryId);
                    isSearch=true;
               }
            if(Model.Name is not null)
               {
                query = query.Where(x=>  x.Name==Model.Name);
                isSearch=true;
               }
            if(Model.SizeId is not null)
               { 
                query = query.Where(x=>  x.SizeId==Model.SizeId);
                isSearch=true;
               }
            
            if(isSearch==false || query.AsQueryable().Count()==0)
                throw new InvalidOperationException("Filtrelemenize uyan ürün bulunamadı!");

            return _mapper.Map<List<SearchViewModel>>(query) ;

        }
    }
    public class SearchModel
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? SizeId { get; set; }
        public int? MaxPrice { get; set; }
    }

    public class SearchViewModel
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
    }

    
}