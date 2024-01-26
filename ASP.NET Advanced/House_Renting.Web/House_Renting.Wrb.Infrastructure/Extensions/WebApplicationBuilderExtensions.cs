namespace House_Renting.Web.Infrastructure.Extensions
{
    using House_Renting.Services;
    using House_Renting.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class WebApplicationBuilderExtensions
    {

        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            var serviceAssembly = Assembly.GetAssembly(serviceType);

            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid service type provided!");
            }

            var serviceTypes = serviceAssembly
                .GetTypes()
                .Where(s => s.Name.EndsWith("Service") && !s.IsInterface)
                .ToArray();

            foreach (Type implementationType in serviceTypes)
            {
                Type? interfaceType = implementationType
                    .GetInterface($"I{implementationType.Name}");

                if (interfaceType == null)
                {
                    throw new InvalidOperationException($"No interface is provided for the service with name: {implementationType.Name}");
                }

                services.AddScoped(interfaceType, implementationType);
            }

            services.AddScoped<IHouseService, HouseService>();
        }
    }
}