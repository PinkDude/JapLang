using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JapLang.BLL.DTO;
using JapLang.BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JapLang.Controllers
{
    [Produces("application/json")]
    [Route("api/news")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService serv)
        {
            _newsService = serv;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetNews(PaginationDTO pagination)
        {
            var res = await _newsService.GetNews(pagination);
            return Ok(res);
        }

        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetNewsById(long newsId)
        {
            var res = await _newsService.GetNewsById(newsId);

            return Ok(res);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateNews([FromBody]NewsFullV2DTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _newsService.AddNews(model);

            return Ok();
        }

        [HttpPut("{newsId}")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> EditNews(long newsId,[FromBody] NewsFullV2DTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _newsService.EditNews(newsId, model);

            return Ok();
        }

        [HttpDelete("{newsId}")]
        [Authorize(Roles="Admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteNews(long newsId)
        {
            if(User.IsInRole("Admin"))
            await _newsService.DeleteNews(newsId);

            return Ok();
        }
    }
}