namespace TopTracks2iTunes
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
            this.messagesTextBox = new System.Windows.Forms.TextBox();
            this.clearCommentsButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.artistTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackLimit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.playlistComboBox = new System.Windows.Forms.ComboBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // messagesTextBox
            // 
            this.messagesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messagesTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messagesTextBox.Location = new System.Drawing.Point(10, 98);
            this.messagesTextBox.Multiline = true;
            this.messagesTextBox.Name = "messagesTextBox";
            this.messagesTextBox.ReadOnly = true;
            this.messagesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messagesTextBox.Size = new System.Drawing.Size(422, 359);
            this.messagesTextBox.TabIndex = 0;
            // 
            // clearCommentsButton
            // 
            this.clearCommentsButton.Location = new System.Drawing.Point(15, 39);
            this.clearCommentsButton.Name = "clearCommentsButton";
            this.clearCommentsButton.Size = new System.Drawing.Size(75, 42);
            this.clearCommentsButton.TabIndex = 1;
            this.clearCommentsButton.Text = "Clear Comments";
            this.clearCommentsButton.UseVisualStyleBackColor = true;
            this.clearCommentsButton.Click += new System.EventHandler(this.clearCommentsButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(360, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 53);
            this.button2.TabIndex = 3;
            this.button2.Text = "Get Top Tracks";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // artistTextBox
            // 
            this.artistTextBox.Location = new System.Drawing.Point(254, 39);
            this.artistTextBox.Name = "artistTextBox";
            this.artistTextBox.Size = new System.Drawing.Size(100, 20);
            this.artistTextBox.TabIndex = 4;
            this.artistTextBox.Text = "Brian Adam";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(213, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Artist:";
            // 
            // trackLimit
            // 
            this.trackLimit.Location = new System.Drawing.Point(254, 66);
            this.trackLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.trackLimit.Name = "trackLimit";
            this.trackLimit.Size = new System.Drawing.Size(100, 20);
            this.trackLimit.TabIndex = 6;
            this.trackLimit.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Limit:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "&Playlist:";
            // 
            // playlistComboBox
            // 
            this.playlistComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.playlistComboBox.FormattingEnabled = true;
            this.playlistComboBox.Location = new System.Drawing.Point(60, 12);
            this.playlistComboBox.Name = "playlistComboBox";
            this.playlistComboBox.Size = new System.Drawing.Size(139, 21);
            this.playlistComboBox.TabIndex = 9;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(206, 12);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(229, 23);
            this.progressBar.TabIndex = 10;
            this.progressBar.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 469);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.playlistComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackLimit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.artistTextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.clearCommentsButton);
            this.Controls.Add(this.messagesTextBox);
            this.Name = "Form1";
            this.Text = "TopTracks to iTunes";
            ((System.ComponentModel.ISupportInitialize)(this.trackLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messagesTextBox;
        private System.Windows.Forms.Button clearCommentsButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox artistTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown trackLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox playlistComboBox;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

