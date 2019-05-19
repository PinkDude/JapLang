using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entity
{
    public class WordRus
    {
        public long Id { get; set; }
        public string Word { get; set; }
        public long JapId { get; set; }

        [ForeignKey("JapId")]
        public WordJapan WordJap{ get; set; }
    }
}
