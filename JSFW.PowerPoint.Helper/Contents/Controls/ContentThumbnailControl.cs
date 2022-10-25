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
    //a. 사이즈 조절
    //b. ppt로 드래그!! 
    public partial class ContentThumbnailControl : UserControl
    {
        public PPT_ImageInfo ImageInfo { get; private set; } = null;

        /*컨트롤 사이즈 변경*/
        bool IsRS = false;
        Rectangle RSZ;
        readonly int OFFSET = 8;
        
        protected ContentThumbnailControl()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 기본 모양 사이즈 기준 : 200
        /// </summary>
        float DEFAULT_SHAPE_SIZE = 200f;
        public ContentThumbnailControl(PPT_ImageInfo info) : this()
        {
            ImageInfo = info;
            SetBackgroundImage();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (ImageInfo != null)
            {
                ImageInfo.Width = this.Width;
                ImageInfo.Height = this.Height;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);            

            this.Disposed += ContentThumnailControl_Disposed;
        }

        public void SetBackgroundImage()
        {
            this.BackgroundImage?.Dispose();
            this.BackgroundImage = null;

            this.BackgroundImageLayout = ImageLayout.Stretch;
            using (Image img = Bitmap.FromFile(ImageInfo.Path))
            {
                if (DEFAULT_SHAPE_SIZE < img.Width || DEFAULT_SHAPE_SIZE < img.Height)
                {
                    this.Width = (int)((DEFAULT_SHAPE_SIZE / 2f) * (img.Width / DEFAULT_SHAPE_SIZE));
                    this.Height = (int)((DEFAULT_SHAPE_SIZE / 2f) * (img.Height / DEFAULT_SHAPE_SIZE));
                }
                else
                {
                    this.Width = img.Width;
                    this.Height = img.Height;
                }
                this.BackgroundImage = img.Clone() as Image;
            }
        }

        private void ContentThumnailControl_Disposed(object sender, EventArgs e)
        {
            BackgroundImage?.Dispose();
            ImageInfo = null;
            BackgroundImage = null;
        }

        bool isMouseDown = false;
        System.Drawing.Point pt;
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsRS = false;
            isMouseDown = false;
            Cursor = Cursors.Default;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isMouseDown)
            {
                int x = e.X - pt.X;
                int y = e.Y - pt.Y;
                int z = (int)Math.Sqrt(Math.Pow((double)Math.Abs(x), 2d) + Math.Pow((double)Math.Abs(y), 2d));

                if (!IsRS && 4 < z)
                {
                    bool hasException = false;
                    PPT_COM_EX.PassTheCreatedSlide(out hasException, (app, slide) =>
                    {
                        Microsoft.Office.Interop.PowerPoint.Shape shape = null; // 텍스트 오브젝트
                        TextRange txtRng = null;  // 텍스트 오브젝트에 속해있는 텍스트

                        try
                        {
                            //string guid = Guid.NewGuid().ToString("N").Replace("-", "");
                            shape = slide.Shapes.AddShape(MsoAutoShapeType.msoShapeRoundedRectangle, 0f, 1.6f, PPT_COM_EX.PixelsToPoints(this.Width), PPT_COM_EX.PixelsToPoints(this.Height));
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

                            string pic = System.IO.Path.GetFullPath(ImageInfo.Path);
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
                else if (IsRS)
                {
                    int w = this.Width + x;
                    int h = this.Height + y;
                    if (this.Width + x < 16)
                    {
                        w = 16;
                    }
                    if (this.Height + y < 16)
                    {
                        h = 16;
                    }
                    this.Width = w;
                    this.Height = h;
                    pt = e.Location;

                    Parent.Refresh();
                }
            }
            else
            {
                ReCalcBox(this as Control);

                if (RSZ.Contains(e.Location))
                {
                    Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    Cursor = Cursors.Default;   
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isMouseDown = e.Button == MouseButtons.Left;
            pt = e.Location;

            if (isMouseDown)
            {
                ReCalcBox(this as Control);

                if (RSZ.Contains(e.Location))
                {
                    IsRS = true;
                    Cursor = Cursors.SizeNWSE;
                    return;
                }
            }
            Cursor = Cursors.Default; 
        }

        private void ReCalcBox(Control sender = null)
        { 
            if (sender != null)
            {
                RSZ.X = sender.Width - OFFSET;
                RSZ.Y = sender.Height - OFFSET;
                RSZ.Width = OFFSET;
                RSZ.Height = OFFSET;
            }
            else
            {
                RSZ.X = -OFFSET;
                RSZ.Y = -OFFSET;
                RSZ.Width = OFFSET;
                RSZ.Height = OFFSET;
            }
        }

        System.Drawing.Font ft;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (ft != null)
            {
                ft = new System.Drawing.Font(this.Font, FontStyle.Bold);
            }
            TextRenderer.DrawText(e.Graphics, $"{ImageInfo.Width}x{ImageInfo.Height}", ft, Rectangle.FromLTRB(0, this.Height - 20, this.Width, this.Height), Color.Black, TextFormatFlags.HorizontalCenter);
        } 
    }
}
