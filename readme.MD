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
 * Check: Add BooksController
