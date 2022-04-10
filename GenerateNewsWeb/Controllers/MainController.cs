using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenerateNewsBLL.Services;
using GenerateNewsDAL.Models;

namespace GenerateNewsWeb.Controllers
{
    public class MainController : Controller
    {
        Service service = new Service();
        WordService wordservice = new WordService();
        PhraseService phraseservice = new PhraseService(); 
        NewsService newsservice = new NewsService();
        ConfigWordService confwordservice = new ConfigWordService();
        ConfigPhraseService confphraseservice = new ConfigPhraseService();
        IndexService indexservice = new IndexService();
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Words()
        {
            return View();
        }

        public ActionResult Vectors()
        {
            return View();
        }

        public ActionResult Phrases()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult ConfigPhrases()
        {
            return View();
        }

        public ActionResult ConfigWords()
        {
            return View();
        }

        #region Wors
        [HttpPost]
        public JsonResult WordCount()
        {
            return Json(wordservice.WordCnt());
        }

        [HttpPost]
        public JsonResult UpdateWord(WordModel model)
        {
            return Json(wordservice.UpdateWord(model));
        }

        [HttpPost]
        public JsonResult InsertWord(WordModel model)
        {
            wordservice.InsertWord(model);
            return Json(true);
        }

        [HttpPost]
        public JsonResult SelectWords(NavigateModel model)
        {            
            return Json(wordservice.Select(model.topmin, model.topmax));
        }

        #endregion

        #region Phrase
        [HttpPost]
        public JsonResult PhraseCount()
        {
            return Json(phraseservice.PhraseCnt());
        }

        [HttpPost]
        public JsonResult UpdatePhrase(PhraseModel model)
        {
            return Json(phraseservice.UpdatePhrase(model));
        }

        [HttpPost]
        public JsonResult InsertPhrase(PhraseModel model)
        {
            phraseservice.InsertPhrase(model);
            return Json(true);
        }

        [HttpPost]
        public JsonResult SelectPhrases(NavigateModel model)
        {
            return Json(phraseservice.SelectPhrase(model.topmin, model.topmax));
        }
        #endregion

        #region News
        [HttpPost]
        public JsonResult NewsCount()
        {
            return Json(newsservice.NewsCnt());
        }

        [HttpPost]
        public JsonResult UpdateNews(NewsModel model)
        {
            return Json(newsservice.UpdateNews(model));
        }

        [HttpPost]
        public JsonResult InsertNews(NewsModel model)
        {
            newsservice.InsertNews(model);
            return Json(true);
        }

        [HttpPost]
        public JsonResult SelectNews(NavigateModel model)
        {
            return Json(newsservice.SelectNews(model.topmin, model.topmax));
        }
        #endregion

        #region ConfigWord
        [HttpPost]
        public JsonResult ConfigWordCount()
        {
            return Json(confwordservice.ConfigWordCnt());
        }

        [HttpPost]
        public JsonResult UpdateConfigWord(ConfigWordModel model)
        {
            return Json(confwordservice.UpdateConfigWord(model));
        }

        [HttpPost]
        public JsonResult InsertConfigWord(ConfigWordModel model)
        {
            confwordservice.InsertConfigWord(model);
            return Json(true);
        }

        [HttpPost]
        public JsonResult SelectConfigWord(NavigateModel model)
        {
            return Json(confwordservice.SelectConfigWord(model.topmin, model.topmax));
        }

        [HttpPost]
        public JsonResult FindConfWord(ConfigWordModel model, int topmax)
        {
            return Json(confwordservice.FindConfigWord(model, topmax));
        }
        #endregion

        #region ConfigPhrase
        [HttpPost]
        public JsonResult ConfigPhraseCount()
        {
            return Json(confphraseservice.ConfigPhraseCnt());
        }

        [HttpPost]
        public JsonResult UpdateConfigPhrase(ConfigPhraseModel model)
        {
            return Json(confphraseservice.UpdateConfigPhrase(model));
        }

        [HttpPost]
        public JsonResult InsertConfigPhrase(ConfigPhraseModel model)
        {
            confphraseservice.InsertConfigPhrase(model);
            return Json(true);
        }

        [HttpPost]
        public JsonResult SelectConfigPhrase(NavigateModel model)
        {
            return Json(confphraseservice.SelectConfigPhrase(model.topmin, model.topmax));
        }
        #endregion

        #region Index
        [HttpPost]
        public JsonResult LearnText(NewsModel model)
        {
            return Json(indexservice.LearnText(model));
        }

        public JsonResult GeneratePhrase(string flagmix)
        {
            return Json(indexservice.GeneratePhrase(flagmix));
        }

        public JsonResult GenerateNews(string ThemeNews, bool newvector, bool newphrases)
        {
            return Json(indexservice.GenerateNews(ThemeNews, newvector, newphrases));
        }
        #endregion


        public JsonResult GetTypeWords()
        {
            return Json(service.GetTypeWords());
        }

        public JsonResult GetThemesNews()
        {
            return Json(service.GetThemesNews());
        }
    }
}