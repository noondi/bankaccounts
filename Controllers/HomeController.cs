using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bankAccounts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bankAccounts.Controllers
{
    public class HomeController : Controller
    {
         private BankingContext _context;
 
        public HomeController(BankingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
 
        public IActionResult Index()
        {
            return View();
        }        

        [HttpPost]
        [Route("register")] 
        public IActionResult Register(RegisterUserView u)
        {
            if(ModelState.IsValid)
            {
                // check for unique email
               List<User> sameEmail = _context.Users.Where(dbuser => dbuser.Email == u.Email).ToList();
                //if ok
                if(sameEmail.Count == 0)
                {
                    User newUser = new User
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email
                    };
                    // hash pw
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    // set newUser password to the one thats typed in
                    newUser.Password = Hasher.HashPassword(newUser, u.Password);
                    // add user to db
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    // Set session variables
                    HttpContext.Session.SetInt32("UserId", newUser.UserId);
                    HttpContext.Session.SetString("Username", newUser.FirstName);
                    // handle successful registration
                    return RedirectToAction("Main", "Bank", new {id = newUser.UserId});
                }                    
                // handle errors
                else
                {
                    TempData["unique"] = "**Email has already been registered.";
                    return RedirectToAction("Index");
                }
            }
            //handle success
            return View("Index");
        }        

        [HttpPost]
        [Route("Login")]
 
        public IActionResult Login(string Email, string Password)
        {
            // get the user from matching email, we want to only return one email.
            User sameEmail = _context.Users.Where(dbuser => dbuser.Email == Email).SingleOrDefault();
            // if no matching email
            if (sameEmail == null)
            {
                TempData["emailerror"] = "**Email not registered";
                return RedirectToAction("Index");
            }
            // check pw
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            // Pass the user object, the hashed password, and the PasswordToCheck
            if(0 == Hasher.VerifyHashedPassword(sameEmail, sameEmail.Password, providedPassword: Password))
            {
                //Handle failure
                TempData["pwerror"] = "**Password is incorrect";
                return RedirectToAction("Index");
            }        
            //Handle success
            // Set session variables
            HttpContext.Session.SetInt32("UserId", sameEmail.UserId);
            HttpContext.Session.SetString("Username", value: sameEmail.FirstName);
            return RedirectToAction("Main", "Bank",  new {id = sameEmail.UserId});
        }
        
    }
}
