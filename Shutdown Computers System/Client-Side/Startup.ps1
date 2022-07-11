## --- Created by Matan Barhoum --- ##
<##
$File = Get-Content C:\Shutdown\ShutDown.txt
$Date1 = [DateTime]::ParseExact($File, 'dd/MM/yyyy HH:mm:ss', $null)
$Date = Get-Date
$TotalSaved = $Date - $Date1 | Select -ExpandProperty TotalHours
$TotalHours = (Get-Date) - [System.Management.ManagementDateTimeconverter]::ToDateTime($(Get-WmiObject -Class Win32_OperatingSystem  | Select-Object -ExpandProperty LastBootUpTime)) | SELECT -ExpandProperty TOTALHOURS
$Body = @{ComputerName=hostname;Month=$Date.Month.ToString();Year=$Date.Year.ToString();TotalHours=$TotalHours;TotalSaved=$TotalSaved}
Invoke-WebRequest -Uri https://localhost:44366/api/EmpStartup -Method POST -Body $Body ##>
$url = 'https://localhost:44366/API/empstartup'
$request = [Net.WebRequest]::Create($url)
$request.ContentType = "application/json; charset=utf-8"
$request.Method = "POST"

$hostname = hostname
$Date = Get-Date
$Month = $Date.Month.ToString()
$Year = $Date.Year.ToString()
$File = Get-Content C:\Shutdown\ShutDown.txt
$Date1 = [DateTime]::ParseExact($File, 'dd/MM/yyyy HH:mm:ss', $null)
$Date = Get-Date
$TotalSaved = $Date - $Date1 | Select -ExpandProperty TotalHours
$TotalHours = (Get-Date) - [System.Management.ManagementDateTimeconverter]::ToDateTime($(Get-WmiObject -Class Win32_OperatingSystem  | Select-Object -ExpandProperty LastBootUpTime)) | SELECT -ExpandProperty TOTALHOURS

$data = @"
{
    "ComputerName":  "$hostname",
    "Month":  "$Month",
    "Year":  "$Year",
    "TotalSaved":  $TotalSaved,
    "TotalHours":  $TotalHours
}
"@

$bytes = [System.Text.Encoding]::ASCII.GetBytes($data)

$request.ContentLength = $bytes.Length

$requestStream = [System.IO.Stream]$request.GetRequestStream()
$requestStream.write($bytes, 0, $bytes.Length)

$response = $request.GetResponse()