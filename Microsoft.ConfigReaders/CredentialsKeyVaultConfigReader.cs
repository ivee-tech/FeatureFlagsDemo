using Microsoft.CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Microsoft.ConfigReaders
{
    public class CredentialsKeyVaultConfigReader : IConfigReader
    {

        private string _keyVaultUri;
        private IKeyVaultClient _keyVaultClient;
        private IConfigReader _config;

        public CredentialsKeyVaultConfigReader()
        {
            _config = new DictionaryConfigReader();
            InitKeyVault();
        }

        public CredentialsKeyVaultConfigReader(IConfigReader config)
        {
            _config = config;
            InitKeyVault();
        }

        public string this[string name]
        {
            get
            {
                return GetValue(name);
            }
        }

        public string GetValue(string name)
        {
            var value = GetKeyValue(name).Result;
            return value;
        }

        public async Task<string> GetValueAsync(string name)
        {
            var value = await GetKeyValue(name);
            return value;
        }

        private void InitKeyVault()
        {
            _keyVaultUri = _config["KeyVaultUri"];
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            _keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(GetAccessToken)
            );
        }

        private async Task<string> GetKeyValue(string key)
        {
            var secret = await _keyVaultClient.GetSecretAsync(_keyVaultUri, key);
            return secret.Value;
        }
        private async Task<string> GetAccessToken(string authority, string resource, string scope)
        {
            string clientId = _config["ida:ClientId"];
            var clientKey = _config["ida:ClientSecret"];
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(clientId, clientKey);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain access token.");

            return result.AccessToken;
        }
    }
}
