using JapLang.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsFullDTO>> GetNews(PaginationDTO pagination);
        Task<NewsFullDTO> GetNewsById(long id);
        Task AddNews(NewsFullV2DTO model);
        Task EditNews(long id, NewsFullV2DTO model);
        Task DeleteNews(long id);
    }
}
