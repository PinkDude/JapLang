using AutoMapper;
using JapLang.BLL.DTO.Account;
using JapLang.BLL.Interfaces;
using JCL.DAL.EF;
using JCL.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JapLang.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountService(ApplicationDbContext cont, IMapper map)
        {
            _context = cont;
            _mapper = map;
        }

        public async Task Rgister(RegisterDTO model)
        {
        }
    }
}
