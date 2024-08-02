namespace CapaPresentacion
{
    partial class Login
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
            this.txt_nrodocumento = new System.Windows.Forms.TextBox();
            this.txt_contrasena = new System.Windows.Forms.TextBox();
            this.Contraseña = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_entrar = new FontAwesome.Sharp.IconButton();
            this.btn_cancelar = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_nrodocumento
            // 
            this.txt_nrodocumento.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_nrodocumento.Location = new System.Drawing.Point(398, 160);
            this.txt_nrodocumento.Name = "txt_nrodocumento";
            this.txt_nrodocumento.Size = new System.Drawing.Size(265, 20);
            this.txt_nrodocumento.TabIndex = 3;
            // 
            // txt_contrasena
            // 
            this.txt_contrasena.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_contrasena.Location = new System.Drawing.Point(398, 220);
            this.txt_contrasena.Name = "txt_contrasena";
            this.txt_contrasena.PasswordChar = '*';
            this.txt_contrasena.Size = new System.Drawing.Size(265, 20);
            this.txt_contrasena.TabIndex = 4;
            // 
            // Contraseña
            // 
            this.Contraseña.AutoSize = true;
            this.Contraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Contraseña.Location = new System.Drawing.Point(395, 204);
            this.Contraseña.Name = "Contraseña";
            this.Contraseña.Size = new System.Drawing.Size(73, 15);
            this.Contraseña.TabIndex = 5;
            this.Contraseña.Text = "Contraseña:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(395, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nro Documento:";
            // 
            // btn_entrar
            // 
            this.btn_entrar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_entrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_entrar.FlatAppearance.BorderSize = 32;
            this.btn_entrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_entrar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btn_entrar.IconColor = System.Drawing.Color.Black;
            this.btn_entrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_entrar.Location = new System.Drawing.Point(552, 267);
            this.btn_entrar.Name = "btn_entrar";
            this.btn_entrar.Size = new System.Drawing.Size(111, 45);
            this.btn_entrar.TabIndex = 7;
            this.btn_entrar.Text = "Entrar";
            this.btn_entrar.UseVisualStyleBackColor = false;
            this.btn_entrar.Click += new System.EventHandler(this.btn_entrar_Click);
            this.btn_entrar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_entrar_KeyDown);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_cancelar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btn_cancelar.IconColor = System.Drawing.Color.Black;
            this.btn_cancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_cancelar.Location = new System.Drawing.Point(398, 267);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(111, 45);
            this.btn_cancelar.TabIndex = 8;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 563);
            this.label1.TabIndex = 0;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.iconPictureBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Industry;
            this.iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 213;
            this.iconPictureBox1.Location = new System.Drawing.Point(25, 144);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(213, 215);
            this.iconPictureBox1.TabIndex = 2;
            this.iconPictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_entrar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Contraseña);
            this.Controls.Add(this.txt_contrasena);
            this.Controls.Add(this.txt_nrodocumento);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_nrodocumento;
        private System.Windows.Forms.TextBox txt_contrasena;
        private System.Windows.Forms.Label Contraseña;
        private System.Windows.Forms.Label label3;
        private FontAwesome.Sharp.IconButton btn_entrar;
        private FontAwesome.Sharp.IconButton btn_cancelar;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
    }
}