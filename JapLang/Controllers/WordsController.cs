using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JapLang.BLL.DTO;
using JapLang.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JapLang.Controllers
{
    [Produces("application/json")]
    [Route("api/words")]
    public class WordsController : Controller
    {
        private readonly IWordsSrvice _wordService;

        public WordsController(IWordsSrvice serv)
        {
            _wordService = serv;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllWords()
        {
            var res = await _wordService.GetWords();

            return Ok(res);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateWord([FromBody]WordV2DTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _wordService.AddWord(model);

            return Ok();
        }

        [HttpPut("{wordId}")]
        public async Task<IActionResult> EditWord([FromRoute]long wordId, [FromBody]WordV2DTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _wordService.EditWord(wordId, model);
            return Ok();
        }

        [HttpDelete("{wordId}")]
        public async Task<IActionResult> DeleteWord([FromRoute]long wordId)
        {
            await _wordService.DeleteWord(wordId);
            return Ok();
        }

        [HttpGet("{wordId}")]
        public async Task<IActionResult> GetWordById([FromRoute]long wordId)
        {
            var res = await _wordService.GetWord(wordId);
            return Ok(res);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var res = await _wordService.GetCategoriesForJapWords();
            return Ok(res);
        }
    }
}