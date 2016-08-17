namespace OMRManagement
{
    partial class Reprocess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reprocess));
            this.buttonReprocess = new System.Windows.Forms.Button();
            this.btnRecover = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.btnError = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonReprocess
            // 
            this.buttonReprocess.BackColor = System.Drawing.Color.Honeydew;
            this.buttonReprocess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReprocess.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReprocess.Location = new System.Drawing.Point(137, 84);
            this.buttonReprocess.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReprocess.Name = "buttonReprocess";
            this.buttonReprocess.Size = new System.Drawing.Size(100, 28);
            this.buttonReprocess.TabIndex = 86;
            this.buttonReprocess.Text = "Reprocess";
            this.buttonReprocess.UseVisualStyleBackColor = false;
            this.buttonReprocess.Click += new System.EventHandler(this.buttonReprocess_Click);
            // 
            // btnRecover
            // 
            this.btnRecover.BackColor = System.Drawing.Color.LightCyan;
            this.btnRecover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecover.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecover.Location = new System.Drawing.Point(13, 84);
            this.btnRecover.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecover.Name = "btnRecover";
            this.btnRecover.Size = new System.Drawing.Size(100, 28);
            this.btnRecover.TabIndex = 85;
            this.btnRecover.Text = "Recover";
            this.btnRecover.UseVisualStyleBackColor = false;
            this.btnRecover.Click += new System.EventHandler(this.btnRecover_Click);
            this.btnRecover.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnRecover_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(32, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(290, 38);
            this.label15.TabIndex = 163;
            this.label15.Text = "The job already exists, Would you like to \r\n   \"Recover\" or \"Reprocess\" or \"Error" +
    "s\" ?";
            // 
            // btnError
            // 
            this.btnError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnError.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnError.Location = new System.Drawing.Point(254, 84);
            this.btnError.Margin = new System.Windows.Forms.Padding(4);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(100, 28);
            this.btnError.TabIndex = 164;
            this.btnError.Text = "Errors";
            this.btnError.UseVisualStyleBackColor = false;
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // Reprocess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(382, 135);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.buttonReprocess);
            this.Controls.Add(this.btnRecover);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Reprocess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alert";
            this.Load += new System.EventHandler(this.Reprocess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonReprocess;
        private System.Windows.Forms.Button btnRecover;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnError;
    }
}