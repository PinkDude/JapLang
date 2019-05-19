using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.DAL.Entity
{
    public class News
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Tittle { get; set; }
        public DateTime Date { get; set; }
        public long ViewCount { get; set; } = 0;
    }
}
