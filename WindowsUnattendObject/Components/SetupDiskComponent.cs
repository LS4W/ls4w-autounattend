namespace WindowsBuilder.WindowsUnattendObject.Components {

    
    public class SetupDiskComponent : Component {
        public SetupDiskComponent() {
            Name = "Microsoft-Windows-Setup";
            DiskConfiguration = new DiskConfiguration();
            DiskConfiguration.Disk = new Disk();
        }


    }
}