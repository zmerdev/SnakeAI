namespace SnakeForms
{
    partial class GameWindow
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
            this.ResetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(267, 162);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 0;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Visible = false;
            this.ResetButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(658, 450);
            this.Controls.Add(this.ResetButton);
            this.Name = "GameWindow";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameWindow_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameWindow_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ResetButton;
    }
}

