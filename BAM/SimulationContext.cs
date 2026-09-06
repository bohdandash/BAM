using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace BAM
{
    internal class SimulationContext
    {
        public static class ModelOfAsteroid
        {
            public static Model3D Model { get; set; }
        }

        public static class AsteroidData
        {
            public static double radius { get; set; } = 0;

            public static double surfaceArea { get; set; } = 0;

        }

        public static class CraterData
        {
            /// <summary>
            /// Depth of crater, m
            /// </summary>
            public static int CraterDepth { get; set; } = 0;

            /// <summary>
            /// Diameter of crater, m 
            /// </summary>
            public static int CraterDiameter { get; set; } = 0;

            /// <summary>
            /// Indicates whether the data from the simulation is valid
            /// </summary>
            /// 

            public static int CraterEntryAngle { get; set; } = 0;

            /// <summary>
            /// Indicates whether the data from the simulation is valid
            /// </summary>

            public static bool HasValidData { get; set; } = false;

            /// <summary>
            /// Data to default
            /// </summary>
        }
    }
}
