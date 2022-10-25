
namespace JSFW.PowerPoint.Helper
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoControls = new System.Windows.Forms.RadioButton();
            this.rdoLabel = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRbnAlign_L = new System.Windows.Forms.Button();
            this.btnRbnAlign_C = new System.Windows.Forms.Button();
            this.btnRbnAlign_R = new System.Windows.Forms.Button();
            this.btnRbnAlign_T = new System.Windows.Forms.Button();
            this.btnRbnAlign_M = new System.Windows.Forms.Button();
            this.btnRbnAlign_B = new System.Windows.Forms.Button();
            this.label1 = new JSFW.PowerPoint.Helper.Controls.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoControls);
            this.panel1.Controls.Add(this.rdoLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 48);
            this.panel1.TabIndex = 1;
            // 
            // rdoControls
            // 
            this.rdoControls.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoControls.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rdoControls.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.rdoControls.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rdoControls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdoControls.Location = new System.Drawing.Point(85, 12);
            this.rdoControls.Name = "rdoControls";
            this.rdoControls.Size = new System.Drawing.Size(67, 24);
            this.rdoControls.TabIndex = 0;
            this.rdoControls.Text = "컨트롤";
            this.rdoControls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoControls.UseVisualStyleBackColor = false;
            this.rdoControls.CheckedChanged += new System.EventHandler(this.rdoControls_CheckedChanged);
            // 
            // rdoLabel
            // 
            this.rdoLabel.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rdoLabel.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.rdoLabel.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rdoLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdoLabel.Location = new System.Drawing.Point(12, 12);
            this.rdoLabel.Name = "rdoLabel";
            this.rdoLabel.Size = new System.Drawing.Size(67, 24);
            this.rdoLabel.TabIndex = 0;
            this.rdoLabel.Text = "라벨";
            this.rdoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoLabel.UseVisualStyleBackColor = false;
            this.rdoLabel.CheckedChanged += new System.EventHandler(this.rdoLabel_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 92);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(704, 708);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.btnRbnAlign_B);
            this.panel3.Controls.Add(this.btnRbnAlign_M);
            this.panel3.Controls.Add(this.btnRbnAlign_T);
            this.panel3.Controls.Add(this.btnRbnAlign_R);
            this.panel3.Controls.Add(this.btnRbnAlign_C);
            this.panel3.Controls.Add(this.btnRbnAlign_L);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(704, 44);
            this.panel3.TabIndex = 2;
            // 
            // btnRbnAlign_L
            // 
            this.btnRbnAlign_L.BackgroundImage = global::JSFW.PowerPoint.Helper.Properties.Resources.LL;
            this.btnRbnAlign_L.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRbnAlign_L.Location = new System.Drawing.Point(3, 3);
            this.btnRbnAlign_L.Name = "btnRbnAlign_L";
            this.btnRbnAlign_L.Size = new System.Drawing.Size(36, 36);
            this.btnRbnAlign_L.TabIndex = 0;
            this.btnRbnAlign_L.UseVisualStyleBackColor = true;
            this.btnRbnAlign_L.Click += new System.EventHandler(this.btnRbnAlign_L_Click);
            // 
            // btnRbnAlign_C
            // 
            this.btnRbnAlign_C.BackgroundImage = global::JSFW.PowerPoint.Helper.Properties.Resources.CC;
            this.btnRbnAlign_C.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRbnAlign_C.Location = new System.Drawing.Point(45, 3);
            this.btnRbnAlign_C.Name = "btnRbnAlign_C";
            this.btnRbnAlign_C.Size = new System.Drawing.Size(36, 36);
            this.btnRbnAlign_C.TabIndex = 0;
            this.btnRbnAlign_C.UseVisualStyleBackColor = true;
            this.btnRbnAlign_C.Click += new System.EventHandler(this.btnRbnAlign_C_Click);
            // 
            // btnRbnAlign_R
            // 
            this.btnRbnAlign_R.BackgroundImage = global::JSFW.PowerPoint.Helper.Properties.Resources.RR;
            this.btnRbnAlign_R.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRbnAlign_R.Location = new System.Drawing.Point(87, 3);
            this.btnRbnAlign_R.Name = "btnRbnAlign_R";
            this.btnRbnAlign_R.Size = new System.Drawing.Size(36, 36);
            this.btnRbnAlign_R.TabIndex = 0;
            this.btnRbnAlign_R.UseVisualStyleBackColor = true;
            this.btnRbnAlign_R.Click += new System.EventHandler(this.btnRbnAlign_R_Click);
            // 
            // btnRbnAlign_T
            // 
            this.btnRbnAlign_T.BackgroundImage = global::JSFW.PowerPoint.Helper.Properties.Resources.TT;
            this.btnRbnAlign_T.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRbnAlign_T.Location = new System.Drawing.Point(129, 3);
            this.btnRbnAlign_T.Name = "btnRbnAlign_T";
            this.btnRbnAlign_T.Size = new System.Drawing.Size(36, 36);
            this.btnRbnAlign_T.TabIndex = 0;
            this.btnRbnAlign_T.UseVisualStyleBackColor = true;
            this.btnRbnAlign_T.Click += new System.EventHandler(this.btnRbnAlign_T_Click);
            // 
            // btnRbnAlign_M
            // 
            this.btnRbnAlign_M.BackgroundImage = global::JSFW.PowerPoint.Helper.Properties.Resources.MM;
            this.btnRbnAlign_M.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRbnAlign_M.Location = new System.Drawing.Point(171, 3);
            this.btnRbnAlign_M.Name = "btnRbnAlign_M";
            this.btnRbnAlign_M.Size = new System.Drawing.Size(36, 36);
            this.btnRbnAlign_M.TabIndex = 0;
            this.btnRbnAlign_M.UseVisualStyleBackColor = true;
            this.btnRbnAlign_M.Click += new System.EventHandler(this.btnRbnAlign_M_Click);
            // 
            // btnRbnAlign_B
            // 
            this.btnRbnAlign_B.BackgroundImage = global::JSFW.PowerPoint.Helper.Properties.Resources.BB;
            this.btnRbnAlign_B.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRbnAlign_B.Location = new System.Drawing.Point(213, 3);
            this.btnRbnAlign_B.Name = "btnRbnAlign_B";
            this.btnRbnAlign_B.Size = new System.Drawing.Size(36, 36);
            this.btnRbnAlign_B.TabIndex = 0;
            this.btnRbnAlign_B.UseVisualStyleBackColor = true;
            this.btnRbnAlign_B.Click += new System.EventHandler(this.btnRbnAlign_B_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "* PPT에서 선택된 도형들 서식 맞춤!";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(704, 800);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(720, 5000);
            this.MinimumSize = new System.Drawing.Size(420, 550);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JSFW] PPT Util";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdoControls;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRbnAlign_B;
        private System.Windows.Forms.Button btnRbnAlign_M;
        private System.Windows.Forms.Button btnRbnAlign_T;
        private System.Windows.Forms.Button btnRbnAlign_R;
        private System.Windows.Forms.Button btnRbnAlign_C;
        private System.Windows.Forms.Button btnRbnAlign_L;
        private Controls.Label label1;
    }
}

