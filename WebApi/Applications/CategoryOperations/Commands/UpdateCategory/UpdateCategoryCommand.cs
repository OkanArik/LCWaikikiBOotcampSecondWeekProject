using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.CategoryOperations.Commands.UpdateCategory
{
    public class UpdateCategoryCommand
    {
        public UpdateCategoryModel Model { get; set; }
        public int CategoryId { get; set; }
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;
        public UpdateCategoryCommand(LCWaikikiSecondWeekProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var category=_dbContext.Categories.SingleOrDefault(x=> x.Id==CategoryId);

            if(category is null)
              throw new InvalidOperationException($"Güncellemek istediğiniz {CategoryId} id'li kategori mevcut değil!");
              
            category.Name= Model.Name==default? category.Name: Model.Name;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateCategoryModel
    {
        public string Name { get; set; }
    }
}