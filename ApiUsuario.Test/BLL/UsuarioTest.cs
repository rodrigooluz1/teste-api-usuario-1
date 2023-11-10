using APIUsuario.BLL;
using APIUsuario.DAL.Repositories;
using APIUsuario.DTO.Model;
using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace ApiUsuario.Test.BLL
{
    public class UsuarioTest
    {
        UsuarioBLL _usuarioBLL;
        Mock<IUsuarioRepository> _usuarioRepository;

        public UsuarioTest()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            //_usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);
        }


        [Fact]
        public async Task ReturnUserTest()
        {
            var user = new UsuarioDTO { Id = 1, email = "teste@teste.com", Nome = "Testerson Silva", senha = "123456", url_foto = "", created_at = DateTime.Now, updated_at = DateTime.Now };

            _usuarioRepository.Setup(repo => repo.GetUsuario(user.Id)).ReturnsAsync(user);
            _usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);

            var retorno = await _usuarioBLL.RetornaUsuario(user.Id);

            Assert.NotNull(retorno);
            Assert.Same(retorno, user);
        }

        [Fact]
        public async Task UserListTest()
        {
            var listResult = new List<UsuarioDTO>();

           listResult.Add(new UsuarioDTO { Id = 1, email = "teste@teste.com", Nome = "Rogério Ceni", senha = "123456", url_foto = "" });
            listResult.Add(new UsuarioDTO { Id = 2, email = "teste2@teste.com", Nome = "Lucas Moura", senha = "123456", url_foto = "" });

            _usuarioRepository.Setup(repo => repo.ListaUsuario()).ReturnsAsync(listResult);

            _usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);

            var lista = await _usuarioBLL.ListaUsuario();

            Assert.NotNull(lista);
        }

        [Fact]
        public async Task CreateUserTest()
        {
            var addUser = new CadastroRequest { Id = 0, email = "teste@teste.com", nome = "Rodrigo Luz", senha = "123456", url_foto = "" };

            var resultUser = new UsuarioDTO { Id = 1, email = "teste@teste.com", Nome = "Rodrigo Luz", senha = "123456", url_foto = "" };
            
            _usuarioRepository.Setup(repo => repo.CadastraUsuario(It.IsAny<UsuarioDTO>()))
                .Returns((UsuarioDTO usuario) => Task.FromResult(resultUser));

            _usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);

            var cadastro = await _usuarioBLL.CadastraUsuario(addUser);

            Assert.NotNull(cadastro);
            Assert.Equal(cadastro.email, resultUser.email);
            Assert.Equal(cadastro.Nome, resultUser.Nome);
            Assert.True(BCrypt.Net.BCrypt.Verify(resultUser.senha, cadastro.senha));
            Assert.Equal(cadastro.url_foto, addUser.url_foto);
        }

        [Fact]
        public async Task EditUserTest()
        {
            var updUser = new CadastroRequest { Id = 1, email = "teste@teste.com", nome = "Rodrigo Luz", senha = "123456", url_foto = "" };

            var resultUser = new UsuarioDTO { Id = 1, email = "teste@teste.com", Nome = "Rodrigo Luz", senha = "123456", url_foto = "" };

            _usuarioRepository.Setup(repo => repo.CadastraUsuario(It.IsAny<UsuarioDTO>()))
                .Returns((UsuarioDTO usuario) => Task.FromResult(resultUser));

            _usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);

            var edit = await _usuarioBLL.EditaUsuario(updUser);

            Assert.NotNull(edit);
            Assert.Equal(edit.Id, resultUser.Id);
            Assert.Equal(edit.email, resultUser.email);
            Assert.Equal(edit.Nome, resultUser.Nome);
            Assert.True(BCrypt.Net.BCrypt.Verify(resultUser.senha, edit.senha));
            Assert.Equal(edit.url_foto, updUser.url_foto);
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            // Arrange
            var idUsuario = 1;
            _usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);

            // Act
            var resultado = await _usuarioBLL.ExcluiUsuario(idUsuario);

            // Assert
            _usuarioRepository.Verify(repo => repo.DeletaUsuario(idUsuario), Times.Once);
            Assert.True(resultado);
        }


    }
}
