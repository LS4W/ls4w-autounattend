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

            windowsPESettings.Component = new System.Collections.Generic.List<Component>();


            /// SETUP DISK COMPONENT

            SetupDiskComponent setupDiskComponent = new SetupDiskComponent();
            Disk disk = new Disk();

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


            //Add components to settings
            windowsPESettings.Component.Add(setupUIComponent);
            windowsPESettings.Component.Add(setupDiskComponent);

            unattendObject.Settings = new System.Collections.Generic.List<Settings>();
            unattendObject.Settings.Add(windowsPESettings);
            unattendObject.Settings.Add(oobeSettings);
            unattendObject.Settings.Add(specializeSettings);

            new XmlSerializer(typeof(WindowsUnattendObject.Unattend)).Serialize(Console.Out, unattendObject);
        }
    }
}
