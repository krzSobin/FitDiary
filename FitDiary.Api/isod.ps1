$ie = new-object -ComObject "InternetExplorer.Application"
$ie.Visible = $True

$requestUri = "https://isod.ee.pw.edu.pl/isod-stud/signin"
$userIdFragment = "username";
$passwordIdFragment = "password";
$buttonIdFragment = "login button";

$ie.navigate($requestUri)
while($ie.Busy) { Start-Sleep -Milliseconds 200 }

$doc = $ie.Document
$doc.getElementsByTagName("input") | % {
    if ($_.id -ne $null){
        if ($_.id.Contains($passwordIdFragment)) { $pwd = $_ }
        if ($_.id.Contains($userIdFragment)) { $user = $_ }
    }
}
$doc.getElementsByClassName("button") | % {
    if ($_.id -ne $null){
        $btn = $_
    }
}

$user.value = "sobinskk";
$pwd.value = "passwooord";
$btn.disabled = $false;
$btn.click();

while($ie.Busy) { Start-Sleep -Milliseconds 400 }
$requestUri = "https://isod.ee.pw.edu.pl/isod-stud/?wicket:bookmarkablePage=:isod.app.declarations.DeclarationsPage"
$ie.navigate($requestUri)

$buttons = $doc.getElementsByClassName("button")

# ------mail sending -----
$From = "krzysiek812@gmail.com"
$To = "krz.sobin@gmail.com"
$Subject = "Email Subject"
$Body = "Insert body text here"
$SMTPServer = "smtp.gmail.com"
$SMTPPort = "587"

$userPassword = ConvertTo-SecureString -String "*****" -AsPlainText -Force


Send-MailMessage -From $From -to $To -Subject $Subject `
-Body $Body -SmtpServer $SMTPServer -port $SMTPPort -UseSsl `
-Credential (New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList "krzysiek812@gmail.com", $userPassword)



echo "beerfeb"
#$isodSite = $web.DownloadString("https://isod.ee.pw.edu.pl/isod-stud/?wicket:bookmarkablePage=:isod.app.declarations.DeclarationsPage")
#add-content -C:\Users\krzysztof.sobinski isodTXT.txt -value$web.DownloadString("https://isod.ee.pw.edu.pl/isod-stud/?wicket:bookmarkablePage=:isod.app.declarations.DeclarationsPage")
#echo "$isodSite"
Start-Sleep -Milliseconds 40000
"Press Enter to exit"

Write-Verbose "Login Complete"