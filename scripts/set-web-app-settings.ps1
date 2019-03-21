<#
Example:
.\set-web-app-settings.ps1 -resourceGroup ff99 -webAppName ff-web-99 -subscriptionId 7f2ee00e-a72b-401c-9388-f2cdfd63c80c -slot ''
#>
param([string]$resourceGroup  = "" # "[Resource group name]"
    , [string]$webAppName     = "" # "[Web app name]"
    , [string]$subscriptionId = "" # "[Subscription Id]"
    , [string]$slot           = "" # "[DeploymentSlot or empty]"
)
 
$hash = @{
    "Features:Names:Uri" = "http://uinames.com/api/";
    "Features:Giphy:Enabled" = "false";
    "Features:Giphy:EnabledExpr" = "System.DateTime.Now.Hour <= 12";
    "Features:Giphy:UriFormat" = "http://api.giphy.com/v1/gifs/search?q={0}&api_key=dc6zaTOxFJmzC&limit=10&offset=0";
}

function SetSubscription([string] $subscriptionId) {
    try { $context = Get-AzContext -ErrorAction SilentlyContinue } catch { }
    if(!$context) {
        Add-AzAccount # set azure account and select subscription
    }
    if($context.Subscription.SubscriptionId -ne $subscriptionId) {
        Set-AzContext -SubscriptionId $subscriptionId
    }
}
 
function UpdateSettings($hash) {
    if([String]::IsNullOrEmpty($slot)) {
        Set-AzWebApp -ResourceGroupName $resourceGroup -Name $webAppName -AppSettings $hash
    }
    else {
        Set-AzWebAppSlot -ResourceGroupName $resourceGroup -Name $webAppName -AppSettings $hash -Slot $slot
    }
}
 
try
{
 
    Write-Host "Set subscription..."
    SetSubscription($subscriptionId)
 
    Write-Host "Load webApplication and settings..."
    $webApp = Get-AzWebApp -ResourceGroupName $resourceGroup -Name $webAppName
    $webAppSettings = $webApp.SiteConfig.AppSettings
 
    Write-Host "The following application settings are already available in the Azure webapp '$webAppName':"
    $webAppSettings | Format-Table
 
    # copy existing app settings to hash
    foreach ($setting in $webAppSettings) {
        $hash[$setting.Name] = $setting.Value
    }
 

    Write-Host "------------------------------------------------------------------------"
    Write-Host "Updating app settings..."
    UpdateSettings($hash)
    Write-Host "App settings updated successfully."
}
catch {
    Write-Host "An error occurred and the script stopped"
    Write-Error $_.Exception.Message
}
