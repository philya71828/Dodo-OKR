using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DodOKR
{
    public class Task:INotifyPropertyChanged
    {
        private string name;
        private string comment;

        private int current;
        private int target;

        private Status status;
        private string statusStr;
        private SolidColorBrush statusColor;

        private int progress;
        private DateTime startDate;
        private DateTime finishDate;

        private Priority priority;
        private string priorityStr;
        private SolidColorBrush priorityColor;

        private Dictionary<int, string> months = new Dictionary<int, string>
        {
            { 1,"Янв" },{ 2,"Фев" },{ 3,"Март" },{ 4,"Апр" },{ 5,"Мая" },{ 6,"Июня" },
            { 7,"Июля" },{ 8,"Авг" },{ 9,"Сент" },{ 10,"Окт" },{ 11,"Нояб" },{ 12,"Дек" }
        };

        private Dictionary<Status, string> statusNames = new Dictionary<Status, string>
        {
            {Status.Bad,"Всё плохо!" },{Status.Good,"Отлично!" },{Status.Great,"Превосходно!" }
        };
        private Dictionary<Status, SolidColorBrush> statusColors = new Dictionary<Status, SolidColorBrush>
        {
            {Status.Bad,new SolidColorBrush(Color.FromRgb(189, 57, 51))},
            {Status.Good,new SolidColorBrush(Color.FromRgb(253, 211, 29))},
            {Status.Great,new SolidColorBrush(Color.FromRgb(62, 237, 34))}
        };

        private Dictionary<Priority, string> priorityNames = new Dictionary<Priority, string>
        {
            {Priority.Low,"Низкий" },{Priority.Middle,"Средний" },{Priority.High,"Высокий" }
        };
        private Dictionary<Priority, SolidColorBrush> priorityColors = new Dictionary<Priority, SolidColorBrush>
        {
            {Priority.Low,new SolidColorBrush(Color.FromRgb(254, 208, 29))},
            {Priority.Middle,new SolidColorBrush(Color.FromRgb(238, 144, 22))},
            {Priority.High,new SolidColorBrush(Color.FromRgb(189, 57, 51))}
        };

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Comment
        {
            get => comment;
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public int Current
        {
            get => current;
            set
            {
                current = value;
                OnPropertyChanged("Current");
            }
        }
        public int Target
        {
            get => target;
            set
            {
                target = value;
                OnPropertyChanged("Target");
            }
        }

        public Status Status
        {
            get => status;
            set
            {
                status = value;
                statusStr = statusNames[status];
                statusColor = statusColors[status];
                OnPropertyChanged("Target");
                OnPropertyChanged("StatusStr");
                OnPropertyChanged("StatusColor");
            }
        }
        public string StatusStr => statusStr;
        public SolidColorBrush StatusColor => statusColor;

        public int Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public DateTime FinishDate
        {
            get => finishDate;
            set
            {
                finishDate = value;
                OnPropertyChanged("FinishDate");
            }
        }
        public string Date
        {
            get
            {
                return startDate.Day + " " + months[startDate.Month] + " - " + finishDate.Day + " " + months[finishDate.Month];
            }
        }

        public Priority Priority
        {
            get => priority;
            set
            {
                priority = value;
                priorityStr = priorityNames[priority];
                priorityColor = priorityColors[priority];
                OnPropertyChanged("Priority");
            }
        }
        public string PriorityStr
        {
            get => priorityStr;
            set
            {
                priorityStr = value;
                OnPropertyChanged("PriorityStr");
            }
        }
        public SolidColorBrush PriorityColor
        {
            get => priorityColor;
            set
            {
                priorityColor = value;
                OnPropertyChanged("PriorityColor");
            }
        }

        public int Index { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
