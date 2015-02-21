namespace TopTracks2iTunes
{
    partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_processingLabel = new System.Windows.Forms.Label();
            this.m_progressBar = new System.Windows.Forms.ProgressBar();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_processingLabel
            // 
            this.m_processingLabel.AutoSize = true;
            this.m_processingLabel.Location = new System.Drawing.Point(12, 9);
            this.m_processingLabel.Name = "m_processingLabel";
            this.m_processingLabel.Size = new System.Drawing.Size(153, 13);
            this.m_processingLabel.TabIndex = 0;
            this.m_processingLabel.Text = "Processing songs (1 of 2112)...";
            // 
            // m_progressBar
            // 
            this.m_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progressBar.Location = new System.Drawing.Point(15, 25);
            this.m_progressBar.Name = "m_progressBar";
            this.m_progressBar.Size = new System.Drawing.Size(252, 23);
            this.m_progressBar.TabIndex = 1;
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelButton.Location = new System.Drawing.Point(192, 64);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_cancelButton.TabIndex = 2;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            this.m_cancelButton.Click += new System.EventHandler(this.m_cancelButton_Click);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 99);
            this.ControlBox = false;
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_progressBar);
            this.Controls.Add(this.m_processingLabel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.Text = "Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_processingLabel;
        private System.Windows.Forms.ProgressBar m_progressBar;
        private System.Windows.Forms.Button m_cancelButton;
    }
}