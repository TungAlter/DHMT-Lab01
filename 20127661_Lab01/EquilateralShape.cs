using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Linq;
using SharpGL;
using System.Diagnostics;
using System.Drawing;

namespace _20127661_Lab01
{
    class EquilateralRectangle : Polygon
    {
        public double angle_step;
        public int step;
        public EquilateralRectangle(Point start, Point end) : base(start, end)
        {
            this.radius = Convert.ToInt32(Math.Sqrt(Math.Pow((end.X - start.X), 2) + Math.Pow((end.Y - start.Y), 2)));
            this.angle_step = 360 / 4;
            this.step = 5;
            this.PointsList= new List<Point>();
        }
        public override bool Draw(OpenGL gl)
        {
            if (this.startPoint == this.endPoint)
            {
                return false;
            }
            Point cur = new Point();
            Point next = new Point();
            cur.X = (int)(this.startPoint.X + radius * System.Math.Cos(0));
            cur.Y = (int)(this.startPoint.Y + radius * System.Math.Sin(0));
            this.PointsList.Add(cur);
            for (double i = 1; i < step; i += 1)
            {
                double angle = angle_step * i * System.Math.PI / 180;

                next.X = (int)(this.startPoint.X + radius * System.Math.Cos(angle));
                next.Y = (int)(this.startPoint.Y + radius * System.Math.Sin(angle));
                this.PointsList.Add(next);
                Line tempLine = new Line(cur, next);               
                tempLine.Draw(gl);          
                for (int j = 0; j < this.width; j++)
                {
                    for (int k = 0; k < this.height; k++)
                    {
                        
                        if(tempLine.wall[j, k]==true)
                        {
                            this.wall[j, k] = true;
                        }    
                        //this.wall[j, k] = this.wall[j, k] ^ tempLine.wall[j, k];
                    }
                }
                cur = next;
            }
            return true;
        }
        
    }
    class EquilateralPentagon : EquilateralRectangle
    {
        public EquilateralPentagon(Point start, Point end) : base(start, end)
        {
            this.angle_step = 360 / 5;
            this.step = 6;
        }
    }
    class EquilateralHexagon : EquilateralRectangle
    {
        public EquilateralHexagon(Point start, Point end) : base(start, end)
        {
            this.angle_step = 360 / 6;
            this.step = 7;
        }
    }
}
