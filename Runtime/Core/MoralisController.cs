/**
 *           Module: MoralisSetup.cs
 *  Descriptiontion: Example class that demonstrates a game menu that incorporates
 *                   Wallet Connect and Moralis Authentication.
 *           Author: Moralis Web3 Technology AB, 559307-5988 - David B. Goodrich 
 *  
 *  MIT License
 *  
 *  Copyright (c) 2021 Moralis Web3 Technology AB, 559307-5988
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in all
 *  copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *  SOFTWARE.
 */
#if UNITY_WEBGL
using Moralis.WebGL.Models;
using Moralis.WebGL.Platform;
using Moralis.WebGL.Web3Api.Models;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
#else
using Moralis.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;
using WalletConnectSharp.Core.Models;
#endif
using UnityEngine;
using WalletConnectSharp.Unity;
using Moralis.Web3Api.Models;

namespace Moralis.Web3UnitySdk
{
    public class MoralisController : MonoBehaviour
    { 
        public WalletConnect walletConnect;
        private void OnValidate()
        {

        }

#if UNITY_WEBGL
        public async UniTask Initialize()
        {
            // Make sure the Moralis Setting data has been loaded.
            if (MoralisSettings.MoralisData == null)
            {
                MoralisSettings.LoadOrCreateSettings(true);
            }

            if (!MoralisInterface.Initialized)
            {
                HostManifestData hostManifestData = new HostManifestData()
                {
                    Version = Version,
                    Identifier = ApplicationName,
                    Name = ApplicationName,
                    ShortVersion = Version
                };

                ClientMeta clientMeta = new ClientMeta()
                {
                    Name = ApplicationName,
                    Description = ApplicationDescription,
                    Icons = ApplicationIcons,
                    URL = ApplicationUrl
                };

                await MoralisInterface.Initialize(MoralisSettings.MoralisData.ApplicationId, MoralisSettings.MoralisData.ServerUri, hostManifestData, clientMeta);
            }
        }

#else
        public async Task Initialize()
        {
            // Make sure the Moralis Setting data has been loaded.
            if (MoralisSettings.MoralisData == null)
            {
                MoralisSettings.LoadOrCreateSettings(true);
            }

            if (!MoralisInterface.Initialized)
            {
                HostManifestData hostManifestData = new HostManifestData()
                {
                    Version = MoralisSettings.MoralisData.ApplicationVersion,
                    Identifier = MoralisSettings.MoralisData.ApplicationName,
                    Name = MoralisSettings.MoralisData.ApplicationName,
                    ShortVersion = MoralisSettings.MoralisData.ApplicationVersion
                };

                ClientMeta clientMeta = new ClientMeta()
                {
                    Name = MoralisSettings.MoralisData.ApplicationName,
                    Description = MoralisSettings.MoralisData.ApplicationDescription,
                    Icons = new string[] { MoralisSettings.MoralisData.ApplicationIconUri },
                    URL = MoralisSettings.MoralisData.ApplicationUrl
                };

                walletConnect.AppData = clientMeta;

                // Initialize and register the Moralis, Moralis Web3Api and NEthereum Web3 clients
                await MoralisInterface.Initialize(MoralisSettings.MoralisData.ApplicationId, MoralisSettings.MoralisData.ServerUri, hostManifestData, clientMeta);
            }
        }
#endif

    }
}
