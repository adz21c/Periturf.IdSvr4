/*
 *     Copyright 2019 Adam Burton (adz21c@gmail.com)
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using IdentityModel.Client;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Periturf.Components;
using Periturf.Hosting.Setup;
using Periturf.IdSvr4.Clients;
using Periturf.IdSvr4.Configuration;
using Periturf.IdSvr4.Verify;
using Periturf.Web.Setup;
using System;
using System.Linq;
using System.Net.Http;

namespace Periturf.IdSvr4.Setup
{
    class IdSvr4SetupSpecification : IWebComponentSetupSpecification, IIdSvr4SetupConfigurator
    {
        public IdSvr4SetupSpecification(string name, PathString path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; }

        public PathString Path { get; }

        public IdentityServerMiddlewareOptions? IdentityServerOptions { get; private set; }

        public void Options(IdentityServerMiddlewareOptions options)
        {
            IdentityServerOptions = options;
        }

        public ConfigureWebAppDto Configure()
        {
            var eventMonitorSink = new EventMonitorSink();
            var store = new Store();
            var httpClient = new HttpClient();
            var client = new ComponentClient(
                httpClient,
                new DiscoveryCache("", httpClient));

            var component = new IdSvr4Component(store, eventMonitorSink, client);

            return new ConfigureWebAppDto(
                component,
                app => app.UseIdentityServer(IdentityServerOptions),
                services =>
                {
                    services
                        .AddSingleton<IClientStore, Store>(sp => store)
                        .AddSingleton<IResourceStore, Store>(sp => store)
                        .AddSingleton<IEventSink, EventMonitorSink>(sp => eventMonitorSink)
                        .AddIdentityServer(i =>
                        {
                            i.Events.RaiseErrorEvents = true;
                            i.Events.RaiseFailureEvents = true;
                            i.Events.RaiseInformationEvents = true;
                            i.Events.RaiseSuccessEvents = true;
                        })
                        .AddDeveloperSigningCredential();
                });

        }
    }
}