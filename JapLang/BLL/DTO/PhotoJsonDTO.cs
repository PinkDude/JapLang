using JapLang.BLL.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.DTO
{
    public class PhotoJsonDTO
    {
        public string Filename { get; set; }
        public string Path { get; set; }
        public bool IsNew { get; set; }
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] Content { get; set; }
    }
}
