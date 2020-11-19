   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using WindowsBuilder.WindowsUnattendObject.Components;

namespace WindowsBuilder.WindowsUnattendObject
{
	[XmlRoot(ElementName="SetupUILanguage", Namespace="urn:schemas-microsoft-com:unattend")]
	public class SetupUILanguage {
		[XmlElement(ElementName="UILanguage", Namespace="urn:schemas-microsoft-com:unattend")]
		public string UILanguage { get; set; }
	}

	[XmlRoot(ElementName="component", Namespace="urn:schemas-microsoft-com:unattend")]
	[XmlInclude(typeof(SetupUIComponent))]
	[XmlInclude(typeof(SetupDiskComponent))]
	[XmlInclude(typeof(OobeUIComponent))]
	[XmlInclude(typeof(OobeAccountsComponent))]
	[XmlInclude(typeof(SystemInformationComponent))]
	public class Component {
		[XmlElement(ElementName="SetupUILanguage", Namespace="urn:schemas-microsoft-com:unattend")]
		public SetupUILanguage SetupUILanguage { get; set; }
		[XmlElement(ElementName="InputLocale", Namespace="urn:schemas-microsoft-com:unattend")]
		public string InputLocale { get; set; }
		[XmlElement(ElementName="SystemLocale", Namespace="urn:schemas-microsoft-com:unattend")]
		public string SystemLocale { get; set; }
		[XmlElement(ElementName="UILanguage", Namespace="urn:schemas-microsoft-com:unattend")]
		public string UILanguage { get; set; }
		[XmlElement(ElementName="UserLocale", Namespace="urn:schemas-microsoft-com:unattend")]
		public string UserLocale { get; set; }
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="processorArchitecture")]
		public string ProcessorArchitecture { get; set; }
		[XmlAttribute(AttributeName="publicKeyToken")]
		public string PublicKeyToken { get; set; }
		[XmlAttribute(AttributeName="language")]
		public string Language { get; set; }
		[XmlAttribute(AttributeName="versionScope")]
		public string VersionScope { get; set; }
		[XmlAttribute(AttributeName="wcm", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Wcm { get; set; }
		[XmlAttribute(AttributeName="xsi", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
		[XmlElement(ElementName="DiskConfiguration", Namespace="urn:schemas-microsoft-com:unattend")]
		public DiskConfiguration DiskConfiguration { get; set; }
		[XmlElement(ElementName="ImageInstall", Namespace="urn:schemas-microsoft-com:unattend")]
		public ImageInstall ImageInstall { get; set; }
		[XmlElement(ElementName="UserData", Namespace="urn:schemas-microsoft-com:unattend")]
		public UserData UserData { get; set; }
		[XmlElement(ElementName="OOBE", Namespace="urn:schemas-microsoft-com:unattend")]
		public OOBE OOBE { get; set; }
		[XmlElement(ElementName="UserAccounts", Namespace="urn:schemas-microsoft-com:unattend")]
		public UserAccounts UserAccounts { get; set; }
		[XmlElement(ElementName="fDenyTSConnections", Namespace="urn:schemas-microsoft-com:unattend")]
		public string FDenyTSConnections { get; set; }
		[XmlElement(ElementName="FirewallGroups", Namespace="urn:schemas-microsoft-com:unattend")]
		public FirewallGroups FirewallGroups { get; set; }
		[XmlElement(ElementName="UserAuthentication", Namespace="urn:schemas-microsoft-com:unattend")]
		public string UserAuthentication { get; set; }

		public Component() {
			ProcessorArchitecture = "amd64";
            PublicKeyToken = "31bf3856ad364e35";
            Language = "neutral";
            VersionScope = "nonSxS";
            Wcm = "https://schemas.microsoft.com/WMIConfig/2002/State";
            Xsi = "http://www.w3.org/2001/XMLSchema-instance";
		}
	}

	[XmlRoot(ElementName="CreatePartition", Namespace="urn:schemas-microsoft-com:unattend")]
	public class CreatePartition {
		[XmlElement(ElementName="Order", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Order { get; set; }
		[XmlElement(ElementName="Size", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Size { get; set; }
		[XmlElement(ElementName="Type", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName="action", Namespace="http://schemas.microsoft.com/WMIConfig/2002/State")]
		public string Action { get; set; }
		[XmlElement(ElementName="Extend", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Extend { get; set; }

	}

	[XmlRoot(ElementName="CreatePartitions", Namespace="urn:schemas-microsoft-com:unattend")]
	public class CreatePartitions {
		[XmlElement(ElementName="CreatePartition", Namespace="urn:schemas-microsoft-com:unattend")]
		public List<CreatePartition> CreatePartition { get; set; }

		public CreatePartitions() {
			CreatePartition = new List<CreatePartition>();
		}
	}

	[XmlRoot(ElementName="ModifyPartition", Namespace="urn:schemas-microsoft-com:unattend")]
	public class ModifyPartition {
		[XmlElement(ElementName="Format", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Format { get; set; }
		[XmlElement(ElementName="Order", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Order { get; set; }
		[XmlElement(ElementName="PartitionID", Namespace="urn:schemas-microsoft-com:unattend")]
		public string PartitionID { get; set; }
		[XmlElement(ElementName="TypeID", Namespace="urn:schemas-microsoft-com:unattend")]
		public string TypeID { get; set; }
		[XmlElement(ElementName="Label", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Label { get; set; }
		[XmlAttribute(AttributeName="action", Namespace="http://schemas.microsoft.com/WMIConfig/2002/State")]
		public string Action { get; set; }
		[XmlElement(ElementName="Letter", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Letter { get; set; }
	}

	[XmlRoot(ElementName="ModifyPartitions", Namespace="urn:schemas-microsoft-com:unattend")]
	public class ModifyPartitions {
		[XmlElement(ElementName="ModifyPartition", Namespace="urn:schemas-microsoft-com:unattend")]
		public List<ModifyPartition> ModifyPartition { get; set; }

		public ModifyPartitions() {
			ModifyPartition = new List<ModifyPartition>();
		}
	}

	[XmlRoot(ElementName="Disk", Namespace="urn:schemas-microsoft-com:unattend")]
	public class Disk {
		[XmlElement(ElementName="CreatePartitions", Namespace="urn:schemas-microsoft-com:unattend")]
		public CreatePartitions CreatePartitions { get; set; }
		[XmlElement(ElementName="ModifyPartitions", Namespace="urn:schemas-microsoft-com:unattend")]
		public ModifyPartitions ModifyPartitions { get; set; }
		[XmlElement(ElementName="DiskID", Namespace="urn:schemas-microsoft-com:unattend")]
		public string DiskID { get; set; }
		[XmlElement(ElementName="WillWipeDisk", Namespace="urn:schemas-microsoft-com:unattend")]
		public string WillWipeDisk { get; set; }
		[XmlAttribute(AttributeName="action", Namespace="http://schemas.microsoft.com/WMIConfig/2002/State")]
		public string Action { get; set; }

		public Disk() {
			CreatePartitions = new CreatePartitions();
			ModifyPartitions = new ModifyPartitions();
		}

		public void createPartition(CreatePartition partition) {
			CreatePartitions.CreatePartition.Add(partition);
		}

		public void modifyPartition(ModifyPartition partition) {
			ModifyPartitions.ModifyPartition.Add(partition);
		}
	}

	[XmlRoot(ElementName="DiskConfiguration", Namespace="urn:schemas-microsoft-com:unattend")]
	public class DiskConfiguration {
		[XmlElement(ElementName="Disk", Namespace="urn:schemas-microsoft-com:unattend")]
		public Disk Disk { get; set; }
		[XmlElement(ElementName="WillShowUI", Namespace="urn:schemas-microsoft-com:unattend")]
		public string WillShowUI { get; set; }
	}

	[XmlRoot(ElementName="InstallTo", Namespace="urn:schemas-microsoft-com:unattend")]
	public class InstallTo {
		[XmlElement(ElementName="DiskID", Namespace="urn:schemas-microsoft-com:unattend")]
		public string DiskID { get; set; }
		[XmlElement(ElementName="PartitionID", Namespace="urn:schemas-microsoft-com:unattend")]
		public string PartitionID { get; set; }
	}

	[XmlRoot(ElementName="OSImage", Namespace="urn:schemas-microsoft-com:unattend")]
	public class OSImage {
		[XmlElement(ElementName="InstallTo", Namespace="urn:schemas-microsoft-com:unattend")]
		public InstallTo InstallTo { get; set; }
	}

	[XmlRoot(ElementName="ImageInstall", Namespace="urn:schemas-microsoft-com:unattend")]
	public class ImageInstall {
		[XmlElement(ElementName="OSImage", Namespace="urn:schemas-microsoft-com:unattend")]
		public OSImage OSImage { get; set; }

		public ImageInstall() {
			OSImage = new OSImage();
		}

		public void setInstallTo(InstallTo installTo) {
			OSImage.InstallTo = installTo;
		}
	}

	[XmlRoot(ElementName="ProductKey", Namespace="urn:schemas-microsoft-com:unattend")]
	public class ProductKey {
		[XmlElement(ElementName="Key", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Key { get; set; }
	}

	[XmlRoot(ElementName="UserData", Namespace="urn:schemas-microsoft-com:unattend")]
	public class UserData {
		[XmlElement(ElementName="ProductKey", Namespace="urn:schemas-microsoft-com:unattend")]
		public ProductKey ProductKey { get; set; }
		[XmlElement(ElementName="AcceptEula", Namespace="urn:schemas-microsoft-com:unattend")]
		public string AcceptEula { get; set; }
		[XmlElement(ElementName="Organization", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Organization { get; set; }

		public UserData() {
			ProductKey = new ProductKey();
		}

		public void setProductKey(string key) {
			ProductKey.Key = key;
		}
	}

	[XmlRoot(ElementName="settings", Namespace="urn:schemas-microsoft-com:unattend")]
	public class Settings {
		[XmlElement(ElementName="component", Namespace="urn:schemas-microsoft-com:unattend")]
		public List<Component> Component { get; set; }
		[XmlAttribute(AttributeName="pass")]
		public string Pass { get; set; }

		public Settings() {
			Component = new List<Component>();
		}
	}

	[XmlRoot(ElementName="OOBE", Namespace="urn:schemas-microsoft-com:unattend")]
	public class OOBE {
		[XmlElement(ElementName="HideEULAPage", Namespace="urn:schemas-microsoft-com:unattend")]
		public string HideEULAPage { get; set; }
		[XmlElement(ElementName="HideOEMRegistrationScreen", Namespace="urn:schemas-microsoft-com:unattend")]
		public string HideOEMRegistrationScreen { get; set; }
		[XmlElement(ElementName="HideOnlineAccountScreens", Namespace="urn:schemas-microsoft-com:unattend")]
		public string HideOnlineAccountScreens { get; set; }
		[XmlElement(ElementName="HideWirelessSetupInOOBE", Namespace="urn:schemas-microsoft-com:unattend")]
		public string HideWirelessSetupInOOBE { get; set; }
		[XmlElement(ElementName="ProtectYourPC", Namespace="urn:schemas-microsoft-com:unattend")]
		public string ProtectYourPC { get; set; }

	}

	[XmlRoot(ElementName="Password", Namespace="urn:schemas-microsoft-com:unattend")]
	public class Password {
		[XmlElement(ElementName="Value", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Value { get; set; }
		[XmlElement(ElementName="PlainText", Namespace="urn:schemas-microsoft-com:unattend")]
		public string PlainText { get; set; }
	}

	[XmlRoot(ElementName="LocalAccount", Namespace="urn:schemas-microsoft-com:unattend")]
	public class LocalAccount {
		[XmlElement(ElementName="Password", Namespace="urn:schemas-microsoft-com:unattend")]
		public Password Password { get; set; }
		[XmlElement(ElementName="Description", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Description { get; set; }
		[XmlElement(ElementName="DisplayName", Namespace="urn:schemas-microsoft-com:unattend")]
		public string DisplayName { get; set; }
		[XmlElement(ElementName="Group", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Group { get; set; }
		[XmlElement(ElementName="Name", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="action", Namespace="http://schemas.microsoft.com/WMIConfig/2002/State")]
		public string Action { get; set; }
	}

	[XmlRoot(ElementName="LocalAccounts", Namespace="urn:schemas-microsoft-com:unattend")]
	public class LocalAccounts {
		[XmlElement(ElementName="LocalAccount", Namespace="urn:schemas-microsoft-com:unattend")]
		public LocalAccount LocalAccount { get; set; }

		public LocalAccounts() {
			LocalAccount = new LocalAccount();
		}
	}

	[XmlRoot(ElementName="UserAccounts", Namespace="urn:schemas-microsoft-com:unattend")]
	public class UserAccounts {
		[XmlElement(ElementName="LocalAccounts", Namespace="urn:schemas-microsoft-com:unattend")]
		public LocalAccounts LocalAccounts { get; set; }

		public UserAccounts() {
			LocalAccounts = new LocalAccounts();
		}
	}

	[XmlRoot(ElementName="FirewallGroup", Namespace="urn:schemas-microsoft-com:unattend")]
	public class FirewallGroup {
		[XmlElement(ElementName="Active", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Active { get; set; }
		[XmlElement(ElementName="Group", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Group { get; set; }
		[XmlElement(ElementName="Profile", Namespace="urn:schemas-microsoft-com:unattend")]
		public string Profile { get; set; }
		[XmlAttribute(AttributeName="action", Namespace="http://schemas.microsoft.com/WMIConfig/2002/State")]
		public string Action { get; set; }
		[XmlAttribute(AttributeName="keyValue", Namespace="http://schemas.microsoft.com/WMIConfig/2002/State")]
		public string KeyValue { get; set; }
	}

	[XmlRoot(ElementName="FirewallGroups", Namespace="urn:schemas-microsoft-com:unattend")]
	public class FirewallGroups {
		[XmlElement(ElementName="FirewallGroup", Namespace="urn:schemas-microsoft-com:unattend")]
		public FirewallGroup FirewallGroup { get; set; }
	}

	[XmlRoot(ElementName="offlineImage", Namespace="urn:schemas-microsoft-com:cpi")]
	public class OfflineImage {
		[XmlAttribute(AttributeName="source", Namespace="urn:schemas-microsoft-com:cpi")]
		public string Source { get; set; }
		[XmlAttribute(AttributeName="cpi", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Cpi { get; set; }
	}

	[XmlRoot(ElementName="unattend", Namespace="urn:schemas-microsoft-com:unattend")]
	public class Unattend {
		[XmlElement(ElementName="settings", Namespace="urn:schemas-microsoft-com:unattend")]
		public List<Settings> Settings { get; set; }
		[XmlElement(ElementName="offlineImage", Namespace="urn:schemas-microsoft-com:cpi")]
		public OfflineImage OfflineImage { get; set; }
		[XmlAttribute(AttributeName="xmlns")]
		public string Xmlns { get; set; }
	}

}
