namespace WindowsFormsSample
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ShowReport = new System.Windows.Forms.Button();
            this.cboxReports = new System.Windows.Forms.ComboBox();
            this.LoadReports = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ShowReport);
            this.panel1.Controls.Add(this.cboxReports);
            this.panel1.Controls.Add(this.LoadReports);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 71);
            this.panel1.TabIndex = 14;
            // 
            // ShowReport
            // 
            this.ShowReport.Location = new System.Drawing.Point(350, 21);
            this.ShowReport.Name = "ShowReport";
            this.ShowReport.Size = new System.Drawing.Size(75, 23);
            this.ShowReport.TabIndex = 26;
            this.ShowReport.Text = "Show Report";
            this.ShowReport.UseVisualStyleBackColor = true;
            this.ShowReport.Click += new System.EventHandler(this.showReport_Click);
            // 
            // cboxReports
            // 
            this.cboxReports.FormattingEnabled = true;
            this.cboxReports.Location = new System.Drawing.Point(167, 21);
            this.cboxReports.Name = "cboxReports";
            this.cboxReports.Size = new System.Drawing.Size(177, 21);
            this.cboxReports.TabIndex = 25;
            // 
            // LoadReports
            // 
            this.LoadReports.Location = new System.Drawing.Point(12, 19);
            this.LoadReports.Name = "LoadReports";
            this.LoadReports.Size = new System.Drawing.Size(149, 23);
            this.LoadReports.TabIndex = 24;
            this.LoadReports.Text = "Get report list && token";
            this.LoadReports.UseVisualStyleBackColor = true;
            this.LoadReports.Click += new System.EventHandler(this.LoadReports_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.webBrowser1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(628, 402);
            this.panel2.TabIndex = 15;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(628, 402);
            this.webBrowser1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PowerBI - New API";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button LoadReports;
        private System.Windows.Forms.Button ShowReport;
        private System.Windows.Forms.ComboBox cboxReports;
    }
}

