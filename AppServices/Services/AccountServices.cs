using System.Text;
using Bank.Management.Api.Data;
using Bank.Management.Api.Models;

using Bank.Management.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Bank.Management.Api;

public class AccountServices : IAccountService
{
    private AppDbContext _dbContext;

    public AccountServices(AppDbContext dbContext)
    {   
        _dbContext = dbContext;
    }
    public Account Authenticate(string AccountNumber, string Pin)
    {
        //Let's Aithenticate
        //1st check wheather the given account is exist or not
        var account = _dbContext.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).FirstOrDefault(); 
        if(account == null)
            return null;
        //ok if we have Account present in database than varfiy for pin
        
        if(!VarifyPinHash(Pin,account.PinHash,account.PinSalt))
            return null;
        
        //ok so Authentication is passed
        return account;

    }
    //this function is used above
    private static bool VarifyPinHash(string Pin, byte[] pinHash,byte[] pinSalt){
        if(string.IsNullOrWhiteSpace(Pin))
            throw new ArgumentException ("Pin"); //first we have to check wheather Given Pin contains any space or it is null
        //now let's verify Pin
        using(var hmac= new System.Security.Cryptography.HMACSHA512(pinSalt)){
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Pin));
            for(int i=1;i< computeHash.Length;i++){
                    if(computeHash[i] != pinHash[i]){
                        return false; //here we are checking array of pin salt for given password is equal or not
                    }
            }
        }
        return true;
    }

    public Account Create(Account account, string Pin, string ConfirmPin)
    {
        //Here We Are Creating A New UserAccount
        //1st we need To Check Wheather the User Already Exist if Exist Then We Cant Create Account With Same Email
        var isUserExist = _dbContext.Accounts.Any(x => x.Email == account.Email);
        if(isUserExist != null){
            throw new ApplicationException("An Account is Already Existed With This Email");
        }
        //Also Valid Pin
        if(!Pin.Equals(ConfirmPin)) throw new ArgumentException("Pin Donot Match", $"{Pin}");

        //Now All THe Validation Is Done Then Create New User
        byte[] PinHash,PinSalt;
        CreatePinHash(Pin,out PinHash,out PinSalt); //this method is create outside below
        //here out keyword is used because the arguments will work as reference of the value which will be stored 

        account.PinHash = PinHash;
        account.PinSalt = PinSalt; //here we have stored Pin Hash And Salt for Account
        //now we can add this account to database
        _dbContext.Accounts.Add(account);
        _dbContext.SaveChanges();
        return account;   
    }
                               // ^
    //this function is used above |
    private static void CreatePinHash(string Pin, out byte[] PinHash, out byte[] PinSalt ){ //we can create this method in helper Folder for currently place it here
        using(var hmac = new System.Security.Cryptography.HMACSHA512()){
            PinSalt = hmac.Key;
            PinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Pin));
        }
    }

    public void Delete(int Id)
    {
        var account = _dbContext.Accounts.Find(Id);
        if(account != null){
            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();
        }
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        return _dbContext.Accounts.ToList();
    }

    public Account GetByAccountNumber(string AccountNumber)
    {
        var account = _dbContext.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).FirstOrDefault();
        if(account == null)
            return null;
        
        return account; 
    }

    public Account GetById(int Id)
    {
        var account = _dbContext.Accounts.Where(x => x.Id == Id).FirstOrDefault();
        if(account == null)
            return null;
        
        return account;
    }

    public void Update(Account account, string Pin = null)
    {
        //update is little tasky/tricky
        var accountToBeUpdated = _dbContext.Accounts.Where(x => x.Email == account.Email).FirstOrDefault();
        if(accountToBeUpdated == null)
            throw new ApplicationException("The Account Does not Exist");
        if(!string.IsNullOrWhiteSpace(account.Email)){
            //this means user want to change his email
            //so we have to check wheather the given new email given by user is already taken
            if(_dbContext.Accounts.Any(x => x.Email == account.Email))
                throw new ApplicationException ("This Email "+ account.Email+" is Already Taken");
            //else change email for him

        accountToBeUpdated.Email = account.Email;
        }
        //as we want to allow user to change only his email, pin, phone number
        if(!string.IsNullOrWhiteSpace(account.PhoneNumber)){
            //this means user want to change his PhoneNumber
            //so we have to check wheather the given new PhoneNumber given by user is already taken
            if(_dbContext.Accounts.Any(x => x.PhoneNumber == account.PhoneNumber))
                throw new ApplicationException ("This Phone Number "+ account.PhoneNumber+" is Already Taken");
            //else change phoneNumber for him

        accountToBeUpdated.PhoneNumber = account.PhoneNumber;
        }
        //now change the pin
        if(!string.IsNullOrWhiteSpace(Pin)){
            //this means user want to change his/her Pin

            byte[] PinHash,PinSalt;
            CreatePinHash(Pin,out PinHash,out PinSalt);

            accountToBeUpdated.PinHash = PinHash;
            accountToBeUpdated.PinSalt = PinSalt;
        }

    accountToBeUpdated.UpdatedAt = DateTime.Now;
    //now we persist all these updates to Database
    _dbContext.Accounts.Update(accountToBeUpdated);
    _dbContext.SaveChanges();

    }
}
