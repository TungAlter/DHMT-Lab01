using System;
using SharpGL;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
namespace _20127661_Lab01
{
    class Rectangle : Line
    {
        public Point leftDown, rightUp;
        int x_min, y_min, x_max, y_max;
        public Rectangle(Point start, Point end) : base(start, end)
        {
            //đỉnh trái dưới
            this.leftDown.X = start.X;
            this.leftDown.Y = end.Y;
            //đỉnh phải trên
            this.rightUp.X = end.X;
            this.rightUp.Y = start.Y;
            if (this.startPoint.X < this.endPoint.X)
            {
                x_min = this.startPoint.X;
                x_max = this.endPoint.X;
            }
            else
            {
                x_min = this.endPoint.X;
                x_max = this.startPoint.X;
            }
            //y_max,y_min
            if (this.startPoint.Y < this.endPoint.Y)
            {
                y_min = this.startPoint.Y;
                y_max = this.endPoint.Y;
            }
            else
            {
                y_min = this.endPoint.Y;
                y_max = this.startPoint.Y;
            }

        }
        public override bool Draw(OpenGL gl)
        {
            if (this.startPoint == this.endPoint)
            {
                return false;
            }
            Line lineUp = new Line(this.startPoint, this.rightUp);
            Line lineRight = new Line(this.startPoint, this.leftDown);
            Line lineDown = new Line(this.leftDown, this.endPoint);
            Line lineLeft = new Line(this.rightUp, this.endPoint);
            
            lineUp.Draw(gl);
            lineRight.Draw(gl);
            lineDown.Draw(gl);
            lineLeft.Draw(gl);
            //Do wall lúc này chỉ lưu vào 4 class line nên phải xài logical để đưa về wall của rectangle
            
            for (int i = 0; i < this.width; i++)
            {
                for(int j=0;j<this.height;j++)
                {
                    this.wall[i, j] = lineRight.wall[i, j] ^ lineUp.wall[i, j] ^ lineDown.wall[i, j] ^ lineLeft.wall[i, j] ;                  
                }    
            }
            return true;
        }
        public override bool isInside(Point clickPoint)
        {
            
            if (clickPoint.X < x_min||clickPoint.X>x_max)
            {
                return false;
            }
            else if(clickPoint.Y<y_min||clickPoint.Y>y_max)
            {
                return false;
            }
            return true;
        }
        //public override void ScanLine_Fill(OpenGL gl)
        //{
        //    if(this.scanline_filled == true)
        //    {
        //        for (int i = y_min; i < y_max; i++)
        //        {
        //            Point left = new Point(x_min, i);
        //            Point right = new Point(x_max, i);
        //            Line drawline = new Line(left, right);
        //            drawline.Draw(gl);
        //        }
        //    }    
           
        //}
        //public override void FloodFill_Fill(OpenGL gl)
        //{
        //    if (floodfill_filled == true)
        //    {
        //        if (coloredPixel.Count > 0)
        //        {
        //            foreach (var p in coloredPixel)
        //            {
        //                gl.Color(fill_color.R, fill_color.G, fill_color.B);
        //                gl.Begin(OpenGL.GL_POINTS);
        //                gl.Vertex(p.X, p.Y);
        //                gl.End();
        //            }
        //            return;
        //        }

        //        Stack<Point> stack = new Stack<Point>();
        //        stack.Push(new Point(x_min+1, y_min+1));
               
        //        while (stack.Count > 0)
        //        {
        //            Point p = stack.Pop();
        //            coloredPixel.Add(p);
        //            //Trace.WriteLine(p);

        //            //đi lên
        //            if (this.wall[p.X + 1, p.Y] == false)
        //            {
        //                Point temp = new Point(p.X + 1, p.Y);
        //                if (this.Find(coloredPixel, temp) == false)
        //                {
        //                    stack.Push(temp);
        //                }
        //            }
        //            // đi xuống
        //            if (this.wall[p.X - 1, p.Y] == false)
        //            {
        //                Point temp = new Point(p.X - 1, p.Y);
        //                if (this.Find(coloredPixel, temp) == false)
        //                {
        //                    stack.Push(temp);
        //                }
        //            }
        //            // qua phải
        //            if (this.wall[p.X, p.Y + 1] == false)
        //            {
        //                Point temp = new Point(p.X, p.Y + 1);
        //                if (this.Find(coloredPixel, temp) == false)
        //                {
        //                    stack.Push(temp);
        //                }
        //            }
        //            // qua trái
        //            if (this.wall[p.X, p.Y - 1] == false)
        //            {
        //                Point temp = new Point(p.X, p.Y - 1);
        //                if (this.Find(coloredPixel, temp) == false)
        //                {
        //                    stack.Push(temp);
        //                }
        //            }
        //        }
        //    }
        //    return;

        //}
    }
}
