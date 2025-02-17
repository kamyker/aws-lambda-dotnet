﻿/*
 * Copyright 2019 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using Amazon.Lambda.Core;
using System;
using Amazon.Lambda.RuntimeSupport.Helpers;
using System.Text.Json;

namespace Amazon.Lambda.RuntimeSupport
{
    internal class LambdaContext : ILambdaContext
    {
        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private LambdaEnvironment _lambdaEnvironment;
        private RuntimeApiHeaders _runtimeApiHeaders;
        private long _deadlineMs;
        private int _memoryLimitInMB;
        private Lazy<CognitoIdentity> _cognitoIdentityLazy;
        private Lazy<CognitoClientContext> _cognitoClientContextLazy;


        public LambdaContext(RuntimeApiHeaders runtimeApiHeaders, LambdaEnvironment lambdaEnvironment)
        {

            _lambdaEnvironment = lambdaEnvironment;
            _runtimeApiHeaders = runtimeApiHeaders;

            int.TryParse(_lambdaEnvironment.FunctionMemorySize, out _memoryLimitInMB);
            long.TryParse(_runtimeApiHeaders.DeadlineMs, out _deadlineMs);
            _cognitoIdentityLazy = new Lazy<CognitoIdentity>(() => JsonSerializer.Deserialize<CognitoIdentity>(runtimeApiHeaders.CognitoIdentityJson));
            _cognitoClientContextLazy = new Lazy<CognitoClientContext>(() => JsonSerializer.Deserialize<CognitoClientContext>(runtimeApiHeaders.ClientContextJson));

            // set environment variable so that if the function uses the XRay client it will work correctly
            _lambdaEnvironment.SetXAmznTraceId(_runtimeApiHeaders.TraceId);
        }

        // TODO If/When Amazon.Lambda.Core is major versioned, add this to ILambdaContext.
        // Until then function code can access it via the _X_AMZN_TRACE_ID environment variable set by LambdaBootstrap.
        public string TraceId => _runtimeApiHeaders.TraceId;

        public string AwsRequestId => _runtimeApiHeaders.AwsRequestId;

        public IClientContext ClientContext => _cognitoClientContextLazy.Value;

        public string FunctionName => _lambdaEnvironment.FunctionName;

        public string FunctionVersion => _lambdaEnvironment.FunctionVersion;

        public ICognitoIdentity Identity => _cognitoIdentityLazy.Value;

        public string InvokedFunctionArn => _runtimeApiHeaders.InvokedFunctionArn;

        public ILambdaLogger Logger => new LambdaConsoleLogger();

        public string LogGroupName => _lambdaEnvironment.LogGroupName;

        public string LogStreamName => _lambdaEnvironment.LogStreamName;

        public int MemoryLimitInMB => _memoryLimitInMB;

        public TimeSpan RemainingTime => TimeSpan.FromMilliseconds(_deadlineMs - (DateTime.UtcNow - UnixEpoch).TotalMilliseconds);
    }
}
