using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Web.ViewModels
{
    public class PortfolioViewModel
    { public Guid Id { get; set; }
        public string ProgectName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }


       
    }
}
