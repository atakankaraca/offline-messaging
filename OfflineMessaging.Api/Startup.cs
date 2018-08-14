using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfflineMessaging.Api.Attributes;
using OfflineMessaging.Api.Extensions;
using OfflineMessaging.Api.Middleware;
using OfflineMessaging.Data.Validator;
using OfflineMessaging.Repository;
using OfflineMessaging.Service;
using OfflineMessaging.Service.DTO;
using OfflineMessaging.Service.Validator;

namespace OfflineMessaging.Api
{
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
            services.AddMvc().AddFluentValidation(fvc =>
                fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAutoMapper();

            services.ConfigureCors();
            services.ConfigureJwt();

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IBlockListService, BlockListService>();
            services.AddTransient<IUserActivityService, UserActivityService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IValidator<UserDto>, UserValidator>();
            services.AddTransient<IValidator<BlockListDto>, BlockListValidator>();
            services.AddTransient<IValidator<MessageDto>, MessageValidator>();
            services.AddTransient<IValidator<UserActivityDto>, UserActivityValidator>();

            services.AddScoped<ModelValidationAttribute>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseMvc();
        }
    }
}
