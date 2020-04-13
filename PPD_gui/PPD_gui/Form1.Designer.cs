namespace PPD_gui
{
    partial class Form1
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
            this.Start_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.State_txt = new System.Windows.Forms.Label();
            this.State_lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UserNums_txt = new System.Windows.Forms.Label();
            this.Pause_btn = new System.Windows.Forms.Button();
            this.Identify = new System.Windows.Forms.Button();
            this.Access = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Start_btn
            // 
            this.Start_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Start_btn.Location = new System.Drawing.Point(669, 12);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(249, 50);
            this.Start_btn.TabIndex = 0;
            this.Start_btn.Text = "Start";
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // State_txt
            // 
            this.State_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.State_txt.BackColor = System.Drawing.Color.White;
            this.State_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_txt.Location = new System.Drawing.Point(758, 129);
            this.State_txt.Name = "State_txt";
            this.State_txt.Size = new System.Drawing.Size(160, 30);
            this.State_txt.TabIndex = 2;
            this.State_txt.Text = "Stop";
            this.State_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // State_lbl
            // 
            this.State_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.State_lbl.Location = new System.Drawing.Point(667, 129);
            this.State_lbl.Name = "State_lbl";
            this.State_lbl.Size = new System.Drawing.Size(67, 30);
            this.State_lbl.TabIndex = 3;
            this.State_lbl.Text = "State";
            this.State_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(667, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "UserNums";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserNums_txt
            // 
            this.UserNums_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserNums_txt.BackColor = System.Drawing.Color.White;
            this.UserNums_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserNums_txt.Location = new System.Drawing.Point(758, 177);
            this.UserNums_txt.Name = "UserNums_txt";
            this.UserNums_txt.Size = new System.Drawing.Size(160, 30);
            this.UserNums_txt.TabIndex = 5;
            this.UserNums_txt.Text = "0";
            this.UserNums_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pause_btn
            // 
            this.Pause_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pause_btn.Location = new System.Drawing.Point(669, 68);
            this.Pause_btn.Name = "Pause_btn";
            this.Pause_btn.Size = new System.Drawing.Size(249, 50);
            this.Pause_btn.TabIndex = 6;
            this.Pause_btn.Text = "Pause";
            this.Pause_btn.UseVisualStyleBackColor = true;
            this.Pause_btn.Click += new System.EventHandler(this.Stop_btn_Click);
            // 
            // Identify
            // 
            this.Identify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Identify.Location = new System.Drawing.Point(669, 336);
            this.Identify.Name = "Identify";
            this.Identify.Size = new System.Drawing.Size(249, 50);
            this.Identify.TabIndex = 7;
            this.Identify.Text = "IDENT";
            this.Identify.UseVisualStyleBackColor = true;
            // 
            // Access
            // 
            this.Access.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Access.BackColor = System.Drawing.Color.White;
            this.Access.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Access.Font = new System.Drawing.Font("휴먼둥근헤드라인", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Access.Location = new System.Drawing.Point(669, 399);
            this.Access.Name = "Access";
            this.Access.Size = new System.Drawing.Size(249, 96);
            this.Access.TabIndex = 8;
            this.Access.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 506);
            this.Controls.Add(this.Access);
            this.Controls.Add(this.Identify);
            this.Controls.Add(this.Pause_btn);
            this.Controls.Add(this.UserNums_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.State_lbl);
            this.Controls.Add(this.State_txt);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Start_btn);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label State_txt;
        private System.Windows.Forms.Label State_lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label UserNums_txt;
        private System.Windows.Forms.Button Pause_btn;
        private System.Windows.Forms.Button Identify;
        private System.Windows.Forms.Label Access;
    }
}

