using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUsuario.Services
{
    public interface IUploadService
    {
        Task<string> UploadImage(IFormFile file);
    }
}
