namespace GaragePi
{
    public class DoorConfig
    {
        public int id { get; set; }
        public string name { get; set; }
        public int CloseSensorPin { get; set; }
        public int OpenSensorPin { get; set; }    
        public int TriggerPin { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0}, Name: {1}, Open/Close/Trigger pins: {2}/{3}/{4}", id, name, OpenSensorPin, CloseSensorPin, TriggerPin);
        }
    }
}
