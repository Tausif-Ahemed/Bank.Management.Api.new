using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bank.Management.Api.Services;
using AutoMapper;
using Bank.Management.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Bank.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        //Now Let's Inject AccountService
        private readonly IAccountService _accountService;
        //let's also Bring AutoMapper
        IMapper _mapper;
        public AccountController( IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        //registerNewAccount
        [HttpPost("RegisterNewAccount")]
        public IActionResult RegisterNewAccount([FromBody] RegisterNewAccountModel model){
            if(!ModelState.IsValid)
                return BadRequest(model);
        
            //map to account
            var account = _mapper.Map<Account>(model);
            // var response = new Account{
            //     FirstName = model.FirstName,
            //     LastName = model.LastName,
            //     PhoneNumber = model.PhoneNumber,
            //     CreatedAt = model.CreatedAt,
            //     UpdatedAt = model.UpdatedAt,
            // };
            return Ok(_accountService.Create(account, model.Pin,model.ConfirmPin));
        }

        [HttpGet("GetAllAccounts")]
        public IActionResult GetAllAccounts(){
            var account = _accountService.GetAllAccounts();
            var mappedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(mappedAccount);
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model){
            if(!ModelState.IsValid) return BadRequest(model);

            var  result = _accountService.Authenticate(model.AccountNumber,model.Pin);
            // if(result == null) return Unauthorized("Invalid Credentials");
            return Ok(result);
            //it will return account if credentials are valid, let's see wheather to map it or not
        }

        [HttpGet("GetByAccountNumber")]
        public IActionResult GetByAccountNumber(string AccountNumber){
            if(!Regex.IsMatch(AccountNumber,@"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) return BadRequest("Account Number Must Be 10 Digit");

            var account = _accountService.GetByAccountNumber(AccountNumber);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpGet("GetAccountById")]
        public IActionResult GetAccountById(int Id){
            var account = _accountService.GetById(Id);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpPut("UpdateAccount")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel model){
            if(!ModelState.IsValid) return BadRequest(model);
            var account = _mapper.Map<Account>(model);
            _accountService.Update(account,model.Pin);
            return Ok();
        }   
    }
}