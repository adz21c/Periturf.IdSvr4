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

namespace Periturf.Verify
{
    /// <summary>
    /// Context within which the verification will be defined.
    /// </summary>
    public interface IVerificationContext
    {
        /// <summary>
        /// Gets a component condition builder for a specified component.
        /// </summary>
        /// <typeparam name="TComponentConditionBuilder">The type of the component condition builder.</typeparam>
        /// <param name="componentName">Name of the component.</param>
        /// <returns></returns>
        TComponentConditionBuilder GetComponentConditionBuilder<TComponentConditionBuilder>(string componentName)
            where TComponentConditionBuilder : IComponentConditionBuilder;

        /// <summary>
        /// Defines an expectation.
        /// </summary>
        /// <param name="conditionSpecification">The component condition specification.</param>
        /// <param name="config">Configures an expectation specification.</param>
        void Expect(IComponentConditionSpecification conditionSpecification, Action<IExpectationConfigurator> config);

        /// <summary>
        /// Sets a default expectation timeout.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        void Timeout(TimeSpan timeout);

        /// <summary>
        /// Fails a verification when the first expectation fails, cancelling the rest.
        /// </summary>
        /// <param name="shortCircuit">if set to <c>true</c> then it will short circuit.</param>
        void ShortCircuit(bool? shortCircuit);
    }
}
