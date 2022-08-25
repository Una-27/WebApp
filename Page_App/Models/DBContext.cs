using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Page_App.Models
{
    public abstract class SerializableObject
    {

    }
    public static class Serialization<T> where T : SerializableObject
    {
        public static List<T> ReadList(string bytes)
        {
            return JsonConvert.DeserializeObject<List<T>>(bytes);
        }

        public static string WriteList(List<T> data)
        {
            return JsonConvert.SerializeObject(data.ToArray());
        }

        public static T Read(string bytes)
        {
            return JsonConvert.DeserializeObject<T>(bytes);
        }

        public static string Write(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
    public class DBContext : DbContext
    {
        public DBContext() : base(nameOrConnectionString: "AdminDB") { }
        public DbSet<Clients> Client_ { get; set; }
        public DbSet<Masters> Master_ { get; set; }
        public DbSet<Services> Service_ { get; set; }
        public DbSet<Finance> Finance_ { get; set; }
        public DbSet<News> News_ { get; set; }
        public DbSet<Timetable> Timetable_ { get; set; }
        public DbSet<ServicesFromMaster> ServicesFromMaster_ { get; set; }

        public void ValidSet()
        {
            Client_.RemoveRange(Client_);
            Master_.RemoveRange(Master_);
            Service_.RemoveRange(Service_);
            Finance_.RemoveRange(Finance_);
            Timetable_.RemoveRange(Timetable_);

            SaveChanges();
            Client_.Add(new Clients { FirstName = "Аленов", Name = "Артур", DateOfBirth = DateTime.Parse("13.07.2000"), PhoneNumber = "89123456789", AdvertisingChannel = "Соцсети", Discount = 1, DateAdded = DateTime.Parse("01.07.2022") });
            Client_.Add(new Clients { FirstName = "Катрова", Name = "Зоя", DateOfBirth = DateTime.Parse("16.09.1987"), PhoneNumber = "89123456789", AdvertisingChannel = "Соцсети", Discount = 3, DateAdded = DateTime.Parse("05.06.2022") });
            Client_.Add(new Clients { FirstName = "Лапронова", Name = "Аделина", DateOfBirth = DateTime.Parse("20.03.1993"), PhoneNumber = "89123456789", AdvertisingChannel = "Соцсети", Discount = 2, DateAdded = DateTime.Now.Date });
            Client_.Add(new Clients { FirstName = "Китерюк", Name = "Каролина", DateOfBirth = DateTime.Parse("26.12.2003"), PhoneNumber = "89123456789", AdvertisingChannel = "Соцсети", Discount = 0, DateAdded = DateTime.Parse("30.05.2022") });
            Client_.Add(new Clients { FirstName = "Сароладская", Name = "Олеся", DateOfBirth = DateTime.Parse("7.05.1969"), PhoneNumber = "89123456789", AdvertisingChannel = "Соцсети", Discount = 10, DateAdded = DateTime.Parse("07.09.2021") });
            Client_.Add(new Clients { FirstName = "Влакотов", Name = "Константин", DateOfBirth = DateTime.Parse("8.10.1977"), PhoneNumber = "89123456789", AdvertisingChannel = "Соцсети", Discount = 1, DateAdded = DateTime.Parse("02.07.2022") });
            Client_.Add(new Clients { FirstName = "Аренов", Name = "Валентин", DateOfBirth = DateTime.Parse("23.03.2003"), PhoneNumber = "89123456789", AdvertisingChannel = "Соцсети", Discount = 3, DateAdded = DateTime.Parse("20.06.2022") });

            SaveChanges();
            Master_.Add(new Masters { FirstName = "Первова", Name = "Мара", MiddleName = "Отчествовна", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Косметолог", Salary = "Почасовая оплата", Rate = 150, ImageData = null, Info = "Хороший местер" });
            Master_.Add(new Masters { FirstName = "Второва", Name = "Лара", MiddleName = "Отчествовна", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Стилист", Salary = "Почасовая оплата", Rate = 200, ImageData = null, Info = "Хороший местер" });
            Master_.Add(new Masters { FirstName = "Третьяккова", Name = "Даропа", MiddleName = "Отчествовна", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Парикмахер", Salary = "Почасовая оплата", Rate = 100, ImageData = null, Info = "Хороший местер" });
            Master_.Add(new Masters { FirstName = "Четвертанов", Name = "Лугв", MiddleName = "Отчествович", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Визажист", Salary = "Почасовая оплата", Rate = 170, ImageData = null, Info = "Хороший местер" });
            Master_.Add(new Masters { FirstName = "Пятницкая", Name = "Кона", MiddleName = "Отчествовна", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Парикмахер", Salary = "Процент от выручки", Rate = 15, ImageData = null, Info = "Хороший местер" });
            Master_.Add(new Masters { FirstName = "Шестеренкова", Name = "Долина", MiddleName = "Отчествовна", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Визажист", Salary = "Процент от выручки", Rate = 17, ImageData = null, Info = "Хороший местер" });
            Master_.Add(new Masters { FirstName = "Семицветникова", Name = "Тарали", MiddleName = "Отчествовна", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Косметолог", Salary = "Процент от выручки", Rate = 19, ImageData = null, Info = "Хороший местер" });
            Master_.Add(new Masters { FirstName = "Восьмиключева", Name = "Вакала", MiddleName = "Отчествовна", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Колорист", Salary = "Процент от выручки", Rate = 20, ImageData = null, Info = "Хороший местер" });
            Master_.Add(new Masters { FirstName = "Девятикова", Name = "Катрина", MiddleName = "Отчествовна", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Администратор", Salary = "Смешанный", Rate = 100, ImageData = null, Info = "Администратор" });
            Master_.Add(new Masters { FirstName = "Десятков", Name = "Артемий", MiddleName = "Отчествович", DateOfBirth = DateTime.Parse("12.12.2000"), PhoneNumber = "89000000000", Profession = "Администратор", Salary = "Смешанный", Rate = 110, ImageData = null, Info = "Администратор" });

            SaveChanges();
            Service_.Add(new Services { Name = "Вечерний макияж", Category = "Макияж", Price = "100руб - 1000руб", TimeOfWork = "0.5ч - 6ч", ImageData = null, Info = "Замечательная услуга" });
            Service_.Add(new Services { Name = "Дневной макияж", Category = "Макияж", Price = "100руб - 1000руб", TimeOfWork = "0.5ч - 6ч", ImageData = null, Info = "Замечательная услуга" });
            Service_.Add(new Services { Name = "Стрижка", Category = "Волосы", Price = "100руб - 1000руб", TimeOfWork = "0.5ч - 6ч", ImageData = null, Info = "Замечательная услуга" });
            Service_.Add(new Services { Name = "Покраска волос", Category = "Волосы", Price = "100руб - 1000руб", TimeOfWork = "0.5ч - 6ч", ImageData = null, Info = "Замечательная услуга" });
            Service_.Add(new Services { Name = "Мелирование", Category = "Волосы", Price = "100руб - 1000руб", TimeOfWork = "0.5ч - 6ч", ImageData = null, Info = "Замечательная услуга" });
            Service_.Add(new Services { Name = "Ламинирование бровей", Category = "Брови", Price = "100руб - 1000руб", TimeOfWork = "0.5ч - 6ч", ImageData = null, Info = "Замечательная услуга" });

            SaveChanges();
            Finance_.Add(new Finance { Type = true, Category = "Акции", Summa = 500, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });
            Finance_.Add(new Finance { Type = true, Category = "Акции", Summa = 1000, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });
            Finance_.Add(new Finance { Type = true, Category = "Другое", Summa = 520, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });
            Finance_.Add(new Finance { Type = true, Category = "Другое", Summa = 360, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });
            Finance_.Add(new Finance { Type = true, Category = "Аренда", Summa = 1590, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });
            Finance_.Add(new Finance { Type = false, Category = "Расходники", Summa = 2030, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });
            Finance_.Add(new Finance { Type = false, Category = "Расходники", Summa = 4520, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });
            Finance_.Add(new Finance { Type = false, Category = "Аренда", Summa = 9630, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });
            Finance_.Add(new Finance { Type = false, Category = "Налоги", Summa = 740, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });
            Finance_.Add(new Finance { Type = false, Category = "Налоги", Summa = 100, Date = DateTime.Now.Date, IdMaster = Master_.ToList().Last(), Info = "" });

            SaveChanges();
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[0], IdMaster = Master_.ToList()[0], IdService = Service_.ToList()[0], Date = DateTime.Parse("01.07.2022"), TimeStart = 9, TimeEnd = 11, Price = 1025, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[0], IdMaster = Master_.ToList()[2], IdService = Service_.ToList()[4], Date = DateTime.Parse("02.07.2022"), TimeStart = 10, TimeEnd = 12, Price = 2035, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[0], IdMaster = Master_.ToList()[3], IdService = Service_.ToList()[0], Date = DateTime.Parse("03.07.2022"), TimeStart = 9.5, TimeEnd = 11.5, Price = 9523, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[1], IdMaster = Master_.ToList()[0], IdService = Service_.ToList()[1], Date = DateTime.Parse("01.07.2022"), TimeStart = 13, TimeEnd = 14, Price = 1023, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[1], IdMaster = Master_.ToList()[2], IdService = Service_.ToList()[2], Date = DateTime.Parse("02.07.2022"), TimeStart = 16, TimeEnd = 16.5, Price = 3002, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[2], IdMaster = Master_.ToList()[0], IdService = Service_.ToList()[1], Date = DateTime.Parse("05.07.2022"), TimeStart = 10, TimeEnd = 11.5, Price = 1103, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[2], IdMaster = Master_.ToList()[2], IdService = Service_.ToList()[3], Date = DateTime.Parse("02.07.2022"), TimeStart = 13, TimeEnd = 13.5, Price = 5223, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[3], IdMaster = Master_.ToList()[2], IdService = Service_.ToList()[2], Date = DateTime.Parse("03.07.2022"), TimeStart = 10, TimeEnd = 11.5, Price = 4120, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[3], IdMaster = Master_.ToList()[4], IdService = Service_.ToList()[4], Date = DateTime.Parse("04.07.2022"), TimeStart = 12, TimeEnd = 13.5, Price = 203, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[1], IdMaster = Master_.ToList()[3], IdService = Service_.ToList()[0], Date = DateTime.Parse("05.07.2022"), TimeStart = 13, TimeEnd = 15.5, Price = 523, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[4], IdMaster = Master_.ToList()[2], IdService = Service_.ToList()[4], Date = DateTime.Parse("01.07.2022"), TimeStart = 14, TimeEnd = 16, Price = 3206, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[5], IdMaster = Master_.ToList()[0], IdService = Service_.ToList()[0], Date = DateTime.Parse("02.07.2022"), TimeStart = 9, TimeEnd = 10.5, Price = 3256, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[5], IdMaster = Master_.ToList()[3], IdService = Service_.ToList()[1], Date = DateTime.Parse("03.07.2022"), TimeStart = 14.5, TimeEnd = 16, Price = 8520, Status = 0, Grade = 5, Message = "" });
            Timetable_.Add(new Timetable { IdClient = Client_.ToList()[5], IdMaster = Master_.ToList()[3], IdService = Service_.ToList()[0], Date = DateTime.Parse("05.07.2022"), TimeStart = 9, TimeEnd = 11, Price = 952, Status = 0, Grade = 5, Message = "" });


            SaveChanges();
        }
    }
}