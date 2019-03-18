using System;
using Microsoft.CommonLib;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using TestFrontEnd.Interfaces;
using TestFrontEnd.Models;

namespace TestFrontEnd.Features
{

    public class Names : INamesClient
    {
        private readonly IConfigReader _config;
        private readonly IHttpClientFactory _httpClientFactory;

        private string _namesUri = "";

        public Names(IConfigReader config, IHttpClientFactory httpClientFactory)
        {
            this._config = config;
            this._httpClientFactory = httpClientFactory;
            _namesUri = _config["Features:Names:Uri"];
        }

        public async Task<NameModel> GetNameAsync()
        {
            var result = await GetDataAsync(_namesUri);
            if(result.Item1)
            {
                NameModel model = JsonConvert.DeserializeObject<NameModel>(result.Item2);
                return model;
            }
            else
            {
                return null;
            }
        }

        private async Task<(bool,string)> GetDataAsync(string uri)
        {
            if (!string.IsNullOrEmpty(uri))
            {
                var client = _httpClientFactory.CreateClient(uri);
                try
                {
                    var body = await client.GetStringAsync(uri);
                    return (true,body);
                }
                catch(Exception e)
                {
                    return (false,e.Message);
                }
            }
            return (false,"No Test Uri provided");
        }

    }
}