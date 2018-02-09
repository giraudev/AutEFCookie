using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutEFCookie.Dados;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AutEFCookie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ambiente = BuildWebHost(args);
            using (var escopo = ambiente.Services.CreateScope())

            {
                /// <summary>
                /// iniciar o banco de dados
                /// </summary>
                var servico = escopo.ServiceProvider;
                try
                {
                    var contexto = servico.GetRequiredService<AutenticacaoContext>();
                    CodeFirstBanco.Inicializar(contexto);
                }
                catch (Exception e)
                {
                    //utiliza ILogger<Program> para analizar o próprio Program
                    var logger = servico.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Ocorreu um erro enquanto os dados foram enviados");
                }
            }
            BuildWebHost(args).Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
