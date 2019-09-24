﻿using Com.Bateeq.Service.Sales.Lib.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Bateeq.Sales.Test
{
    public class HttpClientTestService : IHttpClientService
    {
        public static string Token;

        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            return Task.Run(() => new HttpResponseMessage() {
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }
        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return Task.Run(() => new HttpResponseMessage() {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{data:{'data':'data'}}")
            });
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return Task.Run(() => new HttpResponseMessage() {
                StatusCode = System.Net.HttpStatusCode.Created
            });
        }
    }
}
