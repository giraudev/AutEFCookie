using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutEFCookie.Dados;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutEFCookie
{
    public class Startup
    {
        public IConfiguration configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            //se fossem os dois configutarions escritos iguais, colocaria o this.configuration = configuration
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //servico do DbContext
            services.AddDbContext<AutenticacaoContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("BancoAutenticacao")));
            //serviços de Autenticação
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                //add um cookie, se der errado, direciona ao path
                options.LoginPath = "/Conta/Login";
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc(routes =>
            {
                //rota para direcionamento de página home
                routes.MapRoute(
                    name:"default",
                    template: "{controller=Home}/{action=Index}/{id}"
                );
            });

            app.UseAuthentication();
            app.UseStaticFiles();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            
        }
    }
}
