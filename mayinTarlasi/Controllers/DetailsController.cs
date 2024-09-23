using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mayinTarlasi.Models.Entity;
using System.Data.Entity;

namespace mayinTarlasi.Controllers
{
    public class DetailsController : Controller
    {
        // GET: AboutPageDetails
        public ActionResult Index()
        {
            return View();
        }
        MVCTestEntities2 db = new MVCTestEntities2();
        public ActionResult ScoreTable()
        {
            using (var datamodel = new MVCTestEntities2())
            {
                var j = datamodel.TBLuserDatas.Include(a => a.TBLgameDatas).OrderByDescending(a => a.TBLgameDatas.FirstOrDefault().Score).ToList();
                return View(j);
            }
        }
        public ActionResult GetDataFrom()
        {
            var degerler = db.TBLgameDatas.ToList();
            return View("ScoreTable");
        }
        public ActionResult AboutGame()
        {
            return View();
        }
        public ActionResult HowToPlay()
        {
            return View();
        }
        public bool PmSender(string Message, int Receiver)
        {
            if (Session["ID_Gamedata"] != null)
            {
                var SenderId = Convert.ToInt32(Session["ID_Gamedata"]);
                PersonelMessage PersonelMessager = new PersonelMessage();
                if (PersonelMessager != null)
                {
                    PersonelMessager.SenderID = SenderId;
                    PersonelMessager.ReceiverID = Receiver;
                    PersonelMessager.Message = Message;
                    PersonelMessager.Date = DateTime.Now;
                    db.PersonelMessages.Add(PersonelMessager);
                    db.SaveChanges();
                }
            }
            return true;
        }
    }
}