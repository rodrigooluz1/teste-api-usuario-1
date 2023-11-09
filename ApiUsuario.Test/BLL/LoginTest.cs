using APIUsuario.BLL;
using APIUsuario.DAL.Repositories;
using APIUsuario.DTO.Model;
using AutoFixture;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ApiUsuario.Test.BLL
{
    public class LoginTest
    {
        UsuarioBLL _usuarioBLL;
        Mock<IUsuarioRepository> _usuarioRepository;


        // Arrange
        private string email = "usuario@teste.com";
        private string senha = "senha123";        

        public LoginTest()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            //_usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);
        }

        [Fact]
        public async Task Login_SuccessTest()
        {
            UsuarioDTO usuarioLogado = new UsuarioDTO
            {
                Id = 1,                
                senha = BCrypt.Net.BCrypt.HashPassword(senha),
                email = email,
                Nome = "Rodrigo Luz",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                url_foto = ""
            };

            _usuarioRepository.Setup(repo => repo.Login(email)).ReturnsAsync(usuarioLogado);
            _usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);

            // Act
            var resultado = await _usuarioBLL.Login(email, senha);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal("Usuário logado", resultado.Mensagem);
            Assert.Same(usuarioLogado, resultado.Usuario);
        }


        [Fact]
        public async Task Login_IncorrectPasswordTest()
        {
            UsuarioDTO usuarioLogado = new UsuarioDTO
            {
                Id = 1,
                senha = BCrypt.Net.BCrypt.HashPassword("123"),
                email = email,
                Nome = "Rodrigo Luz",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                url_foto = ""
            };

            _usuarioRepository.Setup(repo => repo.Login(email)).ReturnsAsync(usuarioLogado);
            _usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);

            // Act
            var resultado = await _usuarioBLL.Login(email, senha);

            // Assert
            Assert.NotNull(resultado);
            Assert.False(resultado.Success);
            Assert.Equal("Senha incorreta", resultado.Mensagem);
            Assert.Null(resultado.Usuario);
        }


        [Fact]
        public async Task Login_UserNotFoundTest()
        {
            UsuarioDTO usuarioLogado = new UsuarioDTO
            {
                Id = 1,
                senha = BCrypt.Net.BCrypt.HashPassword("123"),
                email = email,
                Nome = "Rodrigo Luz",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                url_foto = ""
            };

            _usuarioRepository.Setup(repo => repo.Login("usernotfound@teste.com")).ReturnsAsync(usuarioLogado);
            _usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);

            // Act
            var resultado = await _usuarioBLL.Login(email, senha);

            // Assert
            Assert.NotNull(resultado);
            Assert.False(resultado.Success);
            Assert.Equal("Usuário não encontrado", resultado.Mensagem);
            Assert.Null(resultado.Usuario);
        }


        [Fact]
        public async Task Login_ErrorTest()
        {
            UsuarioDTO usuarioLogado = new UsuarioDTO
            {
                Id = 1,
                senha = BCrypt.Net.BCrypt.HashPassword(senha),
                email = email,
                Nome = "Rodrigo Luz",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                url_foto = ""
            };
                        
            _usuarioRepository.Setup(repo => repo.Login(email)).ThrowsAsync(new Exception("Erro no login"));

            _usuarioBLL = new UsuarioBLL(_usuarioRepository.Object);

            // Act
            var resultado = await _usuarioBLL.Login(email, senha);

            // Assert
            Assert.NotNull(resultado);
            Assert.False(resultado.Success);
            Assert.Equal("Erro no login", resultado.Mensagem);
            Assert.Null(resultado.Usuario);
        }

    }
}
