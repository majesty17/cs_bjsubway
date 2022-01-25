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
        private XElement root = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument data = Util.get_subway_data_xml(131);
            root = data.Root;
            //Util.print_data(root);
            //Util.draw(pictureBox1, root);

            updateList(root);
        }





        //更新list
        private void updateList(XElement root)
        {
            listView_lines.Items.Clear();
            foreach (var item in root.Elements("l"))
            {
                string lid = item.Attribute("lid").Value;
                string lb = item.Attribute("lb").Value;
                string lc = item.Attribute("lc").Value;
                ListViewItem lvi = new ListViewItem(lb);
                Color cor = System.Drawing.ColorTranslator.FromHtml("#" + lc.Substring(2));
                lvi.BackColor = cor;
                lvi.ForeColor = Color.White;
                listView_lines.Items.Add(lvi);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (root == null)
                return;// Util.draw(pictureBox1, root);
            Graphics g = e.Graphics;
            Size g_size = pictureBox1.Size;
            float scale = Util.getScale(g_size);
            Font drawFont = new Font("Arial", 8 * scale);

            foreach (var item in root.Elements("l"))
            {
                string lb = item.Attribute("lb").Value;
                string lbx = item.Attribute("lbx").Value;
                string lby = item.Attribute("lby").Value;
                string lc = item.Attribute("lc").Value;
                Color cor = System.Drawing.ColorTranslator.FromHtml("#" + lc.Substring(2));

                float lbx_f = float.Parse(lbx);
                float lby_f = float.Parse(lby);
                PointF pf = Util.pointTrans(g_size, new PointF(lbx_f, lby_f));
                Brush brush = new SolidBrush(cor);


                g.DrawString(lb, drawFont, brush, pf);


                foreach (var p in item.Elements("p"))
                {
                    string sid = p.Attribute("sid").Value;
                    string _lb = p.Attribute("lb").Value;
                    string x = p.Attribute("x").Value;
                    string y = p.Attribute("y").Value;
                    string rx = p.Attribute("rx").Value;
                    string ry = p.Attribute("ry").Value;
                    string st = p.Attribute("st").Value;
                    string ex = p.Attribute("ex").Value;
                    string iu = p.Attribute("iu").Value;
                    string rc = p.Attribute("rc").Value;
                    string slb = p.Attribute("slb").Value;


                    if (st == "true")
                    {
                        float px_f = Util.ajustF(x);
                        float py_f = Util.ajustF(y);
                        PointF ppf = Util.pointTrans(g_size, new PointF(px_f, py_f));
                        g.FillEllipse(brush, new RectangleF(ppf, new SizeF(11 * scale, 11 * scale)));
                    }
                }

            }
        }
    }
}
