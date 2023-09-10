using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastracture;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Conrollers
{
    public class PortfolioController : Controller
    {
        private readonly IUnitOfWork<Portfoilo> _portflioItem;
        private readonly IWebHostEnvironment _host;

        // GET: /<controller>/
        public PortfolioController(IUnitOfWork<Portfoilo> portflioItem,IWebHostEnvironment host)
        {
            _portflioItem = portflioItem;
            _host = host;
        }

        //Get portfolio items

        public IActionResult Index()
        {
            return View(_portflioItem.Entity.GetAll());
        }

        //Get portfolio item detail
        public IActionResult Details(Guid? id)
        { if(id ==null)
            { return NotFound(); }
            var portfolioitem = _portflioItem.Entity.GetById(id);
            if (portfolioitem==null)
            { return NotFound(); }
            return View(portfolioitem);
        }

        //Get portfolio items create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortfolioViewModel viewmodel)
        {if (ModelState.IsValid)
            {
                
                if (viewmodel.File != null)
                {
                    string uploads = Path.Combine(_host.WebRootPath, @"img/portfolio");
                    string fullpath = Path.Combine(uploads, viewmodel.File.FileName);
                    viewmodel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                Portfoilo portfolio = new Portfoilo
                {
                    Description = viewmodel.Description,
                    ProgectName = viewmodel.ProgectName,
                    ImageUrl = viewmodel.File.FileName

                };
                _portflioItem.Entity.Insert(portfolio);
                _portflioItem.save();
                return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);

            
        }
        //Get portfolio item edit
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            { return NotFound(); }
            var portfolioitem = _portflioItem.Entity.GetById(id);
            if (portfolioitem == null)
            { return NotFound(); }
            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            { Id=portfolioitem.Id,
            ProgectName=portfolioitem.ProgectName,
            Description=portfolioitem.Description,
            ImageUrl=portfolioitem.ImageUrl};
            return View(portfolioViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,PortfolioViewModel viewmodel)
        {
           
            if(id!=viewmodel.Id)
            { return NotFound(); }
            if(ModelState.IsValid)
            {
                try
                {
                    if (viewmodel.File != null)
                    {
                        string uploads = Path.Combine(_host.WebRootPath, @"img/portfolio");
                        string fullpath = Path.Combine(uploads, viewmodel.File.FileName);
                        viewmodel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    Portfoilo portfolio = new Portfoilo
                    {Id=viewmodel.Id,
                        Description = viewmodel.Description,
                        ProgectName = viewmodel.ProgectName,
                        ImageUrl = viewmodel.File.FileName

                    };
                    _portflioItem.Entity.Update(portfolio);
                    _portflioItem.save();
                    
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(viewmodel.Id))
                    { return NotFound(); }
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));


            }
            return View(viewmodel);

        }

        private bool PortfolioItemExists(Guid id)
        {
            return _portflioItem.Entity.GetAll().Any(m => m.Id == id);
        }



        //Get portfolio item Delete
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            { return NotFound(); }
            var portfolioitem = _portflioItem.Entity.GetById(id);
            if (portfolioitem == null)
            { return NotFound(); }
            return View(portfolioitem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid ID)
        {

            _portflioItem.Entity.Delete(ID);
            _portflioItem.save();
            return RedirectToAction(nameof(Index));


        }



    }
}
