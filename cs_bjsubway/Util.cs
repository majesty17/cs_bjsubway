using System;
using System.Drawing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using LitJson;

namespace cs_bjsubway
{
    class Util
    {
        public static XDocument get_subway_data_xml(int city_id)
        {
            string url = string.Format("https://map.baidu.com/?qt=subways&c={0}", city_id);
            //string file = @"D:\git\cs_bjsubway\cs_bjsubway\data.xml";

            //return XDocument.Load(file);
            try
            {
                return XDocument.Load(url);
            }
            catch(Exception e)
            {

                Console.Out.WriteLine("get line error!" + e.Message);
                return null;
            }
        }

        public static LitJson.JsonData get_cities()
        {
            string url = "https://map.baidu.com/?qt=subwayscity";
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string str = wc.DownloadString(url);
           // Console.Out.WriteLine(str);
            JsonData jdata = JsonMapper.ToObject(str);
            return jdata;
        }
        //废弃了
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
        private static float base_scale = 1.555f;
        

        /**
         * 把数据中的位点转换成控件里的位点，
         * g_size:控件的size
         * p:原始位点
         * offset:控件内偏移
         * offset：控件内缩放级别
         **/
        
        public static void pointTrans(Size g_size,ref PointF p,PointF offset, int scale)
        {

            float real_scale = (float)Math.Pow(base_scale, scale - 1);
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

            //先处理缩放，默认从中点？后续可以优化成从鼠标位置；
            ret_x = (ret_x - g_w / 2) * real_scale + g_w / 2;
            ret_y = (ret_y - g_h / 2) * real_scale + g_h / 2;

            //后进行偏移，否则偏移不是真正的偏移。。确定是缩放中心会变。。
            ret_x += offset.X;
            ret_y += offset.Y;
            
            p.X = ret_x;
            p.Y = ret_y;

            return;// new PointF(ret_x, ret_y);
        }

        //偏移量也要根据scale的变化而变化!
        public static PointF offsetTrans(int scale_old,int scale_new,PointF offset)
        {
            if (scale_new == scale_old)
                return new PointF(offset.X, offset.Y);
            else
            {
                float real_scale_old = (float)Math.Pow(base_scale, scale_old - 1);
                float real_scale_new = (float)Math.Pow(base_scale, scale_new - 1);
                return new PointF(offset.X / real_scale_old * real_scale_new, offset.Y / real_scale_old * real_scale_new);
            }
        }


        //获取字体，线条粗细等缩放级别；全局基本是矢量的；
        public static float getScale(Size g_size,int scale)
        {
            float real_scale = (float)Math.Pow(base_scale, scale - 1);
            float g_w = g_size.Width;
            float g_h = g_size.Height;
            //瘦高
            if (g_h / g_w >= H / W)
            {
                return g_w / W * real_scale;
            }
            else //矮胖
            {
                return g_h / H * real_scale;
            }
        }


        //把奇怪的小数（有两个.的）转换成float
        public static float ajustF(string str_f)
        {
            if (str_f == "" || str_f=="-")
                return 0;

            if (str_f.Contains("s"))
            {
                Console.WriteLine("strange float found! " + str_f);
                string str_new = str_f.Substring(0, str_f.LastIndexOf('s'));
                return float.Parse(str_new);
            }

            if(str_f.Contains("--"))
            {
                Console.WriteLine("strange float found! " + str_f);
                string str_new = str_f.Replace("--", "-");
                return float.Parse(str_new);
            }

            if (str_f.Contains(".o"))
            {
                Console.WriteLine("strange float found! " + str_f);
                string str_new = str_f.Replace(".o", "");
                return float.Parse(str_new);
            }

            if (str_f.IndexOf('.') != str_f.LastIndexOf('.'))
            {
                Console.WriteLine("strange float found! " + str_f);
                string str_new = str_f.Substring(0, str_f.LastIndexOf('.'));
                return float.Parse(str_new);
            }
            else
                return float.Parse(str_f);
        }

    }
}
