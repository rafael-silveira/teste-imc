using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace TesteIMCTestes.Integracao
{
    public class CalculoIMCIntegracaoFixture : IDisposable
    {
        private TestServer _testServer;

        public CalculoIMCIntegracaoFixture()
        {
            var builder = new WebHostBuilder();
            builder.UseEnvironment("Release");
            builder.UseStartup<TesteIMCWebAPI.Startup>();

            _testServer = new TestServer(builder);
        }

        public HttpClient GetClient()
        {
            return _testServer.CreateClient();
        }

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }
}
