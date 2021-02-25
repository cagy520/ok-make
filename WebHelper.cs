using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace cagy_okex
{
    public class WebHelper
    {
        public string GetUrlJson(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "get";
                request.Headers["x-auth-token"] = "123459"; //添加头
                request.ContentType = "application/json;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string json = GetResponseString(response);
                return json;
            }
            catch (Exception e)
            {
                Console.Write("WebHelper:" + e.Message);
                return "";
            }
        }
        public string GetResponseString(HttpWebResponse response)
        {
            string json = null;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8")))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }

        public string GetJsonFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string a = sr.ReadToEnd();
            sr.Close();
            return a;

        }
    }
}
