using System.Device.Gpio;

namespace GaragePi
{
    public class GarageDoor : IDisposable
    {
        public DoorState LastKnownState { get; set; }

        private GpioController gpio;
        public DoorConfig config { get; set; }  
        private int trigger_interval;
        private int cooldown_interval;

        public GarageDoor(DoorConfig dConfig, int trigger_interval, int cooldown_interval)
        {
            config = dConfig;
            this.trigger_interval = trigger_interval;
            this.cooldown_interval = cooldown_interval;

            PinNumberingScheme pinNumberingScheme = PinNumberingScheme.Board;
            gpio = new GpioController(pinNumberingScheme);
            gpio.OpenPin(config.CloseSensorPin, PinMode.InputPullUp);
            gpio.OpenPin(config.OpenSensorPin, PinMode.InputPullUp);
            gpio.OpenPin(config.TriggerPin, PinMode.Output, PinValue.High);
        }

        public bool trigger()
        {
            // hit the trigger pin and update the last known state of the door
            gpio.Write(config.TriggerPin, PinValue.Low);
            Thread.Sleep(trigger_interval);
            gpio.Write(config.TriggerPin, PinValue.High);
            Thread.Sleep(cooldown_interval);

            LastKnownState = DoorState.Opening_Closing;

            return true;
        }

        public DoorState getState()
        {
            DoorState state;    

            // Query the pin for the state
            if (gpio.Read(config.CloseSensorPin) == PinValue.Low)
            {
                // closed
                LastKnownState = DoorState.Closed;
            }
            else if (gpio.Read(config.OpenSensorPin) == PinValue.Low)
            {
                // open
                LastKnownState = DoorState.Open;
            }
            else
            {
                // opening/closing
                LastKnownState = DoorState.Opening_Closing;
            }

            state = LastKnownState;

            return state;
        }

        public void Dispose()
        {
            gpio.ClosePin(config.CloseSensorPin);
            gpio.ClosePin(config.OpenSensorPin);
            gpio.ClosePin(config.TriggerPin);
        }
    }
}
