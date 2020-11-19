namespace WindowsBuilder.WindowsUnattendObject.Components {

    
    public class SetupDiskComponent : Component {
        public SetupDiskComponent() {
            DiskConfiguration = new DiskConfiguration();
            DiskConfiguration.Disk = new Disk();
        }


    }
}