using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
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
        public void CriarQuatroUsuariosNoBancoRetornaQuatroUsuarios()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _contexto = new BlogPessoalContext(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro 4 usuarios no banco
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO("Gustavo Boaz","gustavo@email.com","134652","URLFOTO")
            );
            
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO("Mallu Boaz","mallu@email.com","134652","URLFOTO")
            );
            
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO("Catarina Boaz","catarina@email.com","134652","URLFOTO")
            );
 
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO("Pamela Boaz","pamela@email.com","134652","URLFOTO")
            );
            
			//WHEN - Quando pesquiso lista total            
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _contexto.Usuarios.Count());
        }
        
        [TestMethod]
        public void PegarUsuarioPeloEmailRetornaNaoNulo()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _contexto = new BlogPessoalContext(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro um usuario no banco
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO("Zenildo Boaz","zenildo@email.com","134652","URLFOTO")
            );
            
            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repositorio.PegarUsuarioPeloEmail("zenildo@email.com");
			
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuario()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _contexto = new BlogPessoalContext(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro um usuario no banco
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO("Neusa Boaz","neusa@email.com","134652","URLFOTO")
            );
            
			//WHEN - Quando pesquiso pelo id 1
            var user = _repositorio.PegarUsuarioPeloId(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Nome);
        }

        [TestMethod]
        public void AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _contexto = new BlogPessoalContext(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro um usuario no banco
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO("Estefânia Boaz","estefania@email.com","134652","URLFOTO")
            );
            
            //WHEN - Quando atualizamos o usuario
            _repositorio.AtualizarUsuario(
                new AtualizarUsuarioDTO(1,"Estefânia Moura","123456","URLFOTONOVA")
            );
            
            //THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura
            var antigo = _repositorio.PegarUsuarioPeloEmail("estefania@email.com");

            Assert.AreEqual(
                "Estefânia Moura",
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