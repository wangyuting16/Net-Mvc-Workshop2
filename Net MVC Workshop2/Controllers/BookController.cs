using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Net_MVC_Workshop2.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        [HttpGet()]
        public ActionResult Index()
        {
            //Models.BookServices bookService = new Models.BookServices();
            //ViewBag.SearchResult = bookService.GetBookByCondtioin();
            //var result = bookService.GetBookByCondtioin();
            return View();
            //return this.Json(ViewBag.SearchResult ,JsonRequestBehavior.AllowGet);
            //return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost()]
        public JsonResult Index(Models.BookSearchArg book)
        {
            Models.BookServices bookService = new Models.BookServices();
            ViewBag.SearchResult = bookService.GetBookByCondtioin(book);
            var result = bookService.GetBookByCondtioin(book);
            return Json(result);
            //return View();
            //return this.Json(ViewBag.SearchResult ,JsonRequestBehavior.AllowGet);
            //return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost()]
        public JsonResult Serach(Models.BookSearchArg book)
        {
            Models.BookServices bookService = new Models.BookServices();
            ViewBag.SearchResult = bookService.GetBookByCondtioin(book);
            var result = bookService.GetBookByCondtioin(book);
            //return View();
            //return this.Json(ViewBag.SearchResult ,JsonRequestBehavior.AllowGet);
            return Json(result);
        }
    }
}