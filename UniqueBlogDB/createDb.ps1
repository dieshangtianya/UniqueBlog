<# Here we use the powershell to execute the sql scripts in batch mode #>


Import-Module Sqlps -DisableNameChecking;


<#tips
if it is first time to use this, we should execute below command to allow the script to run:
run the command prompt as administrator and excute "Set-ExecutionPolicy RemoteSigned"
there are several level about this:
(1)Restricted - No scripts can be run. Windows PowerShell can be used only in interactive mode (This is the default).
(2)AllSigned - Only scripts signed by a trusted publisher can be run.
(3)RemoteSigned - Downloaded scripts must be signed by a trusted publisher before they can be run.
(4)Unrestricted - No restrictions; all Windows PowerShell scripts can be run.
#>

#variables
$sqlserverInstance=".";
$sqlserverUserName="sa";
$sqlserverPassword="sa";
$sqlserverDB="master";

$tableFolder="tables";
$spFolder="storeprocedure";
$presetDataFolder="presetdata";

$isPresetData=$true;


#=====================related functions======================#
function ExecuteSqlFile($sqlFile)
{
	Invoke-SqlCmd -InputFile $sqlFile -serverinstance $sqlserverInstance -Username $sqlserverUserName -Password $sqlserverPassword -Database $sqlserverDB;
    Write-Output "$sqlFile has completed"
}


<#----------------------------create database----------------------------------------#>
ExecuteSqlFile -sqlFile ".\db.sql";


<#----------------------------create data tables-------------------------------------#>
ExecuteSqlFile -sqlFile ".\$tableFolder\t_user.sql";
ExecuteSqlFile -sqlFile ".\$tableFolder\t_blog.sql";
ExecuteSqlFile -sqlFile ".\$tableFolder\t_category.sql";
ExecuteSqlFile -sqlFile ".\$tableFolder\t_blog_post.sql";
ExecuteSqlFile -sqlFile ".\$tableFolder\t_post_category.sql";


<#----------------------------create procedure-------------------------------------#>
ExecuteSqlFile -sqlFile ".\$spFolder\sp_getblogbyusername.sql";
ExecuteSqlFile -sqlFile ".\$spFolder\sp_addblogpost.sql";

<#----------------------------preset data-------------------------------------#>
if($isPresetData){
	ExecuteSqlFile -sqlFile ".\$presetDataFolder\prestoredata.sql";
}