using AutoMapper;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using EventManagement.Application;
using EventManagement.Application.Behaviour;
using EventManagement.Infrastructure;
using EventManagement.Infrastructure.Persistence.DataContext;
using EventManagement.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventManagementAPI
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        private string _jwtSecretKey { get; set; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSecretKey = _configuration["JwtSecretKey"];
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            EventManagement.Application.Utilities.ApplicationConstants.ConnectionString = _configuration.GetConnectionString("EventManagementCS");
            services.AddDbContext<ApplicationDbContext>();

            // Register Infrastructe Dependency Injections here
            InfrastructureDependencyResolver.AddDependencyResolver(services);
            // Register Application Dependency Injections here
            ApplicationDependencyResolver.AddDependencyResolver(services);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Event Management WebApi",
                    Version = "v2",
                    Description = "Event Management endpoints",
                });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = _configuration.GetConnectionString("EventManagementCS");
            });

            services.AddCors(o => o.AddPolicy("EventManagementPolicy", builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                // .AllowCredentials()
                //.WithOrigins(_configuration["CorsOriginUrl"]);
                .AllowAnyOrigin();
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            //services.AddMvc(option => option.EnableEndpointRouting = false);
            //services.AddMvcCore().AddApiExplorer();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.WriteIndented = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment hostingEnvironment)
        {
            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseStaticFiles();
            applicationBuilder.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
            applicationBuilder.UseCors("EventManagementPolicy");
            if (hostingEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseElmah();
            applicationBuilder.UseExceptionMiddleware();
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthorization();

            //applicationBuilder.UseWhen(context => context.Request.Path.StartsWithSegments("/eventOrganiser"), appBuilder =>
            //{
            //    appBuilder.UseApiKeyMiddleware();
            //});
            //applicationBuilder.UseMvc();
            applicationBuilder.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000");
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "PlaceInfo Services"));
        }
    }
}
