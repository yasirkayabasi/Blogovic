using Blogovic.Models.Data;
using Blogovic.ViewModels.Article.Create;
using Blogovic.ViewModels.Article.Edit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Blogovic.Filters;
using Blogovic.Managers;
using Blogovic.Models.Entity;

namespace Blogovic.Controllers
{
    [LoggedUser]
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticleController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Create(string yonlen)
        {
            ViewBag.yonlen = yonlen;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewModel model, string yonlen)
        {
            if (ModelState.IsValid)
            {
                Article article = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    AuthorId = int.Parse(HttpContext.Session.GetString("userId")),
                    ArticlePicture = model.ArticlePicture.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment)
                };
                _context.Articles.Add(article);
                _context.SaveChanges();
                TempData["message"] = "Makale Oluşturuldu..!";
                if (yonlen == null) return RedirectToAction("Index", "Home");
                return Redirect(yonlen);
            }
            else return View(model);
        }

        public IActionResult Edit(int id)
        {
            Article article = _context.Articles.FirstOrDefault(x => x.Id.Equals(id) && x.AuthorId.ToString().Equals(HttpContext.Session.GetString("userId")));

            if (article is not null) return View(new EditViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                ArticlePictureName = article.ArticlePicture
            });
            else
            {
                TempData["error"] = "Data Bulunamadı";
                return RedirectToAction("Profile", "Home", new { email = HttpContext.Session.GetString("email") });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Article article = _context.Articles.FirstOrDefault(x => x.Id.Equals(model.Id) && x.AuthorId.ToString().Equals(HttpContext.Session.GetString("userId")));
                if (article is null)
                {
                    ViewData["error"] = "Düzenleme Başarısız Oldu..!";
                    return View(model);
                }

                article.Title = model.Title;
                article.Content = model.Content;

                if (model.ArticlePicture != null)
                {

                    article.ArticlePicture = model.ArticlePicture.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment);

                    FileManager.RemoveImageFromDisk(model.ArticlePictureName, _webHostEnvironment);
                }

                _context.SaveChanges();

                TempData["message"] = "Makale Güncellendi..!";
                return RedirectToAction("Profile", "Home", new { email = HttpContext.Session.GetString("email") });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            Article article = _context.Articles.FirstOrDefault(x => x.Id.Equals(id) && x.AuthorId.ToString().Equals(HttpContext.Session.GetString("userId")));

            if (article is not null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
                FileManager.RemoveImageFromDisk(article.ArticlePicture, _webHostEnvironment);
                TempData["message"] = "Silindi..!";
            }
            else TempData["error"] = "Data Bulunamadı";

            return RedirectToAction("Profile", "Home", new { email = HttpContext.Session.GetString("email") });
        }
    }
}
