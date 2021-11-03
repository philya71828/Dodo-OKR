using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DodOKR
{
    public abstract class Problem : DbEntity, INotifyPropertyChanged
    {
        private string name;
        private string comment;

        private Status status;
        private string statusStr;
        private SolidColorBrush statusColor;

        private int progress;
        private DateTime startDate;
        private DateTime finishDate;

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

        [Required]
        [MaxLength(50)]
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

        [NotMapped]
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
        [NotMapped]
        public string StatusStr => statusStr;
        [NotMapped]
        public SolidColorBrush StatusColor => statusColor;

        [Required]
        public int Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }
        [Required]
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        [Required]
        public DateTime FinishDate
        {
            get => finishDate;
            set
            {
                finishDate = value;
                OnPropertyChanged("FinishDate");
            }
        }
        [NotMapped]
        public string Date
        {
            get
            {
                return startDate.Day + " " + months[startDate.Month] + " - " + finishDate.Day + " " + months[finishDate.Month];
            }
        }
        [Required]
        public int Index { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
