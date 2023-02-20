using Logic.CommandHandlers;
using Logic.Commands;
using Logic.DTOs;
using Logic.Queries;
using Logic.QueryHandlers;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp;
using WebApp.Controllers;

namespace IntegrationTests.TestEngine
{
    public class IntegrationTestEngine : IAsyncLifetime
    {
        public IServiceProvider _serviceProvider { get; private set; }

        public async Task DisposeAsync()
        {

        }

        public ProcessController Processes { get; private set; }

        public string ServerAddres { get; private set; }

        public async Task InitializeAsync()
        {
            _serviceProvider = GetServiceProvider();

            var approveCommandHandler = _serviceProvider.GetRequiredService<ICommandHandler<ApproveProcessCommand, BusinessProcessDto>>();
            var createCommandHandler = _serviceProvider.GetRequiredService<ICommandHandler<CreateProcessCommand, BusinessProcessDto>>();
            var updateCommandHandler = _serviceProvider.GetRequiredService<ICommandHandler<UpdateProcessCommand, BusinessProcessDto>>();
            var getItemByIdQueryHandler = _serviceProvider.GetRequiredService<IQueryHandler<GetItemById, BusinessProcessDto>>();
            Processes = new ProcessController(approveCommandHandler, createCommandHandler, getItemByIdQueryHandler, updateCommandHandler);
        }

        private IServiceProvider GetServiceProvider()
        {
            var startup = new Startup();
            var host = new HostBuilder()
                    .ConfigureAppConfiguration(
                        (hostBuilder, config) =>
                        {
                            var basePath = hostBuilder.HostingEnvironment.ContentRootPath ?? Directory.GetCurrentDirectory();
                            config.SetBasePath(basePath)
                                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                                  .AddEnvironmentVariables();
                        })
                    .ConfigureServices(
                        (context, sc) =>
                        {
                            startup.ConfigureServices(context, sc);
                            this.ConfigureTestServices(sc);
                        })
                    .Build();

            return host.Services;
        }

        private void ConfigureTestServices(IServiceCollection sc)
        {
            // Add scrutor
        }
    }
}
