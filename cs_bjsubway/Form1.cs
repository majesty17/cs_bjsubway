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

        private bool mouse_pressed = false;
        //偏移和缩放
        private PointF offset = new PointF(0, 0);
        private PointF offset_his = new PointF(0, 0);
        private PointF offset_tmp = new PointF(0, 0);
        private int scale_lvl = 1;
        private int scale_max = 7;
        private List<Line> lines = null;

        private string app_name = "bj-subway";



        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += new MouseEventHandler(this.pictureBox1_MouseWheel);
            refreshTitle();
        }

        //点击draw
        private void button1_Click(object sender, EventArgs e)
        {
            XDocument data = Util.get_subway_data_xml(131);
            XElement root = data.Root;
            lines = Line.getLines(root);
            //Util.print_data(root);
            //Util.draw(pictureBox1, root);

            updateList(lines);
            pictureBox1.Refresh();
        }

        //重置缩放和偏移
        private void button_reset_Click(object sender, EventArgs e)
        {
            offset.X = offset.Y = 0;
            scale_lvl = 1;
            pictureBox1.Refresh();
        }



        //更新list
        private void updateList(List<Line> ls)
        {
            listView_lines.Items.Clear();
            foreach (var l in ls)
            {
                ListViewItem lvi = new ListViewItem(l.lb);
                lvi.BackColor = l.lc;
                lvi.ForeColor = Color.White;
                listView_lines.Items.Add(lvi);
            }
        }


        //主绘图函数
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (lines == null)
                return;// Util.draw(pictureBox1, root);
            Console.Out.WriteLine("======= in paint() ========");
            Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            this.refreshTitle();

            //保留已画过的换乘站
            List<string> ex_P = new List<string>();



            Graphics g = e.Graphics;
            Size g_size = pictureBox1.Size;
            float scale = Util.getScale(g_size, scale_lvl);
            Font drawFont = new Font("Arial", 8 * scale, FontStyle.Bold);
            Font drawFont_p = new Font("Arial", 7 * scale);

            foreach (var l in lines)
            {
                string lb = l.lb;

                bool loop = l.loop;
                Color cor = l.lc;

                float lbx_f = l.lbx;
                float lby_f = l.lby;

                PointF pf = Util.pointTrans(g_size, new PointF(lbx_f, lby_f), offset, scale_lvl);
                Brush brush = new SolidBrush(cor);
                Pen pen = new Pen(cor);

                //线路名
                g.DrawString(lb, drawFont, brush, pf);

                //用来保存第一站和最后一站的真实坐标
                PointF last = new PointF(-99999, -99999);
                PointF first = new PointF(-9999999, -9999999999);
                foreach (var p in l.stations)
                {
                    string sid = p.sid;
                    string _lb = p.lb;
                    //是否是站，是否是换乘站
                    bool st = p.st;
                    bool ex = p.ex;


                    //站点圈圈坐标
                    float px_f = p.x;
                    float py_f = p.y;
                    //站点圆圈实际坐标
                    PointF ppf = Util.pointTrans(g_size, new PointF(px_f, py_f), offset, scale_lvl);

                    //字标偏移
                    float prx_f = p.rx;
                    float pry_f = p.ry;
                    //字标实际坐标   
                    PointF ppf_str = Util.pointTrans(g_size, new PointF(px_f + prx_f, py_f + pry_f), offset, scale_lvl);

                    //是站的话，画站
                    if (st)
                    {
                        //画站点圆圈
                        float radio = 11 * scale / 2; //圆的半径
                        PointF p_0 = new PointF(ppf.X - radio, ppf.Y - radio);
                        g.FillEllipse(brush, new RectangleF(p_0, new SizeF(11 * scale, 11 * scale)));



                        if (ex == false)
                        { //不是换乘站,只写站名
                            g.DrawString(sid, drawFont_p, Brushes.White, ppf_str.X, ppf_str.Y);
                        }
                        else//是换乘站的话，加到已经画过的list里；
                        {
                            if (!ex_P.Contains(sid))
                            {
                                //不在已画过的换乘站的话，才画站名
                                g.DrawString(sid, drawFont_p, Brushes.White, ppf_str.X, ppf_str.Y);
                                //并且覆盖白色圆环
                                radio = 13 * scale / 2; //圆的半径

                                p_0 = new PointF(ppf.X - radio, ppf.Y - radio);
                                Pen pen_ex = new Pen(Color.White, scale * 2);
                                g.DrawEllipse(pen_ex, new RectangleF(p_0, new SizeF(13 * scale, 13 * scale)));

                            }
                            ex_P.Add(sid);//干完事情再加进去。。
                        }

                    }

                    //如果不是第一站，则画线
                    if (last.X != -99999)
                    {
                        pen.Width = scale * 2;
                        g.DrawLine(pen, ppf, last);
                    }
                    else //等于的话，说明是第一站
                    {
                        first.X = ppf.X;
                        first.Y = ppf.Y;
                    }

                    last.X = ppf.X;
                    last.Y = ppf.Y;
                }
                //是环线的话，首尾连上
                if (loop)
                {
                    pen.Width = scale * 2;
                    g.DrawLine(pen, first, last);
                }


            }
            Console.Out.WriteLine("======= end paint() ========");
            Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
        }//end of paint


        //按下左键
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (lines == null)
                return;
            //需要临时保存offset为his，后续的拖动都是基于his进行变化；
            offset_tmp.X = e.X;
            offset_tmp.Y = e.Y;
            offset_his.X = offset.X;
            offset_his.Y = offset.Y;
            Console.Out.WriteLine("mouse down: e pos:" + e.X + "," + e.Y);
            mouse_pressed = true;
            pictureBox1.Cursor = Cursors.SizeAll;
        }
        //弹起左键
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (lines == null)
                return;
            if (mouse_pressed)
            {
                mouse_pressed = false;
                Console.Out.WriteLine("mouse up: e pos:" + e.X + "," + e.Y);
                pictureBox1.Cursor = Cursors.Arrow;
            }

        }

        //鼠标移动
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (lines == null)
                return;
            if (mouse_pressed)
            {

                offset.X = offset_his.X + e.X - offset_tmp.X;
                offset.Y = offset_his.Y + e.Y - offset_tmp.Y;
                pictureBox1.Refresh();
//                Console.Out.WriteLine("moving(): e pos:" + e.X + "," + e.Y);
            }

        }

        //滚轮缩放
        private void pictureBox1_MouseWheel(object sender,MouseEventArgs e)
        {

            if (e.Delta > 0)
                scale_lvl++;
            else if (e.Delta < 0)
                scale_lvl--;

            //超范围修正
            if (scale_lvl <= 0) scale_lvl = 1;
            if (scale_lvl > scale_max) scale_lvl = scale_max;
            Console.Out.WriteLine(scale_lvl);
            pictureBox1.Refresh();
        }

        private void refreshTitle()
        {
            this.Text = string.Format("{0} - offset: x={1}, y={2}; scale level: {3}", this.app_name, offset.X, offset.Y, scale_lvl);
        }


    }
}
