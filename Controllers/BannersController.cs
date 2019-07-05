using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NewsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DataContext;

namespace NewsAPI.Controllers
{

    [Route("api/[controller]")]
    public class BannersController : Controller
    {
        private readonly NewsApiContext _context;

        public BannersController(NewsApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<BannerViewModel> GetBanners()
        {
            var banner = from b in _context.Banners
                         select new BannerViewModel()
                         {
                Id = b.Id,
                Message = b.Message
                         };

            return banner.AsQueryable();
        }

        [HttpGet("{id}")]
        public async Task<BannerViewModel> GetBanner(int id)
        {
            var banner = await _context.Banners.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (banner == null)
            {
                throw new Exception();
            }
            var bannerReturn = new BannerViewModel()
            {
                Id = banner.Id,
                Message = banner.Message
            };

            return bannerReturn;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
