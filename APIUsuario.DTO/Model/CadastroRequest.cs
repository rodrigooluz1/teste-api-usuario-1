using Microsoft.AspNetCore.Http;
using System;
namespace APIUsuario.DTO.Model
{
    public class CadastroRequest
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string url_foto { get; set; }
        public IFormFile file { get; set; }
    }
}
