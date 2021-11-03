using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public static class DataChanger
    {
        public static int ChangeProgress<T>(List<T> problems)
            where T : Problem
        {
            var sum = 0;
            foreach (var task in problems)
                sum += task.Progress;
            return problems.Count == 0 ? 0 : sum / problems.Count;
        }

        public static Status SetStatus(DateTime start, DateTime finish, int progress)
        {
            double a = DateTime.Now.Subtract(start).Days;
            double b = finish.Subtract(start).Days;
            var res = progress - (a / b * 100);
            return res < -20 ? Status.Bad : res < 20 ? Status.Good : Status.Great;
        }

        public static bool CheckTaskField(string name, string comment, string target, string current,
                                    DateTime? start, DateTime? finish)
        {
            int num;
            var a = name != "";
            var b = comment != "";
            var c = start != null;
            var d = finish != null;
            var g = target != "" && int.TryParse(target, out num);
            var h = current != "" && int.TryParse(current, out num);

            return a && b && c && d && g && h;
        }

        public static bool CheckObjectiveField(string name, string comment, DateTime? start, DateTime? finish)
        {
            var a = name != "";
            var b = comment != "";
            var c = start != null;
            var d = finish != null;

            return a & b & c & d;
        }
    }
}
