using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.DTO
{
    public class NewsDTO
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string Tittle { get; set; }
        public DateTime Date { get; set; }
        public long ViewCount { get; set; }
    }

    public class NewsFullDTO : NewsDTO
    {
        public string Text { get; set; }
    }

    public class NewsFullV2DTO
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public PhotoJsonDTO ImageFile { get; set; }
        public bool ImageIsNew { get; set; }
        public string Tittle { get; set; }
        public long ViewCount { get; set; }
    }
}
