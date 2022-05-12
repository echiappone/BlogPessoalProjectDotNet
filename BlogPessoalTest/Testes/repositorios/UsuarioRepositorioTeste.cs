using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
using BlogPessoal.src.utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTest.Testes.repositorios
{
    [TestClass]
    public class UsuarioRepositorioTeste
    {
        private BlogPessoalContext _contexto;
        private IUsuario _repositorio;

        [TestMethod]
        public async Task CriarQuatroUsuariosNoBancoRetornaQuatroUsuarios()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _contexto = new BlogPessoalContext(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro 4 usuarios no banco
            await _repositorio.NovoUsuarioAsync(
                new NovoUsuarioDTO("Erick Braga","erick@email.com","134652","URLFOTO", TipoUsuario.NORMAL)
            );
            
            await _repositorio.NovoUsuarioAsync(
                new NovoUsuarioDTO("Tais Braga","tais@email.com","134652","URLFOTO", TipoUsuario.NORMAL)
            );
            
            await _repositorio.NovoUsuarioAsync(
                new NovoUsuarioDTO("Barbara Braga","barbara@email.com","134652","URLFOTO", TipoUsuario.NORMAL)
            );
 
            await _repositorio.NovoUsuarioAsync(
                new NovoUsuarioDTO("Alex Chiappone","alex@email.com","134652","URLFOTO", TipoUsuario.NORMAL)
            );
            
			//WHEN - Quando pesquiso lista total            
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _contexto.Usuarios.Count());
        }
        
        [TestMethod]
        public async Task PegarUsuarioPeloEmailRetornaNaoNulo()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _contexto = new BlogPessoalContext(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro um usuario no banco
            await _repositorio.NovoUsuarioAsync(
                new NovoUsuarioDTO("Luaninha Dogzera","luaninha@email.com","134652","URLFOTO", TipoUsuario.NORMAL)
            );
            
            //WHEN - Quando pesquiso pelo email deste usuario
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync("luaninha@email.com");
			
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(usuario);
        }

        [TestMethod]
        public async Task PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuario()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _contexto = new BlogPessoalContext(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro um usuario no banco
            await _repositorio.NovoUsuarioAsync(
                new NovoUsuarioDTO("Claubinho Doidao","claubinho@email.com","134652","URLFOTO", TipoUsuario.NORMAL)
            );
            
			//WHEN - Quando pesquiso pelo id 1
            var usuario = await _repositorio.PegarUsuarioPeloIdAsync(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(usuario);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Claubinho Doidao", usuario.Nome);
        }

        [TestMethod]
        public async Task AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _contexto = new BlogPessoalContext(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro um usuario no banco
            await _repositorio.NovoUsuarioAsync(
                new NovoUsuarioDTO("Gustavo Boaz","boaz@email.com","134652","URLFOTO", TipoUsuario.NORMAL)
            );
            
            //WHEN - Quando atualizamos o usuario
            await _repositorio.AtualizarUsuarioAsync(
                new AtualizarUsuarioDTO(1,"Gustavo Boaz","123456","URLFOTONOVA")
            );
            
            //THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura
            var antigo = await _repositorio.PegarUsuarioPeloEmailAsync("boaz@email.com");

            Assert.AreEqual(
                "Gustavo Boaz",
                _contexto.Usuarios.FirstOrDefault(u => u.Id == antigo.Id).Nome
            );
            
            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual(
                "123456",
                _contexto.Usuarios.FirstOrDefault(u => u.Id == antigo.Id).Senha
            );
        }

    }
}