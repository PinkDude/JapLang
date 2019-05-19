using AutoMapper;
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
    public class TestsService : ITestsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TestsService(ApplicationDbContext con, IMapper map)
        {
            _context = con;
            _mapper = map;
        }

        public async Task<List<TestJapDTO>> GetTestJap(int count = 2)
        {
            List<TestJapDTO> res = new List<TestJapDTO>();
            var rand = new Random();
            for (var i = 0; i < count; i++)
            {
                var wordJapCount = rand.Next(0, _context.WordJapans.Count());
                var item = _mapper.Map<TestJapDTO>(await _context.WordJapans.Include(c => c.WordRus)
                    .Skip(wordJapCount)
                    .FirstOrDefaultAsync());
                if (res.Any(c => c.WordJap == item.WordJap))
                {
                    i--;
                    continue;
                }
                res.Add(item);
            }
            return res;
        }
    }
}
