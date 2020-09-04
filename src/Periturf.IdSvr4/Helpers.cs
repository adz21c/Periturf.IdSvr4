﻿/*
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
using Periturf.Verify;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Periturf.IdSvr4.Tests
{
    static class Helpers
    {
        public static async Task<List<ConditionInstance>> AsyncToListAsync(this IAsyncEnumerable<ConditionInstance> stream, TimeSpan? timeout = null)
        {
            using (var cancellationSource = timeout.HasValue ? new CancellationTokenSource(timeout.Value) : new CancellationTokenSource())
            {
                var instances = new List<ConditionInstance>();

                try
                {
                    await foreach (var instance in stream.WithCancellation(cancellationSource.Token))
                    {
                        instances.Add(instance);
                    }
                }
                catch (OperationCanceledException)
                { }

                return instances;
            }
        }
    }
}
