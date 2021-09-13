using System;
using System.IO;
using System.Threading.Tasks;
using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace HiddenVilla_Server.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration configuration;

        public FileUpload(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new(file.Name);
                var fileName = Guid.NewGuid() + fileInfo.Extension;
                var folderDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "RoomImages");
                var path = Path.Combine(folderDirectory, fileName);

                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }

                var url = $"{configuration.GetValue<string>("ServerUrl")}";
                var fullPath = $"{url}RoomImages/{fileName}";
                return fullPath;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteFile(string filename)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\RoomImages\\{filename}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}