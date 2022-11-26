using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.WinForms;

namespace _20127661_Lab01
{
    
    public partial class Form1 : Form
    {
        Polygon drawPolygon = null;
        Point StartPosition;
        Point EndPosition;
        Point CurPosition;
        Color ColorOption = Color.Black;
        int Size;
        int option = 0;
        int selectedIndex = -1;
        bool IsDrawing = false;
        bool IsRightClick = false;
        bool IsLeftClick = false;
        
        List<Shape> nObject = new List<Shape>();
        public Form1()
        {
            InitializeComponent();
        }
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openglControl1.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openglControl1.Width, openglControl1.Height);
            // Dùng chỉnh để lật hệ quy chiếu Oxy lại
            gl.Ortho2D(0, openglControl1.Width, openglControl1.Height, 0);

        }
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            
            // Get the OpenGL object.
            OpenGL gl = openglControl1.OpenGL;
            // Set the clear color.
            gl.ClearColor(1, 1, 1, 1);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
          
        }
        
        private void openGLControl_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {

            OpenGL gl = openglControl1.OpenGL;
           

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);       

            foreach (var shape in nObject)
            {
                
                gl.Color(shape.shape_Color);
                gl.PointSize(shape.pixel_Size);
                shape.Draw(gl);                    
                //shape.FloodFill_Fill(gl);
            }
            if (IsDrawing == true)
            {

                if (option == 1)
                {
                    Line drawline = new Line(StartPosition, CurPosition);
                    drawline.Mode(Size, ColorOption);
                    drawline.Draw(gl);
                    nObject[nObject.Count - 1] = drawline;
                }
                else if (option == 2)
                {
                    Circle drawCircle = new Circle(StartPosition, CurPosition);
                    drawCircle.Mode(Size, ColorOption);
                    drawCircle.Draw(gl);
                    nObject[nObject.Count - 1] = drawCircle;

                }
                else if (option == 3)
                {
                    Ellipse drawEllipse = new Ellipse(StartPosition, CurPosition);
                    drawEllipse.Mode(Size, ColorOption);
                    drawEllipse.Draw(gl);
                    nObject[nObject.Count - 1] = drawEllipse;

                }
                else if (option == 4)
                {
                    Rectangle drawRectangle = new Rectangle(StartPosition, CurPosition);
                    drawRectangle.Mode(Size, ColorOption);
                    drawRectangle.Draw(gl);
                    nObject[nObject.Count - 1] = drawRectangle;

                }
                else if (option == 5)
                {
                    EquilateralRectangle drawSquare = new EquilateralRectangle(StartPosition, CurPosition);
                    drawSquare.Mode(Size, ColorOption);
                    drawSquare.Draw(gl);
                    nObject[nObject.Count - 1] = drawSquare;

                }
                else if (option == 6)
                {
                    EquilateralPentagon drawPentagon = new EquilateralPentagon(StartPosition, CurPosition);
                    drawPentagon.Mode(Size, ColorOption);
                    drawPentagon.Draw(gl);
                    nObject[nObject.Count - 1] = drawPentagon;

                }
                else if (option == 7)
                {
                    EquilateralHexagon drawHexagon = new EquilateralHexagon(StartPosition, CurPosition);
                    drawHexagon.Mode(Size, ColorOption);
                    drawHexagon.Draw(gl);
                    nObject[nObject.Count - 1] = drawHexagon;

                }
                
                //else if (option == 9)
                //{                 
                //    for (int i=0;i<nObject.Count;i++)
                //    {
                //        if (nObject[i].isInside(EndPosition) == true)
                //        {
                //            selectedIndex = i;
                //            Trace.WriteLine(selectedIndex);
                //        }
                //    }
                //}    
            }
          
            gl.Flush();
        }

        private void openglControl1_Load(object sender, EventArgs e)
        { }
        //Event nhấn thả , di chuyển chuột trên opengl
        //Nhấn
        private void openglControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StartPosition = e.Location;
                EndPosition = StartPosition;
                IsDrawing = true;
                if (option != 8 && option != 9)
                {                  
                    nObject.Add(new Shape(StartPosition, StartPosition));
                }
                else if (option==8)
                {                    
                    if (drawPolygon.Count()==0)
                    {             
                        nObject.Add(new Shape(StartPosition, StartPosition));
                    }    
                }    
            }
        }
        //thả
        private void openglControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                EndPosition = e.Location;
                IsDrawing = false;
                IsLeftClick = true;
                //Add thêm đỉnh vào polygon
                if(option==8&&drawPolygon!=null)
                {
                    drawPolygon.addControlPoint(EndPosition);
                    nObject[nObject.Count - 1] = drawPolygon;
                }    
            }
           
            else if (e.Button == MouseButtons.Right)
            {
                IsRightClick = true;
                //dừng polygon
                if (option == 8 && drawPolygon != null)
                {
                    drawPolygon = null;
                    option = -1;
                }                
                
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if(option==8 && drawPolygon!=null)
                {
                    drawPolygon.makeClosed();
                    nObject[nObject.Count - 1] = drawPolygon;
                   
                }    
            }
        }
        // di chuyển, Dùng để vẽ như chức năng brush hay pencil của paint+
        private void openglControl1_MouseMove(object sender, MouseEventArgs e)
        {
            OpenGL gl = openglControl1.OpenGL;
            CurPosition = e.Location;
            if (IsDrawing == true && option == 0)
            {

                Line drawline = new Line(StartPosition, CurPosition);
                drawline.Mode(Size, ColorOption);
                nObject.Add(drawline);
                //drawline.Draw(gl);
                StartPosition = CurPosition;
            }
        }
        //Clear màn hình
        private void button_clear_click(object sender, EventArgs e)
        {
            nObject.Clear();
            option = -1;
        }
        //Chọn kích cỡ vẽ
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Size = Convert.ToInt32(numericUpDown1.Value);
        }
        //Nút chọn màu, color diaglog
        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ColorOption = colorDialog1.Color;
            }
        }

        //nút vẽ tự do
        private void button7_Click(object sender, EventArgs e)
        {
            option = 0;
        }
        //nút vẽ Line
        private void button3_Click(object sender, EventArgs e)
        {
            option = 1;
        }


        //Nút vẽ hình tròn
        private void button4_Click(object sender, EventArgs e)
        {
            option = 2;
        }
        //Nút vẽ hình ellipse
        private void button5_Click(object sender, EventArgs e)
        {
            option = 3;
        }

        //Nút vẽ hình chứ nhật
        private void button6_Click(object sender, EventArgs e)
        {
            option = 4;
        }
        //Nút vẽ hình chữ nhật đều ( hình vuông)
        private void button8_Click(object sender, EventArgs e)
        {
            option = 5;
        }

        //Nút vẽ hình ngũ giác đều
        private void button9_Click(object sender, EventArgs e)
        {
            option = 6;
        }
        //Nút vẽ hình lục giác đều
        private void button10_Click(object sender, EventArgs e)
        {
            option = 7;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Vẽ polygon bằng các đỉnh nối nhau
        private void button11_Click(object sender, EventArgs e)
        {
            drawPolygon = new Polygon();
            option = 8;
        }

        private void labelPixelSize_Click(object sender, EventArgs e)
        {

        }

        private void labelPixelSize_Click_1(object sender, EventArgs e)
        {

        }

        //nút resize
        private void button12_Click(object sender, EventArgs e)
        {
            option = 9;
           
        }


    }

}
