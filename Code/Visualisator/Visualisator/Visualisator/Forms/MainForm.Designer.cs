namespace Visualisator
{
    partial class MainForm
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
            this.btnStopMedium = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openDLGOpenSimulationSettings = new System.Windows.Forms.OpenFileDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.btnShowMediumInfo = new System.Windows.Forms.Button();
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
            this.txtConsole.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtConsole.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtConsole.Location = new System.Drawing.Point(522, 73);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(331, 439);
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
            // btnStopMedium
            // 
            this.btnStopMedium.Location = new System.Drawing.Point(589, 3);
            this.btnStopMedium.Name = "btnStopMedium";
            this.btnStopMedium.Size = new System.Drawing.Size(61, 23);
            this.btnStopMedium.TabIndex = 5;
            this.btnStopMedium.Text = "Stop Med";
            this.btnStopMedium.UseVisualStyleBackColor = true;
            this.btnStopMedium.Click += new System.EventHandler(this.BtnStopMediumClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(711, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 19);
            this.button2.TabIndex = 6;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(625, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 19);
            this.button3.TabIndex = 7;
            this.button3.Text = "Load";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openDLGOpenSimulationSettings
            // 
            this.openDLGOpenSimulationSettings.FileName = "openFileDialog1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(522, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 19);
            this.button4.TabIndex = 8;
            this.button4.Text = "Create";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnShowMediumInfo
            // 
            this.btnShowMediumInfo.Location = new System.Drawing.Point(711, 24);
            this.btnShowMediumInfo.Name = "btnShowMediumInfo";
            this.btnShowMediumInfo.Size = new System.Drawing.Size(77, 22);
            this.btnShowMediumInfo.TabIndex = 9;
            this.btnShowMediumInfo.Text = "Medium";
            this.btnShowMediumInfo.UseVisualStyleBackColor = true;
            this.btnShowMediumInfo.Click += new System.EventHandler(this.btnShowMediumInfo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 513);
            this.Controls.Add(this.btnShowMediumInfo);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnStopMedium);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_AddSTA);
            this.Controls.Add(this.btn_AddAP);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btn_AddAP;
        private System.Windows.Forms.Button btn_AddSTA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStopMedium;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openDLGOpenSimulationSettings;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnShowMediumInfo;
    }
}

