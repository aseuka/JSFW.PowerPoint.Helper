using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PowerPoint.Helper.Contents.Label
{
    public partial class LabelView : UserControl
    {
        public string Kor { get; private set; }

        public string Eng { get; private set; }

        public string RequiredMode { get; private set; } = "";

        public string StringAlign { get; private set; } = "Left";

        public string FontSize { get; private set; } = "9";

        public ShapeType ShapeType { get; set; } = ShapeType.NoLine;

        public float Weight { get; set; } = 1f;


        public LabelView()
        {
            InitializeComponent();
        }

        internal void SetData(string kor, string eng, string requiredMode, string align, string fontSize, ShapeType shapeType, float weight)
        {
            Kor = kor;
            Eng = eng;
            RequiredMode = requiredMode;
            if (RequiredMode == "_")
            {
                Font = new System.Drawing.Font(Font, FontStyle.Underline);
                Kor = kor.TrimStart('*');
            }

            StringAlign = align;
            FontSize = fontSize;
            ShapeType = shapeType;
            Weight = weight;

            if (0f < Weight && ShapeType != ShapeType.NoLine)
            {
                borderPen = new Pen(Color.Black, Weight);
            }
            else
            {
                borderPen = new Pen(Color.Black);
            }
            Invalidate();
        }

        Pen borderPen = new Pen(Color.Black);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (string.IsNullOrWhiteSpace(Kor) && string.IsNullOrWhiteSpace(Eng)) return;

            if (string.IsNullOrWhiteSpace(Kor) && string.IsNullOrWhiteSpace(Eng) == false) {
                Kor = Eng;
            }

            switch (ShapeType)
            {
                default:
                case ShapeType.NoLine:
                    Pen p = new Pen(System.Drawing.Pens.LightGray.Color);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawRectangle(p, Rectangle.FromLTRB(0, 0, Width - 1, Height - 1));
                    break;
                case ShapeType.Box:
                    e.Graphics.DrawRectangle(borderPen, Rectangle.FromLTRB(0, 0, Width - 1, Height - 1));
                    break;
                case ShapeType.RoundBox:
                    //GetRoundImage();
                    //if (roundBox != null) {
                    //    e.Graphics.DrawImage(roundBox, 0, 0, Width, Height);
                    //}

                    float x1 = Width / 5;
                    float x2 = Width / 5 * 4;
                    float y1 = Height / 2;
                    float y2 = Height / 2;

                    e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    e.Graphics.DrawLine(borderPen, x1, 0, x2, 0);
                    e.Graphics.DrawLine(borderPen, x1, Height - 2, x2, Height - 2);
                    e.Graphics.DrawLine(borderPen, 0, y1, 0, y2);
                    e.Graphics.DrawLine(borderPen, Width - 2, y1, Width - 2, y2);

                    e.Graphics.DrawArc(borderPen, new Rectangle(0, 0, (int)(2 * x1), Height - 2), 180, 90);
                    e.Graphics.DrawArc(borderPen, new Rectangle(0, 0, (int)(2 * x1), Height - 2), 90, 90);

                    e.Graphics.DrawArc(borderPen, new Rectangle((int)(x2 - x1), 0, (int)(2 * x1) - 2, Height - 2), 0, 90);
                    e.Graphics.DrawArc(borderPen, new Rectangle((int)(x2 - x1), 0, (int)(2 * x1) - 2, Height - 2), 270, 90);
                    break;
            }



            SizeF sz = TextRenderer.MeasureText(Kor, Font);
            int startPointX = (int)(Width / 2f - sz.Width / 2f);
            int startPointY = (int)(Height / 2f - sz.Height / 2f);

            if (StringAlign == "Center")
            {
                startPointX = (int)(Width / 2f - sz.Width / 2f);
            }
            else if (StringAlign == "Right")
            {
                startPointX = (int)( Width - sz.Width - 5f );
            }
            else
            {
                startPointX = 5;
            }
             
            TextRenderer.DrawText(e.Graphics, Kor, Font, new System.Drawing.Point(startPointX, startPointY), Color.Black);

            if (Kor.StartsWith("*") && RequiredMode == "*")
            {                
                TextRenderer.DrawText(e.Graphics, "*", Font, new System.Drawing.Point(startPointX, startPointY), Color.Red);
            }

            
        }


        bool isMouseDown = false;
        System.Drawing.Point pt;
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isMouseDown = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isMouseDown)
            {
                int x = e.Location.X - pt.X;
                int y = e.Location.Y - pt.Y;
                int z = (int)Math.Sqrt(Math.Pow((double)Math.Abs(x), 2d) + Math.Pow((double)Math.Abs(y), 2d));

                if (4 < z)
                {
                    bool hasException = false;
                    PPT_COM_EX.PassTheCreatedSlide(out hasException, (app, slide) =>
                    {
                        Microsoft.Office.Interop.PowerPoint.Shape shape = null; // 텍스트 오브젝트
                        TextRange txtRng = null;  // 텍스트 오브젝트에 속해있는 텍스트

                        try
                        {
                            //https://learn.microsoft.com/en-us/office/vba/api/Office.MsoAutoShapeType
                            //모양!!!
                            //string guid = Guid.NewGuid().ToString("N").Replace("-", "");
                            switch (ShapeType)
                            {
                                default:
                                case ShapeType.NoLine:
                                    shape = slide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 0f, 1.6f, PPT_COM_EX.PixelsToPoints(100), PPT_COM_EX.PixelsToPoints(22));
                                    shape.Line.Visible = MsoTriState.msoFalse;                                    
                                    break;
                                case ShapeType.Box:
                                    shape = slide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 0f, 1.6f, PPT_COM_EX.PixelsToPoints(100), PPT_COM_EX.PixelsToPoints(22));
                                    shape.Line.Visible = MsoTriState.msoTrue;
                                    shape.Line.Weight = Weight;
                                    break;
                                case ShapeType.RoundBox:
                                    shape = slide.Shapes.AddShape(MsoAutoShapeType.msoShapeFlowchartTerminator, 0f, 1.6f, PPT_COM_EX.PixelsToPoints(100), PPT_COM_EX.PixelsToPoints(22));
                                    shape.Line.Weight = Weight;
                                    shape.Line.Visible = MsoTriState.msoTrue;
                                    shape.Fill.Visible = MsoTriState.msoFalse;
                                    break;                             
                            }
                            //shape.Name = $"{guid}";
                            System.Diagnostics.Debug.WriteLine("Shape.Name=" + shape.Name);
                            shape.Visible = MsoTriState.msoFalse;

                            string pic = System.IO.Path.GetFullPath(@"Category\Controls\Label.PNG");
                            shape.Fill.UserPicture(pic);


                            txtRng = shape.TextFrame.TextRange;
                            txtRng.Text = this.Kor;
                            txtRng.Font.Name = "맑은 고딕";
                            txtRng.Font.Color.RGB = ColorTranslator.ToOle(Color.FromArgb(255, Color.Black)); // 글자색
                            txtRng.Font.Size = Convert.ToInt32(this.FontSize);

                            if (this.StringAlign == "Center")
                            {
                                txtRng.ParagraphFormat.Alignment = PpParagraphAlignment.ppAlignCenter;
                            }
                            else if (this.StringAlign == "Right")
                            {
                                txtRng.ParagraphFormat.Alignment = PpParagraphAlignment.ppAlignRight;
                            }
                            else
                            {
                                txtRng.ParagraphFormat.Alignment = PpParagraphAlignment.ppAlignLeft;
                            }

                            if (txtRng.Text.StartsWith("*"))
                            {
                                if (this.RequiredMode == "*")
                                {
                                    // * ( 필수 ) 빨간색 
                                    txtRng.Characters(1, 1).Font.Color.RGB = ColorTranslator.ToOle(Color.Red);
                                }
                                else if (this.RequiredMode == "_")
                                {
                                    txtRng.Text = this.Kor.TrimStart('*');
                                    txtRng.Font.Underline = MsoTriState.msoTrue;
                                }
                                else
                                {
                                    txtRng.Text = this.Kor.TrimStart('*');
                                }
                            }
                          
                             
                            DoDragDrop(shape, DragDropEffects.Move);

                            var point = PPT_COM_EX.GetCursorPosition(app.ActiveWindow.HWND);
                            var convertedPoint = PPT_COM_EX.ConvertScreenPointToSlideCoordinates(point, app);

                            var slideWidth = slide.CustomLayout.Width;
                            var slideHeight = slide.CustomLayout.Height;

                            RectangleF slideRect = new RectangleF(0, 0, slideWidth + shape.Width, slideHeight + shape.Height);
                            RectangleF shapeRect = new RectangleF(convertedPoint.X, convertedPoint.Y, shape.Width, shape.Height);

                            if (slideRect.Contains(shapeRect))
                            {
                                shape.Visible = MsoTriState.msoTrue;
                                shape.Left = convertedPoint.X - shape.Left;
                                shape.Top = convertedPoint.Y - shape.Top;
                                shape.Select(MsoTriState.msoTrue);
                            }
                            else
                            {
                                shape.Delete();
                            }
                        }
                        catch (Exception ex)
                        {
                            shape?.Delete();
                            throw ex;
                        }
                        finally
                        {
                            PPT_COM_EX.ReleaseComObject(txtRng);
                            PPT_COM_EX.ReleaseComObject(shape);
                            shape = null;
                            txtRng = null;
                            app = null;
                            isMouseDown = false;
                        }
                    });

                    if (hasException)
                    {
                        isMouseDown = false;
                    }
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isMouseDown = e.Button == MouseButtons.Left;
            pt = e.Location;
        }
    }
}
