using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mayinTarlasi.Models.Entity;
using System.Web.Security;
using System.IO;

namespace mayinTarlasi.Controllers
{
    public class AccountController : Controller
    {
        MVCTestEntities2 db = new MVCTestEntities2();
        public ActionResult CreateUser()
        {
            return View();
        }
        public string staticUserName = "";
       
        string NameUsername;
        public bool userCreater(string UserName, string NameSurname, string Password, string EMail)
        {
            TBLuserData Userdata = new TBLuserData();
            TBLgameData Gamedata = new TBLgameData();
            if (Userdata != null)
            {
                Userdata.Username = UserName;
                Userdata.NameSurname = NameSurname;
                Userdata.Password = Password;
                Userdata.Email = EMail;
                db.TBLuserDatas.Add(Userdata);
                Gamedata.Score = 0;
                Gamedata.flagsUsed = 0;
                Gamedata.Time = 0;
                Gamedata.Try = 0;
                db.TBLgameDatas.Add(Gamedata);
                db.SaveChanges();

            }
            System.Threading.Thread.Sleep(1000);
            return true;
        }
        public ActionResult Upload()
        {
            int idForUpdate = Convert.ToInt32(Session["ID"]);
            TBLuserData Userdata = db.TBLuserDatas.Where(b => b.ID.Equals(idForUpdate)).FirstOrDefault();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Pictures/ProfilePhotos"), Convert.ToString(idForUpdate)+".png");
                System.IO.File.Delete(path);
                file.SaveAs(path);
                Userdata.ProfilePic = 1;
                db.SaveChanges();
            }
            return Json("Perfecto");
        }
        public string Loginer(string UserName, string Password)
        {
            TBLuserData Userdata = db.TBLuserDatas.Where(b => b.Username.Equals(UserName) && b.Password.Equals(Password)).FirstOrDefault();
            if (Userdata != null)
            {
                NameUsername = Userdata.NameSurname;
                Session["username"] = Userdata.Username;
                Session["password"] = Userdata.Password;
                Session["ID"] = Userdata.ID;
                Session["Email"] = Userdata.Email;
                Session["NameSurname"] = Userdata.NameSurname;
                Session["IsLoggedIn"] = "loggedIn";
                Session["ProfilePic"] = Userdata.ProfilePic;
                return NameUsername;
            }
            else
            {
                return NameUsername = null;
            }
        }
        public bool accountUpdater(string UserName, string Password, string Mail, string NameSurname)
        {
            int idForUpdate= Convert.ToInt32(Session["ID"]);
            TBLuserData Userdata = db.TBLuserDatas.Where(b => b.ID.Equals(idForUpdate)).FirstOrDefault();
            if (Userdata != null)
            {
                Userdata.Username = UserName;
                Userdata.Password = Password;
                Userdata.Email = Mail;
                Userdata.NameSurname = NameSurname;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult myAccount()
        {
            if ((Session["username"] == null) || (Session["password"] == null) || (Session["ID"] == null) || (Session["Email"] == null) || (Session["NameSurname"] == null))
            {
                return RedirectToAction("../Home/Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            return View();
        }

    }
}