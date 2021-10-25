using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public static class Constants
    {
        //CODATA
        public const double c = 299792458.0;
        public const double h = 6.62607015e-34;
        public const double q = 1.602176634e-19;
        public const double NA = 6.02214076e23;
        public const double kB = 1.380649e-23;
        public const double amu = 1.6605390666e-27;
        public const double K = 1.380649e-23;

        //masses
        public const double m_e = 9.1093837105e-31;
        public const double m_p = 1.67262192369e-27;
        public const double m_n = 1.67492749804e-27;

        //sun
        public const double sun_r = 696342000.0;
        public const double sun_d = 117.8330624292825;
        public const double sun_m = sun_r * sun_r * sun_r * sun_d;

        //earth
        public const double earth_r = 6378137.0;
        public const double earth_d = 460.94333004225217;
        public const double earth_m = earth_r * earth_r * earth_r * earth_d;

        //environment
        public const double atm = 101325;
        public const double T = 293.15 * K;

        //plates
        public const double plates = 1.6e-2;

        //air
        public const double airmass = 2.89645e-2;
        public const double air_d = (atm * airmass) / (NA * T);

        //coefficient
        public const double Cd_sphere = 0.47;
    }
}
