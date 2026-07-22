using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProEventos.Aula.Application;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Persistence;
using ProEventos.Aula.Persistence.Repositories;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Text.Json.Serialization;
using ProEventos.Aula.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProEventos.Aula
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
            services.AddDbContext<ProEventoContext>(
                  context => context.UseSqlServer(Configuration.GetConnectionString("Default"))
                );

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 4;
            })
             .AddRoles<Role>()
             .AddRoleManager<RoleManager<Role>>()
             .AddSignInManager<SignInManager<User>>()
             .AddRoleValidator<RoleValidator<Role>>()
             .AddEntityFrameworkStores<ProEventoContext>()
             .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddControllers()
                .AddJsonOptions(options =>
            {
                // Ignora ciclos de referência na serialização JSON
                // options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                // Configurações adicionais para melhor serialização
                options.JsonSerializerOptions.WriteIndented = true; // Formato legível
                // options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantém os nomes das propriedades conforme definidos
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Ignora nulos
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Converte enums para strings
            });

            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.Aula", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando Bearer.
                                    Entre com 'Bearer' [espaço] coloque seu token.
                                    Exemplo: 'Bearer 1234abcde'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           },
                           Scheme = "oauth2",
                           Name = "Bearer",
                           In = ParameterLocation.Header
                       },
                       new List<string>()
                    }
                });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IEventoRepositorie, EventoRepositorie>();
            services.AddScoped<ILoteRepositorie, LoteRepositorie>();
            services.AddScoped<IUserRepositorie, UserRepositorie>();
            services.AddScoped<IPalestranteRepositorie, PalestranteRepositorie>();
            services.AddScoped<IRedeSocialRepositorie, RedeSocialRepositorie>();

            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPalestranteService, PalestranteService>();
            services.AddScoped<IRedeSocialService, RedeSocialService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.Aula v1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
