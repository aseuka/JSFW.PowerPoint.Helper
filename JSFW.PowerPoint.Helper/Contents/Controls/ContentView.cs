using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PowerPoint.Helper.Contents.Controls
{
    //이미지
    //체크박스 표시(삭제 선택용)    
    public partial class ContentView : UserControl
    {
        public PPT_ImageInfo Info { get; private set; } = null;

        protected ContentView()
        {
            InitializeComponent();
        }

        public ContentView(PPT_ImageInfo info) : this()
        {
            Info = info;
            SetBackgroundImage();
            this.Disposed += ContentView_Disposed;

            ToggleSelectChecking(false);
        }

        private void ContentView_Disposed(object sender, EventArgs e)
        {
            this.BackgroundImage?.Dispose();
            this.BackgroundImage = null;

            Info = null;
        }

        public void SetBackgroundImage()
        {
            this.BackgroundImage?.Dispose();
            this.BackgroundImage = null;

            this.BackgroundImageLayout = ImageLayout.Stretch;
            using (Image img = Bitmap.FromFile(Info.Path))
            { 
                this.BackgroundImage = img.Clone() as Image;
            }

            if (120 <= Info.Width)
            {
                this.Width *= 2;
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
                int x = e.X - pt.X;
                int y = e.Y - pt.Y;
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
                            shape = slide.Shapes.AddShape(MsoAutoShapeType.msoShapeRoundedRectangle, 0f, 1.6f, PPT_COM_EX.PixelsToPoints(Info.Width), PPT_COM_EX.PixelsToPoints(Info.Height));
                            shape.Visible = MsoTriState.msoFalse;

                            shape.Adjustments[1] = 0.02f;// 0.5는 엄청 둥그렇게 나옴... 0.16 이 기본값! 둥근상자 초기값.
                            shape.Fill.Visible = MsoTriState.msoFalse; // 배경색 없음. 
                            shape.Line.Visible = MsoTriState.msoFalse; // 외곽선 없음.

                            shape.TextFrame.TextRange.Font.Color.RGB = ColorTranslator.ToOle(Color.FromArgb(10, Color.Red)); // 글자색

                            //shape.Name = $"{guid}";
                            Debug.WriteLine("Shape.Name=" + shape.Name);

                            txtRng = shape.TextFrame.TextRange;
                            txtRng.Text = "";
                            txtRng.Font.Name = "맑은 고딕";
                            txtRng.Font.Size = 9f;
                            txtRng.ParagraphFormat.Alignment = PpParagraphAlignment.ppAlignCenter;

                            string pic = System.IO.Path.GetFullPath(Info.Path);
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
                            hasException = true;
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

        System.Drawing.Font ft;
        private void ContentView_Paint(object sender, PaintEventArgs e)
        {
            if (ft != null)
            {
                ft = new System.Drawing.Font(this.Font, FontStyle.Bold);
            }
            TextRenderer.DrawText(e.Graphics, $"{Info.Width}x{Info.Height}", ft, Rectangle.FromLTRB(0, this.Height - 20, this.Width, this.Height), Color.Black, TextFormatFlags.HorizontalCenter);
        }

        //삭제처리 관련...

        public void ToggleSelectChecking(bool isDelete = false)
        {
            checkBox1.Checked = false;
            checkBox1.Visible = isDelete;
        }

        public bool IsSelected
        {
            get { return checkBox1.Checked; }
        }
    }
}
