using BlogPessoal.src.data;
using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BlogPessoalTest.Testes.data
{
    [TestClass]
    public class BlogPessoalContextTest
    {
        private BlogPessoalContext _context;
        [TestInitialize]
        public void inicio()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;
            _context = new BlogPessoalContext(opt);
        }

        [TestMethod]
        public void InserirNovoUsuarioNoBancoRetornaUsuario()

        {
            UsuarioModelo usuario = new UsuarioModelo();

            usuario.Name = "KarolBoaz";
            usuario.Email = "Karol@email.com";
            usuario.Senha = "134652";
            usuario.Foto = "AKITAOLINKDAFOTO";

            _context.Usuarios.Add(usuario);

            _context.SaveChanges();

            Assert.IsNotNull(_context.Usuarios.FirstOrDefault(u => u.Email =="Karol@email.com"));
        }
    }
}