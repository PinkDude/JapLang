using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.DTO
{
    public class WordDTO
    {
        public long Id { get; set; }
        public string Word { get; set; }
        public string Gif { get; set; }
        public string Transcription { get; set; }

        public IEnumerable<WordRusDTO> WordRus { get; set; }
        public CategoryDTO Category { get; set; }
    }

    public class WordRusDTO
    {
        public long Id { get; set; }
        public string Word { get; set; }
    }

    public class WordV2DTO
    {
        public string Word { get; set; }
        public bool GifIsNew { get; set; }
        public PhotoJsonDTO Gif { get; set; }
        public string Transcription { get; set; }
        public int WordJapanCategoryId { get; set; }

        public List<WordRusDTO> WordRus { get; set; }
    }
}
