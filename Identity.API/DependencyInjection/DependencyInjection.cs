using Identity.Application.Mapping;
using Identity.Application.Repository.Interfaces;
using Identity.Application.Service.Implementations;
using Identity.Infrastructure.EFCore;
using Identity.Infrastructure.EFCore.Repository.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.API.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthorityService, AuthorityService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<IAuthorityRepository, AuthorityRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}