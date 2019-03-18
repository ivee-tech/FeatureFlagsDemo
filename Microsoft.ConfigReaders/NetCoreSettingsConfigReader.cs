using Microsoft.CommonLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Microsoft.ConfigReaders
{
    public class NetCoreSettingsConfigReader : IConfigReader
    {

        private IConfiguration _configuration;

        public NetCoreSettingsConfigReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string this[string name]
        {
            get
            {
                return _configuration[name];
            }
        }
        public string GetValue(string name)
        {
            return this[name];
        }

        public async Task<string> GetValueAsync(string name)
        {
            return await Task.Run(() => this[name]);
        }

    }
}
