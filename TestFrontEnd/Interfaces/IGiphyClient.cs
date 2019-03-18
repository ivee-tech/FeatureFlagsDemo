using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CommonLib;

namespace TestFrontEnd.Interfaces
{
    public interface IGiphyClient
    {
        Task<IEnumerable<string>> GetGifsAsync(string searchText);
    }
}