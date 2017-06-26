<# Here we use the powershell to execute the sql scripts in batch mode #>
<# If need to execute sql script to persistent the data to MySql database we should refer to the "MySql connector net"
   While writing code, the version is Connector.NET 6.9.
#>

[system.reflection.assembly]::LoadWithPartialName("MySql.Data")

#variables
$mySqlInstance="localhost";
$mySqlUser="frwang";
$mySqlPassword="wxfblog3748511";

$tableFolder="tables";
$viewFolder="views";
$spFolder="storeprocedure";
$presetDataFolder="presetdata";
$tableTypeFolder="types";

$isPresetData=$true;

#=====================related functions======================#
function ExecuteSqlFile($sqlFile)
{
	$sql = (Get-Content $sqlFile);
	$mySqlCommand.CommandText=$sql;
	Write-Output $sql;
	$mySqlCommand.ExecuteNonQuery();
}

Write-Output "Begin to open the mysql connection...";

$mySqlConnectionStr="server="+$mySqlInstance+";database=mysql;uid="+$mySqlUser+";pwd="+$mySqlPassword+";Allow User Variables=True";

$mySqlConnection = New-Object -TypeName MySql.Data.MySqlClient.MySqlConnection($mySqlConnectionStr);
$mySqlConnection.Open();

Write-Output "Open the database connection";

$mySqlCommand = New-Object -TypeName MySql.Data.MySqlClient.MySqlCommand;
$mySqlCommand.Connection=$mySqlConnection;

<#----------------------------create database----------------------------------------#>
Write-Output "Begin to create database";
ExecuteSqlFile -sqlFile ".\db.sql";

<#----------------------------create data tables-------------------------------------#>
ExecuteSqlFile -sqlFile ".\$tableFolder\t_user.sql";
ExecuteSqlFile -sqlFile ".\$tableFolder\t_blog.sql";
ExecuteSqlFile -sqlFile ".\$tableFolder\t_category.sql";
ExecuteSqlFile -sqlFile ".\$tableFolder\t_blog_post.sql";
ExecuteSqlFile -sqlFile ".\$tableFolder\t_post_category.sql";
ExecuteSqlFile -sqlFile ".\$tableFolder\t_comment.sql";

<#----------------------------create data viwes-------------------------------------#>
ExecuteSqlFile -sqlFile ".\$viewFolder\v_category_info.sql";

<#----------------------------create procedure-------------------------------------#>
ExecuteSqlFile -sqlFile ".\$spFolder\sp_get_blogbyusername.sql";
ExecuteSqlFile -sqlFile ".\$spFolder\sp_add_blogpost.sql";
ExecuteSqlFile -sqlFile ".\$spFolder\sp_get_blogpost_categories.sql";
ExecuteSqlFile -sqlFile ".\$spFolder\sp_update_blogpost.sql";
ExecuteSqlFile -sqlFile ".\$spFolder\sp_get_all_categories.sql";
ExecuteSqlFile -sqlFile ".\$spFolder\sp_add_comment.sql"
ExecuteSqlFile -sqlFile ".\$spFolder\sp_get_items_super_pagination.sql";

<#----------------------------preset data-------------------------------------#>
if($isPresetData){
	ExecuteSqlFile -sqlFile ".\$presetDataFolder\prestoredata.sql";
}


$mySqlConnection.Close();
Write-Output "Close the database connection";

Write-Output "Success";