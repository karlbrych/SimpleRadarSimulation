using System.ComponentModel.Design;

namespace RadarSimulation
{
    using System;

    namespace RadarSimulation
    {
        public struct RadarSignature
        {
            public double RCS;
            public double Velocity;
        }
        internal class Program
        {
            const double SpeedOfLight = 3e8;
            const double PI = Math.PI;

            // Simplified RCS function for practical values
            public static double EstimateRCS(double reflectedPower, double transmittedPower, double distance, double gain,double frequency)
            {
                double wavelength = SpeedOfLight / frequency; // Calculate wavelength
                double rcs = (reflectedPower * Math.Pow(distance, 2)) /
                      (transmittedPower * Math.Pow(gain, 2) * Math.Pow(4 * PI, 2) * Math.Pow(wavelength, 2));
                return rcs;


            }

            public static double EstimateDopplerShift(double frequency, double velocity)
            {
                return ((2 * velocity) / (SpeedOfLight)) * frequency;
            }

            public static RadarSignature ProcessSignal(double reflectedPower, double transmittedPower, double distance, double gain, double frequency, double velocity)
            {
                RadarSignature signature;
                signature.RCS = EstimateRCS(reflectedPower, transmittedPower, distance, gain,frequency);
                signature.Velocity = EstimateDopplerShift(frequency, velocity);
                return signature;
            }

            public static string ClasifyAircraft(double rcs, double velocity)
            {
                
                // Debugging: Print the RCS and velocity values
                Console.WriteLine($"RCS: {rcs}, Velocity: {velocity}");

                if (rcs > 100.0)
                {
                    return "Commercial Airplane";
                }
                else if (rcs > 10.0 && velocity > 300)
                {
                    return "Fighter Jet";
                }
                else if (rcs <= 1.0)
                {
                    return "Stealth Jet";
                }
                else
                {
                    return "UFO";
                }
            }

            public static void IssueWarning(string aircraftType)
            {
                if (aircraftType == "Fighter Jet")
                {
                    Console.WriteLine("Warning: Fighter Jet detected!");
                }
                else if (aircraftType == "Stealth Jet")
                {
                    Console.WriteLine("Warning: Stealth Aircraft detected!");
                }
                else if (aircraftType == "Commercial Airplane")
                {
                    Console.WriteLine("Info: Commercial Airplane detected, do not engage!");
                }
                else
                {
                    Console.WriteLine("Alert: Unknown Aircraft detected (UFO)!");
                }
            }

            static void Main(string[] args)
            {
               
                double transmittedPower = 1e6;     
                double reflectedPower = 1e-12;      
                double distance = 5000.0;         
                double gain = 40.0;                
                double velocity = 750.0;           
                double frequency = 10e9;           

                // Process radar signal and classify the aircraft
                RadarSignature signature = ProcessSignal(reflectedPower, transmittedPower, distance, gain, frequency, velocity);
                string aircraftType = ClasifyAircraft(signature.RCS, signature.Velocity);

                // Output the detected aircraft type and issue warnings
                Console.WriteLine($"Detected Aircraft Type: {aircraftType}");
                IssueWarning(aircraftType);
            }
        }
    }
}


