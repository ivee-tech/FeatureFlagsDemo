[System.Environment]::SetEnvironmentVariable("Features:Names:Uri", "http://uinames.com/api/", [System.EnvironmentVariableTarget]::Machine)
[System.Environment]::SetEnvironmentVariable("Features:Giphy:Enabled", "false", [System.EnvironmentVariableTarget]::Machine)
[System.Environment]::SetEnvironmentVariable("Features:Giphy:EnabledExpr", "System.DateTime.Now.Hour <= 12", [System.EnvironmentVariableTarget]::Machine)
[System.Environment]::SetEnvironmentVariable("Features:Giphy:UriFormat", "http://api.giphy.com/v1/gifs/search?q={0}&api_key=dc6zaTOxFJmzC&limit=10&offset=0", [System.EnvironmentVariableTarget]::Machine)
