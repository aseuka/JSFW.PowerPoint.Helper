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
    public partial class ControlViewPanel : UserControl
    {
        public Dictionary<string,List<PPT_ImageInfo>> Data { get; private set; } = new Dictionary<string, List<PPT_ImageInfo>>();

        Dictionary<string, FlowLayoutPanel> CategoryPanel = new Dictionary<string, FlowLayoutPanel>();

        readonly string __FileName = $@"{PPT_COM_EX.ROOT_CATEGORY_DIR}\CV.json";

        public ControlViewPanel()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            btnOK.Left = btnDel.Left;
            btnOK.Top = btnDel.Top;
            btnOK.Width = btnDel.Width;
            btnOK.Height = btnDel.Height;

            btnCancel.Left = btnAdd.Left;
            btnCancel.Top = btnAdd.Top;
            btnCancel.Width = btnAdd.Width;
            btnCancel.Height = btnAdd.Height;

            btnDel.BringToFront();
            btnAdd.BringToFront();

            LoadContents();
        }

        private void LoadContents(bool isReload = false)
        {
            Data.Clear();

            Dictionary<string, List<PPT_ImageInfo>> __pic = Ux.LoadFile<Dictionary<string, List<PPT_ImageInfo>>>(__FileName, Encoding.UTF8);
            if (0 < (__pic?.Count ?? 0))
            {
                foreach (string key in __pic.Keys)
                {
                    if (!Data.ContainsKey(key))
                    {
                        Data.Add(key, new List<PPT_ImageInfo>());
                    }

                    if (0 < __pic[key].Count)
                    {
                        Data[key].AddRange(__pic[key].ToArray());
                    }
                }
                GC.Collect();
            }

            try
            {
                flpCategoryPanel.SuspendLayout();

                if (isReload)
                {
                    while (0 < flpCategoryPanel.Controls.Count)
                    {
                        if (flpCategoryPanel.Controls[0] is FlowLayoutPanel)
                        {
                            while (0 < flpCategoryPanel.Controls[0].Controls.Count)
                            {
                                flpCategoryPanel.Controls[0].Controls[0].Dispose();
                            }
                        }
                        flpCategoryPanel.Controls[0].Dispose();
                    }
                    GC.Collect();
                }

                foreach (string key in Data.Keys)
                {
                    JSFW.PowerPoint.Helper.Controls.Label lbCategoryTitle = GetOrCreateCategoryTitle(key);
                    FlowLayoutPanel flpCategoryPnl = GetOrCreateCategoryPanel(key);

                    foreach (PPT_ImageInfo info in Data[key])
                    {
                        ContentView cv = new ContentView(info);
                        flpCategoryPnl.Controls.Add(cv); 
                    }
                    while (flpCategoryPnl.VerticalScroll.Visible)
                    {
                        int maxTop = 0;
                        foreach (Control ctrl in flpCategoryPnl.Controls)
                        {
                            if (maxTop < ctrl.Top) maxTop = ctrl.Top;
                        }
                        flpCategoryPnl.Height = maxTop + 90;
                    }
                }
            }
            finally
            {
                flpCategoryPanel.ResumeLayout(true);
                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 이미지 등록창!
            using (AddImageFileContentForm fm = new AddImageFileContentForm(Data.Keys.ToArray()))
            {
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    foreach (PPT_ImageInfo info in fm.Pics)
                    {
                        //카피( 원본 -> ./Category/Controls/아래로 복사!
                        //경로를 변경하고 저장!
                        //화면에 디스플레이!! (Thumbnail로 ... )
                        //ContentView로 추가!!! 
                        PPT_ImageInfo _info = info.Clone() as PPT_ImageInfo;
                        _info.Category = fm.Category;
                        _info.CopyTo($@"{PPT_COM_EX.ROOT_CATEGORY_DIR}");

                        if (!Data.ContainsKey(fm.Category))
                        {
                            Data.Add(fm.Category, new List<PPT_ImageInfo>());
                        }
                        Data[fm.Category].Add(_info);

                        //체크박스(버튼)로 만들어서 넣고
                        //아래 flowlayoutpanel을 추가! .. 여기에 카테고리로 등록!
                        JSFW.PowerPoint.Helper.Controls.Label lbCategoryTitle = GetOrCreateCategoryTitle(fm.Category);
                        FlowLayoutPanel flpCategoryPnl = GetOrCreateCategoryPanel(fm.Category);

                        ContentView cv = new ContentView(_info);
                        flpCategoryPnl.Controls.Add(cv);

                        while (flpCategoryPnl.VerticalScroll.Visible)
                        {
                            int maxTop = 0;
                            foreach (Control ctrl in flpCategoryPnl.Controls)
                            {
                                if (maxTop < ctrl.Top) maxTop = ctrl.Top;
                            }
                            flpCategoryPnl.Height = maxTop + 90;
                        }
                    }

                    Ux.SaveFile(Data, __FileName, Encoding.UTF8);
                }
            }
            GC.Collect();
        }

        private JSFW.PowerPoint.Helper.Controls.Label GetOrCreateCategoryTitle(string category)
        {
            JSFW.PowerPoint.Helper.Controls.Label lbCategoryTitle = null;
            
            Control[] ctrls = flpCategoryPanel.Controls.Find($@"chk{category.Replace(" ", "")}", true);

            if (ctrls != null && 0 < ctrls.Length)
            {
                lbCategoryTitle = ctrls[0] as JSFW.PowerPoint.Helper.Controls.Label;
            }
            else
            {
                lbCategoryTitle = new JSFW.PowerPoint.Helper.Controls.Label();
                lbCategoryTitle.Name = $@"chk{category.Replace(" ", "")}";
                lbCategoryTitle.AutoSize = false;
                lbCategoryTitle.Width = flpCategoryPanel.Width - (flpCategoryPanel.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 4) - 8;
                lbCategoryTitle.Height = 22;
                lbCategoryTitle.Font = new Font(lbCategoryTitle.Font, FontStyle.Bold);
                lbCategoryTitle.Text = category;
                lbCategoryTitle.TextAlign = ContentAlignment.MiddleLeft;
                lbCategoryTitle.Padding = new Padding(8, 0, 0, 0);
                lbCategoryTitle.BackColor = Color.LightGray;
                flpCategoryPanel.Controls.Add(lbCategoryTitle);
            }
            return lbCategoryTitle;
        }

        private FlowLayoutPanel GetOrCreateCategoryPanel(string category)
        {
            FlowLayoutPanel flpCategoryPnl = null;

            Control[] ctrls = flpCategoryPanel.Controls.Find($@"flp{category.Replace(" ", "")}", true);

            if (ctrls != null && 0 < ctrls.Length)
            {
                flpCategoryPnl = ctrls[0] as FlowLayoutPanel;
            }
            else
            {
                flpCategoryPnl = new FlowLayoutPanel();
                flpCategoryPnl.Name = $@"flp{category.Replace(" ", "")}";
                flpCategoryPnl.Width = flpCategoryPanel.Width - (flpCategoryPanel.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 4) - 8;
                flpCategoryPnl.Height = 90;
                flpCategoryPnl.AutoScroll = true;
                flpCategoryPnl.WrapContents = true;
                flpCategoryPnl.FlowDirection = FlowDirection.LeftToRight;
                flpCategoryPnl.BorderStyle = BorderStyle.FixedSingle;                
                flpCategoryPanel.Controls.Add(flpCategoryPnl);
            }
            return flpCategoryPnl;
        }
         
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadContents(true);
        }

        private void flpCategoryPanel_Resize(object sender, EventArgs e)
        {
            try
            {
                flpCategoryPanel.SuspendLayout();

                foreach (Control ctrl in flpCategoryPanel.Controls)
                {
                    ctrl.Width = flpCategoryPanel.Width - (flpCategoryPanel.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 4) - 8;
                    ctrl.Refresh();

                    FlpCategoryPnl_Resize(ctrl, e);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{ex}");
            }
            finally {
                flpCategoryPanel.ResumeLayout(true); 
            }
        }

        bool isResizeEvent = false;
        private void FlpCategoryPnl_Resize(object sender, EventArgs e)
        {
            if (isResizeEvent) return;

            FlowLayoutPanel flpCategoryPnl = sender as FlowLayoutPanel;
            if (flpCategoryPnl != null)
            {
                try
                {
                    isResizeEvent = true;
                    flpCategoryPnl.SuspendLayout();
                    int maxTop = 0;
                    flpCategoryPnl.Height = 90;
                    foreach (Control innerCtrl in flpCategoryPnl.Controls)
                    {
                        if (maxTop < innerCtrl.Top) maxTop = innerCtrl.Top;
                    }
                    flpCategoryPnl.Height = maxTop + 90;
                }
                finally
                {
                    flpCategoryPnl.ResumeLayout(true);
                    isResizeEvent = false;
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //삭제
            lbDel.Visible = true;
            btnOK.BringToFront();
            btnCancel.BringToFront();

            foreach (Control ctrl in flpCategoryPanel.Controls)
            {
                if (ctrl is FlowLayoutPanel)
                {
                    foreach (ContentView cv in ctrl.Controls)
                    {
                        cv.ToggleSelectChecking(true);
                    }   
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            lbDel.Visible = false;
         
            List<ContentView> delCV = new List<ContentView>();

            try
            {
                flpCategoryPanel.SuspendLayout();
                foreach (Control ctrl in flpCategoryPanel.Controls)
                {
                    if (ctrl is FlowLayoutPanel)
                    {
                        foreach (ContentView cv in ctrl.Controls)
                        {
                            if (cv.IsSelected)
                            {
                                delCV.Add(cv);
                            }
                        }
                    }
                }

                if (0 < delCV.Count)
                {
                    foreach (ContentView cv in delCV)
                    {
                        PPT_ImageInfo info = cv.Info;
                        Data[info.Category].Remove(info);
                        cv.Dispose();
                        File.Delete(info.Path);
                    }

                    for (int loop = Data.Keys.Count - 1; loop >= 0; loop--)
                    {
                        //비어있는 컨텐츠 카테고리 삭제!
                        string key = Data.Keys.ElementAt(loop);
                        if (Data[key].Count <= 0)
                        {
                            Data.Remove(key);
                            if (Directory.Exists($@"{PPT_COM_EX.ROOT_CATEGORY_DIR}\{key.Replace(" ", "")}"))
                            {
                                Directory.Delete($@"{PPT_COM_EX.ROOT_CATEGORY_DIR}\{key.Replace(" ", "")}");
                            }
                        }
                    }
                    Ux.SaveFile(Data, __FileName, Encoding.UTF8);
                }
            }
            finally {
                flpCategoryPanel.ResumeLayout(true);
            }

            LoadContents(true);

            btnDel.BringToFront();
            btnAdd.BringToFront();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lbDel.Visible = false;
            
            foreach (Control ctrl in flpCategoryPanel.Controls)
            {
                if (ctrl is FlowLayoutPanel)
                {
                    foreach (ContentView cv in ctrl.Controls)
                    {
                        cv.ToggleSelectChecking(false);
                    }
                }
            }

            btnDel.BringToFront();
            btnAdd.BringToFront();
        }
    }

    public class PPT_ImageInfo : ICloneable
    { 
        /// <summary>
        /// 카테고리
        /// </summary>
        public string Category { get; set; }

        public string Path { get; set; }
    
        public int Width { get; set; }

        public int Height { get; set; }

        /// <summary>
        /// 화면에 추가된 아이콘 필터링을 위해!!
        /// </summary>
        internal bool IsControlBinded { get; set; } = false;

        public object Clone()
        {
            PPT_ImageInfo info = new PPT_ImageInfo();
            info.Path = Path;
            info.Width = Width;
            info.Height = Height;
            return info;
        }

        internal void CopyTo(string destDir)
        {
            //파일을 destDir 복사!
            if (string.IsNullOrWhiteSpace($@"{destDir}\{Category}")) throw new Exception("복사 대상 경로명이 비었습니다.");

            if (System.IO.Directory.Exists($@"{destDir}\{Category}") == false)
            {
                System.IO.Directory.CreateDirectory($@"{destDir}\{Category}");
            }

            string fileName = System.IO.Path.GetFileName(this.Path);

            string destFilePath = $@"{destDir}\{Category}\{fileName}";

            if (System.IO.File.Exists(destFilePath))
            {
                string ext = System.IO.Path.GetExtension(this.Path);
                fileName = System.IO.Path.GetFileNameWithoutExtension(this.Path);

                int count = 1;
                do
                {
                    destFilePath = $@"{destDir}\{Category}\{fileName}({count++:D3}){ext}";
                } while (System.IO.File.Exists(destFilePath));
            }
            System.IO.File.Copy(this.Path, destFilePath);
            this.Path = destFilePath;
        }
    }
}
