using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APIUsuario.Services
{
    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public UploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> UploadImage(IFormFile file)
        {
            if (file != null)
            {
                string dtaStr = DateTime.Now.ToString("dd" + "-" + "MM" + "-" + "yyyy" + " " + "HH" + "-" + "mm" + "ss").Replace("-", "").Replace(" ", "");
                string[] arrName = file.FileName.Split('.');

                string newFileName = dtaStr + "." + arrName[arrName.Length - 1];

                var filePath = Path.Combine(_environment.ContentRootPath, "uploads", newFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                return newFileName;

            }

            return string.Empty;
        }
    }
}
