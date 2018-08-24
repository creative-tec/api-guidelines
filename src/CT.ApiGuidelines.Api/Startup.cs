namespace CT.ApiGuidelines.Api
{
    using Core.Common;
    using Domain.Account;
    using Domain.Repositories;
    using Infrastructure;
    using Mediator.Accounts;
    using Mediator.Accounts.Mappers;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Models.Accounts;
    using OdeToCode.AddFeatureFolders;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                options =>
                {
                    options.ReturnHttpNotAcceptable = true;
                })
                .AddFeatureFolders(new FeatureFolderOptions { FeatureFolderName = "Resources" });

            services.AddMediatR(typeof(GetAccountByIdQuery));
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IOwnerRepository, OwnerRepository>();
            services.AddTransient<IMapTo<AccountGetV1, Account>, AccountMapper>();
            services.AddTransient<IMapTo<TransactionGetV1, Transaction>, TransactionMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole();
                loggerFactory.AddDebug(LogLevel.Information);
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
