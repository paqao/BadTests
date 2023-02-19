using Logic.CommandHandlers;
using Logic.Commands;
using Logic.DTOs;
using Logic.Models;
using Logic.Profiles;
using Logic.Queries;
using Logic.QueryHandlers;
using Logic.Services;

namespace WebApp
{
    public class Startup
    {
        public void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ConfigureServices(null, builder.Services);
        }

        internal void ConfigureServices(HostBuilderContext context, IServiceCollection sc)
        {
            sc.AddAutoMapper(x =>
            {
                x.AddProfile<BusinessProcessProfile>();
            });

            sc.AddSingleton<IRepository<BusinessProcess>, BusinessProcessRepository>();
            sc.AddScoped<ICommandHandler<ApproveProcessCommand, BusinessProcessDto>, ApproveProcessCommandHandler>();
            sc.AddScoped<ICommandHandler<CreateProcessCommand, BusinessProcessDto>, CreateProcessCommandHandler>();
            sc.AddScoped<ICommandHandler<UpdateProcessCommand, BusinessProcessDto>, UpdateProcessCommandHandler>();
            sc.AddScoped<IQueryHandler<GetItemById, BusinessProcessDto>, GetItemByIdQueryHandler>();
        }
    }
}
