using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static Physics.Constants;

namespace Physics
{
    public partial class Form1 : Form
    {
        private System.Drawing.Bitmap bmp1;
        private System.Drawing.Graphics g1;
        private System.Timers.Timer timer2;
        private System.Collections.Generic.List<Body> bodies;

        private int ticks1;
        private int ticks2;
        private bool trails;
        private double spaceScale;
        private double timeScale;

        private double voltage;
        private bool E_on;

        public Form1()
        {
            InitializeComponent();
            InitializeGraphics();
            InitializeSettings();
            InitializeBodies();

            InitializeTimers();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = ticks1.ToString();
            label2.Text = ticks2.ToString();
            label3.Text = bodies[0].Ay.ToString();
            label4.Text = "Voltage: " + (voltage / (timeScale * timeScale)).ToString();

            label5.ForeColor = bodies[0].Color;
            label5.Text = bodies[0].Data;

            //label6.ForeColor = bodies[1].Color;
            //label6.Text = bodies[1].Data;

            DrawGraphics();

            ticks1++;
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            UpdateState();

            ticks2++;
        }

        private void InitializeGraphics()
        {
            bmp1 = new System.Drawing.Bitmap(pictureBox1.Width, pictureBox1.Height);

            g1 = Graphics.FromImage(bmp1);
            g1.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
        }

        private void InitializeSettings()
        {
            trails = false;
            spaceScale = 0.01;
            timeScale = (spaceScale * spaceScale);
            E_on = true;
        }

        private void InitializeBodies()
        {
            bodies = new List<Body>
            {
                new(1.0411710709938535e-6, 886.0, -q, Color.Red),
                //new(1.0411710709938535e-6, 886.0, -q, Color.Green)
            };

            bodies[0].Px = 0.0;
            bodies[0].Py = 2500.0;
            bodies[0].Pz = 0.0;
            bodies[0].Vx = 0.0;
            bodies[0].Vy = 0.0;
            bodies[0].Vz = 0.0;

            //bodies[1].Px = -2500.0;
            //bodies[1].Py = 2500.0;
            //bodies[1].Pz = 0.0;
            //bodies[1].Vx = 0.0;
            //bodies[1].Vy = 0.0;
            //bodies[1].Vz = 0.0;
        }

        private void InitializeTimers()
        {
            timer2 = new System.Timers.Timer(1.0);
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(this.Timer2_Tick);

            timer2.Start();
            timer1.Start();
        }

        private void DrawGraphics()
        {
            int z_alpha = 255;

            if (!trails)
            {
                g1.Clear(pictureBox1.BackColor);
            }

            g1.DrawLine(new(Color.FromArgb(30, Color.Yellow), 1), -pictureBox1.Width, (float)(0.0), pictureBox1.Width, (float)(0.0));
            g1.DrawLine(new(Color.FromArgb(30, Color.Yellow), 1), (float)(0.0), -pictureBox1.Height, (float)(0.0), pictureBox1.Height);

            foreach (var b in bodies)
            {
                double body_x = (0.0 - b.Px) * spaceScale;
                double body_y = (0.0 - b.Py) * spaceScale;
                double body_z = (0.0 - b.Pz) * spaceScale;

                double body_radius = b.R * spaceScale;

                if (body_radius < 4.0)
                {
                    body_radius = 4.0;
                }

                g1.FillEllipse(new SolidBrush(Color.FromArgb(z_alpha, b.Color)),
                                                            (float)(body_x - body_radius),
                                                            (float)(body_y - body_radius),
                                                            (float)(body_radius * 2),
                                                            (float)(body_radius * 2));
            }

            pictureBox1.Image = bmp1;
        }

        private void UpdateState()
        {
            foreach (Body b in bodies)
            {              
                Gravity(b);
                Buoyancy(b);
                Drag(b);
                //Electric(b);
            }
        }

