using APIUsuario.DTO.Model;
using System;
using Xunit;

namespace ApiUsuario.Test.Model
{
    public class LoginTest
    {
        [Fact]
        public void LoginDTOTest()
        {
            
            var loginDTO = new LoginDTO();
            
            Assert.False(loginDTO.Success);
            Assert.Null(loginDTO.Mensagem);
            Assert.Null(loginDTO.Token);
            Assert.Null(loginDTO.Usuario);
        }
    }
}
