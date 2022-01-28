using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_bjsubway
{
    public class ComboxItem
    {
        private string text;
        private int values;

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public int Values
        {
            get { return this.values; }
            set { this.values = value; }
        }

        public ComboxItem(string _Text, int _Values)
        {
            Text = _Text;
            Values = _Values;
        }


        public override string ToString()
        {
            return Text;
        }
    }
}
