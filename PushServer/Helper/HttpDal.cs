using PushServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PushServer.Helper
{
    public class HttpDal
    {

        public string ServerAddress = "";

        public HttpDal()
        {
        }


        private string PostData(string url, string data)
        {
            try
            {
                WebClient client = new WebClient();
                byte[] bytearray = Encoding.UTF8.GetBytes(data);
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Headers.Add("ContentLength", bytearray.Length.ToString());
                byte[] respones = client.UploadData(url, "POST", bytearray);
                client.Dispose();
                string result = Encoding.UTF8.GetString(respones);
                return result;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    throw new Exception("连接服务超时！");
                }
                else
                {
                    throw e;
                }
            }
        }

        //private string PostData(string url, string data)
        //{

        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //    request.Method = "POST";


        //    byte[] bs = Encoding.UTF8.GetBytes(data);
        //    request.ContentLength = bs.Length;
        //    using (Stream reqStream = request.GetRequestStream())
        //    {
        //        reqStream.Write(bs, 0, bs.Length);
        //        reqStream.Close();
        //    }
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();


        //    try
        //    {
        //        byte[] bytearray1 = Encoding.UTF8.GetBytes(data);
        //        HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
        //        webrequest.Method = "POST";
        //        webrequest.ContentType = "application/x-www-form-urlencoded";
        //        webrequest.ContentLength = Encoding.UTF8.GetByteCount(data);
        //        Stream webstream = webrequest.GetRequestStream();
        //        webstream.Write(bytearray1, 0, bytearray1.Length);
        //        HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse();
        //        StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
        //        string ret = sr.ReadToEnd();
        //        sr.Close();
        //        response.Close();
        //        webstream.Close();
        //        return ret;
        //    }
        //    catch (WebException e)
        //    {
        //        if (e.Status == WebExceptionStatus.Timeout)
        //        {
        //            throw new Exception("连接服务超时！");
        //        }
        //        else
        //        {
        //            throw e;
        //        }
        //    }
        //}


        public RetBack Push(string url, string sign)
        {
            try
            {
                string result = PostData(url, sign);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<RetBack>(result);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
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