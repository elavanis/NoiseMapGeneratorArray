namespace NoiseMapGeneratorArray
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_BuildArray = new System.Windows.Forms.Button();
            this.button_BuildPictures = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_ETA = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_BuildArray
            // 
            this.button_BuildArray.Location = new System.Drawing.Point(12, 12);
            this.button_BuildArray.Name = "button_BuildArray";
            this.button_BuildArray.Size = new System.Drawing.Size(75, 23);
            this.button_BuildArray.TabIndex = 0;
            this.button_BuildArray.Text = "Build Array";
            this.button_BuildArray.UseVisualStyleBackColor = true;
            this.button_BuildArray.Click += new System.EventHandler(this.button_BuildArray_Click);
            // 
            // button_BuildPictures
            // 
            this.button_BuildPictures.Location = new System.Drawing.Point(93, 12);
            this.button_BuildPictures.Name = "button_BuildPictures";
            this.button_BuildPictures.Size = new System.Drawing.Size(75, 23);
            this.button_BuildPictures.TabIndex = 1;
            this.button_BuildPictures.Text = "Build Pic";
            this.button_BuildPictures.UseVisualStyleBackColor = true;
            this.button_BuildPictures.Click += new System.EventHandler(this.button_BuildPictures_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 41);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(202, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(12, 70);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(202, 23);
            this.progressBar2.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_ETA});
            this.statusStrip1.Location = new System.Drawing.Point(0, 139);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(458, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_ETA
            // 
            this.toolStripStatusLabel_ETA.Name = "toolStripStatusLabel_ETA";
            this.toolStripStatusLabel_ETA.Size = new System.Drawing.Size(26, 17);
            this.toolStripStatusLabel_ETA.Text = "ETA";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(458, 161);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button_BuildPictures);
            this.Controls.Add(this.button_BuildArray);
            this.Name = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private Button button_BuildArray;
        private Button button_BuildPictures;
        private ProgressBar progressBar1;
        private ProgressBar progressBar2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel_ETA;
    }
}