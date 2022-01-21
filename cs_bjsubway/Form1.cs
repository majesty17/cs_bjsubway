using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LitJson;

namespace cs_bjsubway
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JsonData data = Util.get_subway_data(131, "json");
            foreach (JsonData line in data["subways"]["l"])
            {
                Console.Out.WriteLine(line["l_xmlattr"]["lb"]);
                foreach (JsonData st in line["p"])
                {
                    string if_st = st["p_xmlattr"]["st"].ToString();
                 //   Console.Out.WriteLine(if_st);
                    if(if_st=="True")
                        Console.Out.WriteLine(st["p_xmlattr"]["ln"] + "---------" + st["p_xmlattr"]["lb"]);
                }
            }
        }
    }
}
