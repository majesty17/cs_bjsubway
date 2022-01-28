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

            //坑！
            XAttribute att = line.Attribute("slb");
            if (att is null)
                att = line.Attribute("slsb");
            this.slb = att.Value;

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



        //获取整个city视图的size，所有站的横向、纵向跨度
        public PointF getSize(List<Line> lines)
        {
            float min_x = float.MaxValue;
            float min_y = float.MaxValue;
            float max_x = float.MinValue;
            float max_y = float.MinValue;

            for(int i = 0; i < lines.Count; i++)
            {
                Line line = lines[i];
                for(int j = 0; j < line.stations.Length; j++)
                {
                    Station st = line.stations[j];
                    max_x = st.x > max_x ? st.x : max_x;
                    max_y = st.y > max_y ? st.y : max_y;
                    min_x = st.x < min_x ? st.x : min_x;
                    min_y = st.y < min_y ? st.y : min_y;
                }
            }

            return new PointF(max_x - min_x, max_y - min_y);
        }


        //通过一坨东西，拿到所有lines
        public static List<Line> getLines(XElement root)
        {
            List<Line> ret = new List<Line>();
            foreach (var item in root.Elements("l")) {
                ret.Add(new Line(item));
            }
            return ret;
        }

        public static List<Line> getLines(int city_code)
        {
            XDocument data = Util.get_subway_data_xml(city_code);
            if (data == null)
                return null;
            XElement root = data.Root;
            List<Line> ret = getLines(root);
            return ret;
        }
    }
}
