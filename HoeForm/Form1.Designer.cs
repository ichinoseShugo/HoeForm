namespace HoeForm
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.rgbImage = new System.Windows.Forms.PictureBox();
            this.RecordAll = new System.Windows.Forms.CheckBox();
            this.RecordWii = new System.Windows.Forms.CheckBox();
            this.RecordKinect = new System.Windows.Forms.CheckBox();
            this.ax = new System.Windows.Forms.Label();
            this.ay = new System.Windows.Forms.Label();
            this.az = new System.Windows.Forms.Label();
            this.limit = new System.Windows.Forms.Label();
            this.jointNum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rgbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // rgbImage
            // 
            this.rgbImage.Location = new System.Drawing.Point(2, 91);
            this.rgbImage.Name = "rgbImage";
            this.rgbImage.Size = new System.Drawing.Size(640, 480);
            this.rgbImage.TabIndex = 0;
            this.rgbImage.TabStop = false;
            // 
            // RecordAll
            // 
            this.RecordAll.AutoSize = true;
            this.RecordAll.Location = new System.Drawing.Point(2, 2);
            this.RecordAll.Name = "RecordAll";
            this.RecordAll.Size = new System.Drawing.Size(74, 16);
            this.RecordAll.TabIndex = 1;
            this.RecordAll.Text = "RecordAll";
            this.RecordAll.UseVisualStyleBackColor = true;
            this.RecordAll.CheckedChanged += new System.EventHandler(this.RecordAll_CheckedChanged);
            // 
            // RecordWii
            // 
            this.RecordWii.AutoSize = true;
            this.RecordWii.Location = new System.Drawing.Point(2, 25);
            this.RecordWii.Name = "RecordWii";
            this.RecordWii.Size = new System.Drawing.Size(75, 16);
            this.RecordWii.TabIndex = 2;
            this.RecordWii.Text = "RecordWii";
            this.RecordWii.UseVisualStyleBackColor = true;
            // 
            // RecordKinect
            // 
            this.RecordKinect.AutoSize = true;
            this.RecordKinect.Location = new System.Drawing.Point(89, 25);
            this.RecordKinect.Name = "RecordKinect";
            this.RecordKinect.Size = new System.Drawing.Size(92, 16);
            this.RecordKinect.TabIndex = 3;
            this.RecordKinect.Text = "RecordKinect";
            this.RecordKinect.UseVisualStyleBackColor = true;
            // 
            // ax
            // 
            this.ax.AutoSize = true;
            this.ax.Location = new System.Drawing.Point(0, 44);
            this.ax.Name = "ax";
            this.ax.Size = new System.Drawing.Size(25, 12);
            this.ax.TabIndex = 4;
            this.ax.Text = "x軸:";
            // 
            // ay
            // 
            this.ay.AutoSize = true;
            this.ay.Location = new System.Drawing.Point(0, 60);
            this.ay.Name = "ay";
            this.ay.Size = new System.Drawing.Size(25, 12);
            this.ay.TabIndex = 5;
            this.ay.Text = "y軸:";
            // 
            // az
            // 
            this.az.AutoSize = true;
            this.az.Location = new System.Drawing.Point(0, 76);
            this.az.Name = "az";
            this.az.Size = new System.Drawing.Size(24, 12);
            this.az.TabIndex = 6;
            this.az.Text = "z軸:";
            // 
            // limit
            // 
            this.limit.AutoSize = true;
            this.limit.Location = new System.Drawing.Point(87, 44);
            this.limit.Name = "limit";
            this.limit.Size = new System.Drawing.Size(56, 12);
            this.limit.TabIndex = 7;
            this.limit.Text = "ListCount:";
            // 
            // jointNum
            // 
            this.jointNum.AutoSize = true;
            this.jointNum.Location = new System.Drawing.Point(87, 59);
            this.jointNum.Name = "jointNum";
            this.jointNum.Size = new System.Drawing.Size(56, 12);
            this.jointNum.TabIndex = 8;
            this.jointNum.Text = "JointNum:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 575);
            this.Controls.Add(this.jointNum);
            this.Controls.Add(this.limit);
            this.Controls.Add(this.az);
            this.Controls.Add(this.ay);
            this.Controls.Add(this.ax);
            this.Controls.Add(this.RecordKinect);
            this.Controls.Add(this.RecordWii);
            this.Controls.Add(this.RecordAll);
            this.Controls.Add(this.rgbImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.rgbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox rgbImage;
        private System.Windows.Forms.CheckBox RecordAll;
        private System.Windows.Forms.CheckBox RecordWii;
        private System.Windows.Forms.CheckBox RecordKinect;
        private System.Windows.Forms.Label ax;
        private System.Windows.Forms.Label ay;
        private System.Windows.Forms.Label az;
        private System.Windows.Forms.Label limit;
        private System.Windows.Forms.Label jointNum;
    }
}

