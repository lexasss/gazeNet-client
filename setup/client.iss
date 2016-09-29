#define Name "GazeNet client"
#define Host "GazeNetClient"
#define Version "1.0.0"
#define Publisher "University of Tampere"
#define PublisherURL "http://www.sis.uta.fi/~csolsp/"
#define TargetPath "..\bin\Debug\client"

[Setup]
AppName={#Name}
AppVersion={#Version}
AppVerName={#Name} {#Version}"
AppPublisher={#Publisher}
DefaultDirName={pf}\{#Publisher}\{#Host}
DefaultGroupName={#Publisher}\{#Host}
LicenseFile=eula.txt
OutputDir=bin\Debug
OutputBaseFilename={#Host}_Setup
SetupIconFile=..\src\client\icon.ico
Compression=lzma
SolidCompression=yes
WizardImageFile=compiler:WizModernImage-IS.bmp
WizardSmallImageFile=compiler:WizModernSmallImage-IS.bmp

[Files]
Source: "{#TargetPath}\GazeNetClient.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#TargetPath}\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#TargetPath}\websocket-sharp.xml"; DestDir: "{app}"; Flags: ignoreversion

[Dirs]

[Icons]
Name: "{group}\{#Name}"; Filename: "{app}\GazeNetClient.exe"
Name: "{group}\{cm:UninstallProgram,{#Name}}"; Filename: "{uninstallexe}"

[Code]

[Run]

[UninstallDelete]
