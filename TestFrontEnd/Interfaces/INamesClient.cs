using System;
using System.Threading.Tasks;
using TestFrontEnd.Models;

namespace TestFrontEnd.Interfaces
{

    public interface INamesClient
    {
        Task<NameModel> GetNameAsync();
    }
}