using EMAB.Data;
using EMAB.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMAB.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class UsersController : ApiController
    {
        public class UserModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }

        }
        ApplicationDbContext applicationDbContext;
        ApplicationUserManager applicationUserManager;

        public UsersController(
            ApplicationDbContext applicationDbContext,
            ApplicationUserManager applicationUserManager
            )
        {
            this.applicationDbContext = applicationDbContext;
            this.applicationUserManager = applicationUserManager;
        }

        [HttpGet]
        public object List()
        {
            return applicationDbContext
                .Users
                .Select(x => new { Id = x.Id, UserName = x.UserName, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName });
        }

        [HttpGet]
        public async Task<object> Get(string id)
        {
            var db = await applicationUserManager.FindByIdAsync(id);
            return new { Id = db.Id, UserName = db.UserName, Email = db.Email, FirstName = db.FirstName, LastName = db.LastName };
        }

        [HttpPost]
        public async Task<object> Post(UserModel user)
        {
            var result1 = IdentityResult.Success;
            var result2 = IdentityResult.Success;
            var db = await applicationUserManager.FindByNameAsync(user.UserName);
            if (db == null)
            {
                result1 = await applicationUserManager.CreateAsync(new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                }, user.Password);
            }
            else
            {
                db.Email = user.Email;
                db.FirstName = user.FirstName;
                db.LastName = user.LastName;
                result1 = await applicationUserManager.UpdateAsync(db);

                result2 = IdentityResult.Success;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    result2 = await applicationUserManager.ChangePasswordAsync(db.Id, user.Password, user.Password);
                }
            }

            return new
            {
                result = result1.Succeeded && result2.Succeeded,
                messages = result1.Errors.ToList().Concat(result2.Errors).ToList(),
                id = applicationUserManager.FindByName(user.UserName).Id
            };
        }
    }
}
