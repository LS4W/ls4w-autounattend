
namespace WindowsBuilder.WindowsUnattendObject.Components {

    public class NetworkingMPSSVCComponent : Component {
        public NetworkingMPSSVCComponent() {
            Name = "Networking-MPSSVC-Svc";
            FirewallGroups = new FirewallGroups();
        }
    }
}