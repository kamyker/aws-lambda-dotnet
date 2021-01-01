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
using System;

namespace Amazon.Lambda.RuntimeSupport
{
    /// <summary>
    /// Provides access to Environment Variables set by the Lambda runtime environment.
    /// </summary>
    public class LambdaEnvironment
    {
        //internal const string EnvVarExecutionEnvironment = "AWS_EXECUTION_ENV";
        internal const string EnvVarFunctionMemorySize = "AWS_LAMBDA_FUNCTION_MEMORY_SIZE";
        internal const string EnvVarFunctionName = "AWS_LAMBDA_FUNCTION_NAME";
		internal const string EnvVarFunctionVersion = "AWS_LAMBDA_FUNCTION_VERSION";
		internal const string EnvVarHandler = "_HANDLER";
        internal const string EnvVarLogGroupName = "AWS_LAMBDA_LOG_GROUP_NAME";
        internal const string EnvVarLogStreamName = "AWS_LAMBDA_LOG_STREAM_NAME";
        internal const string EnvVarServerHostAndPort = "AWS_LAMBDA_RUNTIME_API";
        internal const string EnvVarTraceId = "_X_AMZN_TRACE_ID";
        
        internal const string AmazonLambdaRuntimeSupportMarker = "amazonlambdaruntimesupport";


        internal LambdaEnvironment()
        {
            FunctionMemorySize = Environment.GetEnvironmentVariable(EnvVarFunctionMemorySize) as string;
            FunctionName = Environment.GetEnvironmentVariable(EnvVarFunctionName) as string;
            FunctionVersion = Environment.GetEnvironmentVariable(EnvVarFunctionVersion) as string;
            LogGroupName = Environment.GetEnvironmentVariable(EnvVarLogGroupName) as string;
            LogStreamName = Environment.GetEnvironmentVariable(EnvVarLogStreamName) as string;
            RuntimeServerHostAndPort = Environment.GetEnvironmentVariable(EnvVarServerHostAndPort) as string;
            Handler = Environment.GetEnvironmentVariable(EnvVarHandler) as string;
        }


        internal void SetXAmznTraceId(string xAmznTraceId)
        {
            Environment.SetEnvironmentVariable(EnvVarTraceId, xAmznTraceId);
        }

        public string FunctionMemorySize { get; private set; }
        public string FunctionName { get; private set; }
        public string FunctionVersion { get; private set; }
        public string LogGroupName { get; private set; }
        public string LogStreamName { get; private set; }
        public string RuntimeServerHostAndPort { get; private set; }
        public string Handler { get; private set; }
        public string XAmznTraceId
        {
            get
            {
                return Environment.GetEnvironmentVariable(EnvVarTraceId);
            }
        }
    }
}
