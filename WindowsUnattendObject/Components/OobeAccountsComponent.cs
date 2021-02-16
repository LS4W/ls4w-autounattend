
namespace WindowsBuilder.WindowsUnattendObject.Components {

    public class OobeAccountsComponent : Component {

        public OobeAccountsComponent() {            
            Name = "Microsoft-Windows-Shell-Setup";

			this.AutoLogon = new AutoLogon();
            this.OOBE = new OOBE();
        }
    }
}
