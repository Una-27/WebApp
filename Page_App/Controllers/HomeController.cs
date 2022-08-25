using Microsoft.AspNetCore.Http;
using NPOI.OpenXml4Net.OPC;
using Page_App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Page_App.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();

        public bool Validate(string var_, int type_)
        {
            string letters_ = "";
            int mis_ = 0;

            if (var_.Length > 0)
            {
                if (type_ == 0)
                {
                    letters_ = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя -";
                }
                else if (type_ == 1)
                {
                    letters_ = "0123456789";
                }
                for (int i = 0; i < var_.Length; i++)
                {
                    for (int j = 0; j < letters_.Length; j++)
                    {
                        if (var_[i] == letters_[j])
                        {
                            mis_++;
                            break;
                        }
                    }
                }
                if (mis_ == var_.Length)
                {
                    return true;
                }
            }

            return false;
        }

        /*App*/
        public ActionResult App()
        {
            ViewBag.Title = "App";
            ViewBag.Data = "0";//Serialization<Masters>.WriteList(db.Master_.ToList());

            return View();
        }

        [HttpGet]
        public ActionResult App(char id = 'c')
        {
            string data = "";

            if (id == 'c')          data = Serialization<Clients>.WriteList(db.Client_.ToList());
            else if (id == 'f')     data = Serialization<Finance>.WriteList(db.Finance_.ToList());
            else if (id == 'm')     data = Serialization<Masters>.WriteList(db.Master_.ToList());
            else if (id == 'n')     data = Serialization<News>.WriteList(db.News_.ToList());
            else if (id == 's')     data = Serialization<Services>.WriteList(db.Service_.ToList());
            else if (id == 'w')     data = Serialization<ServicesFromMaster>.WriteList(db.ServicesFromMaster_.ToList());
            else if (id == 't')     data = Serialization<Timetable>.WriteList(db.Timetable_.ToList());


            return Content(data);
        }

        public ActionResult Index()
        {
            Site.CreateFromUrl("https://169.254.231.184:45455/");
            db.ValidSet();
            return View();
        }

        /*Отображение картинки*/
       /* public FileContentResult GetImage(int masterId)
        {
            Masters master = db.Master_.Find(masterId);
            if (master != null)
            {
                return File(master.ImageData, );
            }
            else
            {
                return null;
            }
        }*/

        /*Рачеты*/
        public void FinanceCalculation (DateTime start_, DateTime end_)
        {
            List<Masters> masters = db.Master_.ToList();
            List<Clients> clients = db.Client_.ToList();
            List<Services> services = db.Service_.ToList();
            List<Finance> finances = db.Finance_.ToList();
            List<Timetable> timetables = db.Timetable_.ToList();
            int CFp = 0, CFm = 0, NCF;

            foreach (Finance f in finances)
            {
                if (f.Type) 
                    CFp += f.Summa;
                else
                    CFm += f.Summa;
            }
            NCF = CFp - CFm;
            
            foreach (Timetable t in timetables)
            {

            }
        }

        /*Страница мастеров*/
        public ActionResult Master_Page()
        {
            ViewBag.M_ = db.Master_.ToList();
            ViewBag.S_ = db.Service_.ToList();
            return View();
        }

        /*Редактирование/добавление сотрудника*/
        [HttpGet]
        public ActionResult Master_ChangePage(int id = 0)
        {
            //добавление
            {
                ViewBag.Id = -1;
                ViewBag.FirstName = "";
                ViewBag.Name = "";
                ViewBag.MiddleName = "";
                ViewBag.DateOfBirth = DateTime.Parse("2000-01-01");
                ViewBag.PhoneNumber = "";
                ViewBag.Profession = "";
                ViewBag.Salary = "Почасовая оплата";
                ViewBag.Rate = 1000;
                ViewBag.ImageData = null;
                ViewBag.Info = "";
                ViewBag.Title = "Добавление карточки";
            }

            //редактирование
            Masters invar = db.Master_.Find(id);
            if ((id != -1) && (invar != null))
            {
                ViewBag.Id = invar.Id;
                ViewBag.FirstName = invar.FirstName;
                ViewBag.Name = invar.Name;
                ViewBag.MiddleName = invar.MiddleName;
                ViewBag.DateOfBirth = invar.DateOfBirth;
                ViewBag.PhoneNumber = invar.PhoneNumber;
                ViewBag.Profession = invar.Profession;
                ViewBag.Salary = invar.Salary;
                ViewBag.Rate = invar.Rate;
                ViewBag.ImageData = invar.ImageData;
                ViewBag.Info = invar.Info;
                ViewBag.Title = "Редактирование карточки";
            }

            ViewBag.S_ = db.Service_.ToList();
            ViewBag.SFM_ = db.ServicesFromMaster_.ToList();

            return View();
        }

        /*Сохранение изменений в базе мастеров*/
        [HttpPost]
        public ActionResult Master_ChangePage(Masters in_master, string action, HttpPostedFileBase image)
        {
            if (image != null)
            {
                in_master.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(in_master.ImageData, 0, image.ContentLength);
            }

            if (action != "Удалить")
            {
                if ((in_master.Id == -1) || (in_master.Id == 0))
                {
                    db.Entry(in_master).State = EntityState.Added;
                }
                else
                {
                    db.Entry(in_master).State = EntityState.Modified;
                }
            }
            else
            {
                try
                {
                    db.Entry(in_master).State = EntityState.Deleted;
                }
                catch { };
            }
            db.SaveChanges();

            return RedirectToAction("Master_Page");
        }

        /*Страница клиентов*/
        public ActionResult Client_Page()
        {
            var client = db.Client_.ToList();
            ViewBag.Client_ = client;
            return View();
        }

        /*Редактирование/добавление клиента*/
        [HttpGet]
        public ActionResult Client_ChangePage(int id = 0)
        {
            //добавление
            {
                ViewBag.Id = -1;
                ViewBag.FirstName = "";
                ViewBag.Name = "";
                ViewBag.MiddleName = "";
                ViewBag.DateOfBirth = DateTime.Parse("2000-01-01");
                ViewBag.PhoneNumber = "";
                ViewBag.AdvertisingChannel = "Соцсети";
                ViewBag.Discount = 0;
                ViewBag.Title = "Добавление карточки";
            }

            //редактирование
            Clients invar = db.Client_.Find(id);
            if ((id != -1) && (invar != null))
            {
                ViewBag.Id = invar.Id;
                ViewBag.FirstName = invar.FirstName;
                ViewBag.Name = invar.Name;
                ViewBag.DateOfBirth = invar.DateOfBirth;
                ViewBag.PhoneNumber = invar.PhoneNumber;
                ViewBag.AdvertisingChannel = invar.AdvertisingChannel;
                ViewBag.Discount = invar.Discount;
                ViewBag.Title = "Редактирование карточки";
            }

            return View();
        }

        /*Сохранение изменений в базе клиентов*/
        [HttpPost]
        public ActionResult Client_ChangePage(Clients in_client, string action, string AdvertisingChannel)
        {
            in_client.AdvertisingChannel = AdvertisingChannel;
            if (action == "Сохранить")
            {
                if ((in_client.Id == -1) || (in_client.Id == 0))
                {
                    in_client.DateAdded = DateTime.Now;
                    db.Entry(in_client).State = EntityState.Added;
                }
                else
                {
                    try
                    {
                        db.Entry(in_client).State = EntityState.Modified;
                    }
                    catch
                    {
                        db.Entry(in_client).State = EntityState.Added;
                    }
                }
            }
            else if (action == "Удалить")
            {
                try
                {
                    db.Entry(in_client).State = EntityState.Deleted;
                }
                catch { };
            }
            db.SaveChanges();

            return RedirectToAction("Client_Page");
        }

        /*Страница услуг*/
        public ActionResult Service_Page()
        {
            ViewBag.S_ = db.Service_.ToList();

            return View();
        }

        /*Редактирование/добавление услуги*/
        [HttpGet]
        public ActionResult Service_ChangePage(int id = 0)
        {
            //добавление
            {
                ViewBag.Id = -1;
                ViewBag.Name = "";
                ViewBag.Category = "";
                ViewBag.Price = "";
                ViewBag.TimeOfWork = "";
                ViewBag.Info = "";
                ViewBag.ImageData = null;
                ViewBag.Title = "Добавление карточки";
            }

            //редактирование
            Services invar = db.Service_.Find(id);
            if ((id != -1) && (invar != null))
            {
                ViewBag.Id = id;
                ViewBag.Name = invar.Name;
                ViewBag.Category = invar.Category;
                ViewBag.Price = invar.Price;
                ViewBag.TimeOfWork = invar.TimeOfWork;
                ViewBag.Info = invar.Info;
                ViewBag.ImageData = invar.ImageData;
                ViewBag.Title = "Редактирование карточки";
            }

            return View();
        }

        /*Сохранение изменений в базе услуг*/
        [HttpPost]
        public ActionResult Service_ChangePage(Services in_service, string action, HttpPostedFileBase image)
        {
            if (image != null)
            {
                in_service.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(in_service.ImageData, 0, image.ContentLength);
            }

            if (action != "Удалить")
            {
                if ((in_service.Id == -1) || (in_service.Id == 0))
                {
                    db.Entry(in_service).State = EntityState.Added;
                }
                else
                {
                    db.Entry(in_service).State = EntityState.Modified;
                }
            }
            else
            {
                try
                {
                    db.Entry(in_service).State = EntityState.Deleted;
                }
                catch { };
            }
            db.SaveChanges();

            return RedirectToAction("Service_Page");
        }

        /*Страница финансов*/
        public ActionResult Finance_Page()
        {
            var finance = db.Finance_.ToList();
            ViewBag.F_ = finance;
            return View();
        }

        /*Редактирование/добавление финансов*/
        [HttpGet]
        public ActionResult Finance_ChangePage(int id = 0)
        {
            //добавление
            {
                ViewBag.Id = -1;
                ViewBag.IdMaster = null;
                ViewBag.Category = "";
                ViewBag.Type = true;
                ViewBag.Summa = 0;
                ViewBag.Date = DateTime.Now;
                ViewBag.Info = "";
                ViewBag.Title = "Добавление карточки";
            }

            //редактирование
            Finance invar = db.Finance_.Find(id);
            if ((id != -1) && (invar != null))
            {
                ViewBag.Id = invar.Id;
                ViewBag.IdMaster = invar.IdMaster;
                ViewBag.Category = invar.Category;
                ViewBag.Type = invar.Type;
                ViewBag.Summa = invar.Summa;
                ViewBag.Date = invar.Date;
                ViewBag.Info = invar.Info;
                ViewBag.Title = "Редактирование карточки";
            }

            return View();
        }

        /*Сохранение изменений в базе финансов*/
        [HttpPost]
        public ActionResult Finance_ChangePage(Finance in_finance, string action)
        {
            if (action == "Сохранить")
            {
                if ((in_finance.Id == -1) || (in_finance.Id == 0))
                {
                    db.Entry(in_finance).State = EntityState.Added;
                }
                else
                {
                    try
                    {
                        db.Entry(in_finance).State = EntityState.Modified;
                    }
                    catch
                    {
                        db.Entry(in_finance).State = EntityState.Added;
                    }
                }
            }
            else if (action == "Удалить")
            {
                try
                {
                    db.Entry(in_finance).State = EntityState.Deleted;
                }
                catch { };
            }
            db.SaveChanges();

            return RedirectToAction("Finance_Page");
        }

        public ActionResult List_Page()
        {
            ViewBag.M_ = db.Master_.ToList();
            ViewBag.S_ = db.Service_.ToList();
            ViewBag.C_ = db.Client_.ToList();
            ViewBag.Data = db.Timetable_.ToList(); 
            ViewBag.Date = DateTime.Now;
            ViewBag.Lastitem = db.Timetable_.ToList().Last();
            return View();
        }

        [HttpPost]
        public ActionResult List_Page(string date_)
        {
            ViewBag.M_ = db.Master_.ToList();
            ViewBag.S_ = db.Service_.ToList();
            ViewBag.C_ = db.Client_.ToList();
            ViewBag.Data = db.Timetable_.ToList(); 
            if ((date_ == null) || (date_ == ""))
                ViewBag.Date = DateTime.Now;
            else 
                ViewBag.Date = DateTime.Parse(date_);
            ViewBag.Lastitem = db.Timetable_.ToList().Last();
            return View();
        }

        public ActionResult Reports_Page()
        {
            ViewBag.M_ = db.Master_.ToList();
            ViewBag.S_ = db.Service_.ToList();
            ViewBag.C_ = db.Client_.ToList();
            ViewBag.T_ = db.Timetable_.ToList();
            ViewBag.F_ = db.Finance_.ToList();
            ViewBag.Date1 = DateTime.Parse("01.07.2022");
            ViewBag.Date2 = DateTime.Now.AddMonths(1);

            return View();
        }

        [HttpPost]
        public ActionResult Reports_Page(string date1_, string date2_)
        {
            ViewBag.M_ = db.Master_.ToList();
            ViewBag.S_ = db.Service_.ToList();
            ViewBag.C_ = db.Client_.ToList();
            ViewBag.T_ = db.Timetable_.ToList();
            ViewBag.F_ = db.Finance_.ToList();
            if ((date1_ == "") || (date2_ == ""))
            {
                ViewBag.Date1 = DateTime.Now;
                ViewBag.Date2 = DateTime.Now.AddMonths(1);
            }
            else
            {
                ViewBag.Date1 = DateTime.Parse(date1_);
                ViewBag.Date2 = DateTime.Parse(date2_);
            }

            return View();
        }









        /*
        public ActionResult List_Page()
        {
            string date_ = null;
            List<List_> l = new List<List_>();
            l.Add(new List_ { Date = DateTime.Now, TimeStart = 10, TimeEnd = 12.5, Client = "Валентина Киторова", Master = "Мастер1", Service = "стрижка" });
            l.Add(new List_ { Date = DateTime.Now, TimeStart = 9, TimeEnd = 9.5, Client = "Гала Риторева", Master = "Мастер2", Service = "маникюр" });
            l.Add(new List_ { Date = DateTime.Now, TimeStart = 10, TimeEnd = 11.5, Client = "Вавилон Тиловин", Master = "Мастер3", Service = "покраска бровей" });
            l.Add(new List_ { Date = DateTime.Now, TimeStart = 9.5, TimeEnd = 11, Client = "Виталий Колготин", Master = "Мастер4", Service = "педикюр" });
            l.Add(new List_ { Date = DateTime.Parse("02.01.2022"), TimeStart = 9, TimeEnd = 9.5, Client = "Василина Кротова", Master = "Мастер2", Service = "наращивание рестниц" });
            ViewBag.Data = l;
            if (date_ == null) ViewBag.Date = DateTime.Now;
            else ViewBag.Date = DateTime.Parse(date_);
            ViewBag.Lastitem = l.Last();
            return View();
        }

        [HttpPost]
        public ActionResult List_Page(string date)
        {
            List<List_> l = new List<List_>();
            l.Add(new List_ { Date = DateTime.Now, TimeStart = 10, TimeEnd = 12.5, Client = "Валентина Киторова", Master = "Мастер1", Service = "стрижка" });
            l.Add(new List_ { Date = DateTime.Now, TimeStart = 11, TimeEnd = 11.5, Client = "Гала Риторева", Master = "Мастер2", Service = "маникюр" });
            l.Add(new List_ { Date = DateTime.Now, TimeStart = 10, TimeEnd = 12.5, Client = "Вавилон Тиловин", Master = "Мастер3", Service = "покраска бровей" });
            l.Add(new List_ { Date = DateTime.Now, TimeStart = 10, TimeEnd = 12.5, Client = "Виталий Колготин", Master = "Мастер4", Service = "педикюр" });
            l.Add(new List_ { Date = DateTime.Parse("02.01.2022"), TimeStart = 9, TimeEnd = 9.5, Client = "Василина Кротова", Master = "Мастер2", Service = "наращивание рестниц" });
            ViewBag.Data = l;
            if ((date == null) || (date == "")) ViewBag.Date = DateTime.Now;
            else ViewBag.Date = DateTime.Parse(date);
            ViewBag.Lastitem = l.Last();
            return View();
        }

        public ActionResult Client_Page()
        {
            var data = db.Client_.ToList();
            ViewBag.Client_ = data;
            return View();
        }

        [HttpGet]
        public ActionResult Client_ChangePage(int id)
        {
            Client inputvar = db.Client_.Find(id);
            if (id != -1)
            {
                ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.Surname = inputvar.Surname;
                ViewBag.Name = inputvar.Name;
                ViewBag.MiddleName = inputvar.MiddleName;
                ViewBag.DateOfBirth = inputvar.DateOfBirth;
                ViewBag.PhoneNumber = inputvar.PhoneNumber;
            }
            else
            {
                ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.Surname = "";
                ViewBag.Name = "";
                ViewBag.MiddleName = "";
                ViewBag.DateOfBirth = DateTime.Parse("2000-01-01");
                ViewBag.PhoneNumber = "";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Client_ChangePage(Client client)
        {
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
            int misletters = 0;
            int miselement = 0;

            if (client.Surname != null)
            {
                for (int i = 0; i < client.Surname.Length; i++)
                    for (int j = 0; j < rusletters.Length; j++)
                        if (client.Surname.ToLower()[i] == rusletters[j]) misletters++;
                if (misletters != client.Surname.Length) ViewBag.Surname = "-1";
                else
                {
                    ViewBag.Surname = client.Surname;
                    miselement++;
                }
            }
            else
            {
                ViewBag.Surname = client.Surname;
                miselement++;
            }

            if (client.Name != null)
            {
                misletters = 0;
                for (int i = 0; i < client.Name.Length; i++)
                    for (int j = 0; j < rusletters.Length; j++)
                        if (client.Name.ToLower()[i] == rusletters[j]) misletters++;
                if (misletters != client.Name.Length) ViewBag.Name = "-1";
                else
                {
                    ViewBag.Name = client.Name;
                    miselement++;
                }
            } else ViewBag.Name = "-1";

            if (client.MiddleName != null)
            {
                misletters = 0;
                for (int i = 0; i < client.MiddleName.Length; i++)
                    for (int j = 0; j < rusletters.Length; j++)
                        if (client.MiddleName.ToLower()[i] == rusletters[j]) misletters++;
                if (misletters != client.MiddleName.Length) ViewBag.MiddleName = "-1";
                else
                {
                    ViewBag.MiddleName = client.MiddleName;
                    miselement++;
                }
            }
            else
            {
                ViewBag.MiddleName = client.MiddleName;
                miselement++;
            }

            if (client.DateOfBirth >= DateTime.Now) ViewBag.DateOfBirth = "-1";
            else
            {
                ViewBag.DateOfBirth = client.DateOfBirth;
                miselement++;
            }

            if (client.PhoneNumber != null)
            {
                misletters = 0;
                for (int i = 0; i < client.PhoneNumber.Length; i++)
                    for (int j = 0; j < numberletters.Length; j++)
                        if (client.PhoneNumber[i] == numberletters[j]) misletters++;
                if (misletters != client.PhoneNumber.Length) ViewBag.PhoneNumber = "-1";
                else
                {
                    ViewBag.PhoneNumber = client.PhoneNumber;
                    miselement++;
                }
            }
            else ViewBag.PhoneNumber = "-1";

            if (miselement == 5)
            {
                if (client.Id != -1)
                {
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Client_.Add(client);
                    db.SaveChanges();
                }
                return RedirectToAction("Client_Page");
            }
            else return View();
        }

        public ActionResult Master_Page()
        {
            ViewBag.Master_ = db.Master_.ToList();
            ViewBag.Service_ = db.Service_.ToList();
            ViewBag.ServiseMaster_ = db.ServiseMaster_.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Master_ChangePage(int id)
        {
            ViewBag.Master_ = db.Master_.ToList();
            ViewBag.Service_ = db.Service_.ToList();
            ViewBag.ServiseMaster_ = db.ServiseMaster_.ToList();
            Master inputvar = db.Master_.Find(id);
            if (id != -1)
            {
                ViewBag.Id = id;
                ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.Surname = inputvar.Surname;
                ViewBag.Name = inputvar.Name;
                ViewBag.MiddleName = inputvar.MiddleName;
                ViewBag.DateOfBirth = inputvar.DateOfBirth;
                ViewBag.PhoneNumber = inputvar.PhoneNumber;
                //ViewBag.ImageUrl = inputvar.ImageUrl;
            }
            else
            {
                ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.Surname = "";
                ViewBag.Name = "";
                ViewBag.MiddleName = "";
                ViewBag.DateOfBirth = DateTime.Parse("2000-01-01");
                ViewBag.PhoneNumber = "";
                //ViewBag.ImageUrl = "";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Master_ChangePage(Master master)
        {
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
            int misletters = 0;
            int miselement = 0;

            if (master.Surname != null)
            {
                for (int i = 0; i < master.Surname.Length; i++)
                    for (int j = 0; j < rusletters.Length; j++)
                        if (master.Surname.ToLower()[i] == rusletters[j]) misletters++;
                if (misletters != master.Surname.Length) ViewBag.Surname = "-1";
                else
                {
                    ViewBag.Surname = master.Surname;
                    miselement++;
                }
            }
            else
            {
                ViewBag.Surname = master.Surname;
                miselement++;
            }

            if (master.Name != null)
            {
                misletters = 0;
                for (int i = 0; i < master.Name.Length; i++)
                    for (int j = 0; j < rusletters.Length; j++)
                        if (master.Name.ToLower()[i] == rusletters[j]) misletters++;
                if (misletters != master.Name.Length) ViewBag.Name = "-1";
                else
                {
                    ViewBag.Name = master.Name;
                    miselement++;
                }
            }
            else ViewBag.Name = "-1";

            if (master.MiddleName != null)
            {
                misletters = 0;
                for (int i = 0; i < master.MiddleName.Length; i++)
                    for (int j = 0; j < rusletters.Length; j++)
                        if (master.MiddleName.ToLower()[i] == rusletters[j]) misletters++;
                if (misletters != master.MiddleName.Length) ViewBag.MiddleName = "-1";
                else
                {
                    ViewBag.MiddleName = master.MiddleName;
                    miselement++;
                }
            }
            else
            {
                ViewBag.MiddleName = master.MiddleName;
                miselement++;
            }

            if (master.DateOfBirth >= DateTime.Now) ViewBag.DateOfBirth = "-1";
            else
            {
                ViewBag.DateOfBirth = master.DateOfBirth;
                miselement++;
            }

            if (master.PhoneNumber != null)
            {
                misletters = 0;
                for (int i = 0; i < master.PhoneNumber.Length; i++)
                    for (int j = 0; j < numberletters.Length; j++)
                        if (master.PhoneNumber[i] == numberletters[j]) misletters++;
                if (misletters != master.PhoneNumber.Length) ViewBag.PhoneNumber = "-1";
                else
                {
                    ViewBag.PhoneNumber = master.PhoneNumber;
                    miselement++;
                }
            }
            else ViewBag.PhoneNumber = "-1";

            HttpPostedFileBase file = Request.Files["ImageUrl"];  
            //int i = service.UploadImageInDataBase(file, model); 
            var photo = WebImage.GetImageFromRequest();

            //else
                //master.ImageUrl = "https://flyclipart.com/thumb2/business-man-21046.png";
            //if (master.ImageUrl == "") master.ImageUrl = "https://flyclipart.com/thumb2/business-man-21046.png";
            //ViewBag.ImageUrl = master.ImageUrl;

            if (miselement == 5)
            {
                if (master.Id != -1)
                {
                    db.Entry(master).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Master_.Add(master);
                    db.SaveChanges();
                }
                return RedirectToAction("Master_Page");
            }
            else return View();
        }

        public ActionResult Service_Page()
        {
            var data = db.Service_.ToList();
            ViewBag.Service_ = data;
            return View();
        }

        [HttpGet]
        public ActionResult Service_ChangePage(int id)
        {
            Service inputvar = db.Service_.Find(id);
            if (id != -1)
            {
                ViewBag.Name = inputvar.Name;
                ViewBag.ImageUrl = inputvar.ImageUrl;
            }
            else
            {
                ViewBag.Name = "";
                ViewBag.ImageUrl = "";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Service_ChangePage(Service service)
        {
            int misletters = 0;

            if (service.Name != null)
            {
                misletters = 0;
                for (int i = 0; i < service.Name.Length; i++)
                    for (int j = 0; j < rusletters.Length; j++)
                        if (service.Name.ToLower()[i] == rusletters[j]) misletters++;
                if (misletters != service.Name.Length)
                { 
                    ViewBag.Name = "-1";
                    return View();
                }
                else
                {
                    ViewBag.Name = service.Name;
                    if (service.Id != -1)
                    {
                        db.Entry(service).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Service_.Add(service);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Service_Page");
                }                
            }
            else
            {
                ViewBag.Name = "-1";
                return View();
            }
        }
        */
    }
}