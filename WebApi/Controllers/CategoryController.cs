using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.CategoryOperations.Commands.CreateCategory;
using WebApi.Applications.CategoryOperations.Commands.DeleteCategory;
using WebApi.Applications.CategoryOperations.Commands.UpdateCategory;
using WebApi.Applications.CategoryOperations.Queries.GetCategories;
using WebApi.Applications.CategoryOperations.Queries.GetCategoryDetail;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class CategoryController : ControllerBase
    {
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryController(LCWaikikiSecondWeekProjectDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories ()
        {
            GetCategoriesQuery query=new GetCategoriesQuery(_dbContext,_mapper);
            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            GetCategoryDetailQuery query= new GetCategoryDetailQuery(_dbContext,_mapper);
            query.CategoryId=id;
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryModel newCategory)
        {
            CreateCategoryCommand command= new CreateCategoryCommand(_dbContext,_mapper);
            try
            {
                command.Model=newCategory;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id,[FromQuery] UpdateCategoryModel updateCategory)
        {
            UpdateCategoryCommand command=new UpdateCategoryCommand(_dbContext);
            try
            {
                command.CategoryId=id;
                command.Model=updateCategory;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            DeleteCategoryCommand command =new DeleteCategoryCommand(_dbContext);
            try
            {
                command.CategoryId=id;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}