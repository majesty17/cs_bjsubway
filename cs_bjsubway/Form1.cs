using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
        private List<City> cities = null;

        private string app_name = "Subway";
        private List<string> check_list = new List<string>();
        private bool list_ok = false;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += new MouseEventHandler(this.pictureBox1_MouseWheel);
            refreshTitle();
        }

        //点击draw，获取lines，初始化缩放级别
        private void button1_Click(object sender, EventArgs e)
        {
            int city = ((ComboxItem)comboBox_city.SelectedItem).Values;
            Console.Out.WriteLine("selected city is " + city);
            lines = Line.getLines(city); 
            check_list.Clear();


            scale_lvl = 1;
            offset.X = offset.Y = 0;
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
            if (ls is null)
                return;
            list_ok = false;
            listView_lines.Items.Clear();
            foreach (var l in ls)
            {
                ListViewItem lvi = new ListViewItem(l.lb);
                lvi.BackColor = l.lc;
                lvi.ForeColor = Color.White;
                lvi.Checked = true;
                listView_lines.Items.Add(lvi);
            }
            list_ok = true;
        }


        //主绘图函数
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (lines == null)
                return;
            DateTime st = DateTime.Now;

            this.refreshTitle();

            //保留已画过的换乘站
            List<string> ex_P = new List<string>();

            Size g_size = pictureBox1.Size;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            float scale = Util.getScale(g_size, scale_lvl);
            Font drawFont = new Font("宋体", 8 * scale, FontStyle.Bold);
            Font drawFont_p = new Font("宋体", 7 * scale);

            foreach (var l in lines)
            {
                if (!check_list.Contains(l.lb))//没勾选的不画
                {
                    continue;
                }



                Brush brush = new SolidBrush(l.lc);
                Pen pen = new Pen(l.lc);

                //写线路名
                float lbx_f = l.lbx;
                float lby_f = l.lby;
                PointF pf = new PointF(lbx_f, lby_f);
                Util.pointTrans(g_size, ref pf, offset, scale_lvl);
                g.DrawString(l.lb, drawFont, brush, pf);


                //写站名，站点圆圈
                foreach (var p in l.stations)
                {
                    string sid = p.sid;
                    string _lb = p.lb;

                    //一次性画完线路
                    PointF[] pts = l.getStationsPoints();
                    for (int i = 0; i < pts.Length; i++)
                    {
                        Util.pointTrans(g_size, ref pts[i], offset, scale_lvl);
                    }
                    pen.Width = scale * 2;
                    g.DrawLines(pen, pts); //总26ms，这里消耗大约7-9ms
                    //g.DrawCurve(pen, pts);  //比drawlines耗时增加1倍。。。

                    //站点圆圈实际坐标
                    PointF ppf = new PointF(p.x, p.y);
                    Util.pointTrans(g_size, ref ppf, offset, scale_lvl);

                    //站名实际坐标   
                    PointF ppf_str = new PointF(p.x + p.rx, p.y + p.ry);
                    Util.pointTrans(g_size, ref ppf_str, offset, scale_lvl);
                    
                    //是站的话，画站
                    if (p.st) //这里消耗18ms左右
                    {
                        //画站点圆圈
                        float rad = 11 * scale / 2; //圆的半径
                        PointF p_0 = new PointF(ppf.X - rad, ppf.Y - rad);
                        g.FillEllipse(brush, new RectangleF(p_0, new SizeF(11 * scale, 11 * scale)));

                        
                        //不是换乘站,只写站名
                        if (p.ex == false)
                        { 
                            //这里大约12-13ms
                            g.DrawString(sid, drawFont_p, Brushes.White, ppf_str.X, ppf_str.Y);
                        }
                        else//是换乘站的话，加到已经画过的list里；
                        {
                            if (!ex_P.Contains(sid))
                            {
                                //不在已画过的换乘站的话，才画站名
                                g.DrawString(sid, drawFont_p, Brushes.White, ppf_str.X, ppf_str.Y);
                                //并且覆盖白色圆环
                                rad = 13 * scale / 2; //圆的半径

                                p_0 = new PointF(ppf.X - rad, ppf.Y - rad);
                                Pen pen_ex = new Pen(Color.White, scale * 2);
                                g.DrawEllipse(pen_ex, new RectangleF(p_0, new SizeF(13 * scale, 13 * scale)));

                            }
                            ex_P.Add(sid);//记录换乘站为已经写过
                        }

                    }


                }



            }//end of line

            //画个中心十字
            g.DrawLine(Pens.Gray, g_size.Width / 2 - 10, g_size.Height / 2, g_size.Width / 2 + 10, g_size.Height / 2);
            g.DrawLine(Pens.Gray, g_size.Width / 2, g_size.Height / 2 - 10, g_size.Width / 2, g_size.Height / 2 + 10);

            DateTime et = DateTime.Now;
            Console.Out.WriteLine(string.Format("Paint() time cost: {0} sec", (et - st).ToString()));
           
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
            //Console.Out.WriteLine("mouse down: e pos:" + e.X + "," + e.Y);
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
                //Console.Out.WriteLine("mouse up: e pos:" + e.X + "," + e.Y);
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
            int scale_old = scale_lvl;
            if (lines == null) return;
            if (e.Delta > 0)
                scale_lvl++;
            else if (e.Delta < 0)
                scale_lvl--;

            //超范围修正
            if (scale_lvl <= 0) scale_lvl = 1;
            if (scale_lvl > scale_max) scale_lvl = scale_max;

            // Console.Out.WriteLine(scale_lvl);

            //注意这里一定是要调整offset的！
            Util.offsetTrans(scale_old, scale_lvl, ref offset);
            pictureBox1.Refresh();
        }

        private void refreshTitle()
        {
            this.Text = string.Format("{0} - offset: x={1}, y={2}; scale level: {3}", this.app_name, offset.X, offset.Y, scale_lvl);
        }

        //选中
        private void listView_lines_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            check_list.Clear();
            //Console.Out.WriteLine(listView_lines.CheckedItems.Count);
            for(int i=0;i<listView_lines.CheckedItems.Count;i++)
            {
                ListViewItem lvi = listView_lines.CheckedItems[i];
                check_list.Add(lvi.Text);
            }
            if(list_ok)
                pictureBox1.Refresh();
        }


        //全选
        private void button_select_all_Click(object sender, EventArgs e)
        {
            list_ok = false;
            for(int i = 0; i < listView_lines.Items.Count; i++)
            {
                listView_lines.Items[i].Checked = true;
            }
            list_ok = true;
            pictureBox1.Refresh();
        }
        //反选
        private void button_select_no_Click(object sender, EventArgs e)
        {
            list_ok = false;
            for (int i = 0; i < listView_lines.Items.Count; i++)
            {
                listView_lines.Items[i].Checked = !listView_lines.Items[i].Checked;
            }
            list_ok = true;
            pictureBox1.Refresh();
        }

        private void button_scale_up_Click(object sender, EventArgs e)
        {
            if (lines == null) return;
            scale_lvl++;
            //超范围修正
            if (scale_lvl <= 0) scale_lvl = 1;
            if (scale_lvl > scale_max) scale_lvl = scale_max;
            pictureBox1.Refresh();
        }

        private void button_scale_down_Click(object sender, EventArgs e)
        {
            if (lines == null) return;
            scale_lvl--;
            //超范围修正
            if (scale_lvl <= 0) scale_lvl = 1;
            if (scale_lvl > scale_max) scale_lvl = scale_max;
            pictureBox1.Refresh();
        }
        
        
        //开始获取城市
        private void Form1_Load(object sender, EventArgs e)
        {
            cities = City.getCities();
            comboBox_city.Items.Clear();
            for(int i = 0; i < cities.Count; i++)
            {
                ComboxItem item = new ComboxItem(cities[i].cn_name, cities[i].code);
                comboBox_city.Items.Add(item);
            }
            comboBox_city.SelectedIndex = 0;

        }


        //
        private void comboBox_city_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
