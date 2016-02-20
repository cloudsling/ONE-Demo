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
            client.DefaultRequestHeaders.Add("UserAgent", "android-async-http/2.0 (http://loopj.com/android-async-http)");

            return client;
        }
    }
}
