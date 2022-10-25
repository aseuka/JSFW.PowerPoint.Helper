
namespace JSFW.PowerPoint.Helper.Contents.Label
{
    partial class LabelViewsPanel
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelSettingPanel1 = new System.Windows.Forms.Panel();
            this.cboFontSize = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdoAlignRight = new System.Windows.Forms.RadioButton();
            this.rdoAlignCenter = new System.Windows.Forms.RadioButton();
            this.rdoAlignLeft = new System.Windows.Forms.RadioButton();
            this.label1 = new JSFW.PowerPoint.Helper.Controls.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdoRequiredUnderLine = new System.Windows.Forms.RadioButton();
            this.rdoRequiredStar = new System.Windows.Forms.RadioButton();
            this.rdoRequiredNone = new System.Windows.Forms.RadioButton();
            this.label3 = new JSFW.PowerPoint.Helper.Controls.Label();
            this.label2 = new JSFW.PowerPoint.Helper.Controls.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelSettingPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSettingPanel1
            // 
            this.labelSettingPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelSettingPanel1.Controls.Add(this.cboFontSize);
            this.labelSettingPanel1.Controls.Add(this.panel3);
            this.labelSettingPanel1.Controls.Add(this.label1);
            this.labelSettingPanel1.Controls.Add(this.panel2);
            this.labelSettingPanel1.Controls.Add(this.label3);
            this.labelSettingPanel1.Controls.Add(this.label2);
            this.labelSettingPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSettingPanel1.Location = new System.Drawing.Point(0, 0);
            this.labelSettingPanel1.Name = "labelSettingPanel1";
            this.labelSettingPanel1.Size = new System.Drawing.Size(581, 61);
            this.labelSettingPanel1.TabIndex = 1;
            // 
            // cboFontSize
            // 
            this.cboFontSize.FormattingEnabled = true;
            this.cboFontSize.Items.AddRange(new object[] {
            "7",
            "9",
            "10",
            "11",
            "12",
            "16",
            "18",
            "24"});
            this.cboFontSize.Location = new System.Drawing.Point(463, 34);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(59, 20);
            this.cboFontSize.TabIndex = 5;
            this.cboFontSize.Text = "9";
            this.cboFontSize.SelectedIndexChanged += new System.EventHandler(this.cboFontSize_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdoAlignRight);
            this.panel3.Controls.Add(this.rdoAlignCenter);
            this.panel3.Controls.Add(this.rdoAlignLeft);
            this.panel3.Location = new System.Drawing.Point(108, 31);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(243, 28);
            this.panel3.TabIndex = 6;
            // 
            // rdoAlignRight
            // 
            this.rdoAlignRight.AutoSize = true;
            this.rdoAlignRight.Location = new System.Drawing.Point(151, 6);
            this.rdoAlignRight.Name = "rdoAlignRight";
            this.rdoAlignRight.Size = new System.Drawing.Size(59, 16);
            this.rdoAlignRight.TabIndex = 5;
            this.rdoAlignRight.Text = "오른쪽";
            this.rdoAlignRight.UseVisualStyleBackColor = true;
            this.rdoAlignRight.CheckedChanged += new System.EventHandler(this.rdoAlign_CheckedChanged);
            // 
            // rdoAlignCenter
            // 
            this.rdoAlignCenter.AutoSize = true;
            this.rdoAlignCenter.Checked = true;
            this.rdoAlignCenter.Location = new System.Drawing.Point(77, 6);
            this.rdoAlignCenter.Name = "rdoAlignCenter";
            this.rdoAlignCenter.Size = new System.Drawing.Size(59, 16);
            this.rdoAlignCenter.TabIndex = 5;
            this.rdoAlignCenter.TabStop = true;
            this.rdoAlignCenter.Text = "가운데";
            this.rdoAlignCenter.UseVisualStyleBackColor = true;
            this.rdoAlignCenter.CheckedChanged += new System.EventHandler(this.rdoAlign_CheckedChanged);
            // 
            // rdoAlignLeft
            // 
            this.rdoAlignLeft.AutoSize = true;
            this.rdoAlignLeft.Location = new System.Drawing.Point(12, 6);
            this.rdoAlignLeft.Name = "rdoAlignLeft";
            this.rdoAlignLeft.Size = new System.Drawing.Size(47, 16);
            this.rdoAlignLeft.TabIndex = 5;
            this.rdoAlignLeft.Text = "왼쪽";
            this.rdoAlignLeft.UseVisualStyleBackColor = true;
            this.rdoAlignLeft.CheckedChanged += new System.EventHandler(this.rdoAlign_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(357, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Font Size";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdoRequiredUnderLine);
            this.panel2.Controls.Add(this.rdoRequiredStar);
            this.panel2.Controls.Add(this.rdoRequiredNone);
            this.panel2.Location = new System.Drawing.Point(108, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(243, 28);
            this.panel2.TabIndex = 6;
            // 
            // rdoRequiredUnderLine
            // 
            this.rdoRequiredUnderLine.AutoSize = true;
            this.rdoRequiredUnderLine.Location = new System.Drawing.Point(163, 6);
            this.rdoRequiredUnderLine.Name = "rdoRequiredUnderLine";
            this.rdoRequiredUnderLine.Size = new System.Drawing.Size(75, 16);
            this.rdoRequiredUnderLine.TabIndex = 5;
            this.rdoRequiredUnderLine.Text = "밑줄 적용";
            this.rdoRequiredUnderLine.UseVisualStyleBackColor = true;
            this.rdoRequiredUnderLine.CheckedChanged += new System.EventHandler(this.rdoRequiredView_CheckedChanged);
            // 
            // rdoRequiredStar
            // 
            this.rdoRequiredStar.AutoSize = true;
            this.rdoRequiredStar.Checked = true;
            this.rdoRequiredStar.Location = new System.Drawing.Point(92, 6);
            this.rdoRequiredStar.Name = "rdoRequiredStar";
            this.rdoRequiredStar.Size = new System.Drawing.Size(57, 16);
            this.rdoRequiredStar.TabIndex = 5;
            this.rdoRequiredStar.TabStop = true;
            this.rdoRequiredStar.Text = "* 적용";
            this.rdoRequiredStar.UseVisualStyleBackColor = true;
            this.rdoRequiredStar.CheckedChanged += new System.EventHandler(this.rdoRequiredView_CheckedChanged);
            // 
            // rdoRequiredNone
            // 
            this.rdoRequiredNone.AutoSize = true;
            this.rdoRequiredNone.Location = new System.Drawing.Point(3, 6);
            this.rdoRequiredNone.Name = "rdoRequiredNone";
            this.rdoRequiredNone.Size = new System.Drawing.Size(71, 16);
            this.rdoRequiredNone.TabIndex = 5;
            this.rdoRequiredNone.Text = "적용안함";
            this.rdoRequiredNone.UseVisualStyleBackColor = true;
            this.rdoRequiredNone.CheckedChanged += new System.EventHandler(this.rdoRequiredView_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(6, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "정렬";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(6, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "필수여부";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 722);
            this.panel1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(269, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(312, 722);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(269, 722);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // LabelViewsPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelSettingPanel1);
            this.Name = "LabelViewsPanel";
            this.Size = new System.Drawing.Size(581, 783);
            this.labelSettingPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel labelSettingPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox textBox1;
        private JSFW.PowerPoint.Helper.Controls.Label label1;
        private System.Windows.Forms.ComboBox cboFontSize;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdoRequiredUnderLine;
        private System.Windows.Forms.RadioButton rdoRequiredStar;
        private System.Windows.Forms.RadioButton rdoRequiredNone;
        private JSFW.PowerPoint.Helper.Controls.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdoAlignRight;
        private System.Windows.Forms.RadioButton rdoAlignCenter;
        private System.Windows.Forms.RadioButton rdoAlignLeft;
        private JSFW.PowerPoint.Helper.Controls.Label label3;
    }
}
