using JapLang.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.Interfaces
{
    public interface IWordsSrvice
    {
        Task<List<WordDTO>> GetWords();
        Task<WordDTO> GetWord(long wordId);
        Task AddWord(WordV2DTO model);
        Task EditWord(long wordId, WordV2DTO model);
        Task DeleteWord(long wordId);
        Task<List<CategoryDTO>> GetCategoriesForJapWords();
    }
}
