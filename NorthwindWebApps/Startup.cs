using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
using Northwind.Services.Data;
using Northwind.Services.DataAccess;
using Northwind.Services.EntityFrameworkCore;
using Northwind.Services.Products;

namespace NorthwindWebApps
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NorthwindContext>(opt => opt.UseInMemoryDatabase("NortwindWebDB"));

            services.AddTransient<IProductManagementService, Northwind.Services.DataAccess.ProductManagementDataAccessService>();
            services.AddTransient<IProductCategoryPicturesService, Northwind.Services.DataAccess.ProductCategoryPicturesManagementDataAccessService>();
            services.AddTransient<IProductCategoryManagementService, Northwind.Services.DataAccess.ProductCategoriesManagementDataAccessService>();

            services.AddScoped((service) =>
            {
                var sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("NorthwindConnection"));
                sqlConnection.Open();
                return sqlConnection;
            });

            services.AddTransient<Northwind.DataAccess.NorthwindDataAccessFactory, Northwind.DataAccess.SqlServerDataAccessFactory>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
