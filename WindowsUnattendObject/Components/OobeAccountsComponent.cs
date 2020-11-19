
namespace WindowsBuilder.WindowsUnattendObject.Components {

    public class OobeAccountsComponent : Component {
        public OobeAccountsComponent() {            
            Name = "Microsoft-Windows-Shell-Setup";

            this.OOBE = new OOBE();
        }
    }
}