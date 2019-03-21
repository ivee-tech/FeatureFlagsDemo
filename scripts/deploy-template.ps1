$resourceGroupName = 'ff99'
$location = 'AustraliaEast'
$filePath = '..\templates\azuredeploy.json'
$paramFilePath = '..\templates\azuredeploy-green.parameters.json'
$paramFilePath = '..\templates\azuredeploy-blue.parameters.json'

New-AzResourceGroup -Name $resourceGroupName -Location $location
New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName `
  -TemplateFile $filePath -TemplateParameterFile $paramFilePath

