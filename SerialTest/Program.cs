using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting GPS Coordinates Reader...");

            // Set up serial port
            SerialPort serialPort = new SerialPort("COM3", 115200); // Replace "COM3" with your port name

            // Configure serial port settings
            serialPort.DataBits = 8;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;

            try
            {
                serialPort.Open();
                Console.WriteLine("Listening for GPS data...");

                while (true)
                {
                    // Read a line from the serial port
                    string dataLine = serialPort.ReadLine();

                    // Look for the "Longitude, Latitude" prefix to parse coordinates
                    if (dataLine.Contains("Longitude, Latitude"))
                    {
                        // Read the next two lines for latitude and longitude
                        string latitudeLine = serialPort.ReadLine();
                        string longitudeLine = serialPort.ReadLine();

                        if (double.TryParse(latitudeLine, out double latitude) &&
                            double.TryParse(longitudeLine, out double longitude))
                        {
                            Console.WriteLine($"Latitude: {latitude}");
                            Console.WriteLine($"Longitude: {longitude}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid data format for latitude and longitude.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Close the serial port when done
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    Console.WriteLine("Serial port closed.");
                }
            }
        }
    }
}
