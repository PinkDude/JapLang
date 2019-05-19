using AutoMapper;
using DAL.Entity;
using JapLang.BLL.DTO;
using JapLang.BLL.Interfaces;
using JCL.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.Services
{
    public class WordsService : IWordsSrvice
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ICommonService _commonService;

        public WordsService(IMapper map, ApplicationDbContext con, ICommonService servCom)
        {
            _mapper = map;
            _context = con;
            _commonService = servCom;
        }

        public async Task<List<WordDTO>> GetWords()
        {
            try
            {
                var res = await _context.WordJapans
                    .Include(c => c.WordRus)
                    .Include(c => c.WordJapanCategory)
                    .ToListAsync();

                return _mapper.Map<List<WordDTO>>(res);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<WordDTO> GetWord(long wordId)
        {
            var res = await _context.WordJapans
                .Include(c => c.WordJapanCategory)
                .Include(c => c.WordRus)
                .Where(c => c.Id == wordId)
                .FirstOrDefaultAsync();

            return _mapper.Map<WordDTO>(res);
        }

        public async Task AddWord(WordV2DTO model)
        {
            try
            {
                var wordJap = _mapper.Map<WordJapan>(model);
                wordJap.Gif = await _commonService.FileSave(model.Gif);
                foreach(var word in wordJap.WordRus)
                {
                    word.Word = word.Word.ToLower();
                }
                await _context.WordJapans.AddAsync(wordJap);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось добваить слово");
            }
        }

        public async Task EditWord(long wordId, WordV2DTO model)
        {
            try
            {
                var word = await _context.WordJapans.Where(c => c.Id == wordId).FirstOrDefaultAsync();
                if (word == null)
                {
                    throw new DllNotFoundException("Нет такого слова!");
                }
                var wordRus = await _context.WordRus.Where(c => c.JapId == wordId).ToListAsync();
                var wordRus2 = wordRus.Except(_mapper.Map<List<WordRus>>(model.WordRus));
                _context.WordRus.RemoveRange(wordRus);

                var oldGif = word.Gif;
                word = _mapper.Map<WordJapan>(model);
                if (model.GifIsNew)
                {
                    word.Gif = await _commonService.FileSave(model.Gif);
                }
                else
                {
                    word.Gif = oldGif;
                }
                word.Id = wordId;
                foreach (var wordJ in word.WordRus)
                {
                    wordJ.Word = wordJ.Word.ToLower();
                }

                _context.WordJapans.Update(word);
                await _context.SaveChangesAsync();
            }
            catch (DllNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось обновить данные");
            }
        }

        public async Task DeleteWord(long wordId)
        {
            try
            {
                var word = await _context.WordJapans.Where(c => c.Id == wordId).FirstOrDefaultAsync();
                if (word == null)
                {
                    throw new DllNotFoundException("Нет такого слова!");
                }
                _context.WordJapans.Remove(word);
                await _context.SaveChangesAsync();
            }
            catch (DllNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось удалить слово");
            }
        }

        public async Task<List<CategoryDTO>> GetCategoriesForJapWords()
        {
            var res = await _context.WordJapanCategories.ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(res);
        }
    }
}
