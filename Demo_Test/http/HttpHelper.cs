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
        static HttpClient httpClient;

        public async static Task<string> GetAsync(Uri uri)
        {
            TryDispose(httpClient);
            HttpResponseMessage response;
            using (httpClient = new HttpClient())
            {
                response = await httpClient.GetAsync(uri);
            }
            if (response == null) return string.Empty;
            return await response.Content.ReadAsStringAsync();
        }

        public static string GetStringAsync(Uri uri)
        {
            TryDispose(httpClient);
            using (httpClient = new HttpClient())
            {
                return httpClient.GetStringAsync(uri).GetResults();
            }
        }


        static void TryDispose(HttpClient httpClient)
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }
        }
    }
}
