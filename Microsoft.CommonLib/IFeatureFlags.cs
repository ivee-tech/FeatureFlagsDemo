using System;

namespace Microsoft.CommonLib
{
    public interface IFeatureFlags
    {
        bool IsFeatureEnabled(string key);
        bool IsFeatureEnabledExpr(string key);
        T Eval<T>(string expression);
    }

}