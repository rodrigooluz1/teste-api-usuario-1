using System;
namespace APIUsuario.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string url_foto { get; set; }
    }
}

