namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.menuMantenedor = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuCategoria = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuMaterial = new FontAwesome.Sharp.IconMenuItem();
            this.menuVentas = new FontAwesome.Sharp.IconMenuItem();
            this.listaCompras = new FontAwesome.Sharp.IconMenuItem();
            this.menuClientes = new FontAwesome.Sharp.IconMenuItem();
            this.menuProveedores = new FontAwesome.Sharp.IconMenuItem();
            this.menuTitulo = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.iconMenuItem1 = new FontAwesome.Sharp.IconMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUsuarios,
            this.menuMantenedor,
            this.menuVentas,
            this.listaCompras,
            this.menuClientes,
            this.menuProveedores});
            this.menu.Location = new System.Drawing.Point(0, 86);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1254, 73);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuUsuarios.IconChar = FontAwesome.Sharp.IconChar.User;
            this.menuUsuarios.IconColor = System.Drawing.Color.Black;
            this.menuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuUsuarios.IconSize = 50;
            this.menuUsuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Size = new System.Drawing.Size(83, 69);
            this.menuUsuarios.Text = "Usuarios";
            this.menuUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuUsuarios.Click += new System.EventHandler(this.menu_usuario_Click);
            // 
            // menuMantenedor
            // 
            this.menuMantenedor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuCategoria,
            this.subMenuMaterial});
            this.menuMantenedor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuMantenedor.IconChar = FontAwesome.Sharp.IconChar.Wrench;
            this.menuMantenedor.IconColor = System.Drawing.Color.Black;
            this.menuMantenedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuMantenedor.IconSize = 50;
            this.menuMantenedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuMantenedor.Name = "menuMantenedor";
            this.menuMantenedor.Size = new System.Drawing.Size(107, 69);
            this.menuMantenedor.Text = "Mantenedor";
            this.menuMantenedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuCategoria
            // 
            this.subMenuCategoria.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuCategoria.IconColor = System.Drawing.Color.Black;
            this.subMenuCategoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuCategoria.Name = "subMenuCategoria";
            this.subMenuCategoria.Size = new System.Drawing.Size(147, 26);
            this.subMenuCategoria.Text = "Categoria";
            this.subMenuCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.subMenuCategoria.Click += new System.EventHandler(this.subMenuCategoria_Click);
            // 
            // subMenuMaterial
            // 
            this.subMenuMaterial.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuMaterial.IconColor = System.Drawing.Color.Black;
            this.subMenuMaterial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuMaterial.Name = "subMenuMaterial";
            this.subMenuMaterial.Size = new System.Drawing.Size(147, 26);
            this.subMenuMaterial.Text = "Material";
            this.subMenuMaterial.Click += new System.EventHandler(this.subMenuMaterial_Click_1);
            // 
            // menuVentas
            // 
            this.menuVentas.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuVentas.IconChar = FontAwesome.Sharp.IconChar.ShoppingCart;
            this.menuVentas.IconColor = System.Drawing.Color.Black;
            this.menuVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVentas.IconSize = 50;
            this.menuVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuVentas.Name = "menuVentas";
            this.menuVentas.Size = new System.Drawing.Size(68, 69);
            this.menuVentas.Text = "Ventas";
            this.menuVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuVentas.Click += new System.EventHandler(this.menuVentas_Click);
            // 
            // listaCompras
            // 
            this.listaCompras.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.listaCompras.IconChar = FontAwesome.Sharp.IconChar.ShoppingBag;
            this.listaCompras.IconColor = System.Drawing.Color.Black;
            this.listaCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.listaCompras.IconSize = 50;
            this.listaCompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.listaCompras.Name = "listaCompras";
            this.listaCompras.Size = new System.Drawing.Size(85, 69);
            this.listaCompras.Text = "Compras";
            this.listaCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.listaCompras.Click += new System.EventHandler(this.listaCompras_Click);
            // 
            // menuClientes
            // 
            this.menuClientes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuClientes.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.menuClientes.IconColor = System.Drawing.Color.Black;
            this.menuClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuClientes.IconSize = 50;
            this.menuClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuClientes.Name = "menuClientes";
            this.menuClientes.Size = new System.Drawing.Size(77, 69);
            this.menuClientes.Text = "Clientes";
            this.menuClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuClientes.Click += new System.EventHandler(this.menuClientes_Click);
            // 
            // menuProveedores
            // 
            this.menuProveedores.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuProveedores.IconChar = FontAwesome.Sharp.IconChar.Truck;
            this.menuProveedores.IconColor = System.Drawing.Color.Black;
            this.menuProveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuProveedores.IconSize = 50;
            this.menuProveedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuProveedores.Name = "menuProveedores";
            this.menuProveedores.Size = new System.Drawing.Size(109, 69);
            this.menuProveedores.Text = "Proveedores";
            this.menuProveedores.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuProveedores.Click += new System.EventHandler(this.menuProveedores_Click);
            // 
            // menuTitulo
            // 
            this.menuTitulo.AutoSize = false;
            this.menuTitulo.BackColor = System.Drawing.Color.SteelBlue;
            this.menuTitulo.Location = new System.Drawing.Point(0, 0);
            this.menuTitulo.Name = "menuTitulo";
            this.menuTitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuTitulo.Size = new System.Drawing.Size(1254, 86);
            this.menuTitulo.TabIndex = 1;
            this.menuTitulo.Text = "menuStrip2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(777, 43);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fabrica Automatica de Mosaicos Defranco";
            // 
            // contenedor
            // 
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 159);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1254, 702);
            this.contenedor.TabIndex = 3;
            this.contenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.contenedor_Paint);
            // 
            // iconMenuItem1
            // 
            this.iconMenuItem1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItem1.IconColor = System.Drawing.Color.Black;
            this.iconMenuItem1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem1.Name = "iconMenuItem1";
            this.iconMenuItem1.Size = new System.Drawing.Size(32, 19);
            this.iconMenuItem1.Text = "iconMenuItem1";
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 861);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.menuTitulo);
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(1270, 900);
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.MenuStrip menuTitulo;
        private FontAwesome.Sharp.IconMenuItem menuUsuarios;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconMenuItem menuMantenedor;
        private FontAwesome.Sharp.IconMenuItem menuVentas;
        private FontAwesome.Sharp.IconMenuItem menuClientes;
        private FontAwesome.Sharp.IconMenuItem menuProveedores;
        private System.Windows.Forms.Panel contenedor;
        private FontAwesome.Sharp.IconMenuItem subMenuCategoria;
        private FontAwesome.Sharp.IconMenuItem subMenuMaterial;
        private FontAwesome.Sharp.IconMenuItem listaCompras;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem1;
        private System.Windows.Forms.Timer timer1;
    }
}

