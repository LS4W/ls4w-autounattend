using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WindowsBuilder.WindowsUnattendObject;
using WindowsBuilder.WindowsUnattendObject.Components;
using CommandLine;
using System.Collections.Generic;

namespace WindowsBuilder
{
    class Program
    {

        public class Options {
            [Option('l', "locale", Required=false, Default="0409:00000409")]
            public string locale {get; set;}

            [Option('g',"language", Required=false, Default="EN-US")]
            public string language {get;set;}

            [Option('o', "organization", Required=false, Default="LS4W")]
            public string organization {get;set;}

            [Option('k', "product-key", Required=false, Default="VK7JG-NPHTM-C97JM-9MPGT-3V66T")]
            public string productkey {get;set;}

            [Option('u', "username", Required=false, Default="LS4W")]
            public string username {get;set;}

            [Option('p', "password", Required=false, Default="LS4WPass")]
            public string password {get;set;}

            [Option('n', "computername", Required=false, Default="LS4W")]
            public string computername {get;set;}
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);

        }

        static void HandleParseError(IEnumerable<Error> errors) {
            
        }

        static void RunOptions(Options options) {
            
            Unattend unattendObject = new Unattend();

            Settings windowsPESettings  = new Settings();
            Settings oobeSettings       = new Settings();
            Settings specializeSettings = new Settings();

            windowsPESettings.Pass  = "windowsPE";
            oobeSettings.Pass       = "oobeSystem";
            specializeSettings.Pass = "specialize";
            
            /// SETUP UI COMPONENT
            SetupUIComponent setupUIComponent = new SetupUIComponent();

            setupUIComponent.SetupUILanguage.UILanguage = options.language;
            setupUIComponent.InputLocale    = options.locale;
            setupUIComponent.SystemLocale   = options.language;
            setupUIComponent.UILanguage     = options.language;
            setupUIComponent.UserLocale     = options.language;


            /// SETUP DISK COMPONENT

            SetupDiskComponent setupDiskComponent = new SetupDiskComponent();
            
            Disk disk = new Disk();
            disk.Action = "add";
            disk.DiskID = "0";
            disk.WillWipeDisk = "true";

            CreatePartition primaryPartition    = new CreatePartition();
            CreatePartition efiPartition        = new CreatePartition();
            CreatePartition msrPartition        = new CreatePartition();
            CreatePartition windowsPartition    = new CreatePartition();

            primaryPartition.Order  = "1";
            primaryPartition.Size   = "450";
            primaryPartition.Type   = "Primary";

            efiPartition.Order      = "2";
            efiPartition.Size       = "100";
            efiPartition.Type       = "EFI";

            msrPartition.Order      = "3";
            msrPartition.Size       = "16";
            msrPartition.Type       = "MSR";

            windowsPartition.Order  = "4";
            windowsPartition.Extend = "true";
            windowsPartition.Type   = "Primary";

            disk.createPartition(primaryPartition);
            disk.createPartition(efiPartition);
            disk.createPartition(msrPartition);
            disk.createPartition(windowsPartition);

            /// Setup Modify partitions

            ModifyPartition primaryModifyPartition  = new ModifyPartition();
            ModifyPartition efiModifyPartition      = new ModifyPartition();
            ModifyPartition msrModifyPartition      = new ModifyPartition();
            ModifyPartition windowsModifyPartition  = new ModifyPartition();

            primaryModifyPartition.Format       = "NTFS";
            primaryModifyPartition.Label        = "WinRE";
            primaryModifyPartition.Order        = "1";
            primaryModifyPartition.PartitionID  = "1";
            primaryModifyPartition.TypeID       = "DE94BBA4-06D1-4D40-A16A-BFD50179D6AC";

            efiModifyPartition.Format           = "FAT32";
            efiModifyPartition.Label            = "System";
            efiModifyPartition.Order            = "2";
            efiModifyPartition.PartitionID     = "2";

            msrModifyPartition.Order            = "3";
            msrModifyPartition.PartitionID      = "3";

            windowsModifyPartition.Format       = "NTFS";
            windowsModifyPartition.Label        = "Windows";
            windowsModifyPartition.Letter       = "C";
            windowsModifyPartition.Order        = "4";
            windowsModifyPartition.PartitionID  = "4";

            disk.modifyPartition(primaryModifyPartition);
            disk.modifyPartition(efiModifyPartition);
            disk.modifyPartition(msrModifyPartition);
            disk.modifyPartition(windowsModifyPartition);

            setupDiskComponent.DiskConfiguration.Disk = disk;

            //SETUP USER DATA
            UserData userData = new UserData();
            userData.AcceptEula = "true";
            userData.Organization = options.organization;
            userData.setProductKey(options.productkey);
            setupDiskComponent.UserData = userData;

            //SETUP IMAGE INSTALL
            ImageInstall imageInstall = new ImageInstall();
            InstallTo installTo = new InstallTo();
            installTo.DiskID = "0";
            installTo.PartitionID = "4";
            imageInstall.setInstallTo(installTo);
            setupDiskComponent.ImageInstall = imageInstall;

            //SETUP OOBE
            OobeUIComponent oobeUIComponent = new OobeUIComponent();

            oobeUIComponent.InputLocale = options.locale;
            oobeUIComponent.SystemLocale = options.language;
            oobeUIComponent.UILanguage = options.language;
            oobeUIComponent.UserLocale = options.language;

            //SETUP USER ACCOUNT
            OobeAccountsComponent oobeAccountsComponent = new OobeAccountsComponent();

			oobeAccountsComponent.AutoLogon.Enabled = "true";
			oobeAccountsComponent.AutoLogon.LogonCount = "1";
			oobeAccountsComponent.AutoLogon.Username = "Administrator";
			Password defaultAdminPassword = new Password();
			defaultAdminPassword.Value = "password";
			defaultAdminPassword.PlainText = "true";
			oobeAccountsComponent.AutoLogon.Password = defaultAdminPassword;

			oobeAccountsComponent.Name = "Microsoft-Windows-Shell-Setup";
			oobeAccountsComponent.ProcessorArchitecture = "amd64";
			oobeAccountsComponent.PublicKeyToken = "31bf3856ad364e35";
			oobeAccountsComponent.Language = "neutral";
			oobeAccountsComponent.VersionScope = "nonSxS";

            UserAccounts userAccounts = new UserAccounts();
            LocalAccounts localAccounts = new LocalAccounts();
            LocalAccount localAccount = new LocalAccount();
            
			AdministratorPassword administratorPassword = new AdministratorPassword();
			administratorPassword.Value = "password";
			administratorPassword.PlainText = "true";
			userAccounts.AdministratorPassword = administratorPassword;

            localAccount.Action = "add";
            localAccount.Description = "Local admin account";
            localAccount.DisplayName = options.username;
            localAccount.Group = "Administrators";
            localAccount.Name = options.username;
            
            Password password = new Password();
            password.Value = options.password;
            password.PlainText = "true";
            localAccount.Password = password;

            localAccounts.LocalAccount = localAccount;
            userAccounts.LocalAccounts = localAccounts;
            oobeAccountsComponent.UserAccounts = userAccounts;


            OOBE oOBE = new OOBE();
            oOBE.HideEULAPage = "true";
			oOBE.HideLocalAccountScreen = "true";
			oOBE.NetworkLocation = "Home";
            oOBE.HideOEMRegistrationScreen = "true";
            oOBE.HideOnlineAccountScreens = "true";
            oOBE.ProtectYourPC = "1";
            oOBE.HideWirelessSetupInOOBE = "true";
            oobeAccountsComponent.OOBE = oOBE;

            //SETUP SYSTEM INFORMATION
            SystemInformationComponent systemInformationComponent = new SystemInformationComponent();
			systemInformationComponent.Name = "Microsoft-Windows-Shell-Setup";
			systemInformationComponent.ProcessorArchitecture = "amd64";
			systemInformationComponent.PublicKeyToken = "31bf3856ad364e35";
			systemInformationComponent.VersionScope = "nonSxS";
            systemInformationComponent.ComputerName = options.computername;


            //SETUP TERMINAL SERVICES & FIREWALL (ENABLE RDP)
            TerminalServicesComponent terminalServicesComponent = new TerminalServicesComponent();
			terminalServicesComponent.Name = "Microsoft-Windows-TerminalServices-LocalSessionManager";
			terminalServicesComponent.ProcessorArchitecture = "amd64";
			terminalServicesComponent.PublicKeyToken = "31bf3856ad364e35";
			terminalServicesComponent.Language = "neutral";
			terminalServicesComponent.VersionScope = "nonSxS";
            terminalServicesComponent.FDenyTSConnections = "false";

            NetworkingMPSSVCComponent networkingMPSSVCComponent = new NetworkingMPSSVCComponent();
			networkingMPSSVCComponent.Name = "Networking-MPSSVC-Svc";
			networkingMPSSVCComponent.PublicKeyToken = "31bf3856ad364e35";
			networkingMPSSVCComponent.ProcessorArchitecture = "amd64";
			networkingMPSSVCComponent.Language = "neutral";
			networkingMPSSVCComponent.VersionScope = "nonSxS";
            FirewallGroups firewallGroups = new FirewallGroups();
            FirewallGroup firewallGroup = new FirewallGroup();

            firewallGroup.Action = "add";
            firewallGroup.KeyValue = "RemoteDesktop";
            firewallGroup.Active = "true";
            firewallGroup.Group = "@FirewallAPI.dll,-28752";
            firewallGroup.Profile = "all";

            firewallGroups.FirewallGroup = firewallGroup;
            networkingMPSSVCComponent.FirewallGroups = firewallGroups;

            //SETTING FOR ENABLE/DISABLE NLA 
            RdpExtensionComponent rdpExtensionComponent = new RdpExtensionComponent();
            rdpExtensionComponent.UserAuthentication = "0";


            //Add components to settings
            windowsPESettings.Component.Add(setupUIComponent);
            windowsPESettings.Component.Add(setupDiskComponent);
            // oobeSettings.Component.Add(oobeUIComponent);
            oobeSettings.Component.Add(oobeAccountsComponent);
            specializeSettings.Component.Add(systemInformationComponent);
            specializeSettings.Component.Add(terminalServicesComponent);
            specializeSettings.Component.Add(networkingMPSSVCComponent);
            specializeSettings.Component.Add(rdpExtensionComponent);

            unattendObject.Settings = new System.Collections.Generic.List<Settings>();
            unattendObject.Settings.Add(windowsPESettings);
            unattendObject.Settings.Add(specializeSettings);
            unattendObject.Settings.Add(oobeSettings);

            new XmlSerializer(typeof(WindowsUnattendObject.Unattend)).Serialize(Console.Out, unattendObject);
        }
    }
}
