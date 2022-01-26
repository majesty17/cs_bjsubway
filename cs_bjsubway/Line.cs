using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cs_bjsubway
{
    class Line
    {
        public Line(XElement line)
        {
            this.lid = line.Attribute("lid").Value;
            this.lb = line.Attribute("lb").Value;
            this.slb = line.Attribute("slb").Value;

            this.loop = line.Attribute("loop").Value == "true" ? true : false;

            this.lbx = Util.ajustF(line.Attribute("lbx").Value);
            this.lby = Util.ajustF(line.Attribute("lby").Value);

            string str_cor = line.Attribute("lc").Value;
            this.lc = ColorTranslator.FromHtml("#" + str_cor.Substring(2));


            this.stations = new Station[line.Elements("p").Count<XElement>()];
            int i = 0;
            foreach (var st in line.Elements("p"))
            {
                this.stations[i++] = new Station(st);
            }
        }




        public string lid;    //全名
        public string lb;     //简化名
        public string slb;    //更简化
        //public string n;      //?

        public bool loop;    //是否环线
        public float lbx;   //线路名位置
        public float lby;   //线路名位置
        //public string lbr;
        public Color lc;   //颜色
        //public string uid;
        //public string uid2;
        public Station[] stations;


        //通过一坨东西，拿到所有lines
        public static List<Line> getLines(XElement root)
        {
            List<Line> ret = new List<Line>();
            foreach (var item in root.Elements("l")) {
                ret.Add(new Line(item));
            }
            return ret;
        }
    }
}
