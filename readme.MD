## Project: Buiding an Async API with ASP.NET Core
## Date: 5/6/2019
## Author: Geoff Fox

## Check: Add Database and seed data
* Run: ```Add-Migration InitialDB ```
* Run: ```Update-Database ```

## Check: Add Repository
* Impliment two interfaces...
```
 public class BooksRepository : IBooksRepository, IDisposable
    {
        private BooksContext _context;

        public BooksRepository(BooksContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
		...
		...
		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
```
* Register the service DI (Startup:ConfigureServices)
* The DbContext is scoped in order to conserve memory by requiring disposal to be invoked. 
```
 // services.AddTransient will return a new instance each time - This will not keep state TOO SHORT!
 // services.AddSingleton is not equal to OR shorter than the DbContext scope TOO LONG!
 
 services.AddScoped<IBooksRepository, BooksRepository>();
 ```
 ## Check: Add BooksController

 ## Check: Add an AsyncResultFilter - BookResultFilterAttribute : ResultFilterAttribute

 * Ready for Automapper
 ```
namespace BooksAsync.Api.Filters
{
    public class BooksResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if (resultFromAction?.Value == null
                || resultFromAction.StatusCode < 200
                || resultFromAction.StatusCode >= 300)
            {
                await next();
                return;
            }
            resultFromAction.Value = Mapper.Map<IEnumerable<Models.Book>>(resultFromAction.Value);

            await next();
        }
    }
}
```
## Check Add Automapper
* Add: AutoMapper.Extensions.MicrosoftDependencyInjection
* Add: BookProfile.cs
```
using AutoMapper;

namespace Books.api
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<BooksAsync.Api.Entities.Book, BooksAsync.Api.Models.Book>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));
        }
    }
}
```
* Startup -  Automapper registration
```
// services.AddTransient will return a new instance each time - This will not keep state TOO SHORT!
// services.AddSingleton is not equal to OR shorter than the DbContext scope TOO LONG!
services.AddScoped<IBooksRepository, BooksRepository>();

services.AddAutoMapper(); // needs update

services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

```
## Check: Add BookResultFilterAttribute and BooksResultFilterAttribute with Automapper Profile

* Note: Startup now requires for Automapper 5/9/2019:
```
//services.AddAutoMapper(); needs update as shown below
Mapper.Initialize(cfg =>
{
    cfg.AddProfile<BooksProfile>();
});
```
## Check: Add Create Book (creating a Resource Demo)

* Note now required for Automapper DI in Controller:
```
// AutoMapper Configrtions #2
            // https://stackoverflow.com/questions/54239669/asp-net-core-2-2-unable-to-resolve-service-for-type-automapper-imapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BooksProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
```
## Check: Use WebSurge to create a new book
![](/BooksAsync.Api/Resources/WebSurge.png)
![](/BooksAsync.Api/Resources/WebSureSave.png)
