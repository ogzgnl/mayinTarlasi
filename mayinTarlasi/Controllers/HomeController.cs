using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mayinTarlasi.Models.Entity;
using System.Web.Security;
namespace mayinTarlasi.Controllers
{
    public class HomeController : Controller
    {
        //Değişkenler
        public static List<List<int>> dict;
        List<int> returnList = new List<int>();
        List<int> visitedItemsList = new List<int>();
        List<String> ChatboxMessages = new List<String>();
        List<String> PersonelMessageList = new List<String>();
        List<String> NotificationList = new List<String>();
        List<String> Users = new List<String>();
        public int visiTimes = 0;
        Random random;

        MVCTestEntities2 db = new MVCTestEntities2();//Db Bağlantısı

        //Mesajlaşma
        public JsonResult GetUsernames()
        {
            TBLgameData UsernameCount = db.TBLgameDatas.OrderByDescending(a => a.ID).FirstOrDefault();
            int UsernameNumber = UsernameCount.ID;

            for (int i = 1; i < UsernameNumber + 1; i++)
            {
                TBLuserData forUsername = db.TBLuserDatas.Where(q => q.ID.Equals(i)).FirstOrDefault();
                Users.Add(forUsername.Username);
            }
            return Json(Users);
        }
        public bool MessageSender(string Message)
        {
            var updateID = Convert.ToInt32(Session["ID_Gamedata"]);
            if (Session["ID_Gamedata"] != null)
            {
                PublicMessenger messageConnection = new PublicMessenger();
                TBLuserData forUsername = db.TBLuserDatas.Where(y => y.ID.Equals(updateID)).FirstOrDefault();
                if (messageConnection != null)
                {
                    messageConnection.SenderID = updateID;
                    messageConnection.Message = Message;
                    messageConnection.Senddate = DateTime.Now;
                    messageConnection.SenderName = forUsername.Username;
                    db.PublicMessengers.Add(messageConnection);
                    db.SaveChanges();
                }
                else
                {
                    return (false);
                }
            }
            return (true);
        }
        public JsonResult MessageReader()
        {
            PublicMessenger messageConnection = db.PublicMessengers.OrderByDescending(a => a.ID).FirstOrDefault();

            if (messageConnection == null)
            {
                ChatboxMessages.Add("Henüz Mesaj Atılmadı");
                return Json(ChatboxMessages);

            }
            int messageNumber = messageConnection.ID;
            for (int i = 1; i < messageNumber + 1; i++)
            {
                PublicMessenger messageConnectionForText = db.PublicMessengers.Where(y => y.ID.Equals(i)).FirstOrDefault();
                if (messageConnectionForText != null)
                {
                    DateTime dt = messageConnectionForText.Senddate ?? DateTime.Now; ;
                    string Timeonly = dt.ToLongTimeString();
                    ChatboxMessages.Add(messageConnectionForText.SenderName + " : " + messageConnectionForText.Message + " " + Timeonly + ".  ");
                }
            }
            return Json(ChatboxMessages);
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
        public JsonResult PmReader(int Receiver)
        {
            var UserId = Convert.ToInt32(Session["ID_Gamedata"]);
            PersonelMessage PmessageConnection = db.PersonelMessages.OrderByDescending(a => a.ID).FirstOrDefault();

            if (PmessageConnection == null)
            {
                PersonelMessageList.Add("Henüz Mesaj Atılmadı");
                return Json(PersonelMessageList);

            }
            int messageNumber = PmessageConnection.ID;
            for (int i = 1; i < messageNumber + 1; i++)
            {
                PersonelMessage PmessageConnectionForText = db.PersonelMessages.Where(y => y.ID.Equals(i)).FirstOrDefault();
                if (PmessageConnectionForText != null)
                {
                    if ((PmessageConnectionForText.ReceiverID == Receiver) && (PmessageConnectionForText.SenderID == UserId))
                    {
                        DateTime dt = PmessageConnectionForText.Date ?? DateTime.Now; ;
                        string Timeonly = dt.ToLongTimeString();

                        TBLuserData UsernameConnection = db.TBLuserDatas.Where(y => y.ID.Equals(UserId)).FirstOrDefault();
                        PersonelMessageList.Add(UsernameConnection.Username + " : " + PmessageConnectionForText.Message + " " + Timeonly);
                    }
                    if ((PmessageConnectionForText.ReceiverID == UserId) && (PmessageConnectionForText.SenderID == Receiver))
                    {
                        DateTime dt = PmessageConnectionForText.Date ?? DateTime.Now; ;
                        string Timeonly = dt.ToLongTimeString();

                        TBLuserData UsernameConnection = db.TBLuserDatas.Where(y => y.ID.Equals(Receiver)).FirstOrDefault();
                        PersonelMessageList.Add(UsernameConnection.Username + " : " + PmessageConnectionForText.Message + " " + Timeonly);
                    }
                }
            }
            return Json(PersonelMessageList);
        }
        public JsonResult PmReaderForNotification()
        {
            var UserId = Convert.ToInt32(Session["ID_Gamedata"]);
            PersonelMessage PmessageConnection = db.PersonelMessages.OrderByDescending(a => a.ID).FirstOrDefault();
            List<String> usernamesList = new List<String>();

            if (PmessageConnection == null)
            {
                NotificationList.Add("Henüz Mesaj Atılmadı");
                return Json(NotificationList);

            }
            int messageNumber = PmessageConnection.ID;
            for (int i = 1; i < messageNumber + 1; i++)
            {
                PersonelMessage PmessageConnectionForText = db.PersonelMessages.Where(y => y.ID.Equals(i)).FirstOrDefault();
                if (PmessageConnectionForText != null)
                {
                    
                    if (PmessageConnectionForText.ReceiverID == UserId)
                    {
                        TBLuserData UsernameConnection = db.TBLuserDatas.Where(y => y.ID.Equals(PmessageConnectionForText.SenderID)).FirstOrDefault();
                        var username = UsernameConnection.Username;
                        if (!usernamesList.Contains(username))
                        {
                            usernamesList.Add(username);
                        }
                    }
                }
            }
            for (int z = 0; z < usernamesList.Count; z++)
            {
                NotificationList.Add(usernamesList[z] + " sana mesaj gönderdi.");
            }
            if (NotificationList.Count==0)
            {
                NotificationList.Add("Henüz mesaj atılmadı.");
            }
            return Json(NotificationList);
        }


        //Mayın Tarlası
        public int RandomIndex(List<int> currentLine, List<int> prevLine)
        {
            List<int> notCluster = new List<int>();// mayın değil listesi açıyor

            for (int i = 0; i < currentLine.Count; i++)//satırın değeri kadar çalışıyor
            {
                int index = currentLine[i]; //şu anki satırı alıyor

                if (index > 1) //mayın değil listesine değerleri eklemeye başlıyor
                {
                    notCluster.Add(index - 1);
                }
                notCluster.Add(index);
                if (index < 21)
                {
                    notCluster.Add(index + 1);
                }
            }

            for (int i = 0; i < prevLine.Count; i++)
            {
                int index = prevLine[i];
                notCluster.Add(index);
            }
            //mayın değil listesi eklemeyi bitiriyor

            List<int> cluster = new List<int>();
            for (int i = 1; i <= 20; i++)
            {
                if (!notCluster.Contains(i))//mayın değil listesinde olmayanlar mayındır diyor
                {
                    cluster.Add(i);
                }
            }

            int resultIndex = random.Next(cluster.Count);//mayın dönüyor

            int result = cluster[resultIndex];


            return result;
        }
        public int GetCount(int index)
        {
            int x = (index % 20);
            int y = (index / 20);

            int count = 0;

            if (y > 0)
            {
                count += GetCountForLine(x, y - 1);
            }

            count += GetCountForLine(x, y);

            if (y < 21)
            {
                count += GetCountForLine(x, y + 1);
            }

            if (HasMine(index))
            {
                return count = 7;
            }
            else
            {
                return count;
            }

        }
        private int GetCountForLine(int x, int y)
        {
            int count = 0;
            if (y < 0)
            {
                y = 0;
            }
            if (y > 20)
            {
                y = 20;
            }
            List<int> line = dict[y];

            if (x > 1)
            {
                if (line.Contains(x - 1))
                {
                    count++;
                }
            }
            if (line.Contains(x))
            {
                count++;
            }
            if (x < 21)
            {
                if (line.Contains(x + 1))
                {
                    count++;
                }
            }
            return count;
        }
        public bool mineChecker(string id)
        {
            int index = Convert.ToInt32(id);
            bool hasMine = HasMine(index);

            if (hasMine)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        public JsonResult EightMineChecker(int index)
        {
            if (GetCount(index) == 0)
            {
                if ((GetCount(index - 1) == 0) && (HasMine(index - 1) == false))
                {
                    if (!returnList.Contains(index - 1) && (index >= 0) && (index <= 400))
                    {
                        returnList.Add(index - 1);
                    }
                }
                if ((GetCount(index + 1) == 0) && (HasMine(index + 1) == false))
                {
                    if (!returnList.Contains(index + 1) && (index >= 0) && (index <= 400))
                    {
                        returnList.Add(index + 1);
                    }
                }
                if ((GetCount(index - 20) == 0) && (HasMine(index - 20) == false))
                {
                    if (!returnList.Contains(index - 20) && (index >= 0) && (index <= 400))
                    {
                        returnList.Add(index - 20);
                    }
                }
                if ((GetCount(index - 21) == 0) && (HasMine(index - 21) == false))
                {
                    if (!returnList.Contains(index - 21) && (index >= 0) && (index <= 400))
                    {
                        returnList.Add(index - 21);
                    }
                }
                if ((GetCount(index - 19) == 0) && (HasMine(index - 19) == false))
                {
                    if (!returnList.Contains(index - 19) && (index >= 0) && (index <= 400))
                    {
                        returnList.Add(index - 19);
                    }
                }
                if ((GetCount(index + 20) == 0) && (HasMine(index + 20) == false))
                {
                    if (!returnList.Contains(index + 20) && (index >= 0) && (index <= 400))
                    {
                        returnList.Add(index + 20);
                    }
                }
                if ((GetCount(index + 21) == 0) && (HasMine(index + 21) == false))
                {
                    if (!returnList.Contains(index + 21) && (index >= 0) && (index <= 400))
                    {
                        returnList.Add(index + 21);
                    }
                }
                if ((GetCount(index + 19) == 0) && (HasMine(index + 19) == false))
                {
                    if (!returnList.Contains(index + 19) && (index >= 0) && (index <= 400))
                    {
                        returnList.Add(index + 19);
                    }
                }
                if (visiTimes < returnList.Count)
                {
                    visiTimes += 1;
                    visitedItemsList.Add(returnList[visiTimes - 1]);
                    EightMineChecker(returnList[visiTimes - 1]);
                }
            }
            visiTimes = 0;
            return Json(returnList);
        }
        public bool gameUpdater(string point, string flag, string time)
        {
            var updateID = Convert.ToInt32(Session["ID_Gamedata"]);
            if (Session["ID_Gamedata"] != null)
            {
                TBLgameData updateConnection = db.TBLgameDatas.Where(a => a.UserID.Equals(updateID)).FirstOrDefault();
                if (updateConnection != null)
                {
                    updateConnection.Score = Convert.ToInt32(point);
                    updateConnection.Time = Convert.ToInt32(time);
                    updateConnection.flagsUsed = Convert.ToInt32(flag);
                    db.SaveChanges();
                    System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    return (false);
                }
            }
            return (true);
        }
        public bool HasMine(int index)
        {
            int x = 0;
            int y = 0;
            if (index < 20)
            {
                y = 0;
                x = index;
            }
            else
            {
                x = (index % 20);
                y = index / 20;
            }
            if ((y < 0) || (y > 20))
            {
                y = 0;
            }
            List<int> line = dict[y];

            if (line.Contains(x))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Action Result'lar
        public ActionResult Index(string submit)
        {
            random = new Random();
            dict = new List<List<int>>();
            if (Session["ID"] != null)
            {
                var id = Convert.ToInt32(Session["ID"]);
                TBLgameData resultData = db.TBLgameDatas.Where(y => y.ID.Equals(id)).FirstOrDefault();
                if (resultData != null)
                {
                    Session["ID_Gamedata"] = resultData.ID;
                    Session["Score_Gamedata"] = resultData.Score;
                    Session["Time_Gamedata"] = resultData.Time;
                    Session["Try_Gamedata"] = resultData.Try;
                    Session["Flag_Gamedata"] = resultData.flagsUsed;
                    Session["IsLoggedIn_Gamedata"] = "logged";
                }
            }
            ViewBag.Submit = submit;
            random = new Random();
            dict = new List<List<int>>();

            for (int i = 0; i <= 20; i++)//sutunları ekledi.
            {
                List<int> line = new List<int>();
                dict.Add(line);
            }

            for (int i = 0; i < dict.Count; i++)
            {
                int count = random.Next(2, 3);

                for (int j = 0; j < count; j++)//mayın ekleme foru
                {
                    int randomValue = 0;

                    if (i == 0) //
                    {
                        randomValue = RandomIndex(dict[i], new List<int>());//mayın döndü
                    }
                    else
                    {
                        randomValue = RandomIndex(dict[i], dict[i - 1]);//mayın döndü
                    }

                    dict[i].Add(randomValue);
                }
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult LoginPage()
        {
            return View();
        }
    }
}