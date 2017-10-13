namespace PafEcf.View
{
	partial class FCancelaPreVenda
    {
        private System.Windows.Forms.PictureBox Image1;
		private System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.DataGridView GridMestre;
		private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.DataGridView GridDetalhe;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.GridMestre = new System.Windows.Forms.DataGridView();
            this.GridMestre_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_DATA_PV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_HORA_PV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridMestre_VALOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.GridDetalhe = new System.Windows.Forms.DataGridView();
            this.GridDetalhe_DESCRICAO_PDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridDetalhe_QUANTIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridDetalhe_VALOR_UNITARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridDetalhe_VALOR_TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.botaoConfirma = new System.Windows.Forms.Button();
            this.botaoCancela = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridMestre)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDetalhe)).BeginInit();
            this.SuspendLayout();
            // 
            // Image1
            // 
            this.Image1.Image = global::PafEcf.Properties.Resources.telaMesclar01;
            this.Image1.Location = new System.Drawing.Point(10, 10);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(48, 48);
            this.Image1.TabIndex = 0;
            this.Image1.TabStop = false;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.GridMestre);
            this.GroupBox2.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.GroupBox2.ForeColor = System.Drawing.Color.Black;
            this.GroupBox2.Location = new System.Drawing.Point(70, 8);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(611, 137);
            this.GroupBox2.TabIndex = 2;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Pré-Vendas disponíveis para cancelamento";
            // 
            // GridMestre
            // 
            this.GridMestre.AllowUserToAddRows = false;
            this.GridMestre.AllowUserToDeleteRows = false;
            this.GridMestre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridMestre_ID,
            this.GridMestre_DATA_PV,
            this.GridMestre_HORA_PV,
            this.GridMestre_VALOR});
            this.GridMestre.Font = new System.Drawing.Font("Tahoma", 8F);
            this.GridMestre.Location = new System.Drawing.Point(2, 21);
            this.GridMestre.MultiSelect = false;
            this.GridMestre.Name = "GridMestre";
            this.GridMestre.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridMestre.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GridMestre.Size = new System.Drawing.Size(607, 110);
            this.GridMestre.TabIndex = 0;
            this.GridMestre.Text = "Select columns";
            this.GridMestre.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridMestre_CellEnter);
            // 
            // GridMestre_ID
            // 
            this.GridMestre_ID.DataPropertyName = "Id";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridMestre_ID.DefaultCellStyle = dataGridViewCellStyle1;
            this.GridMestre_ID.HeaderText = "Número";
            this.GridMestre_ID.Name = "GridMestre_ID";
            this.GridMestre_ID.ReadOnly = true;
            // 
            // GridMestre_DATA_PV
            // 
            this.GridMestre_DATA_PV.DataPropertyName = "DataEmissao";
            this.GridMestre_DATA_PV.HeaderText = "Data";
            this.GridMestre_DATA_PV.Name = "GridMestre_DATA_PV";
            this.GridMestre_DATA_PV.ReadOnly = true;
            // 
            // GridMestre_HORA_PV
            // 
            this.GridMestre_HORA_PV.DataPropertyName = "HoraEmissao";
            this.GridMestre_HORA_PV.HeaderText = "Hora";
            this.GridMestre_HORA_PV.Name = "GridMestre_HORA_PV";
            this.GridMestre_HORA_PV.ReadOnly = true;
            // 
            // GridMestre_VALOR
            // 
            this.GridMestre_VALOR.DataPropertyName = "Valor";
            this.GridMestre_VALOR.HeaderText = "Valor";
            this.GridMestre_VALOR.Name = "GridMestre_VALOR";
            this.GridMestre_VALOR.ReadOnly = true;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.GridDetalhe);
            this.GroupBox1.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.GroupBox1.Location = new System.Drawing.Point(73, 151);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(611, 170);
            this.GroupBox1.TabIndex = 3;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Produtos da Pré-Venda selecionada";
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
            this.GridDetalhe.Location = new System.Drawing.Point(2, 21);
            this.GridDetalhe.MultiSelect = false;
            this.GridDetalhe.Name = "GridDetalhe";
            this.GridDetalhe.ReadOnly = true;
            this.GridDetalhe.Size = new System.Drawing.Size(607, 143);
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
            this.botaoConfirma.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.botaoConfirma.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoConfirma.ForeColor = System.Drawing.Color.Black;
            this.botaoConfirma.Image = global::PafEcf.Properties.Resources.confirmar16;
            this.botaoConfirma.Location = new System.Drawing.Point(431, 333);
            this.botaoConfirma.Name = "botaoConfirma";
            this.botaoConfirma.Size = new System.Drawing.Size(120, 25);
            this.botaoConfirma.TabIndex = 14;
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
            this.botaoCancela.Location = new System.Drawing.Point(563, 333);
            this.botaoCancela.Name = "botaoCancela";
            this.botaoCancela.Size = new System.Drawing.Size(120, 25);
            this.botaoCancela.TabIndex = 15;
            this.botaoCancela.Text = "Ca&ncela (ESC)";
            this.botaoCancela.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoCancela.Click += new System.EventHandler(this.botaoCancela_Click);
            // 
            // FCancelaPreVenda
            // 
            this.ClientSize = new System.Drawing.Size(692, 370);
            this.Controls.Add(this.botaoConfirma);
            this.Controls.Add(this.botaoCancela);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(239, 363);
            this.Name = "FCancelaPreVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancelamento de Pre-Vendas";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FCancelaPreVenda_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridMestre)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridDetalhe)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.Button botaoConfirma;
        private System.Windows.Forms.Button botaoCancela;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDetalhe_DESCRICAO_PDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDetalhe_QUANTIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDetalhe_VALOR_UNITARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDetalhe_VALOR_TOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_DATA_PV;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_HORA_PV;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridMestre_VALOR;

	}
}
