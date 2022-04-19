using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.ProductOperations.Commands.CreateProduct;
using WebApi.Applications.ProductOperations.Commands.DeleteProduct;
using WebApi.Applications.ProductOperations.Commands.UpdateProduct;
using WebApi.Applications.ProductOperations.Queries.GetProductDetail;
using WebApi.Applications.ProductOperations.Queries.GetProducts;
using WebApi.Applications.ProductOperations.Queries.SearchProduct;
using WebApi.DBOperations;


namespace WebApi.Controllers
{
    [ApiController] //ApiController attribute ı ile controller'ımızın bir Http Response döneceğinin taahhüt ederiz.
    [Route("[Controller]s")] // Route attribute u ile requestle gelen Endpoint i hangi resource ın karşılayacağı bilgisini tanıtırız.Ve recources lar çoğul olmalısır bu tüzden burada endpontimiz bu route daki tanımlamaya göre Products şeklinde gelecek.
    public class ProductController : ControllerBase //ProductController class'ını Microsftun MVC tasarım desenini mimari olarak kabul edip MVC adında framework oluşturduğu , Miscrosoft.AspNetCore.MVC isim uzayı altındaki ControllerBase class'ından kalıtım aldırdık.Bunun amacı bunun ile projede bu sınıfın Controller olduğunun projemize tanıtmak eğer ControllerBase sınıfından kalıtım aldırmasaydık normak vir sınıf olurdu bizim için. Controller olarak tanımlayacağımız sınıfların isimlendirmesinde Resource(kaynak) adının devamın Controller kullanılmalıdır. 
    {
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext; //Sadece burada kullanacağım context in _dbContext adında instance ını oluşturdum .readonly değişkenler uygulama içerisinden değiştirilemezler sadece constructor vasıtası ile set edilebilirler.

        private readonly IMapper _mapper;

        public ProductController(LCWaikikiSecondWeekProjectDbContext dbContext, IMapper mapper) // Constructor (kurucu metod) ile context'i setledim.
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet] // Bu Action methodun Get olduğunu yani CRUD işlemlerden Read işleminin yapılacağını belirtliğim attribute. Endpoint ==> ...Porducts/GET
        public IActionResult GetProducts()
        {
            GetProductsQuery  query = new GetProductsQuery(_dbContext,_mapper);

            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")] // (For Example)Endpoint ==> ...Products/GET/1
        public IActionResult GetById(int id)
        {
            GetProductDetailQuery query = new GetProductDetailQuery(_dbContext,_mapper);
            try// try catch bloğunu kullanmamın sebebi buradaki operasyonlarımda exception le karşılaşırsam exception ı handle edip ekrana yazdırmak.
            {
                query.ProductId=id;

                var result= query.Handle();
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        
        [HttpPost] // Bu Action methodun Post olduğunu yani CRUD işlemlerden Create işleminin yapılacağını belirttiğim attribute. Enpoint ==> ...Products/POST
        public IActionResult CreateProduct([FromBody] CreateProductModel newProduct )
        {
          
           try// try catch bloğunu kullanmamın sebebi buradaki operasyonlarımda exception le karşılaşırsam exception ı handle edip ekrana yazdırmak.
           {
               CreateProductCommand command = new CreateProductCommand(_dbContext,_mapper);
               if(!ModelState.IsValid)//Burada  newProduct adında client dan requestle gelen objedeki validasyonlarım valid(geçerli) değilse sebebleri ile beraber cliente response dönecem.
               {
                   return BadRequest();
               }
               command.Model=newProduct;
               command.Handle();
               return Ok();
           }
           catch (Exception ex)
           {
               return BadRequest(ex);
           }
        }
        
        [HttpPut("{id}")] // Bu Action methodun Put olduğunun yani CRUD işlemlerden Update işleminin yapılacağını belirttiğim attribute. (For Example) Endpoint ==> ...Products/PUT/1
        public IActionResult UpdateProduct([FromBody] UpdateProductModel updatedProduct,int id)
        {
            UpdateProductCommand command= new UpdateProductCommand(_dbContext);
            try
            {
                command.ProductId=id;
                command.Model=updatedProduct;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //UpdateProductCommand servisismde bir exception olursa try bloğunda handle edicem ve bu exception ı ex ismindeki değişkenime atayarak catch bulağında bunu cilent e BadRequest ile mesaj olarak dönecem.
            }
        }

        [HttpDelete("{id}")] //Bu Action methodun Delete olduğunun yani CRUD işlemşerden Delete işleminin yapılacağını belirttiğim attribute. (For Example) Endpoint ==> ...Products/DELETE/1
        public IActionResult DeleteProduct(int id)
        {
            DeleteProductCommand command = new DeleteProductCommand(_dbContext);
            try
            {
                command.ProductId=id;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search")] 
        public IActionResult GetBySearch([FromQuery] SearchModel searchProduct)// Burada SerachModel objemdeki nitelikler nazarında filtreleme yapması sağlanıyor client'ın.Yaptığı filtlereme nazarında ürünleri listeleyebilmektedir.
        {
            SearchProductQuery query = new SearchProductQuery(_dbContext,_mapper);
            try
            {
                query.Model=searchProduct;
                var resultList=query.Handle();
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}