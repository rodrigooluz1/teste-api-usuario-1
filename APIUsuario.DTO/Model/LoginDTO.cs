namespace APIUsuario.DTO.Model
{
    public class LoginDTO
    {
        public bool Success { get; set; }
        public string Mensagem { get; set; }
        public string Token { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}
