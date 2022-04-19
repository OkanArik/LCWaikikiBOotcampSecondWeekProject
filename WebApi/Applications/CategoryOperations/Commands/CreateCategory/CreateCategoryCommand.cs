using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.CategoryOperations.Commands.CreateCategory
{
    public class CreateCategoryCommand
    {
        public CreateCategoryModel Model { get; set; }
        private readonly LCWaikikiSecondWeekProjectDbContext  _dbContext;
        private readonly IMapper _mapper;

        public CreateCategoryCommand(LCWaikikiSecondWeekProjectDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var category = _dbContext.Categories.SingleOrDefault(x=> x.Name ==Model.Name);

            if(category is not null)
              throw new InvalidOperationException($"{Model.Name} isimli kategori zaten mevcut!");
            
            var newCategory = _mapper.Map<Category>(Model);
            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();
        }
    }

    public class CreateCategoryModel
    {
        public string Name { get; set; }
    }
}