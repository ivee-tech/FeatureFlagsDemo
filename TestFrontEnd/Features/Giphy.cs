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
using Newtonsoft.Json.Linq;

namespace TestFrontEnd.Features
{

    public class Giphy : IGiphyClient
    {
        private readonly IConfigReader _config;
        private readonly IHttpClientFactory _httpClientFactory;

        private string _giphyUriFormat = "";

        public Giphy(IConfigReader config, IHttpClientFactory httpClientFactory)
        {
            this._config = config;
            this._httpClientFactory = httpClientFactory;
            _giphyUriFormat = _config["Features:Giphy:UriFormat"];
        }

        public async Task<IEnumerable<string>> GetGifsAsync(string searchText)
        {
            var uri = String.Format(_giphyUriFormat, searchText);
            var result = await GetDataAsync(uri);
            if(result.Item1)
            {
                JObject o = JObject.Parse(result.Item2);
                var list = new List<string>();
                var data = o["data"].Children<JObject>();
                foreach(var child in data)
                {
                    // var imgUrl = child["images"]?["original"]?["url"]?.Value<string>();
                    var imgUrl = child["images"]?["fixed_width"]?["url"]?.Value<string>();
                    list.Add(imgUrl);
                }
                return list;
            }
            else
            {
                return new List<string>();
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