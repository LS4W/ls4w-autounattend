using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WindowsBuilder.WindowsUnattendObject;
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

            Component setupUIComponent = new Component();
            setupUIComponent.SetupUILanguage = new SetupUILanguage();
            setupUIComponent.SetupUILanguage.UILanguage = "EN-US";

            setupUIComponent.InputLocale    = locale;
            setupUIComponent.SystemLocale   = language;
            setupUIComponent.UILanguage     = language;
            setupUIComponent.UserLocale     = language;

            windowsPESettings.Component = new System.Collections.Generic.List<Component>();


            /// SETUP DISK COMPONENT

            Component setupDiskComponent = new Component();
            setupDiskComponent.DiskConfiguration = new DiskConfiguration();
            setupDiskComponent.DiskConfiguration.Disk = new Disk();
            setupDiskComponent.DiskConfiguration.Disk.CreatePartitions = new CreatePartitions();
            setupDiskComponent.DiskConfiguration.Disk.ModifyPartitions = new ModifyPartitions();
            setupDiskComponent.DiskConfiguration.Disk.CreatePartitions.CreatePartition = new System.Collections.Generic.List<CreatePartition>();
            setupDiskComponent.DiskConfiguration.Disk.ModifyPartitions.ModifyPartition = new System.Collections.Generic.List<ModifyPartition>();

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

            setupDiskComponent.DiskConfiguration.Disk.CreatePartitions.CreatePartition.Add(primaryPartition);
            setupDiskComponent.DiskConfiguration.Disk.CreatePartitions.CreatePartition.Add(efiPartition);
            setupDiskComponent.DiskConfiguration.Disk.CreatePartitions.CreatePartition.Add(msrPartition);
            setupDiskComponent.DiskConfiguration.Disk.CreatePartitions.CreatePartition.Add(windowsPartition);

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

            setupDiskComponent.DiskConfiguration.Disk.ModifyPartitions.ModifyPartition.Add(primaryModifyPartition);
            setupDiskComponent.DiskConfiguration.Disk.ModifyPartitions.ModifyPartition.Add(efiModifyPartition);
            setupDiskComponent.DiskConfiguration.Disk.ModifyPartitions.ModifyPartition.Add(msrModifyPartition);
            setupDiskComponent.DiskConfiguration.Disk.ModifyPartitions.ModifyPartition.Add(windowsModifyPartition);



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
