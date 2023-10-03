using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Acme.BookLibrary;

public class BookLibraryWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<BookLibraryWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
