using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Practice.EFCore.AngelSix.Context;
using Practice.EFCore.AngelSix.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;

namespace Practice.EFCore.AngelSix.Controllers
{

    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            InitializeEntities(_appDbContext);

            #region From SQL

            //// Work!! No need to specify database.dbo prefix
            //IQueryable<User> users = _appDbContext.Users
            //                         .FromSql("select * from users");


            //// Work
            //IIncludableQueryable<User, Phone> users2 =
            //    _appDbContext.Users.FromSql("select * from users")
            //                        .Include(u => u.Phone);

            #endregion

            #region Projection Query
            //// PROJECTION QUERY

            //// Return null for Phone navigation property, which is normal behavior
            //var user = _appDbContext.Users.Where(usr => usr.Name.Equals("Steve"))
            //                .FirstOrDefault();
            //// user.Phone is null


            //User newUser = _appDbContext.Users.Where(usr => usr.Name.Equals("Steve"))
            //                                .Select(u => new Context.User()
            //                                {
            //                                    Name = u.Name,
            //                                    Phone = u.Phone // works, not null
            //                                }).FirstOrDefault();
            //// newUser.Phone is OBJECT

            #endregion


            return Content("Just Fine");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private void InitializeEntities(AppDbContext _appDbContext)
        {
            if (!_appDbContext.Users.Any())
            {
                #region Phone intialization
                var phone = new Phone()
                {
                    Manufacturer = "Honor"
                };
                var phone2 = new Phone()
                {
                    Manufacturer = "Nokia"
                };

                _appDbContext.Phones.AddRange(phone, phone2);
                _appDbContext.SaveChanges();
                #endregion

                #region User Initialization
                _appDbContext.Users.AddRange(new User()
                {
                    Name = "Steve",
                    Phone = phone
                },
                new User()
                {
                    Name = "Alek",
                    Phone = phone2
                },
                new User()
                {
                    Name = "Troelsen",
                    Phone = phone
                },
                new User()
                {
                    Name = "Remark",
                    Phone = phone2
                });
                _appDbContext.SaveChanges();
                #endregion


            }
        }
    }
}
