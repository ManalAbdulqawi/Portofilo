using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Entities;
using Web.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Conrollers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<Portfoilo> _portfolio;

        // GET: /<controller>/
        public HomeController(IUnitOfWork<Owner> owner,IUnitOfWork<Portfoilo> portfolio)
        {
            _owner = owner;
            _portfolio = portfolio;
        }

        public IActionResult Index()
        {
            var HomeViewModel=new HomeViewModel
            {Owner=_owner.Entity.GetAll().First(),
            portfoilo=_portfolio.Entity.GetAll().ToList()
            };
            return View(HomeViewModel);
        }
    }
}
