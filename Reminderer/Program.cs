using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());

        if (hostingContext.HostingEnvironment.IsDevelopment())
        {
            config.AddJsonFile("local.settings.json", optional: false, reloadOnChange: true);
        }

        config.AddEnvironmentVariables();
    })
    .Build();

host.Run();