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
using System.Xml.Linq;

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
            XDocument data = Util.get_subway_data_xml(131);
            XElement root = data.Root;
            Util.print_data(root);



        }


    }
}
