#define Name "GazeNet client"
#define App "GazeNetClient"
#define Version "1.0.0"
#define Publisher "University of Tampere"
#define PublisherURL "http://gasp.sis.uta.fi/"
#define BuildType "Release"

#define TargetPath "..\bin\" + BuildType + "\client"

[Setup]
AppName={#Name}
AppVersion={#Version}
AppVerName={#Name} {#Version}"
AppPublisher={#Publisher}
DefaultDirName={pf}\{#Publisher}\{#App}
DefaultGroupName={#Publisher}\{#App}
LicenseFile=eula.txt
OutputDir=bin\{#BuildType}
OutputBaseFilename={#App}_Setup
SetupIconFile=..\src\client\icon.ico
Compression=lzma
SolidCompression=yes
UninstallDisplayIcon={app}\GazeNetClient.exe
WizardImageFile=compiler:WizModernImage-IS.bmp
WizardSmallImageFile=compiler:WizModernSmallImage-IS.bmp

[Files]
Source: "{#TargetPath}\GazeNetClient.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#TargetPath}\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#TargetPath}\websocket-sharp.xml"; DestDir: "{app}"; Flags: ignoreversion

[Dirs]

[Icons]
Name: "{group}\{#Name}"; Filename: "{app}\GazeNetClient.exe"
Name: "{group}\{cm:UninstallProgram,}"; Filename: "{uninstallexe}"

[Code]

[Run]

[UninstallDelete]
