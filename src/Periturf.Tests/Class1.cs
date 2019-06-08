﻿using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Periturf.Tests
{
    [TestFixture]
    class Class1
    {
        [Test]
        public async System.Threading.Tasks.Task SomethingAsync()
        {
            var env = Environment.Setup(x =>
            {
                x.WebHost(w =>
                {
                    w.UseUrls("http://localhost:3500/");
                    w.SetupIdSvr4();
                });
            });

            await env.StartAsync();

            var configId = env.Configure(c =>
            {
                c.ConfigureIdSvr4(i =>
                {
                    i.Client(cl =>
                    {
                        cl.ClientId = "Client";
                        cl.ClientName = "Client";
                        cl.Secret("secret".Sha256());
                        cl.AllowedGrantTypes = GrantTypes.ClientCredentials;
                        cl.Scope("Resource");
                    });
                    i.ApiResource(new ApiResource("Resource", "Resource Name"));
                });
            });

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3500/connect/token");
            var token = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = "Client",
                ClientSecret = "secret",
                Scope = "Resource"
            });

            env.RemoveConfiguration(configId);
        }
    }
}