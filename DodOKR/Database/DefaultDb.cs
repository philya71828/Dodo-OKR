using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public static class DefaultDb
    {
        public static void ResetDb()
        {
            using (ApplicationContext db = new ApplicationContext(Connector.Options))
            {
                Objective objective1 = new Objective
                {
                    Name = "Заказы",
                    Comment = "Нам нужно как заказов",
                    Current = 1,
                    Target = 10,
                    StartDate = new DateTime(2001, 6, 28),
                    FinishDate = new DateTime(2021, 10, 19),
                    Index = 1
                };
                db.Objectives.AddRange(objective1);
                db.SaveChanges();
            }
            
        }
    }
}
