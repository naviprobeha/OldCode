#region Copyright Header
// ----------------------------------------------------------------------------
// <copyright file="HttpTransport.cs" company="Klarna AB">
//     Copyright 2012 Klarna AB
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//         http://www.apache.org/licenses/LICENSE-2.0
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
// <author>Klarna Support: support@klarna.com</author>
// <link>http://developers.klarna.com/</link>
// ----------------------------------------------------------------------------
#endregion
namespace Klarna.Checkout.HTTP
{
    /// <summary>
    /// The http transport factory.
    /// </summary>
    public class HttpTransport
    {
        /// <summary>
        /// Creates a http transport.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpTransport"/>.
        /// </returns>
        public static IHttpTransport Create()
        {
            var httptransport = new BasicHttpTransport();
            return httptransport;
        }
    }
}