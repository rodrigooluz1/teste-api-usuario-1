using APIUsuario.BLL;
using APIUsuario.DTO.Model;
using APIUsuario.Model;
using APIUsuario.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace APIUsuario.Controllers
{
    [ApiController]
    [Route(template: "api/usuario")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioBLL _usuario;
        private readonly IUploadService _upload;

        public UsuarioController(IUsuarioBLL usuario, IUploadService upload)
        {
            _usuario = usuario;
            _upload = upload;
        }

        [HttpPost]
        [Route(template: "login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var usuarioLogado = await _usuario.Login(request.Email, request.Senha);
            if(usuarioLogado.Usuario != null)
                usuarioLogado.Token = TokenService.GenerateToken(usuarioLogado.Usuario);
            return Ok(usuarioLogado);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListaUsuarios()
        {
            var usuarios = await _usuario.ListaUsuario();
            return Ok(usuarios);
        }

        [HttpGet]
        [Authorize]
        [Route(template:"{id}")]
        public async Task<IActionResult> RetornaUsuario([FromRoute] int id)
        {
            var usuario = await _usuario.RetornaUsuario(id);
            return Ok(usuario);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CadastraUsuario([FromForm] CadastroRequest cadastro)
        {
            cadastro.url_foto = cadastro.file != null ? _upload.UploadImage(cadastro.file).Result : cadastro.url_foto;

            var usuarios = await _usuario.CadastraUsuario(cadastro);
            return Ok(usuarios);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditaUsuario([FromForm] CadastroRequest cadastro)
        {
            cadastro.url_foto = cadastro.file != null ? _upload.UploadImage(cadastro.file).Result : cadastro.url_foto;

            var usuarios = await _usuario.EditaUsuario(cadastro);
            return Ok(usuarios);
        }

        [HttpDelete]
        [Authorize(Roles = "rluz@rluz.com")]
        [Route(template:"{id}")]
        public async Task<IActionResult> DeletaUsuario([FromRoute] int id)
        {
            var usuario = await _usuario.ExcluiUsuario(id);
            return Ok(usuario);
        }
    }
}
