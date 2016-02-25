using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Demo.http
{
    static class HttpHelper
    {
        public static HttpClient DisguiseUserAgent(out HttpClient client)
        {
            client = new HttpClient();
            //client.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Linux; Android 4.1.1; Nexus 7 Build/JRO03D) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166  Safari/535.19");
            // client.DefaultRequestHeaders.Add("UserAgent", "android-async-http/2.0 (http://loopj.com/android-async-http)");

            return client;
        }

        /// <summary>
        /// 创建一个安卓useragent的httpclient
        /// </summary>
        /// <returns></returns>
        public static HttpClient CreateHttpClientWithUserAgent()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Linux; Android 4.1.1; Nexus 7 Build/JRO03D) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166  Safari/535.19");
            //client.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
            return client;
        }
    }
}
