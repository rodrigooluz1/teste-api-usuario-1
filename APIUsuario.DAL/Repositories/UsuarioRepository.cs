using APIUsuario.DTO.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUsuario.DAL.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly APIContext _contexto;
        public UsuarioRepository(APIContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<UsuarioDTO> Login(string email)
        {
            return await _contexto.Set<UsuarioDTO>().Where(u => u.email == email).FirstOrDefaultAsync();
        }
        public async Task CadastraUsuario(UsuarioDTO usuario)
        {
            _contexto.Set<UsuarioDTO>().Add(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task DeletaUsuario(int id)
        {
            var usuario = await _contexto.Set<UsuarioDTO>().Where(u => u.Id == id).FirstOrDefaultAsync();
            _contexto.Set<UsuarioDTO>().Remove(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task EditaUsuario(UsuarioDTO usuario)
        {
            _contexto.Set<UsuarioDTO>().Update(usuario).Property(u => u.created_at).IsModified = false;
            await _contexto.SaveChangesAsync();
        }

        public async Task<UsuarioDTO> GetUsuario(int id)
        {
            return await _contexto.Set<UsuarioDTO>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<UsuarioDTO>> ListaUsuario()
        {
            return await _contexto.Set<UsuarioDTO>().AsNoTracking().ToListAsync();
        }
    }
}
