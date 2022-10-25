using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using System.Collections;
using System.Runtime.InteropServices;

namespace JSFW.PowerPoint.Helper.Contents.Label
{
    public partial class LabelViewsPanel : UserControl
    {
        CallToDelayOnTriggerClass CallByDelayOnTrigger = new CallToDelayOnTriggerClass();

        public LabelViewsPanel()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            isAppling = false;
            Clear();
        }

        bool isAppling = false; 
        private void Clear()
        { 
            //적용된 내용 클리어!
            LabelViewsClear();

            if(string.IsNullOrWhiteSpace( textBox1.Text.Trim())) return;

            CallByDelayOnTrigger.CallBy(ApplyTextToLabelViews); 
        }

        private void ApplyTextToLabelViews()
        {
            //*비동기!! 별도 쓰레드 구역임. 
            isAppling = true;

            string[] labels = textBox1.Text.Trim().Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            flowLayoutPanel1.Sync(f => 
            {
                try
                {
                    f.SuspendLayout();
                    for (int loop = 0; isAppling && loop < labels.Length; loop++)
                    {
                        string kor = labels[loop].Trim();

                        if (string.IsNullOrWhiteSpace(kor)) continue;

                        string eng = "";
                        if (kor.Contains(":"))
                        {
                            string[] korEng = kor.Split(':');
                            kor = korEng[0];
                            eng = korEng[1];
                        }
                        LabelView lv = new LabelView();
                        string requiredMode = "";
                        if (rdoRequiredStar.Checked)
                        {
                            requiredMode = "*";
                        }
                        else if (rdoRequiredUnderLine.Checked)
                        {
                            requiredMode = "_";
                        }

                        string align = "";
                        if (rdoAlignRight.Checked)
                        {
                            align = "Right";
                        }
                        else if (rdoAlignCenter.Checked)
                        {
                            align = "Center";
                        }
                        else
                        {
                            align = "Left";
                        }

                        string fontSize = "9";
                        if (0 <= cboFontSize.SelectedIndex)
                        {
                            fontSize = cboFontSize.Text;
                        }

                        lv.SetData(kor, eng, requiredMode, align, fontSize);                       
                        f.Controls.Add(lv);
                    }
                }
                finally
                {
                    f.ResumeLayout();
                }
            });
            isAppling = false;
        }

        private void LabelViewsClear()
        {
            if (0 < flowLayoutPanel1.Controls.Count)
            {
                try
                {
                    flowLayoutPanel1.SuspendLayout();

                    for (int loop = flowLayoutPanel1.Controls.Count - 1; loop >= 0; loop--)
                    {
                        flowLayoutPanel1.Controls[loop]?.Dispose();
                        Debug.WriteLine("Lv Dispose()");
                    }
                }
                finally
                {
                    flowLayoutPanel1.ResumeLayout(false);
                }
            }
        }
         
        private void rdoRequiredView_CheckedChanged(object sender, EventArgs e)
        {
            isAppling = false;
            Clear();
        }

        private void rdoAlign_CheckedChanged(object sender, EventArgs e)
        {
            isAppling = false;
            Clear();
        }
         
        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAppling = false;
            Clear();
        } 
    }
}
