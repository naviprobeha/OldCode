﻿#region Copyright Header
//-----------------------------------------------------------------------
// <copyright file="Insurance.cs" company="Klarna AB">
//     Copyright 2015 Klarna AB
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------
#endregion
namespace Klarna.Rest.Models.EMD
{
    using Newtonsoft.Json;

    /// <summary>
    /// The model for insurances.
    /// </summary>
    public class Insurance : Model
    {
        /// <summary>
        /// Gets or sets the insurance company name.
        /// </summary>
        [JsonProperty("insurance_company")]
        public string InsuranceCompany { get; set; }

        /// <summary>
        /// Gets or sets the insurance type.
        /// <para>Allowed values are "cancellation", "travel", "cancellation_travel" or "bankruptcy".</para>
        /// </summary>
        [JsonProperty("insurance_type")]
        public string InsuranceType { get; set; }

        /// <summary>
        /// Gets or sets the insurance price.
        /// </summary>
        [JsonProperty("insurance_price")]
        public long? InsurancePrice { get; set; }
    }
}