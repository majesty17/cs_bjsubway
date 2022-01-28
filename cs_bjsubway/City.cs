using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LitJson;

namespace cs_bjsubway
{
    class City
    {
        public City(LitJson.JsonData city)
        {
            this.cn_name = city["cn_name"].ToString();
            this.cename = city["cename"].ToString();
            this.code = int.Parse(city["code"].ToString());
            this.cpre = city["cpre"].ToString();
        }
        public string cn_name;
        public string cename;
        public int code;
        public string cpre;

        public static List<City> getCities()
        {


            List<City> ret = new List<City>();

            LitJson.JsonData jdata = Util.get_cities();
            foreach(var d in jdata["subways_city"]["cities"])
            {
                ret.Add(new City((JsonData)d));
                //Console.Out.WriteLine(d.ToString());
            }
            return ret;
        }
    }
}
