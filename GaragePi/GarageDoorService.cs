namespace GaragePi
{
    public class GarageDoorService
    {
        private DoorConfigList doorConfigList;
        private ServiceParameters serviceParameters;
        public List<GarageDoor> doors { get; set; }

        public GarageDoorService(ServiceParameters parms, DoorConfigList doorConfigList)
        {
            this.doorConfigList = doorConfigList;
            this.serviceParameters = parms;

            doors = new List<GarageDoor>();
            foreach (DoorConfig d in doorConfigList.doorconfigs)
            {
                GarageDoor door = new GarageDoor(d, serviceParameters.trigger_interval, serviceParameters.cooldown_interval);
                doors.Add(door);
            }
        }
    }
}
