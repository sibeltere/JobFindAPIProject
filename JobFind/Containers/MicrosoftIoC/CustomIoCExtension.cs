using JobFind.BusinessLayer.Abstracts;
using JobFind.BusinessLayer.Concrete;
using JobFind.CoreLayer.Settings;
using JobFind.DataLayer.Context;
using JobFind.DataLayer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;


namespace JobFind.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var _serverSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            var context = new MongoDbContext(_serverSettings);

            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddScoped(typeof(IMongoRepositoryBase<>), typeof(MongoRepositoryBase<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFirmService, FirmService>();


            services.AddHttpContextAccessor();
            services.AddControllers().AddNewtonsoftJson(options =>options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddHealthChecks();
            services.AddMemoryCache();
        }

        public static void AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            services.Configure<SwaggerSettings>(configuration.GetSection(nameof(SwaggerSettings)));
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var _swaggerSettings = configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();

            services.AddSwaggerGen(options =>
            {
                //Dökümantasyon Ayarları
                options.SwaggerDoc(_swaggerSettings.SwaggerDoc.DocName, new OpenApiInfo
                {
                    Title = _swaggerSettings.SwaggerDoc.Title,
                    Description = _swaggerSettings.SwaggerDoc.Description,
                    Version = _swaggerSettings.SwaggerDoc.Version,
                    Contact = new OpenApiContact
                    {
                        Email = _swaggerSettings.SwaggerDoc.Email,
                        Name = _swaggerSettings.SwaggerDoc.FullName,
                        Url = new Uri(_swaggerSettings.SwaggerDoc.Url)
                    }
                });

                // JWT
                //var jwtSecurityScheme = new OpenApiSecurityScheme
                //{
                //    In = ParameterLocation.Header,
                //    Name = _swaggerSettings.SecurityDefinition.Name,
                //    Type = SecuritySchemeType.Http,
                //    Description = _swaggerSettings.SecurityDefinition.Description,
                //    BearerFormat = _swaggerSettings.SecurityDefinition.BearerFormat,
                //    Scheme = _swaggerSettings.SecurityDefinition.Scheme,
                //    Reference = new OpenApiReference()
                //    {
                //        Id = JwtBearerDefaults.AuthenticationScheme,
                //        Type = ReferenceType.SecurityScheme
                //    }
                //};

                //// Basic Auth
                //var basicSecurityScheme = new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.Http,
                //    Scheme = _swaggerSettings.SecurityScheme.Basic,
                //    Reference = new OpenApiReference
                //    {
                //        Id = _swaggerSettings.SecurityScheme.BasicAuth,
                //        Type = ReferenceType.SecurityScheme
                //    }
                //};

                //Güvenlik Kurallarına her iki yönteminde ekliyorum.
                //options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
                //options.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);

                ////Secret Key
                //options.OperationFilter<AddRequiredHeaderParameterFilter>();

                ////Gerekliliği belirtiyorum.
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //   {basicSecurityScheme, new string[] { }},
                //    {jwtSecurityScheme, new string[] { }}
                //});

            });
        }
    }
}
