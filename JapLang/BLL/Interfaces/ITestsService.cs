using JapLang.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.Interfaces
{
    public interface ITestsService
    {
        Task<List<TestJapDTO>> GetTestJap(int count = 2);
    }
}
