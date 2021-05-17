using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.Email;
using System.Net;
using Serilog;
using Serilog.Events;

namespace MonsterAir
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                                 .Build()
                                 .GetConnectionString("DefaultConnection");

            const string logFilePath = "Logs.txt";
            var columnOptions = new ColumnOptions();

            var emailInfo = new EmailConnectionInfo
            {
                FromEmail = "stayhome726@gmail.com",
                ToEmail = "atmahad726@gmail.com",
                MailServer = "smtp.gmail.com",
                EmailSubject = "An Log Error Occured in MonsterAir Project, Please check it",
                Port = 465,
                EnableSsl = true,
                NetworkCredentials = new NetworkCredential("stayhome726@gmail.com", "@1110597219@")
            };

            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs" }
               , null, null, LogEventLevel.Warning, null, columnOptions: columnOptions, null, null)
                .WriteTo.Email(emailInfo,
                           outputTemplate: "Time : {Timestamp:HH:mm:ss}\t\n{Level:u3}\t\n" +
                           "{SourceContext}\t\n{Message}{NewLine}{Exception}",
                           LogEventLevel.Fatal, 1)
                .WriteTo.File(logFilePath, restrictedToMinimumLevel: LogEventLevel.Warning)
               .CreateLogger();

            try
            {
                Log.Information("Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
