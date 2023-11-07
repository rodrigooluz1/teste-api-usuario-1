using APIUsuario.DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIUsuario.BLL
{
    public interface IUsuarioBLL
    {
        Task<LoginDTO> Login(string email, string senha);
        Task<List<UsuarioDTO>> ListaUsuario();
        Task<UsuarioDTO> RetornaUsuario(int id);
        Task<UsuarioDTO> CadastraUsuario(CadastroRequest cadastro);
        Task<UsuarioDTO> EditaUsuario(CadastroRequest cadastro);
        Task<bool> ExcluiUsuario(int id);

    }
}
