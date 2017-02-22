using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushServer.Models
{
    public class PushViewModel
    {
    }

    public class PushMsg
    {
        public PushMsg()
        { }
        public string appkey { get; set; }
        public string timestamp { get; set; }
        public string type { get; set; }
        public string device_tokens { get; set; }
        public string alias_type { get; set; }
        public string alias { get; set; }
        public string file_id { get; set; }
        public List<object> filter { get; set; }
        public Payload payload { get; set; }

    }

    //消息内容
    public class Payload
    {
        public Payload()
        { }
        public string display_type { get; set; }
        public Body body { get; set; }
    }

    //消息体
    public class Body
    {
        public Body()
        { }
        public string custom { get; set; }
    }

    //返回结果

    public class RetBack
    {
        public RetBack()
        { }
        public string ret { get; set; }
        public RetData data { get; set; }
    }

    public class RetData
    {
        public RetData()
        { }

        public string msg_id { get; set; }
        public string task_id { get; set; }
        public string error_code { get; set; }
        public string thirdparty_id { get; set; }
    }

}