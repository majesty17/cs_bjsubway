using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cs_bjsubway
{
    class Station
    {

        public Station(XElement st)
        {
            this.sid = st.Attribute("sid").Value;
            this.lb = st.Attribute("lb").Value;

            this.x = Util.ajustF(st.Attribute("x").Value);
            this.y = Util.ajustF(st.Attribute("y").Value);
            
            this.rx = Util.ajustF(st.Attribute("rx").Value);
            this.ry = Util.ajustF(st.Attribute("ry").Value);

            this.st = st.Attribute("st").Value == "true" ? true : false;
            this.ex = st.Attribute("ex").Value == "true" ? true : false;
            this.ln = st.Attribute("ln").Value;
        }
        public string sid;//人民大学	站名
        public string lb;//人民大学	站名too
        public float x;//-319.6	站坐标
        public float y;//-34.0	站坐标
        public float rx;//站名偏移量
        public float ry;//站名偏移量
        public bool st;//true	是否是站
        public bool ex;//false	是否是换乘站
        //public string iu;//true	？
        //public string rc;//false	？
        //public string slb;//true	？
        public string ln;//北京市|地铁4号线大兴线	哪些站经过，换乘站
        //public string _int	;//2	
        //public float px;//12949724.67	真实经度
        //public float py;//4834278.42	真实维度
        //public string uid;//ae80941a3f2fe5a796ec8428	站id，会重复
    }
}
