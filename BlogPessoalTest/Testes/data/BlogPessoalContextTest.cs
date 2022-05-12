using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogPessoal.src.modelos;
using System.Linq;
using BlogPessoal.src.utilidades;

namespace BlogPessoalTest.Testes.data
{
    [TestClass]
    public class BlogPessoalContextTest
    {
        private BlogPessoalContext _contexto;

        [TestInitialize]
        public void inicio()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;
            _contexto = new BlogPessoalContext(opt);
        }

        [TestMethod]
        public void InserirNovoUsuarioNoBancoRetornaUsuario()

        {
            UsuarioModelo usuario = new UsuarioModelo();

            usuario.Nome = "ErickBoaz";
            usuario.Email = "erick@email.com";
            usuario.Senha = "134652";
            usuario.Foto = "LINKDAFOTOERICK";
		usuario.Tipo = TipoUsuario.NORMAL;

            _contexto.Usuarios.Add(usuario); // Adcionando usuario

            _contexto.SaveChanges(); // Commita criação

            Assert.IsNotNull(_contexto.Usuarios.FirstOrDefault(u => u.Email =="erick@email.com"));
        }
    }
}