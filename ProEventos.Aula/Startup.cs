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

            services.AddControllers()
                .AddJsonOptions(options =>
            {
                // Ignora ciclos de referência na serialização JSON
                // options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                // Configurações adicionais para melhor serialização
                options.JsonSerializerOptions.WriteIndented = true; // Formato legível
                // options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantém os nomes das propriedades conforme definidos
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Ignora nulos
            });
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.Aula", Version = "v1" });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<IEventoRepositorie, EventoRepositorie>();
            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<ILoteRepositorie, LoteRepositorie>();

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

            app.UseAuthorization();

            app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
