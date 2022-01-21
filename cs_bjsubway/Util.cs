using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LitJson;

namespace cs_bjsubway
{
    class Util
    {
        public static JsonData get_subway_data(int city_id,string format)
        {
            string url = string.Format("https://map.baidu.com/?qt=subways&c={0}&format={1}", city_id, format);
            //https://map.baidu.com/?qt=subways&c=131&format=json
            Console.WriteLine(url);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Headers.Add("Accept-Language", "zh-CN,en-US;q=0.8");
            myRequest.Headers.Add("X-Requested-With", "com.mihoyo.hyperion");

            myRequest.UserAgent = "Mozilla/5.0 (Linux; Android 9; Unspecified Device) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36 miHoYoBBS/2.2.0";
            myRequest.Referer = "https://webstatic.mihoyo.com/app/community-game-records/index.html?v=6";
            myRequest.Accept = "application/json, text/plain, */*";

            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            //Console.WriteLine(content);

            return JsonMapper.ToObject(content);
        }
    }
}
