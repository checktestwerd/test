namespace Visualisator
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.btn_AddAP = new System.Windows.Forms.Button();
            this.btn_AddSTA = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(522, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Draw";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtConsole.Enabled = false;
            this.txtConsole.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtConsole.Location = new System.Drawing.Point(522, 45);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(291, 429);
            this.txtConsole.TabIndex = 1;
            // 
            // btn_AddAP
            // 
            this.btn_AddAP.Location = new System.Drawing.Point(667, 3);
            this.btn_AddAP.Name = "btn_AddAP";
            this.btn_AddAP.Size = new System.Drawing.Size(70, 23);
            this.btn_AddAP.TabIndex = 2;
            this.btn_AddAP.Text = "Add AP";
            this.btn_AddAP.UseVisualStyleBackColor = true;
            this.btn_AddAP.Click += new System.EventHandler(this.btn_AddAP_Click);
            // 
            // btn_AddSTA
            // 
            this.btn_AddSTA.Location = new System.Drawing.Point(743, 3);
            this.btn_AddSTA.Name = "btn_AddSTA";
            this.btn_AddSTA.Size = new System.Drawing.Size(70, 23);
            this.btn_AddSTA.TabIndex = 3;
            this.btn_AddSTA.Text = "Add STA";
            this.btn_AddSTA.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(529, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Console";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 486);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_AddSTA);
            this.Controls.Add(this.btn_AddAP);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btn_AddAP;
        private System.Windows.Forms.Button btn_AddSTA;
        private System.Windows.Forms.Label label1;
    }
}

