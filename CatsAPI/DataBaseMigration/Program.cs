using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataBaseMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<ApplicationDataContext>(options =>
                {
                    options.UseNpgsql
                    (
                        hostContext.Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(nameof(DataBaseMigration))
                    ).UseLoggerFactory(LoggerFactory.Create(builder =>
                    {
                        builder
                            .AddConsole((_) => { })
                            .AddFilter((category, level) =>
                                category == DbLoggerCategory.Database.Command.Name
                                && level == LogLevel.Information);
                    }));
                });
                //services.AddScoped<Worker>(); // без этого миграции не собирались
                // да, я читал чат. написанноое звучало как санскрит - корни отдельных слов угадываются, но общий смысл непонятен.
                services.AddHostedService<Worker>(); // после сборки миграций вернул AddHosted и сделал изменения в бд.
            });
    }
}
