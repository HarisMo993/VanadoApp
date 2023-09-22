using MachineCheckup.Application.Interfaces;
using MachineCheckup.Application.Profiles;
using MachineCheckup.Infrastructure.Data.Contexts;
using MachineCheckup.Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MachineCheckup.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection ConfigureIoCService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
