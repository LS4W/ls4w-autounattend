using System.Xml.Serialization;

namespace WindowsBuilder.WindowsUnattendObject.Components {

    public class SetupUIComponent : Component {
        public SetupUIComponent() {
            this.SetupUILanguage = new SetupUILanguage();
        }
    }
}