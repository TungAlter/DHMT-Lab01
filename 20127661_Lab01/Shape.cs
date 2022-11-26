using System;
using SharpGL;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
namespace _20127661_Lab01
{
    class Shape
    {
        public int width = 1078;
        public int height = 500;
        public bool[,] wall = new bool[1078*2, 500*2];
        public List<Point> coloredPixel = new List<Point>();

        public Point startPoint, endPoint;
        public int radius = 0;
        public int pixel_Size = 0;
        public Color shape_Color;

        public Shape()
        { }
        public Shape(Point start, Point end)
        {
            this.startPoint = start;
            this.endPoint = end;
        }
        ~Shape() { }
       
        public virtual void Mode(int size, Color userColor)
        {
            this.pixel_Size = size;
            this.shape_Color = userColor;
        }
        public virtual bool Draw(OpenGL gl)
        {
            return true;
        }
        public virtual bool isInside(Point clickPoint)
        {
            return false;
        }
        //public void SetFillColor(Color userColor)
        //{
        //    fill_color = userColor;
        //}
        //public void TurnOnScanLine()
        //{
        //    scanline_filled = true;
        //}
        //public virtual void ScanLine_Fill(OpenGL gl)
        //{

        //}
        //public bool Find(List<Point> points, Point cur)
        //{
        //    foreach (var p in points)
        //    {
        //        if (p == cur)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //public virtual void TurnOnFloodFill()
        //{
        //    floodfill_filled = true;
        //}
        
        //public virtual void FloodFill_Fill(OpenGL gl)
        //{
        //    if (floodfill_filled == true)
        //    {
        //        if(coloredPixel.Count>0)
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
        //        stack.Push(new Point(this.startPoint.X + 1, this.startPoint.Y + 1));
        //        //gl.Color(fill_color.R, fill_color.G, fill_color.B);
        //        while (stack.Count > 0)
        //        {
        //            Point p = stack.Pop();
        //            coloredPixel.Add(p);
        //            //Trace.WriteLine(p);
                   
        //            //đi lên
        //            if (this.wall[p.X+1,p.Y]==false)
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
        //            if (this.wall[p.X, p.Y+1] == false)
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
