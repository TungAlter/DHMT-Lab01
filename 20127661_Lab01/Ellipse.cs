using System;
using SharpGL;
using System.Diagnostics;
using System.Drawing;

namespace _20127661_Lab01
{
    class Ellipse : Shape
    {
        public double radiusX, radiusY;
        public Ellipse(Point start, Point end) : base(start, end)
        {
            this.radiusX = Convert.ToInt32(Math.Sqrt(Math.Pow((end.X - start.X), 2) + Math.Pow((end.Y - start.Y), 2)));
            //cố định radius Y, do chưa biết cách lấy radius Y bằng mouse event @@
            this.radiusY = this.radiusX / 2;
        }
        public override bool Draw(OpenGL gl)
        {
            if (this.startPoint == this.endPoint)
            {
                return false;
            }
            //Thuật toán midpoint----------------------------------------------------------------------------------------------------------
            int x = 0;
            int y = Convert.ToInt32(this.radiusY);
            //Vùng 1
            //P1_0=ry^2+rx^2*ry+1/4*rx^2
            double P1 = Math.Pow(this.radiusY,2)+ Math.Pow(this.radiusX, 2)*this.radiusY+1/ 4*Math.Pow(this.radiusX, 2);
            //2ry^2x
            double deltaX = 2 * Math.Pow(this.radiusY, 2) * x;
            //2rx^2y
            double deltaY = 2 * Math.Pow(this.radiusX,2) * y;
            gl.Begin(OpenGL.GL_POINTS);
            gl.Vertex(this.startPoint.X + x, this.startPoint.Y + y);
            gl.Vertex(this.startPoint.X + x, this.startPoint.Y + -y);
            gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + -y);
            gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + y);
            this.wall[this.startPoint.X + x, this.startPoint.Y + y] = true;
            this.wall[this.startPoint.X + x, this.startPoint.Y + -y] = true;
            this.wall[this.startPoint.X + -x, this.startPoint.Y + -y]=true;
            this.wall[this.startPoint.X + -x, this.startPoint.Y + y] = true;
            gl.End();
            while(deltaX < deltaY)
            {
                x += 1;
                if(P1<0)
                {
                    deltaX = deltaX + 2 * Math.Pow(this.radiusY, 2);
                    P1 = P1 + deltaX + Math.Pow(this.radiusY, 2);
                }
                else
                {
                    y -= 1;
                    deltaX = deltaX + 2 * Math.Pow(this.radiusY, 2);
                    deltaY = deltaY - 2 * Math.Pow(this.radiusX, 2);
                    P1 = P1 + deltaX- deltaY + Math.Pow(this.radiusY, 2);
                }
                gl.Begin(OpenGL.GL_POINTS);
                gl.Vertex(this.startPoint.X + x, this.startPoint.Y + y);
                gl.Vertex(this.startPoint.X + x, this.startPoint.Y + -y);
                gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + -y);
                gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + y);
                this.wall[this.startPoint.X + x, this.startPoint.Y + y] = true;
                this.wall[this.startPoint.X + x, this.startPoint.Y + -y] = true;
                this.wall[this.startPoint.X + -x, this.startPoint.Y + -y] = true;
                this.wall[this.startPoint.X + -x, this.startPoint.Y + y] = true;
                gl.End();
            }
            
            //Vùng 2
            deltaX = 2 * Math.Pow(this.radiusY, 2)*x;
            deltaY = 2 * Math.Pow(this.radiusX, 2) * y;
            //P2_0=ry^2*(x+1/2)^2 + rx^2*(y-1)^2 - rx^2*ry^2;
            double P2 =Math.Pow(this.radiusY, 2)* Math.Pow(x+1/2, 2) + Math.Pow(this.radiusX,2)*Math.Pow(y-1,2) - Math.Pow(this.radiusY, 2)* Math.Pow(this.radiusX, 2);
            while (y>=0)
            {
                y-= 1;
                if (P2 < 0)
                {
                    x += 1;
                    deltaX = deltaX + 2 * Math.Pow(this.radiusY, 2);
                    deltaY = deltaY - 2 * Math.Pow(this.radiusX, 2);
                    P2 = P2 + deltaX - deltaY + Math.Pow(this.radiusX, 2);
                }
                else
                {
                    deltaY = deltaY - 2 * Math.Pow(this.radiusX, 2);
                    P2 = P2 -deltaY + Math.Pow(this.radiusX, 2);
                }
                gl.Begin(OpenGL.GL_POINTS);
                gl.Vertex(this.startPoint.X + x, this.startPoint.Y + y);
                gl.Vertex(this.startPoint.X + x, this.startPoint.Y + -y);
                gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + -y);
                gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + y);
                this.wall[this.startPoint.X + x, this.startPoint.Y + y] = true;
                this.wall[this.startPoint.X + x, this.startPoint.Y + -y] = true;
                this.wall[this.startPoint.X + -x, this.startPoint.Y + -y] = true;
                this.wall[this.startPoint.X + -x, this.startPoint.Y + y] = true;
                gl.End();
            }
            // sử dụng Phương trình tham số của ellipse----------------------------------------------------------------------------------------------------------
            /*int x, y;
            for (double i = 0.0; i < 360.0; i += 0.1)

            {
                double angle = i * System.Math.PI / 180;

                x = (int)(this.startPoint.X + this.radiusX * System.Math.Cos(angle));

                y = (int)(this.startPoint.Y + this.radiusY * System.Math.Sin(angle));

                gl.Begin(OpenGL.GL_POINTS);
                gl.Vertex(x, y);
                gl.End();
            }*/
            return true;
        }
        public override bool isInside(Point clickPoint)
        {
            double dx = Math.Pow(clickPoint.X - this.startPoint.X,2)/Math.Pow(this.radiusX,2);
            double dy = Math.Pow(clickPoint.Y - this.startPoint.Y, 2) / Math.Pow(this.radiusY, 2);
            
            return (dx+dy <= 1);
        }
    }
}
