using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public static class DefaultDb
    {
        public static User User { get; set; }
        public static Team Team { get; set; }
        public static void ResetDb()
        {
            using (ApplicationContext db = new ApplicationContext(Connector.Options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Company company1 = new Company { Name = "Company 1" };
                db.Companies.AddRange(company1);

                Project project1 = new Project
                {
                    Name = "Project1",
                    Company = company1,
                    Head = new User
                    {
                        FirstName = "Иван",
                        SurName = "Иванов",
                        Patronymic = "Иванович",
                        Email = "ivan@mail.ru",
                        Password = "123456"
                    }
                };
                Project project2 = new Project
                {
                    Name = "Project2",
                    Company = company1,
                    Head = new User
                    {
                        FirstName = "Алексей",
                        SurName = "Алексеев",
                        Patronymic = "Алексеевич",
                        Email = "alexey@mail.ru",
                        Password = "123456"
                    }
                };
                Project project3 = new Project
                {
                    Name = "Project3",
                    Head = new User
                    {
                        FirstName = "Влад",
                        SurName = "Владов",
                        Patronymic = "Владиславович",
                        Email = "vlad@mail.ru",
                        Password = "123456"
                    }
                };
                db.Projects.AddRange(project1, project2, project3);

                Team team1 = new Team { Name = "team1", Company = company1, Project = project1 };
                Team team2 = new Team { Name = "team2", Company = company1, Project = project3 };
                Team team3 = new Team { Name = "team3", Company = company1, Project = project2 };
                db.Teams.AddRange(team1, team2, team3);

                User user1 = new User 
                {
                    FirstName = "User",
                    SurName = "User",
                    Patronymic = "User",
                    Email = "user@user.ru",
                    Password = "123586",
                    Team = team1
                };                
                

                Objective objective1 = new Objective
                {
                    Name = "Objective 1",
                    Comment = "Objective 1",
                    StartDate = new DateTime(2001, 6, 28),
                    FinishDate = new DateTime(2021, 10, 19),
                    Progress = 10,
                    User = user1,
                    Index = 0
                };
                Objective objective2 = new Objective
                {
                    Name = "Objective 1",
                    Comment = "Objective 1",
                    StartDate = new DateTime(2001, 8, 28),
                    FinishDate = new DateTime(2025, 10, 11),
                    Progress = 10,
                    User = user1,
                    Index = 1
                };
                Task task1 = new Task
                {
                    Name = "Заказы",
                    Comment = "Заказы",
                    Current = 1,
                    Target = 10,
                    StartDate = new DateTime(2001, 6, 28),
                    FinishDate = new DateTime(2021, 10, 19),
                    Objective = objective1,
                    Progress = 10,
                    Index = 0
                };
                Task task2 = new Task
                {
                    Name = "Пицца",
                    Comment = "Делайте много пиццы",
                    Current = 1,
                    Target = 10,
                    StartDate = new DateTime(2001, 8, 28),
                    FinishDate = new DateTime(2025, 10, 11),
                    Objective = objective2,
                    Progress = 10,
                    Index = 0
                };
                Task task3 = new Task
                {
                    Name = "Дополнение",
                    Comment = "Не знаю что ещё нужно Додо",
                    Current = 1,
                    Target = 10,
                    StartDate = new DateTime(2001, 6, 28),
                    FinishDate = new DateTime(2005, 10, 19),
                    Objective = objective2,
                    Progress = 10,
                    Index = 1
                };
                Objective teamObjective1 = new Objective
                {
                    Name = "Objective 1",
                    Comment = "Objective 1",
                    StartDate = new DateTime(2001, 6, 28),
                    FinishDate = new DateTime(2021, 10, 19),
                    Progress = 10,
                    Team = team1,
                    Index = 0
                };
                Task teamTask1 = new Task
                {
                    Name = "Заказы",
                    Comment = "Заказы",
                    Current = 1,
                    Target = 10,
                    StartDate = new DateTime(2001, 6, 28),
                    FinishDate = new DateTime(2021, 10, 19),
                    Objective = objective1,
                    Progress = 10,
                    Index = 0
                };
                Task teamTask2 = new Task
                {
                    Name = "Пицца",
                    Comment = "Делайте много пиццы",
                    Current = 1,
                    Target = 10,
                    StartDate = new DateTime(2001, 8, 28),
                    FinishDate = new DateTime(2025, 10, 11),
                    Objective = objective2,
                    Progress = 10,
                    Index = 0
                };
                user1.Objectives.AddRange(new[] { objective1, objective2 });
                db.Users.AddRange(user1);
                db.Objectives.AddRange(objective1, objective2);
                db.Tasks.AddRange(task1, task2, task3);
                User = user1;
                teamObjective1.Tasks.Add(teamTask1);
                teamObjective1.Tasks.Add(teamTask2);
                User.Team.Objectives.Add(teamObjective1);

                db.SaveChanges();
            }
            
        }
    }
}
