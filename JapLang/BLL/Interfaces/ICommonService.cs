using JapLang.BLL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.Interfaces
{
    public interface ICommonService
    {
        Task<string> SaveImage(IFormFile file);
        Task<string> FileSave(PhotoJsonDTO file);
    }
}
