using Microsoft.CommonLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ConfigReaders
{
    public class DictionaryConfigReader : IConfigReader
    {

        private IDictionary<string, string> _dict = new Dictionary<string, string>();

        public DictionaryConfigReader()
        {

        }

        public DictionaryConfigReader(IDictionary<string, string> dict) : this()
        {
            _dict = dict;
        }

        public string this[string name]
        {
            get
            {
                if (_dict.ContainsKey(name))
                    return string.Empty;
                return _dict[name];
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
