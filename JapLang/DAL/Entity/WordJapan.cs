using JapLang.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entity
{
    public class WordJapan
    {
        public long Id { get; set; }
        public string Word { get; set; }
        public string Gif { get; set; }
        public int? WordJapanCategoryId { get; set; }
        public string Transcription { get; set; }

        public IEnumerable<WordRus> WordRus { get; set; }
        [ForeignKey("WordJapanCategoryId")]
        public WordJapanCategory WordJapanCategory { get; set; }
    }
}
