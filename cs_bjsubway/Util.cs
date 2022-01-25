using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace cs_bjsubway
{
    class Util
    {
        public static XDocument get_subway_data_xml(int city_id)
        {
            string url = string.Format("https://map.baidu.com/?qt=subways&c={0}", city_id);
            string file = @"D:\git\cs_bjsubway\cs_bjsubway\data.xml";

            //return XDocument.Load(file);
            return XDocument.Load(url);
        }


        public static void print_data(XElement data)
        {
            Console.Out.WriteLine("========== print data =========");
            foreach (var item in data.Elements("l"))
            {
                string lid = item.Attribute("lid").Value;
                string lb = item.Attribute("lb").Value;
                string n = item.Attribute("n").Value;
                string loop = item.Attribute("loop").Value;
                string lbx = item.Attribute("lbx").Value;
                string lby = item.Attribute("lby").Value;
                string lbr = item.Attribute("lbr").Value;
                string lc = item.Attribute("lc").Value;

                string ret = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    lid, lb, n, loop, lbx, lby, lbr, lc);
                //Console.Out.WriteLine(ret);

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
                    //string ln = p.Attribute("ln").Value;
                    //string _int = p.Attribute("int").Value;
                    //string px = p.Attribute("px").Value;
                    //string py = p.Attribute("py").Value;
                    //string uid = p.Attribute("uid").Value;
                    if (st == "true")
                    {
                        ret = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}"
                            , sid, _lb, x, y, rx, ry, st, ex, iu, rc, slb);

                        Console.Out.WriteLine(ret);
                    }
                }
            }

        }


        ///坐标转换:把固定长宽比例的坐标等比缩放到g_size上；
        ///P:
        ///x: -1516~921.8
        ///y: -509~1100
        ///L:
        ///x: -1280~450
        ///y: -390~990
        /// 
        /// final: x[-1540:950]=2490
        /// final: y[-530:1120]=1650
        private static float W = 2490f;
        private static float H = 1650f;
        private static float x_0 = -1540f;
        private static float y_0 = -530f;
        
        public static PointF pointTrans(Size g_size,PointF p)
        {
            float g_w = g_size.Width;
            float g_h = g_size.Height;
            float ret_x = 0f, ret_y = 0f;
            //瘦高
            if(g_h/g_w >= H / W)
            {
                ret_x = (p.X - x_0) * g_w / W;
                ret_y = (p.Y - y_0) * g_w / W  + (g_h - g_w * H / W) / 2;
            }
            else //矮胖
            {
                ret_y = (p.Y - y_0) * g_h / H;
                ret_x = (p.X - x_0) * g_h / H + (g_w - g_h * W / H) / 2;
            }



            return new PointF(ret_x, ret_y);
        }

        public static float getScale(Size g_size)
        {
            float g_w = g_size.Width;
            float g_h = g_size.Height;
            //瘦高
            if (g_h / g_w >= H / W)
            {
                return g_w / W;
            }
            else //矮胖
            {
                return g_h / H;
            }
        }


        public static float ajustF(string str_f)
        {
            if (str_f.IndexOf('.') != str_f.LastIndexOf('.'))
            {
                string str_new = str_f.Substring(0, str_f.LastIndexOf('.'));
                return float.Parse(str_new);
            }
            else
                return float.Parse(str_f);
        }


    }
}
