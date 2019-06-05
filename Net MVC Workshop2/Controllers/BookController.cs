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
        public JsonResult BookClass(Models.BookClass bookclass)
        {
            Models.BookServices bookService = new Models.BookServices();
            var result = bookService.GetBookClass(bookclass);
            return Json(result);
        }
        [HttpPost()]
        public JsonResult BookKeeper(Models.BookKeeper bookkeeper)
        {
            Models.BookServices bookService = new Models.BookServices();
            var result = bookService.GetBookKeeper(bookkeeper);
            return Json(result);
        }
        [HttpPost()]
        public JsonResult BookStatus(Models.BookStatus bookstatus)
        {
            Models.BookServices bookService = new Models.BookServices();
            var result = bookService.GetBookStatus(bookstatus);
            return Json(result);
        }
    }
}