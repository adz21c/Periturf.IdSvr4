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
using Microsoft.AspNetCore.Http;
using Periturf.Hosting.Setup;
using Periturf.IdSvr4.Setup;
using Periturf.Web.Setup;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Periturf
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class GenericHostExtensions
    {
        public static void IdSvr4(this IWebSetupConfigurator configurator, Action<IIdSvr4SetupConfigurator>? config = null)
        {
            configurator.IdSvr4("IdSvr4", "/IdSvr4", config);
        }

        public static void IdSvr4(this IWebSetupConfigurator configurator, string name, PathString path, Action<IIdSvr4SetupConfigurator>? config = null)
        {
            var spec = new IdSvr4SetupSpecification(name, path);
            config?.Invoke(spec);

            configurator.AddWebComponentSpecification(spec);
        }
    }
}
