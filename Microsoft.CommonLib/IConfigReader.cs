using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.CommonLib
{
    public interface IConfigReader
    {
        string this[string name] { get; }

        string GetValue(string name);
        Task<string> GetValueAsync(string name);
    }
}
