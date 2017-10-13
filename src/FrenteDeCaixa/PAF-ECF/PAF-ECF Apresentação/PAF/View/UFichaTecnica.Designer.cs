namespace PafEcf.View
{
	partial class FFichaTecnica
    {
		private System.Windows.Forms.PictureBox Image1;
		internal System.Windows.Forms.Label Label1;
		private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnRemover;
		private System.Windows.Forms.Panel TPanel1;
		internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.DataGridView GridPrincipal;
		private System.Windows.Forms.Panel Panel3;
		internal System.Windows.Forms.Label Label5;
		private System.Windows.Forms.Button SpeedButton1;
		internal System.Windows.Forms.TextBox EditLocaliza;
		private System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.DataGridView GridProducao;
		private System.Windows.Forms.Panel Panel4;
		internal System.Windows.Forms.Label Label6;
		private System.Windows.Forms.Button SpeedButton2;
		internal System.Windows.Forms.TextBox editLocalizaProducao;
		private System.Windows.Forms.Panel Panel2;
		internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.DataGridView GridComposicao;
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
			//base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Label1 = new System.Windows.Forms.Label();
            this.TPanel1 = new System.Windows.Forms.Panel();
            this.GridPrincipal = new System.Windows.Forms.DataGridView();
            this.GridPrincipal_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridPrincipal_NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridPrincipal_UNIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridPrincipal_VALOR_VENDA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.EditLocaliza = new System.Windows.Forms.TextBox();
            this.SpeedButton1 = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.GridProducao = new System.Windows.Forms.DataGridView();
            this.GridProducao_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridProducao_NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridProducao_UNIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Label6 = new System.Windows.Forms.Label();
            this.editLocalizaProducao = new System.Windows.Forms.TextBox();
            this.SpeedButton2 = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.GridComposicao = new System.Windows.Forms.DataGridView();
            this.GridComposicao_ID_PRODUTO_FILHO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridComposicao_DESCRICAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridComposicao_QUANTIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label4 = new System.Windows.Forms.Label();
            this.botaoConfirma = new System.Windows.Forms.Button();
            this.botaoCancela = new System.Windows.Forms.Button();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.editQuantidade = new System.Windows.Forms.TextBox();
            this.TPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPrincipal)).BeginInit();
            this.Panel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridProducao)).BeginInit();
            this.Panel4.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridComposicao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(420, 314);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "&Quantidade";
            // 
            // TPanel1
            // 
            this.TPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TPanel1.Controls.Add(this.GridPrincipal);
            this.TPanel1.Controls.Add(this.Panel3);
            this.TPanel1.Controls.Add(this.Label2);
            this.TPanel1.Location = new System.Drawing.Point(80, 8);
            this.TPanel1.Name = "TPanel1";
            this.TPanel1.Size = new System.Drawing.Size(766, 233);
            this.TPanel1.TabIndex = 115;
            // 
            // GridPrincipal
            // 
            this.GridPrincipal.AllowUserToAddRows = false;
            this.GridPrincipal.AllowUserToDeleteRows = false;
            this.GridPrincipal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridPrincipal_ID,
            this.GridPrincipal_NOME,
            this.GridPrincipal_UNIDADE,
            this.GridPrincipal_VALOR_VENDA});
            this.GridPrincipal.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GridPrincipal.Location = new System.Drawing.Point(9, 93);
            this.GridPrincipal.MultiSelect = false;
            this.GridPrincipal.Name = "GridPrincipal";
            this.GridPrincipal.ReadOnly = true;
            this.GridPrincipal.Size = new System.Drawing.Size(747, 132);
            this.GridPrincipal.TabIndex = 103;
            this.GridPrincipal.Text = "Select columns";
            this.GridPrincipal.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPrincipal_CellEnter);
            // 
            // GridPrincipal_ID
            // 
            this.GridPrincipal_ID.DataPropertyName = "Id";
            this.GridPrincipal_ID.HeaderText = "Código";
            this.GridPrincipal_ID.Name = "GridPrincipal_ID";
            // 
            // GridPrincipal_NOME
            // 
            this.GridPrincipal_NOME.DataPropertyName = "Nome";
            this.GridPrincipal_NOME.HeaderText = "Nome";
            this.GridPrincipal_NOME.Name = "GridPrincipal_NOME";
            this.GridPrincipal_NOME.Width = 405;
            // 
            // GridPrincipal_UNIDADE
            // 
            this.GridPrincipal_UNIDADE.DataPropertyName = "UnidadeProduto";
            this.GridPrincipal_UNIDADE.HeaderText = "Unidade";
            this.GridPrincipal_UNIDADE.Name = "GridPrincipal_UNIDADE";
            this.GridPrincipal_UNIDADE.Width = 57;
            // 
            // GridPrincipal_VALOR_VENDA
            // 
            this.GridPrincipal_VALOR_VENDA.DataPropertyName = "ValorVenda";
            this.GridPrincipal_VALOR_VENDA.HeaderText = "Preço de Venda";
            this.GridPrincipal_VALOR_VENDA.Name = "GridPrincipal_VALOR_VENDA";
            this.GridPrincipal_VALOR_VENDA.Width = 148;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Panel3.Controls.Add(this.EditLocaliza);
            this.Panel3.Controls.Add(this.SpeedButton1);
            this.Panel3.Controls.Add(this.Label5);
            this.Panel3.Location = new System.Drawing.Point(9, 28);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(747, 58);
            this.Panel3.TabIndex = 104;
            // 
            // EditLocaliza
            // 
            this.EditLocaliza.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.EditLocaliza.Location = new System.Drawing.Point(6, 27);
            this.EditLocaliza.Name = "EditLocaliza";
            this.EditLocaliza.Size = new System.Drawing.Size(633, 20);
            this.EditLocaliza.TabIndex = 0;
            this.EditLocaliza.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditLocaliza_KeyDown);
            // 
            // SpeedButton1
            // 
            this.SpeedButton1.BackColor = System.Drawing.Color.White;
            this.SpeedButton1.Location = new System.Drawing.Point(645, 27);
            this.SpeedButton1.Name = "SpeedButton1";
            this.SpeedButton1.Size = new System.Drawing.Size(93, 21);
            this.SpeedButton1.TabIndex = 7;
            this.SpeedButton1.Text = "Localiza (F2)";
            this.SpeedButton1.UseVisualStyleBackColor = false;
            this.SpeedButton1.Click += new System.EventHandler(this.SpeedButton1_Click);
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label5.ForeColor = System.Drawing.Color.Black;
            this.Label5.Location = new System.Drawing.Point(5, 9);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(79, 13);
            this.Label5.TabIndex = 6;
            this.Label5.Text = "Procura p&or:";
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.Label2.ForeColor = System.Drawing.Color.Black;
            this.Label2.Location = new System.Drawing.Point(9, 4);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(747, 19);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "Mercadoria a Ser Produzida";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel1
            // 
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.GridProducao);
            this.Panel1.Controls.Add(this.Panel4);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Location = new System.Drawing.Point(80, 245);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(329, 229);
            this.Panel1.TabIndex = 5;
            // 
            // GridProducao
            // 
            this.GridProducao.AllowUserToAddRows = false;
            this.GridProducao.AllowUserToDeleteRows = false;
            this.GridProducao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridProducao_ID,
            this.GridProducao_NOME,
            this.GridProducao_UNIDADE});
            this.GridProducao.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GridProducao.Location = new System.Drawing.Point(8, 92);
            this.GridProducao.MultiSelect = false;
            this.GridProducao.Name = "GridProducao";
            this.GridProducao.ReadOnly = true;
            this.GridProducao.Size = new System.Drawing.Size(313, 128);
            this.GridProducao.TabIndex = 1;
            this.GridProducao.Text = "Select columns";
            // 
            // GridProducao_ID
            // 
            this.GridProducao_ID.DataPropertyName = "Id";
            this.GridProducao_ID.HeaderText = "Código";
            this.GridProducao_ID.Name = "GridProducao_ID";
            this.GridProducao_ID.Width = 66;
            // 
            // GridProducao_NOME
            // 
            this.GridProducao_NOME.DataPropertyName = "Nome";
            this.GridProducao_NOME.HeaderText = "Nome";
            this.GridProducao_NOME.Name = "GridProducao_NOME";
            this.GridProducao_NOME.Width = 164;
            // 
            // GridProducao_UNIDADE
            // 
            this.GridProducao_UNIDADE.DataPropertyName = "UnidadeProduto";
            this.GridProducao_UNIDADE.HeaderText = "Unidade";
            this.GridProducao_UNIDADE.Name = "GridProducao_UNIDADE";
            this.GridProducao_UNIDADE.Width = 43;
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Panel4.Controls.Add(this.Label6);
            this.Panel4.Controls.Add(this.editLocalizaProducao);
            this.Panel4.Controls.Add(this.SpeedButton2);
            this.Panel4.Location = new System.Drawing.Point(7, 28);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(314, 58);
            this.Panel4.TabIndex = 151;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label6.ForeColor = System.Drawing.Color.Black;
            this.Label6.Location = new System.Drawing.Point(7, 10);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(79, 13);
            this.Label6.TabIndex = 9;
            this.Label6.Text = "Proc&ura por:";
            // 
            // editLocalizaProducao
            // 
            this.editLocalizaProducao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.editLocalizaProducao.Location = new System.Drawing.Point(7, 26);
            this.editLocalizaProducao.Name = "editLocalizaProducao";
            this.editLocalizaProducao.Size = new System.Drawing.Size(192, 20);
            this.editLocalizaProducao.TabIndex = 150;
            this.editLocalizaProducao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editLocalizaProducao_KeyDown);
            // 
            // SpeedButton2
            // 
            this.SpeedButton2.BackColor = System.Drawing.Color.White;
            this.SpeedButton2.Location = new System.Drawing.Point(204, 25);
            this.SpeedButton2.Name = "SpeedButton2";
            this.SpeedButton2.Size = new System.Drawing.Size(105, 21);
            this.SpeedButton2.TabIndex = 10;
            this.SpeedButton2.Text = "Localiza (F2)";
            this.SpeedButton2.UseVisualStyleBackColor = false;
            this.SpeedButton2.Click += new System.EventHandler(this.SpeedButton2_Click);
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(10, 7);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(311, 16);
            this.Label3.TabIndex = 8;
            this.Label3.Text = "Índices Para Composição";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel2
            // 
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.GridComposicao);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Location = new System.Drawing.Point(512, 247);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(334, 227);
            this.Panel2.TabIndex = 6;
            // 
            // GridComposicao
            // 
            this.GridComposicao.AllowUserToAddRows = false;
            this.GridComposicao.AllowUserToDeleteRows = false;
            this.GridComposicao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridComposicao_ID_PRODUTO_FILHO,
            this.Id,
            this.GridComposicao_DESCRICAO,
            this.GridComposicao_QUANTIDADE});
            this.GridComposicao.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GridComposicao.Location = new System.Drawing.Point(7, 29);
            this.GridComposicao.MultiSelect = false;
            this.GridComposicao.Name = "GridComposicao";
            this.GridComposicao.ReadOnly = true;
            this.GridComposicao.Size = new System.Drawing.Size(319, 189);
            this.GridComposicao.TabIndex = 102;
            this.GridComposicao.Text = "Select columns";
            // 
            // GridComposicao_ID_PRODUTO_FILHO
            // 
            this.GridComposicao_ID_PRODUTO_FILHO.DataPropertyName = "IdProdutoFilho";
            this.GridComposicao_ID_PRODUTO_FILHO.HeaderText = "Código";
            this.GridComposicao_ID_PRODUTO_FILHO.Name = "GridComposicao_ID_PRODUTO_FILHO";
            this.GridComposicao_ID_PRODUTO_FILHO.Width = 61;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // GridComposicao_DESCRICAO
            // 
            this.GridComposicao_DESCRICAO.DataPropertyName = "Descricao";
            this.GridComposicao_DESCRICAO.HeaderText = "Descrição";
            this.GridComposicao_DESCRICAO.Name = "GridComposicao_DESCRICAO";
            this.GridComposicao_DESCRICAO.Width = 153;
            // 
            // GridComposicao_QUANTIDADE
            // 
            this.GridComposicao_QUANTIDADE.DataPropertyName = "Quantidade";
            this.GridComposicao_QUANTIDADE.HeaderText = "Quantidade";
            this.GridComposicao_QUANTIDADE.Name = "GridComposicao_QUANTIDADE";
            this.GridComposicao_QUANTIDADE.Width = 61;
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.Label4.ForeColor = System.Drawing.Color.Black;
            this.Label4.Location = new System.Drawing.Point(3, 5);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(326, 16);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "Tabela de índice Técnico de Produção";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // botaoConfirma
            // 
            this.botaoConfirma.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoConfirma.ForeColor = System.Drawing.Color.Black;
            this.botaoConfirma.Image = global::PafEcf.Properties.Resources.confirmar16;
            this.botaoConfirma.Location = new System.Drawing.Point(594, 491);
            this.botaoConfirma.Name = "botaoConfirma";
            this.botaoConfirma.Size = new System.Drawing.Size(120, 25);
            this.botaoConfirma.TabIndex = 12;
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
            this.botaoCancela.Location = new System.Drawing.Point(726, 491);
            this.botaoCancela.Name = "botaoCancela";
            this.botaoCancela.Size = new System.Drawing.Size(120, 25);
            this.botaoCancela.TabIndex = 13;
            this.botaoCancela.Text = "Ca&ncela (ESC)";
            this.botaoCancela.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoCancela.Click += new System.EventHandler(this.botaoCancela_Click);
            // 
            // Image1
            // 
            this.Image1.Image = global::PafEcf.Properties.Resources.telaRegistradora01;
            this.Image1.Location = new System.Drawing.Point(12, 8);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(48, 48);
            this.Image1.TabIndex = 0;
            this.Image1.TabStop = false;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Image = global::PafEcf.Properties.Resources.arrowright_green16;
            this.btnAdicionar.Location = new System.Drawing.Point(419, 385);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(87, 28);
            this.btnAdicionar.TabIndex = 100;
            this.btnAdicionar.Text = "&Adicionar";
            this.btnAdicionar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.Image = global::PafEcf.Properties.Resources.arrowleft_green16;
            this.btnRemover.Location = new System.Drawing.Point(419, 425);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(87, 28);
            this.btnRemover.TabIndex = 101;
            this.btnRemover.Text = "  &Remover";
            this.btnRemover.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // editQuantidade
            // 
            this.editQuantidade.Location = new System.Drawing.Point(419, 331);
            this.editQuantidade.Name = "editQuantidade";
            this.editQuantidade.Size = new System.Drawing.Size(87, 20);
            this.editQuantidade.TabIndex = 14;
            this.editQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FFichaTecnica
            // 
            this.ClientSize = new System.Drawing.Size(861, 530);
            this.Controls.Add(this.editQuantidade);
            this.Controls.Add(this.botaoConfirma);
            this.Controls.Add(this.botaoCancela);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.TPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(146, 302);
            this.Name = "FFichaTecnica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tabela Índice Técnico de Produção";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FFichaTecnica_KeyDown);
            this.TPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridPrincipal)).EndInit();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridProducao)).EndInit();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridComposicao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private System.Windows.Forms.Button botaoConfirma;
        private System.Windows.Forms.Button botaoCancela;
        private System.Windows.Forms.TextBox editQuantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridPrincipal_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridPrincipal_NOME;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridPrincipal_UNIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridPrincipal_VALOR_VENDA;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridProducao_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridProducao_NOME;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridProducao_UNIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridComposicao_ID_PRODUTO_FILHO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridComposicao_DESCRICAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridComposicao_QUANTIDADE;

	}
}
