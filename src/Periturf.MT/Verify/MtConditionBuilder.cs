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
using System;
using Periturf.MT.Configuration;
using Periturf.Verify;

namespace Periturf.MT.Verify
{
    class MtConditionBuilder : IMtConditionBuilder
    {
        private readonly IBusManager _busManager;

        public MtConditionBuilder(IBusManager busManager)
        {
            _busManager = busManager;
        }

        public IComponentConditionSpecification WhenMessagePublished<TMessage>(Func<IMessageReceivedContext<TMessage>, bool> condition) where TMessage : class
        {
            return new WhenMessagePublishedSpecification<TMessage>(_busManager, new[] { condition });
        }
    }
}
