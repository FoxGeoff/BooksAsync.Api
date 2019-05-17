using AutoMapper;
using BooksAsync.api;
using BooksAsync.Api.Contexts;
using BooksAsync.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BooksAsync.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;
        private readonly string _dbconnet;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
            _dbconnet = _config["ConnectionStrings:BooksDbConnectionString"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                // register the DbContext on the container, getting the connection string from
                // appSettings (note: use this only for development: in a production enviroment,
                // use an eviroment variable OR a 'user secret')
                //TODO:
                /* options.UseMySql(_config.GetConnectionString("dbconnect"))); */

                services.AddDbContext<BooksContext>(option =>
                    option.UseSqlServer(_dbconnet));

            }

            // services.AddTransient will return a new instance each time - This will not keep state TOO SHORT!
            // services.AddSingleton is not equal to OR shorter than the DbContext scope TOO LONG!
            services.AddScoped<IBooksRepository, BooksRepository>();

            //services.AddAutoMapper(); needs update as shown below:-
            // AutoMapper Configrtions #1
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BooksProfile>();
            });

            // AutoMapper Configrtions #2
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BooksProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpClient();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                 .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //TODO:  
                /* AudioImageSeeder.InitializeData(app.ApplicationServices, loggerFactory); */

                app.UseCors(builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    );
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
