/*
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
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Amazon.Lambda.RuntimeSupport
{
    internal class CognitoClientApplication : IClientApplication
    {
        [JsonPropertyName( "app_package_name" )]
        public string AppPackageName { get; internal set; }

        [JsonPropertyName( "app_title" )]
        public string AppTitle { get; internal set; }

        [JsonPropertyName("app_version_code" )]
        public string AppVersionCode { get; internal set; }

        [JsonPropertyName( "app_version_name" )]
        public string AppVersionName { get; internal set; }

        [JsonPropertyName("installation_id")]
        public string InstallationId { get; internal set; }
    }
}
