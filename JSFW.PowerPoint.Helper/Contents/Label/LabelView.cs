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


        public LabelView()
        {
            InitializeComponent();
        }

        internal void SetData(string kor, string eng, string requiredMode, string align, string fontSize)
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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (string.IsNullOrWhiteSpace(Kor)) return;

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
                            //string guid = Guid.NewGuid().ToString("N").Replace("-", "");
                            shape = slide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 0f, 1.6f, PPT_COM_EX.PixelsToPoints(100), PPT_COM_EX.PixelsToPoints(22));
                            //shape.Name = $"{guid}";
                            System.Diagnostics.Debug.WriteLine("Shape.Name=" + shape.Name);
                            shape.Visible = MsoTriState.msoFalse;

                            txtRng = shape.TextFrame.TextRange;
                            txtRng.Text = this.Kor;
                            txtRng.Font.Name = "맑은 고딕";
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

                            string pic = System.IO.Path.GetFullPath(@"Category\Controls\Label.PNG");
                            shape.Fill.UserPicture(pic);

                            DoDragDrop(string.Empty, DragDropEffects.Move);

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
