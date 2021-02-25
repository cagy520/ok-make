using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace cagy_okex
{
    public class GetData
    {
        public void Start()
        {

            while (true)
            {
                try
                {
                    //获取JSON串GetJsonFile
                    WebHelper wh = new WebHelper();
                    string json = wh.GetUrlJson("http://www.okexcn.com/api/swap/v3/instruments/BTC-USDT-SWAP/candles?start=2021-01-15T02:31:00.000Z&end=2021-01-25T02:55:00.000Z&granularity=3600");
                    //if (json.Contains("数据不存在")) continue;
                    Console.WriteLine(json);

                    GetToJsonList(json);

                    //将JSON串处理
                    Thread.Sleep(200);//500毫秒获取一次
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("有错:" + ex.Message);
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }
        public void GetToJsonList(string json)
        {
            JArray jsonArr = (JArray)JsonConvert.DeserializeObject(json);
            for (int i = 0; i < jsonArr.Count; i++)
            {
                DataItem di = new DataItem();
                var it = jsonArr[i].ToString().Replace("[", "").Replace("]", "").Replace("\r\n", "").Replace("\\","").Replace(" ","").Replace(@"""","'");
                string sql = "insert into bh(tm,kai,gao,di,shou,vm,vmb) values(" + it + ");";
                Console.WriteLine(sql);
                DBHelper db = new DBHelper();
                db.Exec(sql);
               
            }
        }
        /// <summary>
        /// 插入数据库
        /// </summary>
        /// <returns></returns>
        public void SaveAlarm()
        {
           
           // string sql = "";
           // for (int i = 0; i < obj.data.Count; i++)
           // {

           // }
           //// DBHelper db = new DBHelper();
            //if (sql == "") return;
           // int row = db.Exec(sql);
          //  Console.WriteLine("BDMS报警信息插入:" + row);
        }

    }
}
