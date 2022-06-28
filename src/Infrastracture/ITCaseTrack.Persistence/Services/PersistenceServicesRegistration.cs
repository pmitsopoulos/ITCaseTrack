using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCaseTrack.Application.Contracts.Persistence;
using ITCaseTrack.Persistence.Models;
using ITCaseTrack.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ITCaseTrack.Persistence.Services
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices
        (this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CaseStudyDbSettings>(config.GetSection("CaseStudyDb"));
            services.AddSingleton<DbContext>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

           

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICaseRepository,CaseRepository>();
            services.AddScoped<IAppSystemRepository, AppSystemRepository>();

            return services;
        }
    }
}