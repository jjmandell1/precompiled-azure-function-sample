﻿using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace PreCompiledFunctionDemo
{
    public class NameFunction
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req)
        {
            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            // Get request body
            if (req.Content != null)
            {
                dynamic data = await req.Content.ReadAsAsync<object>();
                // Set name to query string or body data
                name = name ?? data?.name;
            }

            return name == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
        }
       
    }
}
