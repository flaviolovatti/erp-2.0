namespace PafEcf.View
{
	partial class FMesclaPreVenda
    {
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
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.GridMestre = new System.Windows.Forms.DataGridView();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.GridDetalhe = new System.Windows.Forms.DataGridView();
            this.GridDetalhe_DESCRICAO_PDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridDetalhe_QUANTIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridDetalhe_VALOR_UNITARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridDetalhe_VALOR_TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.botaoConfirma = new System.Windows.Forms.Button();
            this.botaoCancela = new System.Windows.Forms.Button();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.Situacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_NUMERO_DAV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_DATA_EMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_HORA_EMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_VALOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridMestre)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDetalhe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.GridMestre);
            this.GroupBox2.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.GroupBox2.ForeColor = System.Drawing.Color.Black;
            this.GroupBox2.Location = new System.Drawing.Point(69, 11);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(611, 137);
            this.GroupBox2.TabIndex = 25;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Pré-vendas disponíveis para mesclagem:";
            // 
            // GridMestre
            // 
            this.GridMestre.AllowUserToAddRows = false;
            this.GridMestre.AllowUserToDeleteRows = false;
            this.GridMestre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Situacao,
            this.GridMestre_NUMERO_DAV,
            this.GridMestre_DATA_EMISSAO,
            this.GridMestre_HORA_EMISSAO,
            this.GridMestre_VALOR});
            this.GridMestre.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GridMestre.Location = new System.Drawing.Point(3, 21);
            this.GridMestre.Name = "GridMestre";
            this.GridMestre.ReadOnly = true;
            this.GridMestre.Size = new System.Drawing.Size(605, 110);
            this.GridMestre.TabIndex = 0;
            this.GridMestre.Text = "Select columns";
            this.GridMestre.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridMestre_CellEnter);
            this.GridMestre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridMestre_KeyDown);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.GridDetalhe);
            this.GroupBox1.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.GroupBox1.Location = new System.Drawing.Point(72, 154);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(611, 170);
            this.GroupBox1.TabIndex = 26;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Produtos da pré-venda selecionada";
            // 
            // GridDetalhe
            // 
            this.GridDetalhe.AllowUserToAddRows = false;
            this.GridDetalhe.AllowUserToDeleteRows = false;
            this.GridDetalhe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridDetalhe_DESCRICAO_PDV,
            this.GridDetalhe_QUANTIDADE,
            this.GridDetalhe_VALOR_UNITARIO,
            this.GridDetalhe_VALOR_TOTAL});
            this.GridDetalhe.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GridDetalhe.Location = new System.Drawing.Point(0, 21);
            this.GridDetalhe.Name = "GridDetalhe";
            this.GridDetalhe.ReadOnly = true;
            this.GridDetalhe.Size = new System.Drawing.Size(606, 143);
            this.GridDetalhe.TabIndex = 0;
            this.GridDetalhe.Text = "Select columns";
            // 
            // GridDetalhe_DESCRICAO_PDV
            // 
            this.GridDetalhe_DESCRICAO_PDV.DataPropertyName = "NomeProduto";
            this.GridDetalhe_DESCRICAO_PDV.HeaderText = "Descrição";
            this.GridDetalhe_DESCRICAO_PDV.Name = "GridDetalhe_DESCRICAO_PDV";
            this.GridDetalhe_DESCRICAO_PDV.ReadOnly = true;
            this.GridDetalhe_DESCRICAO_PDV.Width = 200;
            // 
            // GridDetalhe_QUANTIDADE
            // 
            this.GridDetalhe_QUANTIDADE.DataPropertyName = "Quantidade";
            this.GridDetalhe_QUANTIDADE.HeaderText = "Quantidade";
            this.GridDetalhe_QUANTIDADE.Name = "GridDetalhe_QUANTIDADE";
            this.GridDetalhe_QUANTIDADE.ReadOnly = true;
            this.GridDetalhe_QUANTIDADE.Width = 120;
            // 
            // GridDetalhe_VALOR_UNITARIO
            // 
            this.GridDetalhe_VALOR_UNITARIO.DataPropertyName = "ValorUnitario";
            this.GridDetalhe_VALOR_UNITARIO.HeaderText = "Valor Unitário";
            this.GridDetalhe_VALOR_UNITARIO.Name = "GridDetalhe_VALOR_UNITARIO";
            this.GridDetalhe_VALOR_UNITARIO.ReadOnly = true;
            this.GridDetalhe_VALOR_UNITARIO.Width = 120;
            // 
            // GridDetalhe_VALOR_TOTAL
            // 
            this.GridDetalhe_VALOR_TOTAL.DataPropertyName = "ValorTotal";
            this.GridDetalhe_VALOR_TOTAL.HeaderText = "Valor Total";
            this.GridDetalhe_VALOR_TOTAL.Name = "GridDetalhe_VALOR_TOTAL";
            this.GridDetalhe_VALOR_TOTAL.ReadOnly = true;
            this.GridDetalhe_VALOR_TOTAL.Width = 120;
            // 
            // botaoConfirma
            // 
            this.botaoConfirma.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoConfirma.ForeColor = System.Drawing.Color.Black;
            this.botaoConfirma.Image = global::PafEcf.Properties.Resources.confirmar16;
            this.botaoConfirma.Location = new System.Drawing.Point(431, 338);
            this.botaoConfirma.Name = "botaoConfirma";
            this.botaoConfirma.Size = new System.Drawing.Size(120, 25);
            this.botaoConfirma.TabIndex = 27;
            this.botaoConfirma.Text = "&Confirma (F12)";
            this.botaoConfirma.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoConfirma.Click += new System.EventHandler(this.botaoConfirma_Click);
            // 
            // botaoCancela
            // 
            this.botaoCancela.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botaoCancela.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoCancela.ForeColor = System.Drawing.Color.Black;
            this.botaoCancela.Image = global::PafEcf.Properties.Resources.cancelar16;
            this.botaoCancela.Location = new System.Drawing.Point(563, 338);
            this.botaoCancela.Name = "botaoCancela";
            this.botaoCancela.Size = new System.Drawing.Size(120, 25);
            this.botaoCancela.TabIndex = 28;
            this.botaoCancela.Text = "Ca&ncela (ESC)";
            this.botaoCancela.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoCancela.Click += new System.EventHandler(this.botaoCancela_Click);
            // 
            // Image1
            // 
            this.Image1.Image = global::PafEcf.Properties.Resources.telaMesclar01;
            this.Image1.Location = new System.Drawing.Point(9, 11);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(48, 48);
            this.Image1.TabIndex = 24;
            this.Image1.TabStop = false;
            // 
            // Situacao
            // 
            this.Situacao.DataPropertyName = "Situacao";
            this.Situacao.HeaderText = "Seleção";
            this.Situacao.Name = "Situacao";
            this.Situacao.ReadOnly = true;
            this.Situacao.Width = 60;
            // 
            // GridMestre_NUMERO_DAV
            // 
            this.GridMestre_NUMERO_DAV.DataPropertyName = "Id";
            this.GridMestre_NUMERO_DAV.HeaderText = "Número";
            this.GridMestre_NUMERO_DAV.Name = "GridMestre_NUMERO_DAV";
            this.GridMestre_NUMERO_DAV.ReadOnly = true;
            // 
            // GridMestre_DATA_EMISSAO
            // 
            this.GridMestre_DATA_EMISSAO.DataPropertyName = "DataEmissao";
            this.GridMestre_DATA_EMISSAO.HeaderText = "Data";
            this.GridMestre_DATA_EMISSAO.Name = "GridMestre_DATA_EMISSAO";
            this.GridMestre_DATA_EMISSAO.ReadOnly = true;
            // 
            // GridMestre_HORA_EMISSAO
            // 
            this.GridMestre_HORA_EMISSAO.DataPropertyName = "HoraEmissao";
            this.GridMestre_HORA_EMISSAO.HeaderText = "Hora";
            this.GridMestre_HORA_EMISSAO.Name = "GridMestre_HORA_EMISSAO";
            this.GridMestre_HORA_EMISSAO.ReadOnly = true;
            // 
            // GridMestre_VALOR
            // 
            this.GridMestre_VALOR.DataPropertyName = "Valor";
            this.GridMestre_VALOR.HeaderText = "Valor";
            this.GridMestre_VALOR.Name = "GridMestre_VALOR";
            this.GridMestre_VALOR.ReadOnly = true;
            this.GridMestre_VALOR.Width = 110;
            // 
            // FMesclaPreVenda
            // 
            this.ClientSize = new System.Drawing.Size(692, 373);
            this.Controls.Add(this.botaoConfirma);
            this.Controls.Add(this.botaoCancela);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(219, 410);
            this.Name = "FMesclaPreVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mesclagem de Pre-Vendas";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FMesclaPreVenda_KeyDown);
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridMestre)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridDetalhe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.Button botaoConfirma;
        private System.Windows.Forms.Button botaoCancela;
        private System.Windows.Forms.PictureBox Image1;
        private System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.DataGridView GridMestre;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.DataGridView GridDetalhe;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDetalhe_DESCRICAO_PDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDetalhe_QUANTIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDetalhe_VALOR_UNITARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDetalhe_VALOR_TOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Situacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_NUMERO_DAV;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_DATA_EMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_HORA_EMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_VALOR;

	}
}
