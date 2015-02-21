using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopTracks2iTunes
{
    public partial class ProgressForm : Form
    {
        private CancellationTokenSource m_cts;

        public ProgressForm()
        {
            InitializeComponent();
        }

        public CancellationToken CancellationToken
        {
            get 
            {
                if (m_cts == null)
                    m_cts = new CancellationTokenSource();

                return m_cts.Token; 
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            if (m_cts != null)
                m_cts.Dispose();

            base.Dispose(disposing);
        }

        public string ProgressText
        {
            get { return m_processingLabel.Text; }
            set { m_processingLabel.Text = value; }
        }

        public int CurrentProgress
        {
            get { return m_progressBar.Value; }
            set { m_progressBar.Value = value; }
        }

        private void m_cancelButton_Click(object sender, EventArgs e)
        {
            if (m_cts != null && !m_cts.IsCancellationRequested)
                m_cts.Cancel();
            this.Close();
        }
    }
}
