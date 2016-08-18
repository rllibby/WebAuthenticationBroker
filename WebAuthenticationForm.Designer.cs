namespace Sage.WebAuthenticationBroker
{
    partial class WebAuthenticationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null)) components.Dispose();
                if (_browser == null) return;

                _browser.Navigating -= OnNavigating;
                _browser.BeforeNavigate2 -= OnBeforeNavigate2;
                _browser.DocumentComplete -= OnDocumentComplete;
                _browser.NavigateError -= OnNavigateError;
                _browser.Dispose();
                _browser = null;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Header = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.Caption = new System.Windows.Forms.Label();
            this.Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.Controls.Add(this.CloseButton);
            this.Header.Controls.Add(this.Caption);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(1, 1);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(648, 36);
            this.Header.TabIndex = 1;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnHeaderMouseDown);
            // 
            // CloseButton
            // 
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(452, 6);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(24, 24);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.TabStop = false;
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.OnCloseClick);
            this.CloseButton.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCloseButton);
            this.CloseButton.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.CloseButton.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // Caption
            // 
            this.Caption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Caption.ForeColor = System.Drawing.Color.White;
            this.Caption.Location = new System.Drawing.Point(12, 0);
            this.Caption.Name = "Caption";
            this.Caption.Size = new System.Drawing.Size(215, 36);
            this.Caption.TabIndex = 0;
            this.Caption.Text = "Connecting to a service";
            this.Caption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Caption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnHeaderMouseDown);
            // 
            // WebAuthenticationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 580);
            this.Controls.Add(this.Header);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebAuthenticationForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connecting to a service";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnFormPaint);
            this.Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label Caption;
        private System.Windows.Forms.Button CloseButton;
    }
}