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

        public Node()
        {
        }

        public Node(string text)
        {
            this.text = text;
            progress = 100;
            head = "Head";
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
