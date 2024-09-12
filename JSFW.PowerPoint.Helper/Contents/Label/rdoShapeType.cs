using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PowerPoint.Helper.Contents.Label
{
    public class rdoShapeType : RadioButton
    {
        ShapeType _ShapeType = ShapeType.NoLine;
        public ShapeType ShapeType
        {
            get { return _ShapeType; }
            set
            {
                _ShapeType = value;
                Invalidate();
            }
        }

        float _BorderWeight = 1f;
        public float BorderWeight
        {
            get { return _BorderWeight; }
            set
            {
                _BorderWeight = value;
                if (0f < _BorderWeight)
                {
                    borderPen = new Pen(Color.Black, _BorderWeight);
                }
                else
                {
                    borderPen = new Pen(Color.Black);
                }

                Invalidate();
            }
        }

        public rdoShapeType() : base()
        {            
            Appearance = Appearance.Button;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.CheckedBackColor = Color.AliceBlue;
            AutoSize = false;
            TextAlign = ContentAlignment.MiddleCenter;
            BackColor = Color.Transparent;
        }

        Pen borderPen = new Pen(Color.Black);

        StringFormat stringFormatCenter = new StringFormat() { 
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        };
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); 
            
            switch (ShapeType)
            {
                default:
                case ShapeType.NoLine:
                    Text = "외곽선 없음";
                    Pen p = new Pen(System.Drawing.Pens.LightGray.Color);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawRectangle(p, Rectangle.FromLTRB(0, 0, Width-1, Height-1));
                    break;
                case ShapeType.Box:
                    Text = "사각형";
                    e.Graphics.DrawRectangle(borderPen, Rectangle.FromLTRB(0, 0, Width - 1, Height - 1));
                    break;
                case ShapeType.RoundBox:
                    Text = "둥근 사각형";
                   
                    float x1 = Width / 5;
                    float x2 = Width / 5 * 4;
                    float y1 = Height / 2;
                    float y2 = Height / 2;

                    e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    e.Graphics.DrawLine(borderPen, x1, 0, x2, 0);
                    e.Graphics.DrawLine(borderPen, x1, Height-2, x2, Height-2);
                    e.Graphics.DrawLine(borderPen, 0, y1, 0, y2);
                    e.Graphics.DrawLine(borderPen, Width-2, y1, Width-2, y2);

                    e.Graphics.DrawArc(borderPen, new Rectangle(0, 0, (int)(2* x1), Height-2), 180, 90);
                    e.Graphics.DrawArc(borderPen, new Rectangle(0, 0, (int)(2* x1), Height-2), 90, 90);

                    e.Graphics.DrawArc(borderPen, new Rectangle((int)(x2-x1), 0, (int)(2 * x1)-2, Height - 2), 0, 90);
                    e.Graphics.DrawArc(borderPen, new Rectangle((int)(x2-x1), 0, (int)(2 * x1)-2, Height - 2), 270, 90);
                    break;
            }
        }
    }

    public enum ShapeType { 
        NoLine,
        Box,
        RoundBox,
    }
}
