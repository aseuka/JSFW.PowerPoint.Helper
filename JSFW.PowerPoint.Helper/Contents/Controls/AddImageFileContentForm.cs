using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PowerPoint.Helper.Contents.Controls
{
    public partial class AddImageFileContentForm : Form
    {
        public string Category { get; private set; } = "";

        public List<PPT_ImageInfo> Pics { get; private set; } = new List<PPT_ImageInfo>();

        ContentThumbnailControl CurrentThumb = null;

        private AddImageFileContentForm()
        {
            InitializeComponent();
        }

        public AddImageFileContentForm(string[] categories) : this()
        {
            cboCategory.Items.AddRange(categories);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SetCurrentThumb(null);            
            base.OnFormClosed(e);            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Category = "";
            Category = cboCategory.Text.Trim();

            foreach (char ch in Path.GetInvalidPathChars())//디렉토리명에 사용못하는 문자 제거
            {
                Category = Category.Replace(ch.ToString(), "");
            }

            if (string.IsNullOrWhiteSpace(Category))
            {
                MessageBox.Show("카테고리를 입력하세요.");
                cboCategory.Focus();
                return;
            }

            if (panel1.Controls.Count <= 0)
            {
                MessageBox.Show("등록할 이미지가 없습니다.");
                return;
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Category = "";
            this.Close();
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        { 
            if (e.AllowedEffect == (DragDropEffects.Copy| DragDropEffects.Move| DragDropEffects.Link))
            {
                string[] fmts = e.Data.GetFormats();
                string[] linkFiles = e.Data.GetData("FileDrop") as string[];

                if (linkFiles != null && 0 < linkFiles.Length)
                {
                    e.Effect = e.AllowedEffect;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == (DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link))
            {
                string[] linkFiles = e.Data.GetData("FileDrop") as string[];

                try
                {
                    panel1.SuspendLayout();
                    SetCurrentThumb(null);
                    while ( 0 < panel1.Controls.Count )
                    {
                        panel1.Controls[0].MouseDown -= Ctrl_MouseDown;
                        panel1.Controls.RemoveAt(0);
                    }
                }
                finally
                {
                    panel1.ResumeLayout(false);
                }
                Pics.Clear();

                foreach (var file in linkFiles)
                {
                    string ext = Path.GetExtension(file);

                    //System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().Select( s=> s.FilenameExtension)
                    if (!".bmp;.gif;.jpg;.jpeg;.png;".Contains(ext.ToLower())) continue;

                    //파일 이미지!!
                    Pics.Add(new PPT_ImageInfo() { Path = file, Width = 120, Height = 22 });
                }

                if (0 < Pics.Count)
                {
                    int y = 0;
                    foreach (var pic in Pics)
                    {
                        ContentThumbnailControl ctrl = new ContentThumbnailControl(pic);
                        if (panel1.Height <= (y + ctrl.Height + 3))
                        {
                            ctrl.Dispose();
                            break;
                        }
                        panel1.Controls.Add(ctrl);
                        pic.IsControlBinded = true; // 화면에 추가된 아이콘 필터링을 위해!! 
                        ctrl.MouseDown += Ctrl_MouseDown;
                        ctrl.Top = y;
                        y += ctrl.Height + 10;
                        ctrl.Refresh();

                    }

                    for (int loop = Pics.Count - 1; loop >= 0; loop--)
                    {
                        if (Pics[loop].IsControlBinded == false) Pics.RemoveAt(loop);
                    }

                    if (0 < panel1.Controls.Count)
                    {
                        SetCurrentThumb(panel1.Controls[0] as ContentThumbnailControl);
                    }
                }
            }
        }

        private void Ctrl_MouseDown(object sender, MouseEventArgs e)
        {
            SetCurrentThumb(sender as ContentThumbnailControl);
        }

        private void SetCurrentThumb(ContentThumbnailControl ctrl = null)
        {
            if (CurrentThumb != null)
            {
                CurrentThumb.BorderStyle = BorderStyle.None;
            }
            
            CurrentThumb = ctrl;

            if (CurrentThumb != null)
            {
                CurrentThumb.BorderStyle = BorderStyle.FixedSingle;
                Invalidate();
            }
        }

        private void btnReSize_Click(object sender, EventArgs e)
        {
            if (CurrentThumb == null) return;

            Control btn = sender as Control;
            int sz = Convert.ToInt32(btn.Tag ?? "0");
            switch (sz)
            {
                default:break;
                case 16:
                case 24:
                case 48:
                case 64:
                case 128:
                    CurrentThumb.Width = sz;
                    CurrentThumb.Height = sz;
                    break;
                case 22:
                case 40:
                case 80:
                case 100:
                case 120:
                case 140:
                case 160:
                    CurrentThumb.Width = sz;
                    CurrentThumb.Height = 22;
                    break;
            }
        }

        System.Drawing.Font ft;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentThumb != null)
            {
                if (ft != null)
                {
                    ft = new System.Drawing.Font(this.Font, FontStyle.Bold);
                }
                TextRenderer.DrawText(e.Graphics, $"{CurrentThumb.ImageInfo.Width}x{CurrentThumb.ImageInfo.Height}", ft, new Point( CurrentThumb.Right + 10, CurrentThumb.Top + 2), Color.Black);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //키보드로 사이즈 조정!! 
            if (CurrentThumb != null && !cboCategory.Focused)
            {
                switch (keyData)
                {             
                    case Keys.Up:
                        CurrentThumb.Height--;
                        break;
                    case Keys.Down:
                        CurrentThumb.Height++;
                        break;
                    case Keys.Left:
                        CurrentThumb.Width--;
                        break;
                    case Keys.Right:
                        CurrentThumb.Width++;
                        break;
                }
                panel1.Invalidate();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }


}
