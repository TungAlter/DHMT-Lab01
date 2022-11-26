using System;
using SharpGL;
using System.Diagnostics;
using System.Drawing;

namespace _20127661_Lab01
{
    class Circle : Shape
    {
        public Circle(Point start, Point end) : base(start, end)
        {
            this.radius = Convert.ToInt32(Math.Sqrt(Math.Pow((end.X - start.X), 2) + Math.Pow((end.Y - start.Y), 2)));

        }
        public override bool Draw(OpenGL gl)
        {
            if (this.startPoint == this.endPoint)
            {
                return false;
            }

            // Thuật toán midpoind-----------------------------
            int x = 0, y = radius;
            int P = 1 - radius;
            gl.Begin(OpenGL.GL_POINTS);
            //1/4 thứ nhất (trên phải)
            gl.Vertex(this.startPoint.X + x, this.startPoint.Y + y);
            gl.Vertex(this.startPoint.X + y, this.startPoint.Y + x);
            this.wall[this.startPoint.X + x, this.startPoint.Y + y] = true;
            this.wall[this.startPoint.X + y, this.startPoint.Y + x] = true;
            //1/4 thứ hai (dưới phải)
            gl.Vertex(this.startPoint.X + x, this.startPoint.Y + -y);
            gl.Vertex(this.startPoint.X + y, this.startPoint.Y + -x);
            this.wall[this.startPoint.X + x, this.startPoint.Y + -y] = true;
            this.wall[this.startPoint.X + y, this.startPoint.Y + -x] = true;
            //1/4 thứ ba (dưới trái)
            gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + -y);
            gl.Vertex(this.startPoint.X + -y, this.startPoint.Y + -x);
            this.wall[this.startPoint.X + -x, this.startPoint.Y + -y] = true;
            this.wall[this.startPoint.X + -y, this.startPoint.Y + -x] = true;
            //1/4 thứ bốn (trên trái)
            gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + y);
            gl.Vertex(this.startPoint.X + -y, this.startPoint.Y + x);
            this.wall[this.startPoint.X + -x, this.startPoint.Y + y] = true;
            this.wall[this.startPoint.X + -y, this.startPoint.Y + x] = true;
            gl.End();

            while (x < y)
            {
                if (P < 0)
                {
                    x += 1;
                    P = P + 2 * x + 1;
                }
                else
                {
                    x += 1;
                    y -= 1;
                    P = P + 2 * (x - y) + 1;
                }
                gl.Begin(OpenGL.GL_POINTS);
                //1/4 thứ nhất (trên phải)
                gl.Vertex(this.startPoint.X + x, this.startPoint.Y + y);
                gl.Vertex(this.startPoint.X + y, this.startPoint.Y + x);
                this.wall[this.startPoint.X + x, this.startPoint.Y + y] = true;
                this.wall[this.startPoint.X + y, this.startPoint.Y + x] = true;
                //1/4 thứ hai (dưới phải)
                gl.Vertex(this.startPoint.X + x, this.startPoint.Y + -y);
                gl.Vertex(this.startPoint.X + y, this.startPoint.Y + -x);
                this.wall[this.startPoint.X + x, this.startPoint.Y + -y] = true;
                this.wall[this.startPoint.X + y, Math.Abs(this.startPoint.Y + -x)] = true;
                //1/4 thứ ba (dưới trái)
                gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + -y);
                gl.Vertex(this.startPoint.X + -y, this.startPoint.Y + -x);
                this.wall[this.startPoint.X + -x, this.startPoint.Y + -y] = true;
                this.wall[this.startPoint.X + -y, Math.Abs(this.startPoint.Y + -x)] = true;
                //1/4 thứ bốn (trên trái)
                gl.Vertex(this.startPoint.X + -x, this.startPoint.Y + y);
                gl.Vertex(this.startPoint.X + -y, this.startPoint.Y + x);
                this.wall[this.startPoint.X + -x, this.startPoint.Y + y] = true;
                this.wall[this.startPoint.X + -y, this.startPoint.Y + x] = true;
                gl.End();
            }

            return true;
        }
        public override bool isInside(Point clickPoint)
        {
            int dx = Math.Abs(clickPoint.X - this.startPoint.X);
            if (dx > radius)
            {
                return false;
            }
            int dy = Math.Abs(clickPoint.Y - this.startPoint.Y);
            if (dy > radius)
            {
                return false;
            }
            if (dx + dy <= radius) 
            {
                return true;
            }
            return (dx * dx + dy * dy <= radius * radius);
        }
    }
}
