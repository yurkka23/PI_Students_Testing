using EduHub.Integration.Providers.Abstractions;
using EduHub.Integration.Providers.Realizations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHub.Integration.Bootstrap;

public static class IntegrationConfigurations
{
    public static void RegisterIntegration(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddTransient<IEmailProvider, EmailProvider>();
    }
}
