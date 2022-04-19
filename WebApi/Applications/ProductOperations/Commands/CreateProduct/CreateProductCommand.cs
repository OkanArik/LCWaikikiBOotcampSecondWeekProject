using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.ProductOperations.Commands.CreateProduct
{
    public class CreateProductCommand
    {
        public CreateProductModel Model { get; set; }
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateProductCommand(LCWaikikiSecondWeekProjectDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var product = _dbContext.Products.FirstOrDefault(x=> x.Name == Model.Name&&x.SizeId==Model.SizeId&&x.CategoryId==Model.CategoryId);
            if(product is not null)
                throw new InvalidOperationException("Eklemek istediğin ürün zaten mevcut!");

            
            
            product = _mapper.Map<Product>(Model);
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }
    }    

    public class CreateProductModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Range(1,3)]
        public int CategoryId { get; set; }
        [Range(1,8)]
        public int SizeId { get; set; }
        [Range(10,10000)]
        public int Price { get; set; }
    }
}