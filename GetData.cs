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
                    string json = wh.GetUrlJson("http://www.okexcn.com/api/swap/v3/instruments/BTC-USDT-SWAP/candles?granularity=3600");
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
              
                //前两条都是插入或者跟新
                if ((i == 0)||(i==1))//只更新前面两条
                {
                    DBHelper db0 = new DBHelper();
                    //'2021-02-25T07:00:00.000Z','50487.2','50597.8','50128.1','50409.6','69778','697.78'
                    string xit=it.Substring(1, 19).Replace("T"," ");
                    if (db0.Exist(xit) == 1)
                    {
                        //更新
                        string[] objs = it.Split(',');
                        string update_sql = "update bh set kai=" + objs[1] + ", gao=" + objs[2] + ",di=" + objs[3] + ",shou=" + objs[4] + ",vm=" + objs[5] + ",vmb=" + objs[6]+" where tm='"+xit+"'";
                        Console.WriteLine(update_sql);
                        DBHelper db = new DBHelper();
                        db.Exec(update_sql);
                    }
                    else
                    {
                        //插入
                        string sql = "insert into bh(tm,kai,gao,di,shou,vm,vmb) values(" + it + ");";
                        Console.WriteLine(sql);
                        DBHelper db = new DBHelper();
                        db.Exec(sql);
                    }
                }
                if (i == 1) break;

               
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
