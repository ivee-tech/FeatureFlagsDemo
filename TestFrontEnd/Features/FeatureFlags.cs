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

    public class FeatureFlags : IFeatureFlags
    {
        private readonly IConfigReader _config;

        public FeatureFlags(IConfigReader config)
        {
            this._config = config;
        }

        public bool IsFeatureEnabled(string key)
        {
            bool result = false;
            bool.TryParse(_config[key], out result);
            return result;
        }


        public bool IsFeatureEnabledExpr(string key)
        {
            bool result = false;
            try
            {
                var expression = _config[key];
                result = CSharpScript.EvaluateAsync<bool>(expression).Result;
            }
            catch(Exception ex)
            {
                // log ex
                result = false;
            }
            return result;
        }
        public T Eval<T>(string expression)
        {
            T result = default(T);
            try
            {
                result = CSharpScript.EvaluateAsync<T>(expression).Result;
            }
            catch(Exception ex)
            {
                // log ex
                result = default(T);
            }
            return result;
        }

    }
}