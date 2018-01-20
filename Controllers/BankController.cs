using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bankAccounts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bankAccounts.Controllers
{
    public class BankController : Controller
    {
         private BankingContext _context;
 
        public BankController(BankingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("account/{id}")] 
        public IActionResult Main(int id)
        {
            // make sure user is logged in
            if(HttpContext.Session.GetInt32("UserId") == null) 
            {
                return RedirectToAction("Index", "Home");
            }
            // make sure current user is user that is logged in
            if((int)HttpContext.Session.GetInt32("UserId") == id) 
            {                
                //show user id from session
                ViewBag.username = HttpContext.Session.GetString(key: "Username");
                //grab logged-in user from session
                // User currentUser = _context.Users.Single(u => u.UserId == id);
                // Include helpls join users to their transactions
                User currentUser = _context.Users.Include(u => u.UserTransactions).Single(u => u.UserId == id);
                return View(currentUser);
            }
            // send them back to own page
            return RedirectToAction("Main", new {id = (int)HttpContext.Session.GetInt32("UserId")});
        }

        [HttpPost]
        [Route("income")]
        public IActionResult ProcessTransaction(int monies) 
        {
            int userid = (int)HttpContext.Session.GetInt32("UserId");
            //check value of withdrawal against balance
            // create transaction for user
            Transaction t = new Transaction
            {
                Amount = monies,  
                UserId = userid, 
            };
            // save transaction in db
            _context.Transactions.Add(t);
            // update user balance
            User user = _context.Users.Single(u => u.UserId == userid);
            user.Balance += monies;
            _context.SaveChanges();
            return RedirectToAction("Main", new {id = userid});
        }
    }
}

