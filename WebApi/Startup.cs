using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.DBOperations;
using WebApi.MiddleWares;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    
        // ConfigureServices moethoduna uygulama içerisinde kullanıcak olan servislerin implente edildiği yani uygulamaya gösterildiği yerdir.Yani dependency injection ile uygulamada kullancağımız servisleri(mödülleri) buraya implemente etmeliyiz.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.AddDbContext<LCWaikikiSecondWeekProjectDbContext>(options=> options.UseInMemoryDatabase(databaseName:"LcWakikiSecondWeekProjectDB"));//Burayı ekleyerek artık uygulamamızda istediğimiz yerden Dependency injection ile context ime erişebilirim.

            services.AddAutoMapper(Assembly.GetExecutingAssembly());//Uygulama içerisinde AutoMapper ı service olarak kullanabilmemiz için burada bunu tanıttık.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
