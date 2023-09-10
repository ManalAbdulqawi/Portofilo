using System;
using System.Collections.Generic;
using Core.Entities;
namespace Web.ViewModels
{
    public class HomeViewModel
    { public Owner Owner { set; get; }
        public List<Portfoilo> portfoilo { set; get; }
        public HomeViewModel()
        {
        }
    }
}