        private void Gravity(Body b1)
        {
            double G = (earth_r * earth_r * earth_r * earth_d * timeScale) /
                       (c * ((-earth_r - b1.Py) * (-earth_r - b1.Py)));

            b1.Ax = 0.0;
            b1.Ay = -G;
            b1.Az = 0.0;

            //foreach (var b2 in bodies)
            //{
            //    if (!ReferenceEquals(b1, b2))
            //    {
            //        double d_x = b2.Px - b1.Px;
            //        double d_y = b2.Py - b1.Py;
            //        double d_z = b2.Pz - b1.Pz;

            //        double distance = Math.Sqrt((d_x * d_x) + (d_y * d_y) + (d_z * d_z));

            //        b1.Ax += b2.M * timeScale * d_x / (c * (distance * distance * distance));
            //        b1.Ay += b2.M * timeScale * d_y / (c * (distance * distance * distance));
            //        b1.Az += b2.M * timeScale * d_z / (c * (distance * distance * distance));
            //    }
            //}

            b1.Vx += b1.Ax;
            b1.Vy += b1.Ay;
            b1.Vz += b1.Az;

            b1.Px += b1.Vx;
            b1.Py += b1.Vy;
            b1.Pz += b1.Vz;
        }

        private void Buoyancy(Body b1)
        {
            double B = (earth_r * earth_r * earth_r * earth_d * timeScale * air_d * timeScale) /
                       (c * ((-earth_r - b1.Py) * (-earth_r - b1.Py)) * b1.D * timeScale);

            b1.Ax += 0.0;
            b1.Ay += B;
            b1.Az += 0.0;

            //foreach (var b2 in bodies)
            //{
            //    if (!ReferenceEquals(b1, b2))
            //    {

            //    }
            //}

            b1.Vx += b1.Ax;
            b1.Vy += b1.Ay;
            b1.Vz += b1.Az;

            b1.Px += b1.Vx;
            b1.Py += b1.Vy;
            b1.Pz += b1.Vz;
        }

        private void Drag(Body b1)
        {
            double Dx;
            double Dy;
            double Dz;

            if (b1.Vel > 0.0)
            {
                Dx = (b1.Vx * b1.Vx * Cd_sphere * Math.PI * b1.R * b1.R * air_d * timeScale) /
                     (2 * b1.R * b1.R * b1.R * b1.D * timeScale);

                Dy = (b1.Vy * b1.Vy * Cd_sphere * Math.PI * b1.R * b1.R * air_d * timeScale) /
                     (2 * b1.R * b1.R * b1.R * b1.D * timeScale);

                Dz = (b1.Vz * b1.Vz * Cd_sphere * Math.PI * b1.R * b1.R * air_d * timeScale) /
                     (2 * b1.R * b1.R * b1.R * b1.D * timeScale);
            }
            else
            {
                Dx = 0.0;
                Dy = 0.0;
                Dz = 0.0;
            }

            b1.Ax += Dx;
            b1.Ay += Dy;
            b1.Az += Dz;

            //foreach (var b2 in bodies)
            //{
            //    if (!ReferenceEquals(b1, b2))
            //    {

            //    }
            //}

            b1.Vx += b1.Ax;
            b1.Vy += b1.Ay;
            b1.Vz += b1.Az;

            b1.Px += b1.Vx;
            b1.Py += b1.Vy;
            b1.Pz += b1.Vz;
        }

        private void Electric(Body b1)
        {
            //if (E_on)
            //{
            //    voltage = (earth_r * earth_r * earth_r * earth_d * timeScale * b1.R * b1.R * b1.R * b1.D * timeScale * plates) /
            //              (c * ((-earth_r - b1.Py) * (-earth_r - b1.Py)) * b1.Q);
            //}
            //else
            //{
            //    voltage = 0.0;
            //}

            voltage = -977.2355573428112;

            double E = (voltage * b1.Q) / (plates * b1.R * b1.R * b1.R * b1.D * timeScale);

            b1.Ax += 0.0;
            b1.Ay += E;
            b1.Az += 0.0;

            foreach (var b2 in bodies)
            {
                if (!ReferenceEquals(b1, b2))
                {

                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            E_on = !E_on;
        }
    }
}
