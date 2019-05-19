using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.DTO
{
    public class TestJapDTO
    {
        public string WordJap { get; set; }

        public List<WordRusDTO> Answer { get; set; }

        public bool Next { get; set; } = false;

    }
}
