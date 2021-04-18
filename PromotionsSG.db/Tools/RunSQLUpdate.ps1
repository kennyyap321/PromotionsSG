# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\" -Configuration "Debug" -DBProjectName "PromotionsSG.DB.Claim.db" -DBServerName "promotionssg.crgnn74iana3.ap-southeast-1.rds.amazonaws.com" -DBServerPort "1433" -DBCatalogName "PromotionsSG.DB.Claim" -DBUser "admin" -DBPass "54321password" -Register "P1322074" -Dacpac "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\PromotionsSG.db\PromotionsSG.DB.Claim\Snapshots\PromotionsSG.DB.Claim_20210418_15-08-54.dacpac"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\" -Configuration "Debug" -DBProjectName "PromotionsSG.DB.CustomerProfile.db" -DBServerName "promotionssg.crgnn74iana3.ap-southeast-1.rds.amazonaws.com" -DBServerPort "1433" -DBCatalogName "PromotionsSG.DB.CustomerProfile" -DBUser "admin" -DBPass "54321password" -Register "P1322074" -Dacpac "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\PromotionsSG.db\PromotionsSG.DB.CustomerProfile\Snapshots\PromotionsSG.DB.CustomerProfile_20210418_15-32-59.dacpac"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\" -Configuration "Debug" -DBProjectName "PromotionsSG.DB.Feedback.db" -DBServerName "promotionssg.crgnn74iana3.ap-southeast-1.rds.amazonaws.com" -DBServerPort "1433" -DBCatalogName "PromotionsSG.DB.Feedback" -DBUser "admin" -DBPass "54321password" -Register "P1322074" -Dacpac "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\PromotionsSG.db\PromotionsSG.DB.Feedback\Snapshots\PromotionsSG.DB.Feedback_20210418_15-44-05.dacpac"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\" -Configuration "Debug" -DBProjectName "PromotionsSG.DB.Login.db" -DBServerName "promotionssg.crgnn74iana3.ap-southeast-1.rds.amazonaws.com" -DBServerPort "1433" -DBCatalogName "PromotionsSG.DB.Login" -DBUser "admin" -DBPass "54321password" -Register "P1322074" -Dacpac "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\PromotionsSG.db\PromotionsSG.DB.Login\Snapshots\PromotionsSG.DB.Login_20210418_15-49-30.dacpac"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\" -Configuration "Debug" -DBProjectName "PromotionsSG.DB.Promotion.db" -DBServerName "promotionssg.crgnn74iana3.ap-southeast-1.rds.amazonaws.com" -DBServerPort "1433" -DBCatalogName "PromotionsSG.DB.Promotion" -DBUser "admin" -DBPass "54321password" -Register "P1322074" -Dacpac "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\PromotionsSG.db\PromotionsSG.DB.Promotion\Snapshots\PromotionsSG.DB.Promotion_20210418_15-54-38.dacpac"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\" -Configuration "Debug" -DBProjectName "PromotionsSG.DB.ShopProfile.db" -DBServerName "promotionssg.crgnn74iana3.ap-southeast-1.rds.amazonaws.com" -DBServerPort "1433" -DBCatalogName "PromotionsSG.DB.ShopProfile" -DBUser "admin" -DBPass "54321password" -Register "P1322074" -Dacpac "D:\Kenny\Kenny\Projects\PromotionsSG\PromotionsSG\PromotionsSG.db\PromotionsSG.DB.ShopProfile\Snapshots\PromotionsSG.DB.ShopProfile_20210418_15-58-49.dacpac"

param(
    [Parameter(Mandatory=$true)]
    [string]$SolutionDirectory = "D:\GEMS2Code\GEMS2\",
    [Parameter(Mandatory=$true)]
    [string]$Configuration = "Release",
    [Parameter(Mandatory=$true)]
    [string]$DBProjectName="GEMS2.DB",
    [Parameter(Mandatory=$true)]
    [string]$DBServerName="localhost",
    [Parameter(Mandatory=$true)]
    [string]$DBServerPort="1433",
    [Parameter(Mandatory=$true)]
    [string]$DBCatalogName="GEMS2",
    [Parameter(Mandatory=$true)]
    [string]$DBUser="sa",
    [Parameter(Mandatory=$true)]
    [string]$DBPass="P@ssw0rd",
    [Parameter(Mandatory=$true)]
    [string]$Register="Path32",
    [string]$SQLPackage,
    [string]$NugetDirectory,
    [string]$Dacpac
)

function FindTool([string] $FileName)
{
    Get-ChildItem -Path $ToolsDirectory -Include $FileName -File -Recurse -ErrorAction SilentlyContinue | Select-Object -First 1
}

$SourceDacpacFile = [System.IO.Path]::Combine($SolutionDirectory, $DBProjectName, "bin", $Configuration, "$DBProjectName.dacpac")
if(-not [string]::IsNullOrEmpty($Dacpac))
{
	#$Dacpac = $DBProjectName
	$SourceDacpacFile = $Dacpac
#	$SourceDacpacFile = [System.IO.Path]::Combine($SolutionDirectory, $DBProjectName, "bin", $Configuration, "$DBProjectName.dacpac")
} 
#else 
#{
#	$SourceDacpacFile = $Dacpac
#}
if([string]::IsNullOrEmpty($NugetDirectory))
{
	$NugetDirectory = "C:\Users\$Register\.nuget\"
}
$ToolsDirectory = [System.IO.Path]::Combine($NugetDirectory, "packages")
$TargetConnectionString = "Server=$DBServerName,$DBServerPort;Database=$DBCatalogName;User Id=$DBUser;Password="

Write-Output "`r`nTools used:"

if([string]::IsNullOrEmpty($SQLPackage))
{
    $SQLPackage = FindTool("SQLPackage.exe")
}
Write-Output "    SQLPackage: $SQLPackage"


Write-Output "`r`nRunning SQLPackage"

Write-Output "$SQLPackage /Action:Publish /SourceFile:"$SourceDacpacFile" /TargetConnectionString:"$TargetConnectionString"*********"
&$SQLPackage /Action:Publish /SourceFile:"$SourceDacpacFile" /TargetConnectionString:"$TargetConnectionString$DBPass"


