using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public class Node
    {
        private List<Node> childNodes;
        private string text;
        private int progress;
        private string head;
        private Project project;
        private Team team;

        public Node()
        {
        }

        public Node(string text)
        {
            this.text = text;
            progress = 100;
            head = "Head";
        }

        public Node(Project project)
        {
            text = project.Name;
            progress = project.Progress;
            head = project.Head.SurName + " " + project.Head.FirstName;
        }

        public Node(Team team)
        {
            text = team.Name;
            progress = team.Progress;
            this.team = team;
        }

        public List<Node> ChildNodes
        {
            get
            {
                if (this.childNodes == null)
                    this.childNodes = new List<Node>();
                return this.childNodes;
            }
        }

        public string Text
        {
            get => text;
            set => text = value;
        }

        public int Progress
        {
            get => progress;
            set => progress = value;
        }

        public string Head
        {
            get => head;
            set => head = value;
        }
    }
}
