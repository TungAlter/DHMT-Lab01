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
    class Polygon : Shape
    {
        static int INF = 10000;
        public List<Point> PointsList;
        public Polygon(Point start, Point end) : base(start, end)
        { }
        public Polygon() : base()
        {
            PointsList = new List<Point>();
        }
        public int Count()
        {
            return PointsList.Count;
        }
        //public override void TurnOnFloodFill()
        //{
        //    if(this.isClosed()==true && this.isConvex()==true)
        //    {
        //        floodfill_filled = true;
        //    }
        //}
        //Closed Polygon
        public bool isClosed()
        {
            if (PointsList.Count >= 2)
            {
                if (PointsList.First() == PointsList.Last())
                {
                    return true;
                }
            }
            return false;
        }
        public void makeClosed()
        {
            if (PointsList.Count >= 2)
            {
                PointsList.Add(PointsList.First());
            }
        }
        // Convex polygon
        public static float CrossProductLength(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {

            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            return (BAx * BCy - BAy * BCx);
        }
        public bool isConvex()
        {
            if (PointsList.Count <= 2)
            {
                return false;
            }
            bool got_negative = false;
            bool got_positive = false;
            int num_points = PointsList.Count;
            int B, C;
            for (int A = 0; A < num_points; A++)
            {
                B = (A + 1) % num_points;
                C = (B + 1) % num_points;

                float cross_product =
                    CrossProductLength(
                        PointsList[A].X, PointsList[A].Y,
                        PointsList[B].X, PointsList[B].Y,
                        PointsList[C].X, PointsList[C].Y);
                if (cross_product < 0)
                {
                    got_negative = true;
                }
                else if (cross_product > 0)
                {
                    got_positive = true;
                }
                if (got_negative && got_positive)
                {
                    return false;
                }
            }
            return true;
        }
        public void addControlPoint(Point new_point)
        {
            PointsList.Add(new_point);
        }
        public override bool Draw(OpenGL gl)
        {
            for (var i = 0; i < PointsList.Count - 1; i++)
            {
                Line drawline = new Line(PointsList[i], PointsList[i + 1]);
                drawline.Draw(gl);
                for (int j = 0; j < this.width; j++)
                {
                    for (int k = 0; k < this.height; k++)
                    {
                        if (drawline.wall[j, k] == true)
                        {
                            this.wall[j, k] = true;
                        }
                       
                    }
                }
            }
            return true;
        }
        // Check clicked point inside------------------------------------------
        static bool onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) &&
                q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) &&
                q.Y >= Math.Min(p.Y, r.Y))
            {
                return true;
            }
            return false;
        }

        static int orientation(Point p, Point q, Point r)
        {
            int val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            if (val == 0)
            {
                return 0; // collinear 
            }
            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }

        // The function that returns true if 
        // line segment 'p1q1' and 'p2q2' intersect. 
        static bool doIntersect(Point p1, Point q1,
                                Point p2, Point q2)
        {
            // Find the four orientations needed for 
            // general and special cases 
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            // Special Cases 
            // p1, q1 and p2 are collinear and 
            // p2 lies on segment p1q1 
            if (o1 == 0 && onSegment(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are collinear and 
            // q2 lies on segment p1q1 
            if (o2 == 0 && onSegment(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are collinear and 
            // p1 lies on segment p2q2 
            if (o3 == 0 && onSegment(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are collinear and 
            // q1 lies on segment p2q2 
            if (o4 == 0 && onSegment(p2, q1, q2))
            {
                return true;
            }

            // Doesn't fall in any of the above cases 
            return false;
        }

        public override bool isInside(Point clickPoint)
        {
            int n = PointsList.Count;
            if (n <= 2)
            {
                return false;
            }

            // Create a point for line segment from p to infinite 
            Point extreme = new Point(INF, clickPoint.Y);

            // Count intersections of the above line 
            // with sides of polygon 
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to 
                // 'extreme' intersects with the line 
                // segment from 'polygon[i]' to 'polygon[next]' 
                if (doIntersect(PointsList[i],
                                PointsList[next], clickPoint, extreme))
                {
                    // If the point 'p' is collinear with line 
                    // segment 'i-next', then check if it lies 
                    // on segment. If it lies, return true, otherwise false 
                    if (orientation(PointsList[i], clickPoint, PointsList[next]) == 0)
                    {
                        return onSegment(PointsList[i], clickPoint,
                                        PointsList[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);
            // Return true if count is odd, false otherwise 
            if (count % 2 == 1)
            {
                return true;
            }
            return false;
        }
       
        //overide floodfill
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
        //        Point startedPoint = new Point();
        //        bool flag = false;
        //        foreach (var i in points)
        //        {
        //            Trace.WriteLine(i);
        //            if (isInside(new Point(i.X+1,i.Y+1)))
        //            {
        //                Trace.WriteLine("hello");
        //                startedPoint.X = i.X + 1;
        //                startedPoint.Y = i.Y + 1;
        //                break;                     
        //            }
        //            if (isInside(new Point(i.X + 1, i.Y - 1)))
        //            {
        //                Trace.WriteLine("hello");
        //                startedPoint.X = i.X + 1;
        //                startedPoint.Y = i.Y - 1;
        //                break;
        //            }
        //            if (isInside(new Point(i.X - 1, i.Y + 1)))
        //            {
        //                Trace.WriteLine("hello");
        //                startedPoint.X = i.X - 1;
        //                startedPoint.Y = i.Y + 1;
        //                break;
        //            }
        //            if (isInside(new Point(i.X - 1, i.Y - 1)))
        //            {
        //                Trace.WriteLine("hello");
        //                startedPoint.X = i.X - 1;
        //                startedPoint.Y = i.Y - 1;
        //                break;
        //            }
        //        }               
        //        Stack<Point> stack = new Stack<Point>();
        //        stack.Push(startedPoint);
        //        gl.Color(fill_color.R, fill_color.G, fill_color.B);
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

        //}
    }
}
