namespace JSFW.PowerPoint.Helper.Contents.Label
{
    partial class LineTypeSettingView
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
            this.cboFontSize = new System.Windows.Forms.ComboBox();
            this.label1 = new JSFW.PowerPoint.Helper.Controls.Label();
            this.rdoShapeType1 = new JSFW.PowerPoint.Helper.Contents.Label.rdoShapeType();
            this.rdoShapeType2 = new JSFW.PowerPoint.Helper.Contents.Label.rdoShapeType();
            this.rdoShapeType3 = new JSFW.PowerPoint.Helper.Contents.Label.rdoShapeType();
            this.SuspendLayout();
            // 
            // cboFontSize
            // 
            this.cboFontSize.FormattingEnabled = true;
            this.cboFontSize.Items.AddRange(new object[] {
            "1",
            "3",
            "5"});
            this.cboFontSize.Location = new System.Drawing.Point(104, 3);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(59, 20);
            this.cboFontSize.TabIndex = 7;
            this.cboFontSize.Text = "1";
            this.cboFontSize.SelectedIndexChanged += new System.EventHandler(this.cboFontSize_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "LineWeight";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdoShapeType1
            // 
            this.rdoShapeType1.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoShapeType1.BackColor = System.Drawing.Color.Transparent;
            this.rdoShapeType1.BorderWeight = 1F;
            this.rdoShapeType1.FlatAppearance.BorderSize = 0;
            this.rdoShapeType1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdoShapeType1.Location = new System.Drawing.Point(3, 42);
            this.rdoShapeType1.Name = "rdoShapeType1";
            this.rdoShapeType1.ShapeType = JSFW.PowerPoint.Helper.Contents.Label.ShapeType.NoLine;
            this.rdoShapeType1.Size = new System.Drawing.Size(160, 36);
            this.rdoShapeType1.TabIndex = 8;
            this.rdoShapeType1.TabStop = true;
            this.rdoShapeType1.Text = "외곽선 없음";
            this.rdoShapeType1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoShapeType1.UseVisualStyleBackColor = true;
            this.rdoShapeType1.CheckedChanged += new System.EventHandler(this.rdoShapeType1_CheckedChanged);
            // 
            // rdoShapeType2
            // 
            this.rdoShapeType2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoShapeType2.BackColor = System.Drawing.Color.Transparent;
            this.rdoShapeType2.BorderWeight = 1F;
            this.rdoShapeType2.FlatAppearance.BorderSize = 0;
            this.rdoShapeType2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdoShapeType2.Location = new System.Drawing.Point(3, 84);
            this.rdoShapeType2.Name = "rdoShapeType2";
            this.rdoShapeType2.ShapeType = JSFW.PowerPoint.Helper.Contents.Label.ShapeType.Box;
            this.rdoShapeType2.Size = new System.Drawing.Size(160, 36);
            this.rdoShapeType2.TabIndex = 8;
            this.rdoShapeType2.TabStop = true;
            this.rdoShapeType2.Text = "사각형";
            this.rdoShapeType2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoShapeType2.UseVisualStyleBackColor = true;
            this.rdoShapeType2.CheckedChanged += new System.EventHandler(this.rdoShapeType2_CheckedChanged);
            // 
            // rdoShapeType3
            // 
            this.rdoShapeType3.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoShapeType3.BackColor = System.Drawing.Color.Transparent;
            this.rdoShapeType3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rdoShapeType3.BorderWeight = 1F;
            this.rdoShapeType3.FlatAppearance.BorderSize = 0;
            this.rdoShapeType3.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rdoShapeType3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdoShapeType3.Location = new System.Drawing.Point(3, 126);
            this.rdoShapeType3.Name = "rdoShapeType3";
            this.rdoShapeType3.ShapeType = JSFW.PowerPoint.Helper.Contents.Label.ShapeType.RoundBox;
            this.rdoShapeType3.Size = new System.Drawing.Size(160, 36);
            this.rdoShapeType3.TabIndex = 8;
            this.rdoShapeType3.TabStop = true;
            this.rdoShapeType3.Text = "둥근 사각형";
            this.rdoShapeType3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoShapeType3.UseVisualStyleBackColor = false;
            this.rdoShapeType3.CheckedChanged += new System.EventHandler(this.rdoShapeType3_CheckedChanged);
            // 
            // LineTypeSettingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.rdoShapeType3);
            this.Controls.Add(this.rdoShapeType2);
            this.Controls.Add(this.rdoShapeType1);
            this.Controls.Add(this.cboFontSize);
            this.Controls.Add(this.label1);
            this.Name = "LineTypeSettingView";
            this.Size = new System.Drawing.Size(166, 644);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboFontSize;
        private Helper.Controls.Label label1;
        private rdoShapeType rdoShapeType1;
        private rdoShapeType rdoShapeType2;
        private rdoShapeType rdoShapeType3;
    }
}
