; Script complet Inno Setup avec page personnalisée + vérification espace disque
[Setup]
AppId={{F67D105F-F8EF-464E-8988-D3DE08BF993B}
AppName=setupFINAL-boualem
AppVersion=1.5
AppPublisher=My Company, Inc.
AppPublisherURL=https://www.example.com/
AppSupportURL=https://www.example.com/
AppUpdatesURL=https://www.example.com/
DefaultDirName={autopf}\setupFINAL-boualem
UninstallDisplayIcon={app}\GestionnaireLicences.exe
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
ChangesAssociations=yes
DefaultGroupName=setupFINAL-boualem
PrivilegesRequiredOverridesAllowed=dialog
OutputDir=C:\Bureau\Projet_final_Deploiement
OutputBaseFilename=mysetup-boualem-final
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Bureau\Projet_final_Deploiement\GestionnaireLicences\GestionnaireLicences\GestionnaireLicences\bin\Release\net6.0-windows\GestionnaireLicences.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Bureau\Projet_final_Deploiement\GestionnaireLicences\GestionnaireLicences\GestionnaireLicences\bin\Release\net6.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Registry]
Root: HKA; Subkey: "Software\Classes\.myp\OpenWithProgids"; ValueType: string; ValueName: "setupFINAL-boualemFile.myp"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\setupFINAL-boualemFile.myp"; ValueType: string; ValueName: ""; ValueData: "setupFINAL-boualem File"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\setupFINAL-boualemFile.myp\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\GestionnaireLicences.exe,0"
Root: HKA; Subkey: "Software\Classes\setupFINAL-boualemFile.myp\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\GestionnaireLicences.exe"" ""%1"""

[Icons]
Name: "{group}\setupFINAL-boualem"; Filename: "{app}\GestionnaireLicences.exe"
Name: "{group}\{cm:UninstallProgram,setupFINAL-boualem}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\setupFINAL-boualem"; Filename: "{app}\GestionnaireLicences.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\GestionnaireLicences.exe"; Description: "{cm:LaunchProgram,setupFINAL-boualem}"; Flags: nowait postinstall skipifsilent

[Code]
var
  InfoPage: TWizardPage;

procedure InitializeWizard;
var
  LabelInfo: TNewStaticText;
begin
  { Création d'une page personnalisée avant l'installation }
  InfoPage := CreateCustomPage(
    wpWelcome,
    'Information importante',
    'Veuillez lire ce message avant de continuer l''installation'
  );
  
  { Ajout du texte en dur }
  LabelInfo := TNewStaticText.Create(InfoPage);
  LabelInfo.Parent := InfoPage.Surface;
  LabelInfo.Caption :=
    'Ce logiciel a été développé par Steven Duquette' + #13#10 +
    'et c''est une application proche de la perfection.';
  LabelInfo.Left := 0;
  LabelInfo.Top := 0;
  LabelInfo.Width := InfoPage.SurfaceWidth;
end;

function InitializeSetup(): Boolean;
var
  InstallPath: String;
  FreeSpace, TotalSpace: Cardinal;
  FreeMB: Cardinal;
begin
  { Vérification d'au moins 1GB d'espace libre }
  InstallPath := ExpandConstant('{autopf}');
  if GetSpaceOnDisk(InstallPath, False, FreeSpace, TotalSpace) then
  begin
    FreeMB := FreeSpace div (1024*1024);
    if FreeMB < 1024 then
    begin
      MsgBox('Erreur : Il faut au moins 1 GB d''espace disque libre pour installer cette application.',
             mbError, MB_OK);
      Result := False;  { Annule l'installation }
    end
    else
      Result := True;
  end
  else
  begin
    { Si on ne peut pas vérifier l'espace, on continue quand même }
    Result := True;
  end;
end;