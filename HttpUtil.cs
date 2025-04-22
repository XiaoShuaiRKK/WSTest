using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Session1Winform.Model
{
    public class HttpUtil
    {
        private static HttpClient client = new HttpClient();
        public static HttpClient GetHttp()
        {
            return client;
        }
        public static void SetToken(string token)
        {
            client.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
