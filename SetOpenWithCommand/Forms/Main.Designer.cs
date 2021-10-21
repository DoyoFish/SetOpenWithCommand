using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SetOpenWithCommand.Forms
{
    partial class Main
    {
        private IContainer _components;

        private Label label1;

        private Button _registerBtn;

        private Button _unregisterBtn;

        private Label label2;

        private Label label3;

        private ComboBox _fileTypeList;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this._registerBtn = new System.Windows.Forms.Button();
            this._unregisterBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._fileTypeList = new System.Windows.Forms.ComboBox();
            this._rightExtraParams = new System.Windows.Forms.TextBox();
            this._extraParamTog = new System.Windows.Forms.CheckBox();
            this._browserBtn = new System.Windows.Forms.Button();
            this._settings = new System.Windows.Forms.Button();
            this._commandNameText = new System.Windows.Forms.ComboBox();
            this._pathText = new System.Windows.Forms.ComboBox();
            this._leftExtraParams = new System.Windows.Forms.TextBox();
            this._leftParamLabel = new System.Windows.Forms.Label();
            this._rightParamLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置打开所用exe的绝对路径";
            // 
            // _registerBtn
            // 
            this._registerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._registerBtn.Location = new System.Drawing.Point(36, 299);
            this._registerBtn.Name = "_registerBtn";
            this._registerBtn.Size = new System.Drawing.Size(75, 23);
            this._registerBtn.TabIndex = 6;
            this._registerBtn.Text = "注册";
            this._registerBtn.UseVisualStyleBackColor = true;
            this._registerBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
            // 
            // _unregisterBtn
            // 
            this._unregisterBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._unregisterBtn.Location = new System.Drawing.Point(230, 299);
            this._unregisterBtn.Name = "_unregisterBtn";
            this._unregisterBtn.Size = new System.Drawing.Size(75, 23);
            this._unregisterBtn.TabIndex = 7;
            this._unregisterBtn.Text = "注销";
            this._unregisterBtn.UseVisualStyleBackColor = true;
            this._unregisterBtn.Click += new System.EventHandler(this.UnregisterBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "设置命令名";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "文件类型";
            // 
            // _fileTypeList
            // 
            this._fileTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._fileTypeList.FormattingEnabled = true;
            this._fileTypeList.Location = new System.Drawing.Point(254, 42);
            this._fileTypeList.Name = "_fileTypeList";
            this._fileTypeList.Size = new System.Drawing.Size(121, 20);
            this._fileTypeList.TabIndex = 1;
            // 
            // _rightExtraParams
            // 
            this._rightExtraParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._rightExtraParams.Location = new System.Drawing.Point(34, 259);
            this._rightExtraParams.Name = "_rightExtraParams";
            this._rightExtraParams.Size = new System.Drawing.Size(271, 21);
            this._rightExtraParams.TabIndex = 5;
            // 
            // _extraParamTog
            // 
            this._extraParamTog.AutoSize = true;
            this._extraParamTog.Location = new System.Drawing.Point(36, 153);
            this._extraParamTog.Name = "_extraParamTog";
            this._extraParamTog.Size = new System.Drawing.Size(96, 16);
            this._extraParamTog.TabIndex = 4;
            this._extraParamTog.Text = "设置额外参数";
            this._extraParamTog.UseVisualStyleBackColor = true;
            this._extraParamTog.CheckedChanged += new System.EventHandler(this.ExtraParamTog_CheckedChanged);
            // 
            // _browserBtn
            // 
            this._browserBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browserBtn.Location = new System.Drawing.Point(345, 109);
            this._browserBtn.Name = "_browserBtn";
            this._browserBtn.Size = new System.Drawing.Size(31, 23);
            this._browserBtn.TabIndex = 3;
            this._browserBtn.Text = "...";
            this._browserBtn.UseVisualStyleBackColor = true;
            this._browserBtn.Click += new System.EventHandler(this._browserBtn_Click);
            // 
            // _settings
            // 
            this._settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._settings.Image = ((System.Drawing.Image)(resources.GetObject("_settings.Image")));
            this._settings.Location = new System.Drawing.Point(406, 4);
            this._settings.Name = "_settings";
            this._settings.Size = new System.Drawing.Size(26, 26);
            this._settings.TabIndex = 8;
            this._settings.UseVisualStyleBackColor = true;
            this._settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // _commandNameText
            // 
            this._commandNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._commandNameText.FormattingEnabled = true;
            this._commandNameText.Location = new System.Drawing.Point(34, 42);
            this._commandNameText.Name = "_commandNameText";
            this._commandNameText.Size = new System.Drawing.Size(155, 20);
            this._commandNameText.TabIndex = 0;
            // 
            // _pathText
            // 
            this._pathText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pathText.FormattingEnabled = true;
            this._pathText.Location = new System.Drawing.Point(34, 111);
            this._pathText.Name = "_pathText";
            this._pathText.Size = new System.Drawing.Size(313, 20);
            this._pathText.TabIndex = 2;
            // 
            // _leftExtraParams
            // 
            this._leftExtraParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._leftExtraParams.Location = new System.Drawing.Point(34, 205);
            this._leftExtraParams.Name = "_leftExtraParams";
            this._leftExtraParams.Size = new System.Drawing.Size(271, 21);
            this._leftExtraParams.TabIndex = 5;
            // 
            // _leftParamLabel
            // 
            this._leftParamLabel.AutoSize = true;
            this._leftParamLabel.Location = new System.Drawing.Point(35, 188);
            this._leftParamLabel.Name = "_leftParamLabel";
            this._leftParamLabel.Size = new System.Drawing.Size(155, 12);
            this._leftParamLabel.TabIndex = 9;
            this._leftParamLabel.Text = "目标文件左侧命令, \";\"隔开";
            // 
            // _rightParamLabel
            // 
            this._rightParamLabel.AutoSize = true;
            this._rightParamLabel.Location = new System.Drawing.Point(35, 243);
            this._rightParamLabel.Name = "_rightParamLabel";
            this._rightParamLabel.Size = new System.Drawing.Size(155, 12);
            this._rightParamLabel.TabIndex = 9;
            this._rightParamLabel.Text = "目标文件右侧命令, \";\"隔开";
            // 
            // Main
            // 
            this.AcceptButton = this._registerBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 343);
            this.Controls.Add(this._rightParamLabel);
            this.Controls.Add(this._leftParamLabel);
            this.Controls.Add(this._pathText);
            this.Controls.Add(this._commandNameText);
            this.Controls.Add(this._settings);
            this.Controls.Add(this._browserBtn);
            this.Controls.Add(this._extraParamTog);
            this.Controls.Add(this._fileTypeList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._unregisterBtn);
            this.Controls.Add(this._registerBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._leftExtraParams);
            this.Controls.Add(this._rightExtraParams);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(452, 323);
            this.Name = "Main";
            this.Text = "右键关联助手";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox _rightExtraParams;
        private CheckBox _extraParamTog;
        private Button _browserBtn;
        private Button _settings;
        private ComboBox _commandNameText;
        private ComboBox _pathText;
        private TextBox _leftExtraParams;
        private Label _leftParamLabel;
        private Label _rightParamLabel;
    }
}

