using System;

using System.Xml.Linq;

namespace cs_bjsubway
{
    class Util
    {
        public static XDocument get_subway_data_xml(int city_id)
        {
            string url = string.Format("https://map.baidu.com/?qt=subways&c={0}", city_id);
            string file = @"D:\git\cs_bjsubway\cs_bjsubway\data.xml";

            return XDocument.Load(file);
            //return XDocument.Load(url);
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
    }
}
