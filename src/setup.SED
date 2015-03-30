[Version]
Class=IEXPRESS
SEDVersion=3
[Options]
CheckAdminRights=1
PackagePurpose=InstallApp
ShowInstallProgramWindow=0
HideExtractAnimation=0
UseLongFileName=1
InsideCompressed=0
CAB_FixedSize=0
CAB_ResvCodeSigning=0
RebootMode=I
InstallPrompt=%InstallPrompt%
DisplayLicense=%DisplayLicense%
FinishMessage=%FinishMessage%
TargetName=%TargetName%
FriendlyName=%FriendlyName%
AppLaunched=%AppLaunched%
PostInstallCmd=%PostInstallCmd%
AdminQuietInstCmd=%AdminQuietInstCmd%
UserQuietInstCmd=%UserQuietInstCmd%
SourceFiles=SourceFiles
[Strings]
InstallPrompt=This will install the sample NodePoint Support Request app
DisplayLicense=
FinishMessage=The app has been installed on your Desktop, ready to submit tickets to https://nodepoint.ca/nodepoint
TargetName=C:\scripts\git\supportrequest\supportrequest-setup-1.0.0.EXE
FriendlyName=Sample NodePoint Support Request App
AppLaunched=cmd /c install.bat
PostInstallCmd=<None>
AdminQuietInstCmd=
UserQuietInstCmd=
FILE0="install.bat"
FILE1="supportrequest.exe"
[SourceFiles]
SourceFiles0=C:\scripts\git\supportrequest\bin\
[SourceFiles0]
%FILE0%=
%FILE1%=
