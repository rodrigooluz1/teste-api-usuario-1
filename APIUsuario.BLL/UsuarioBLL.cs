using APIUsuario.DAL.Repositories;
using APIUsuario.DTO.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIUsuario.BLL
{
    public class UsuarioBLL : IUsuarioBLL
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public object TokenService { get; private set; }
        public object UploadService { get; private set; }

        public UsuarioBLL(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public async Task<LoginDTO> Login(string email, string senha)
        {
            var retornoLogin = new LoginDTO();

            try
            {
                var usuarioLogado = await _usuarioRepo.Login(email);                

                if (usuarioLogado != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(senha, usuarioLogado.senha)){
                        retornoLogin.Usuario = usuarioLogado;
                        retornoLogin.Success = true;
                        retornoLogin.Mensagem = "Usuário logado";
                    }
                    else
                        retornoLogin.Mensagem = "Senha incorreta";
                }
                else
                {
                    retornoLogin.Mensagem = "Usuário não encontrado";
                }

            }
            catch (Exception er)
            {
                retornoLogin.Mensagem = er.Message;
            }

            
            return retornoLogin;
            
        }

        public async Task<UsuarioDTO> CadastraUsuario(CadastroRequest cadastro)
        {
            var usuario = ConverteUsuario(cadastro);

            await _usuarioRepo.CadastraUsuario(usuario);

            return usuario;
        }

        public async Task<UsuarioDTO> EditaUsuario(CadastroRequest cadastro)
        {
            var usuario = ConverteUsuario(cadastro);

            await _usuarioRepo.EditaUsuario(usuario);

            return usuario;
        }

        public async Task<bool> ExcluiUsuario(int id)
        {
            await _usuarioRepo.DeletaUsuario(id);

            return true;
        }

        public async Task<List<UsuarioDTO>> ListaUsuario()
        {
            return await _usuarioRepo.ListaUsuario();
        }

        public async Task<UsuarioDTO> RetornaUsuario(int id)
        {
            return await _usuarioRepo.GetUsuario(id);
        }

        private UsuarioDTO ConverteUsuario(CadastroRequest cadastro)
        {
            var usuario = new UsuarioDTO();

            usuario.Nome = cadastro.nome;
            usuario.email = cadastro.email;
            usuario.senha = BCrypt.Net.BCrypt.HashPassword(cadastro.senha);
            usuario.url_foto = cadastro.url_foto == null ? "" : cadastro.url_foto; 

            if(cadastro.Id > 0)
            {
                usuario.Id = cadastro.Id;
                usuario.updated_at = DateTime.Now;
            }else
                usuario.created_at = usuario.updated_at = DateTime.Now;

            return usuario;
        }

    }
}
