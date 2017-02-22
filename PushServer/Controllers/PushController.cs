using Newtonsoft.Json;
using PushServer.Helper;
using PushServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PushServer.Controllers
{
    public class PushController : Controller
    {
        // GET: Push
        public ActionResult Index()
        {
            return View();
        }



        public string PushTxt(string msg)
        {
            try
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                string timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
                string appkey = string.Empty;
                string app_master_secret = string.Empty;
                var param = new object();

                appkey = "58acec4a734be462300000b1";
                app_master_secret = "yrfkopg68qtctylrzwwkvjjeadlobvbi";
                param = new
                {
                    appkey = appkey,
                    timestamp = timestamp,
                    type = "broadcast",
                    payload = new
                    {
                        body = new
                        {
                            custom = msg,
                            //ticker = "test",
                            //title = "test",
                            //text = "test",
                            //after_open = "go_app",
                        },
                        display_type = "message"   //消息类型：通知
                    }
                };
                string url = "http://msg.umeng.com/api/send";
                var requestJson = JsonConvert.SerializeObject(param);
                string mysign = getMD5Hash("POST" + url + requestJson + app_master_secret);
                url = url + "?sign=" + mysign;
                HttpDal httpDal = new HttpDal();
                RetBack retBack = httpDal.Push(url, requestJson);
                return retBack.ToString();
            }
            catch (WebException e)
            {
                return e.Message;
            }
        }


        public static String getMD5Hash(String str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            StringBuilder strReturn = new StringBuilder();


            for (int i = 0; i < result.Length; i++)
            {
                strReturn.Append(Convert.ToString(result[i], 16).PadLeft(2, '0'));
            }


            return strReturn.ToString().PadLeft(32, '0');
        }
    }



}