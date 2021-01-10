using System;
using System.IO.Ports;

namespace ArduinoTest
{
    public class Arduino
    {
        private const string _portName = "COM4";
        private SerialPort _port;

        private ArduinoStatus _statusBackingField;
        private ArduinoStatus _status
        {
            get { return _statusBackingField; }
            set
            {
                _statusBackingField = value;
                Console.WriteLine("Status changed: " + _statusBackingField);
            }
        }

        public Arduino()
        {
            _status = ArduinoStatus.Disconnected;

            Connect();
        }

        private void Connect()
        {
            _status = ArduinoStatus.Connecting;

            _port = new SerialPort(_portName);

            try
            {
                _port.Open();

                _status = ArduinoStatus.Connected;
            }
            catch (Exception ex)
            {
                _status = ArduinoStatus.Disconnected;
            }
        }

        public void Disconnect()
        {
            ArduinoStatus previousState = _status;

            _status = ArduinoStatus.Disconnecting;

            try
            {
                _port.Dispose();

                _status = ArduinoStatus.Disconnected;
            }
            catch (Exception ex)
            {
                _status = previousState;
            }
        }

        public void SwitchLED(int state)
        {
            if (_status == ArduinoStatus.Connected)
            {
                _port.Write(state.ToString());
            }
        }
    }

    public enum ArduinoStatus
    {
        Connected,
        Connecting,
        Disconnected,
        Disconnecting
    }
}
