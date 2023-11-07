using APIUsuario.DTO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIUsuario.DAL.Repositories
{
    public interface IUsuarioRepository
    {
        Task<UsuarioDTO> Login(string email);
        Task<List<UsuarioDTO>> ListaUsuario();
        Task<UsuarioDTO> GetUsuario(int id);
        Task CadastraUsuario(UsuarioDTO usuario);
        Task EditaUsuario(UsuarioDTO usuario);
        Task DeletaUsuario(int id);
    }
}
