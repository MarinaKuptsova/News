using News_List.DAL;
using News_List.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace News_List.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Index(int page = 1)
        {
            var pageSize = 3;
            List<NewsItem> newsList = new List<NewsItem>();
            try
            {
                using (var ctx = new NewsDataContext())
                {
                    newsList =
                        Mapper.Map<IEnumerable<News>, List<NewsItem>>(ctx.News.ToList());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            IEnumerable<NewsItem> newsPerPages = newsList.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = newsList.Count
            };
            IndexViewModel model = new IndexViewModel
            {
                PageInfo = pageInfo,
                News = newsPerPages
            };

            return View(model);
        }

        public ActionResult NewsEdit(long id)
        {
            var model = new NewsModel();
            try
            {
                using (var ctx = new NewsDataContext())
                {
                    var entityNew = ctx.News
                        .Include("Source")
                        .Include("Theme")
                        .FirstOrDefault(s => s.Id == id);

                    if (entityNew == null)
                        throw new Exception("");
                    
                    model = Mapper.Map<News, NewsModel>(entityNew);
                    model.Source = ctx.Sources.ToDictionary(key => key.Id, value => value.Name);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult NewsEdit(NewsModel model)
        {
            var response = new JsonResponse();
            var newModel = new News();
            try
            {
                using (var ctx = new NewsDataContext())
                {
                    newModel =
                        Mapper.Map<NewsModel, News>(model);
                    ctx.Entry(newModel).State = EntityState.Modified;
                    response.Success = true;
                    response.Result = "Success";
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                response.Result = "Error" + e.Message;
            }
            RedirectToAction("Index");
            return Json(response);
        }

        public ActionResult CreateNews()
        {
            var model = new NewsModel();
            try
            {
                using (var ctx = new NewsDataContext())
                {
                    model.Source = ctx.Sources.ToDictionary(key => key.Id, value => value.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult CreateNews(NewsModel model)
        {
            var response = new JsonResponse();
            News newModel = null;
            try
            {
                using (var cxt = new NewsDataContext())
                {
                    newModel = Mapper.Map<NewsModel, News>(model);
                    newModel.Theme = new Theme()
                    {
                        Name = model.ThemeName
                    };
                    
                    cxt.News.Add(newModel);
                    response.Success = true;
                    response.Result = "Success";
                    cxt.SaveChanges();
                }

            }
            catch (Exception e)
            {
                response.Result = e.Message;
            }
            RedirectToAction("Index");
            return Json(response);
        }

        [HttpPost]
        public JsonResult DeleteNews(long id)
        {
            var response = new JsonResponse();
            try
            {
                using (var cxt = new NewsDataContext())
                {
                    News newsModel = null;
                    newsModel = cxt.News.Find(id);
                    if (newsModel != null)
                    {
                        cxt.News.Remove(newsModel);

                        response.Success = true;
                        response.Result = "Success";

                        cxt.SaveChanges();
                    }

                    response.Result = "Элемент не найден";
                }
            }
            catch (Exception e)
            {
                response.Result = "Error" + e.Message;
            }

            return Json(response);
        }

        [HttpGet]
        public ActionResult Search(string searchText)
        {
            var response = new JsonResponse();
            List<NewsItem> model = new List<NewsItem>();
            try
            {
                using (var cxt = new NewsDataContext())
                {
                    model = Mapper.Map<List<News>, List<NewsItem>>(cxt.News.ToList()
                        .Where(x => x.Name.Contains(searchText)).ToList());
                    if (model.Count != 0)
                    {
                        response.Success = true;
                        response.Result = "Success";
                    }
                    else
                    {
                        response.Result = "List is empty";
                    }
                }
            }
            catch (Exception e)
            {
                response.Result = e.Message;
            }

            return PartialView("ListNews", model);
        }
    }
}