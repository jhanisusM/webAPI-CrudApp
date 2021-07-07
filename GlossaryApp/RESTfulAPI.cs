using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace GlossaryApp
{
    public class RESTfulAPI
    {
        public static HttpClient httpRequest = new HttpClient();
        static RESTfulAPI()
        {
            httpRequest.BaseAddress = new Uri("https://localhost:44357/api/");
            httpRequest.DefaultRequestHeaders.Clear();
            httpRequest.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}