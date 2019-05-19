using AutoMapper;
using JapLang.BLL.DTO;
using JapLang.BLL.Interfaces;
using JapLang.DAL.Entity;
using JCL.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.Services
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICommonService _common;

        public NewsService(IMapper map, ApplicationDbContext con, ICommonService com)
        {
            _mapper = map;
            _context = con;
            _common = com;
        }

        public async Task<List<NewsFullDTO>> GetNews(PaginationDTO pagination)
        {
            try
            {
                var res = await _context.News
                    .Take(pagination.Take)
                    .Skip(pagination.Skip)
                    .OrderByDescending(c => c.Date)
                    .ToListAsync();

                foreach(var i in res)
                {
                    i.ViewCount++;
                }

                _context.News.UpdateRange(res);
                await _context.SaveChangesAsync();

                return _mapper.Map<List<NewsFullDTO>>(res);
            }
            catch(Exception ex)
            {
                throw new Exception("Не удалось достать новости");
            }
        }

        public async Task<NewsFullDTO> GetNewsById(long id)
        {
            var res = await _context.News
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<NewsFullDTO>(res);
        }

        public async Task AddNews(NewsFullV2DTO model)
        {
            try
            {
                var news = _mapper.Map<News>(model);
                news.Date = DateTime.Now;
                news.Image = await _common.FileSave(model.ImageFile);

                await _context.News.AddAsync(news);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task EditNews(long id, NewsFullV2DTO model)
        {
            try
            {
                var news = await _context.News
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();
                if (news == null)
                {
                    throw new DllNotFoundException("Не удалось найти такую новость");
                }
                var oldImg = news.Image;
                var viewCount = news.ViewCount;
                var newsModel = _mapper.Map<News>(model);

                if (model.ImageIsNew)
                {
                    newsModel.Image = await _common.FileSave(model.ImageFile);
                }
                else
                {
                    newsModel.Image = oldImg;
                }
                newsModel.ViewCount = viewCount;
                newsModel.Id = id;
                newsModel.Date = news.Date;

                _context.News.Update(newsModel);
                await _context.SaveChangesAsync();
            }
            catch(DllNotFoundException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteNews(long id)
        {
            try
            {
                var news = await _context.News.Where(c => c.Id == id).FirstOrDefaultAsync();
                if(news == null)
                {
                    throw new DllNotFoundException("Не удалось найти такую новость");
                }

                _context.News.Remove(news);
                await _context.SaveChangesAsync();
            }
            catch(DllNotFoundException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
