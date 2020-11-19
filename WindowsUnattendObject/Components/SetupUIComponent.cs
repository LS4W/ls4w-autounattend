using System.Xml.Serialization;

namespace WindowsBuilder.WindowsUnattendObject.Components {

    public class SetupUIComponent : Component {
        public SetupUIComponent() {
            Name = "Microsoft-Windows-International-Core-WinPE";
            this.SetupUILanguage = new SetupUILanguage();
        }
    }
}