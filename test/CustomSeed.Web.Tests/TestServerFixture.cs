﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CustomSeed.Web.Tests
{
    public class TestServerFixture<TStartup> : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client { get; }
        public string Url { get; } = "http://localhost/";

        public TestServerFixture()
        {
            IWebHostBuilder builder = new WebHostBuilder()
#if NET451
                .UseContentRoot("../../../../../../src/CustomSeed.Web")
#else
                .UseContentRoot("../../../../../src/CustomSeed.Web")
#endif
                .UseStartup(typeof(TStartup));

            _server = new TestServer(builder);

            Client = _server.CreateClient();

            Client.BaseAddress = new Uri(Url);
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
