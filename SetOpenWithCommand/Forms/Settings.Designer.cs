namespace SetOpenWithCommand.Forms
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._openLogBtn = new System.Windows.Forms.Button();
            this._openLogFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _openLogBtn
            // 
            this._openLogBtn.Location = new System.Drawing.Point(34, 39);
            this._openLogBtn.Name = "_openLogBtn";
            this._openLogBtn.Size = new System.Drawing.Size(105, 23);
            this._openLogBtn.TabIndex = 0;
            this._openLogBtn.Text = "打开日志";
            this._openLogBtn.UseVisualStyleBackColor = true;
            this._openLogBtn.Click += new System.EventHandler(this.OpenLogBtn_Click);
            // 
            // _openLogFolder
            // 
            this._openLogFolder.Location = new System.Drawing.Point(173, 39);
            this._openLogFolder.Name = "_openLogFolder";
            this._openLogFolder.Size = new System.Drawing.Size(105, 23);
            this._openLogFolder.TabIndex = 1;
            this._openLogFolder.Text = "打开日志文件夹";
            this._openLogFolder.UseVisualStyleBackColor = true;
            this._openLogFolder.Click += new System.EventHandler(this.OpenLogFolderBtn_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 159);
            this.Controls.Add(this._openLogFolder);
            this.Controls.Add(this._openLogBtn);
            this.Name = "Settings";
            this.Text = "设置";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _openLogBtn;
        private System.Windows.Forms.Button _openLogFolder;
    }
}