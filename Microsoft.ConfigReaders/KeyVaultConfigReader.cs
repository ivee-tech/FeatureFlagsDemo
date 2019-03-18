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
    public class KeyVaultConfigReader : IConfigReader
    {

        private string _keyVaultUri;
        private IKeyVaultClient _keyVaultClient;
        private IConfigReader _config;

        public KeyVaultConfigReader()
        {
            _config = new DictionaryConfigReader();
            InitKeyVault();
        }

        public KeyVaultConfigReader(IConfigReader config)
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
                new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback)
            );
        }

        private async Task<string> GetKeyValue(string key)
        {
            var secret = await _keyVaultClient.GetSecretAsync(_keyVaultUri, key);
            return secret.Value;
        }
    }
}
