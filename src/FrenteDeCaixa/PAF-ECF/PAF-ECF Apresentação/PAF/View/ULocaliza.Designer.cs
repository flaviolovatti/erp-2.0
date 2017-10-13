namespace PafEcf.View
{
	partial class FLocaliza
    {
		private System.Windows.Forms.Button btnProdutos;
		private System.Windows.Forms.Button btnClientes;
		private System.Windows.Forms.Button btnVendedor;
		// Required designer variable.
		private System.ComponentModel.IContainer components = null;

		// Clean up any resources being used.
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnProdutos = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnVendedor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnProdutos
            // 
            this.btnProdutos.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.btnProdutos.ForeColor = System.Drawing.Color.Black;
            this.btnProdutos.Location = new System.Drawing.Point(10, 8);
            this.btnProdutos.Name = "btnProdutos";
            this.btnProdutos.Size = new System.Drawing.Size(280, 35);
            this.btnProdutos.TabIndex = 0;
            this.btnProdutos.Text = "  1 - Pesquisa Produtos";
            this.btnProdutos.Click += new System.EventHandler(this.btnProdutos_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.btnClientes.ForeColor = System.Drawing.Color.Black;
            this.btnClientes.Location = new System.Drawing.Point(10, 47);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(280, 35);
            this.btnClientes.TabIndex = 1;
            this.btnClientes.Text = "  2 - Identifica Clientes";
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnVendedor
            // 
            this.btnVendedor.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.btnVendedor.ForeColor = System.Drawing.Color.Black;
            this.btnVendedor.Location = new System.Drawing.Point(10, 85);
            this.btnVendedor.Name = "btnVendedor";
            this.btnVendedor.Size = new System.Drawing.Size(280, 35);
            this.btnVendedor.TabIndex = 2;
            this.btnVendedor.Text = "  3 - Identifica Vendedor";
            this.btnVendedor.Click += new System.EventHandler(this.btnVendedor_Click);
            // 
            // FLocaliza
            // 
            this.ClientSize = new System.Drawing.Size(301, 129);
            this.Controls.Add(this.btnProdutos);
            this.Controls.Add(this.btnClientes);
            this.Controls.Add(this.btnVendedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(361, 441);
            this.Name = "FLocaliza";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Localiza - Teclado Reduzido";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FLocaliza_KeyDown);
            this.ResumeLayout(false);

		}
		#endregion

	}
}
