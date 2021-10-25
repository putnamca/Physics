using System;
using System.Drawing;

namespace Physics
{
    class Body
    {
        public Body(double radius, double density, double charge, Color color)
        {
            R = radius;
            D = density;
            Q = charge;
            Color = color;
        }

        public double R { get; }
        public double D { get; }
        public double Q { get; }
        public Color Color { get; }

        public double M => R * R * R * D;

        public double Px { get; set; }
        public double Py { get; set; }
        public double Pz { get; set; }
        public double Ax { get; set; }
        public double Ay { get; set; }
        public double Az { get; set; }
        public double Vx { get; set; }
        public double Vy { get; set; }
        public double Vz { get; set; }

        public double Pos => Math.Sqrt((Px * Px) + (Py * Py) + (Pz * Pz));
        public double Acc => Math.Sqrt((Ax * Ax) + (Ay * Ay) + (Az * Az));
        public double Vel => Math.Sqrt((Vx * Vx) + (Vy * Vy) + (Vz * Vz));

        public string Data => "Mass: " + M + Environment.NewLine +
                              "****************************" + Environment.NewLine +
                              "Px: " + Px + Environment.NewLine +
                              "Py: " + Py + Environment.NewLine +
                              "Pz: " + Pz + Environment.NewLine +
                              "Pos: " + Pos + Environment.NewLine +
                              "****************************" + Environment.NewLine +
                              "Ax: " + Ax + Environment.NewLine +
                              "Ay: " + Ay + Environment.NewLine +
                              "Az: " + Az + Environment.NewLine +
                              "Acc: " + Acc + Environment.NewLine +
                              "****************************" + Environment.NewLine +
                              "Vx: " + Vx + Environment.NewLine +
                              "Vy: " + Vy + Environment.NewLine +
                              "Vz: " + Vz + Environment.NewLine +
                              "Vel: " + Vel + Environment.NewLine +
                              "";
    }
}
