using System;
using SharpGL;
using System.Diagnostics;
using System.Drawing;

namespace _20127661_Lab01
{
    class Line : Shape
    {
        public Line(Point start, Point end) : base(start, end) { }

        public override bool Draw(OpenGL gl)
        {
            if (this.startPoint == this.endPoint)
            {
                return false;
            }
            //thuật toán DDA
            int deltaX, deltaY, stepX = 1, stepY = 1, x, y;
            deltaX = this.endPoint.X - this.startPoint.X;
            deltaY = this.endPoint.Y - this.startPoint.Y;

            if (deltaX < 0)
            {
                deltaX = -deltaX;
                stepX = -1;
            }
            if (deltaY < 0)
            {
                deltaY = -deltaY;
                stepY = -1;
            }

            x = this.startPoint.X;
            y = this.startPoint.Y;
            gl.Begin(OpenGL.GL_POINTS);
            gl.Vertex(x, y);
            this.wall[x, y] = true;
            gl.End();
            if (deltaX > deltaY)
            {
                int P = 2 * deltaY - deltaX;
                while (x != this.endPoint.X)
                {
                    x += stepX;
                    if (P < 0)
                    {
                        P = P + 2 * deltaY;
                    }
                    else
                    {
                        y += stepY;
                        P = P + 2 * deltaY - 2 * deltaX;
                    }
                    gl.Begin(OpenGL.GL_POINTS);
                    gl.Vertex(x, y);
                    this.wall[x, y] = true;
                    gl.End();

                }
            }
            else
            {
                int P = 2 * deltaX - deltaY;
                while (y != this.endPoint.Y)
                {
                    y += stepY;
                    if (P < 0)
                    {
                        P = P + 2 * deltaX;
                    }
                    else
                    {
                        x += stepX;
                        P = P + 2 * deltaX - 2 * deltaY;
                    }
                    gl.Begin(OpenGL.GL_POINTS);
                    gl.Vertex(x, y);
                    this.wall[x, y] = true;
                    gl.End();
                }
            }

            return true;
        }
    }
}
