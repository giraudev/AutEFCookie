using AutEFCookie.Models;
using Microsoft.EntityFrameworkCore;

namespace AutEFCookie.Dados
{
    public class AutenticacaoContext:DbContext
    {
        public AutenticacaoContext(DbContextOptions<AutenticacaoContext>options):base(options){}

        public DbSet<Usuario> Usuarios {get;set;}
        public DbSet <Permissao> Permissao { get; set; }
        public DbSet <UsuarioPermissao> UsuariosPermissoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Permissao>().ToTable("Permissao");
            modelBuilder.Entity<UsuarioPermissao>().ToTable("UsuarioPermissao");

            base.OnModelCreating(modelBuilder);
        }
    }
}