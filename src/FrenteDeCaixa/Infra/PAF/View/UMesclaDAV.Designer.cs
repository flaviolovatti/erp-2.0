namespace PafEcf.View
{
	partial class FMesclaDAV
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
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.editCpfCnpj = new System.Windows.Forms.TextBox();
            this.EditDestinatario = new System.Windows.Forms.TextBox();
            this.panPeriodo = new System.Windows.Forms.Panel();
            this.Situacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_NUMERO_DAV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_NOME_DESTINATARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_CPF_CNPJ_DESTINATARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_DATA_EMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_HORA_EMISSAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_VALOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridMestre)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDetalhe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            this.panPeriodo.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.GridMestre);
            this.GroupBox2.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.GroupBox2.ForeColor = System.Drawing.Color.Black;
            this.GroupBox2.Location = new System.Drawing.Point(71, 12);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(611, 137);
            this.GroupBox2.TabIndex = 19;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "DAVs disponíveis para mesclagem:";
            // 
            // GridMestre
            // 
            this.GridMestre.AllowUserToAddRows = false;
            this.GridMestre.AllowUserToDeleteRows = false;
            this.GridMestre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Situacao,
            this.GridMestre_NUMERO_DAV,
            this.GridMestre_NOME_DESTINATARIO,
            this.GridMestre_CPF_CNPJ_DESTINATARIO,
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
            this.GridMestre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridMestreKeyDown);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.GridDetalhe);
            this.GroupBox1.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.GroupBox1.Location = new System.Drawing.Point(74, 155);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(611, 170);
            this.GroupBox1.TabIndex = 20;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Produtos do DAV selecionado";
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
            this.botaoConfirma.Location = new System.Drawing.Point(433, 393);
            this.botaoConfirma.Name = "botaoConfirma";
            this.botaoConfirma.Size = new System.Drawing.Size(120, 25);
            this.botaoConfirma.TabIndex = 21;
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
            this.botaoCancela.Location = new System.Drawing.Point(565, 393);
            this.botaoCancela.Name = "botaoCancela";
            this.botaoCancela.Size = new System.Drawing.Size(120, 25);
            this.botaoCancela.TabIndex = 22;
            this.botaoCancela.Text = "Ca&ncela (ESC)";
            this.botaoCancela.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoCancela.Click += new System.EventHandler(this.botaoCancela_Click);
            // 
            // Image1
            // 
            this.Image1.Image = global::PafEcf.Properties.Resources.telaMesclar01;
            this.Image1.Location = new System.Drawing.Point(11, 14);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(48, 48);
            this.Image1.TabIndex = 18;
            this.Image1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(9, 5);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(63, 13);
            this.Label1.TabIndex = 26;
            this.Label1.Text = "CPF/CNPJ:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(187, 5);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(42, 13);
            this.Label3.TabIndex = 27;
            this.Label3.Text = "Nome:";
            // 
            // editCpfCnpj
            // 
            this.editCpfCnpj.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.editCpfCnpj.ForeColor = System.Drawing.Color.Black;
            this.editCpfCnpj.Location = new System.Drawing.Point(12, 21);
            this.editCpfCnpj.Name = "editCpfCnpj";
            this.editCpfCnpj.Size = new System.Drawing.Size(168, 20);
            this.editCpfCnpj.TabIndex = 24;
            // 
            // EditDestinatario
            // 
            this.EditDestinatario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.EditDestinatario.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.EditDestinatario.ForeColor = System.Drawing.Color.Black;
            this.EditDestinatario.Location = new System.Drawing.Point(190, 21);
            this.EditDestinatario.Name = "EditDestinatario";
            this.EditDestinatario.Size = new System.Drawing.Size(415, 20);
            this.EditDestinatario.TabIndex = 25;
            // 
            // panPeriodo
            // 
            this.panPeriodo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panPeriodo.Controls.Add(this.Label3);
            this.panPeriodo.Controls.Add(this.Label1);
            this.panPeriodo.Controls.Add(this.EditDestinatario);
            this.panPeriodo.Controls.Add(this.editCpfCnpj);
            this.panPeriodo.Location = new System.Drawing.Point(74, 331);
            this.panPeriodo.Name = "panPeriodo";
            this.panPeriodo.Size = new System.Drawing.Size(611, 48);
            this.panPeriodo.TabIndex = 23;
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
            this.GridMestre_NUMERO_DAV.DataPropertyName = "NumeroDav";
            this.GridMestre_NUMERO_DAV.HeaderText = "Número";
            this.GridMestre_NUMERO_DAV.Name = "GridMestre_NUMERO_DAV";
            this.GridMestre_NUMERO_DAV.ReadOnly = true;
            // 
            // GridMestre_NOME_DESTINATARIO
            // 
            this.GridMestre_NOME_DESTINATARIO.DataPropertyName = "NomeDestinatario";
            this.GridMestre_NOME_DESTINATARIO.HeaderText = "Destinatário";
            this.GridMestre_NOME_DESTINATARIO.Name = "GridMestre_NOME_DESTINATARIO";
            this.GridMestre_NOME_DESTINATARIO.ReadOnly = true;
            this.GridMestre_NOME_DESTINATARIO.Width = 150;
            // 
            // GridMestre_CPF_CNPJ_DESTINATARIO
            // 
            this.GridMestre_CPF_CNPJ_DESTINATARIO.DataPropertyName = "CpfCnpjDestinatario";
            this.GridMestre_CPF_CNPJ_DESTINATARIO.HeaderText = "CPF/CNPJ";
            this.GridMestre_CPF_CNPJ_DESTINATARIO.Name = "GridMestre_CPF_CNPJ_DESTINATARIO";
            this.GridMestre_CPF_CNPJ_DESTINATARIO.ReadOnly = true;
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
            // FMesclaDAV
            // 
            this.ClientSize = new System.Drawing.Size(697, 428);
            this.Controls.Add(this.panPeriodo);
            this.Controls.Add(this.botaoConfirma);
            this.Controls.Add(this.botaoCancela);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(146, 274);
            this.Name = "FMesclaDAV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mesclagem de DAVs";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FMesclaDAV_KeyDown);
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridMestre)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridDetalhe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            this.panPeriodo.ResumeLayout(false);
            this.panPeriodo.PerformLayout();
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
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox editCpfCnpj;
        internal System.Windows.Forms.TextBox EditDestinatario;
        private System.Windows.Forms.Panel panPeriodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Situacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_NUMERO_DAV;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_NOME_DESTINATARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_CPF_CNPJ_DESTINATARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_DATA_EMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_HORA_EMISSAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_VALOR;

	}
}
