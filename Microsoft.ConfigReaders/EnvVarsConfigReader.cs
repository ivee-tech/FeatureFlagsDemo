using Microsoft.CommonLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ConfigReaders
{
    public class EnvVarsConfigReader : IConfigReader
    {

        private EnvironmentVariableTarget _target = EnvironmentVariableTarget.User;

        public EnvVarsConfigReader()
        {
            
        }

        public EnvVarsConfigReader(EnvironmentVariableTarget target) : this()
        {
            _target = target;
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
            return Environment.GetEnvironmentVariable(name, _target);
        }

        public async Task<string> GetValueAsync(string name)
        {
            return await Task.Run(() => GetValue(name));
        }

    }
}
