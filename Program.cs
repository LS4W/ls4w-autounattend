using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WindowsBuilder.WindowsUnattendObject;
using WindowsBuilder.WindowsUnattendObject.Components;
using CommandLine;

namespace WindowsBuilder
{
    class Program
    {

        public class Options {
            
        }

        static void Main(string[] args)
        {
            string language = "EN-US";
            string locale = "0409:00000409";
            string organization = "L4WS";

            Unattend unattendObject = new Unattend();

            Settings windowsPESettings  = new Settings();
            Settings oobeSettings       = new Settings();
            Settings specializeSettings = new Settings();

            windowsPESettings.Pass  = "windowsPE";
            oobeSettings.Pass       = "oobeSystem";
            specializeSettings.Pass = "specialize";
            
            /// SETUP UI COMPONENT
            SetupUIComponent setupUIComponent = new SetupUIComponent();

            setupUIComponent.SetupUILanguage.UILanguage = "EN-US";
            setupUIComponent.InputLocale    = locale;
            setupUIComponent.SystemLocale   = language;
            setupUIComponent.UILanguage     = language;
            setupUIComponent.UserLocale     = language;


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
            userData.Organization = organization;
            userData.setProductKey("VK7JG-NPHTM-C97JM-9MPGT-3V66T");
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

            oobeUIComponent.InputLocale = locale;
            oobeUIComponent.SystemLocale = language;
            oobeUIComponent.UILanguage = language;
            oobeUIComponent.UserLocale = language;

            //SETUP USER ACCOUNT
            OobeAccountsComponent oobeAccountsComponent = new OobeAccountsComponent();

            UserAccounts userAccounts = new UserAccounts();
            LocalAccounts localAccounts = new LocalAccounts();
            LocalAccount localAccount = new LocalAccount();
            
            localAccount.Action = "add";
            localAccount.Description = "Local admin account";
            localAccount.DisplayName = "LS4WAdmin";
            localAccount.Group = "Administrators";
            localAccount.Name = "LS4WAdmin";
            
            Password password = new Password();
            password.Value = "LS4WPass";
            password.PlainText = "true";
            localAccount.Password = password;

            localAccounts.LocalAccount = localAccount;
            userAccounts.LocalAccounts = localAccounts;
            oobeAccountsComponent.UserAccounts = userAccounts;


            OOBE oOBE = new OOBE();
            oOBE.HideEULAPage = "true";
            oOBE.HideOEMRegistrationScreen = "true";
            oOBE.HideOnlineAccountScreens = "true";
            oOBE.ProtectYourPC = "1";
            oOBE.HideWirelessSetupInOOBE = "true";
            oobeAccountsComponent.OOBE = oOBE;

            //SETUP TERMINAL SERVICES & FIREWALL (ENABLE RDP)
            TerminalServicesComponent terminalServicesComponent = new TerminalServicesComponent();
            terminalServicesComponent.FDenyTSConnections = "false";

            NetworkingMPSSVCComponent networkingMPSSVCComponent = new NetworkingMPSSVCComponent();
            FirewallGroups firewallGroups = new FirewallGroups();
            FirewallGroup firewallGroup = new FirewallGroup();

            firewallGroup.Action = "add";
            firewallGroup.KeyValue = "RemoteDesktop";
            firewallGroup.Active = "true";
            firewallGroup.Group = "@FirewallAPI.dll.-28752";
            firewallGroup.Profile = "all";

            firewallGroups.FirewallGroup = firewallGroup;
            networkingMPSSVCComponent.FirewallGroups = firewallGroups;

            //SETTING FOR ENABLE/DISABLE NLA 
            RdpExtensionComponent rdpExtensionComponent = new RdpExtensionComponent();
            rdpExtensionComponent.UserAuthentication = "0";


            //Add components to settings
            windowsPESettings.Component.Add(setupUIComponent);
            windowsPESettings.Component.Add(setupDiskComponent);
            oobeSettings.Component.Add(oobeUIComponent);
            oobeSettings.Component.Add(oobeAccountsComponent);
            specializeSettings.Component.Add(terminalServicesComponent);
            specializeSettings.Component.Add(networkingMPSSVCComponent);
            specializeSettings.Component.Add(rdpExtensionComponent);

            unattendObject.Settings = new System.Collections.Generic.List<Settings>();
            unattendObject.Settings.Add(windowsPESettings);
            unattendObject.Settings.Add(oobeSettings);
            unattendObject.Settings.Add(specializeSettings);

            new XmlSerializer(typeof(WindowsUnattendObject.Unattend)).Serialize(Console.Out, unattendObject);
        }
    }
}
