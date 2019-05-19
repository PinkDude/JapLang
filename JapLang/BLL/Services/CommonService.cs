using JapLang.BLL.DTO;
using JapLang.BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JapLang.BLL.Services
{
    public class CommonService : ICommonService
    {
        private readonly IHostingEnvironment _environment;

        public CommonService(IHostingEnvironment env)
        {
            _environment = env;
        }

        public async Task<string> FileSave(PhotoJsonDTO file)
        {
            try
            {
                if (file == null || file.Content.Length == 0 || string.IsNullOrEmpty(_environment.WebRootPath))
                    return null;
                

                var guid = Guid.NewGuid();
                
                string dir = _environment.WebRootPath + "/Files/Images/" + guid.ToString() + "/";
                
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                
                string filePath = dir + file.Filename;

                await File.WriteAllBytesAsync(filePath, file.Content);
                
                return filePath.Replace(_environment.WebRootPath, "");
            }
            catch (Exception ex)
            {
                throw new ValidationException($"Failed to load picture: {file.Filename}");
            }
        }

        public async Task<string> SaveImage(IFormFile file)
        {
            return await Task.Run(() =>
            {
                if (file == null || string.IsNullOrEmpty(_environment.WebRootPath))
                    return null;
                Image<Rgba32> image = Image.Load(file.OpenReadStream());

                string hash = GetHashFromFile(file.OpenReadStream());

                string dir1 = _environment.WebRootPath + "/Files/Images/" + hash.Substring(0, 2);
                string dir2 = $"{dir1}/{hash.Substring(2, 2)}/";

                if (!Directory.Exists(dir1))
                {
                    Directory.CreateDirectory(dir1);
                    Directory.CreateDirectory(dir2);
                }
                else if (!Directory.Exists(dir2))
                    Directory.CreateDirectory(dir2);

                string result = dir2 + file.FileName;
                image.Save(result);

                return result.Replace(_environment.WebRootPath, "");
            });
        }

        private string GetHashFromFile(Stream stream)
        {
            var hash = SHA1.Create().ComputeHash(stream);
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
