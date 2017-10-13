namespace PafEcf.View
{
	partial class FEspelhoMfd
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
            this.RadioGroup2 = new System.Windows.Forms.GroupBox();
            this.cbPeriododeData = new System.Windows.Forms.RadioButton();
            this.cbIntervalodeCOO = new System.Windows.Forms.RadioButton();
            this.editFim = new System.Windows.Forms.TextBox();
            this.mkeDataFim = new System.Windows.Forms.MaskedTextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.botaoCancela = new System.Windows.Forms.Button();
            this.mkeDataIni = new System.Windows.Forms.MaskedTextBox();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.editInicio = new System.Windows.Forms.TextBox();
            this.panPeriodo = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.panCOO = new System.Windows.Forms.Panel();
            this.botaoConfirma = new System.Windows.Forms.Button();
            this.RadioGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            this.panPeriodo.SuspendLayout();
            this.panCOO.SuspendLayout();
            this.SuspendLayout();
            // 
            // RadioGroup2
            // 
            this.RadioGroup2.Controls.Add(this.cbPeriododeData);
            this.RadioGroup2.Controls.Add(this.cbIntervalodeCOO);
            this.RadioGroup2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.RadioGroup2.ForeColor = System.Drawing.Color.Black;
            this.RadioGroup2.Location = new System.Drawing.Point(81, 11);
            this.RadioGroup2.Name = "RadioGroup2";
            this.RadioGroup2.Size = new System.Drawing.Size(449, 50);
            this.RadioGroup2.TabIndex = 6;
            this.RadioGroup2.TabStop = false;
            this.RadioGroup2.Text = "Tipo de Filtro:";
            // 
            // cbPeriododeData
            // 
            this.cbPeriododeData.Location = new System.Drawing.Point(14, 18);
            this.cbPeriododeData.Name = "cbPeriododeData";
            this.cbPeriododeData.Size = new System.Drawing.Size(128, 24);
            this.cbPeriododeData.TabIndex = 0;
            this.cbPeriododeData.Text = "Período de Data";
            this.cbPeriododeData.Click += new System.EventHandler(this.cbPeriododeData_Click);
            // 
            // cbIntervalodeCOO
            // 
            this.cbIntervalodeCOO.Location = new System.Drawing.Point(251, 18);
            this.cbIntervalodeCOO.Name = "cbIntervalodeCOO";
            this.cbIntervalodeCOO.Size = new System.Drawing.Size(127, 24);
            this.cbIntervalodeCOO.TabIndex = 1;
            this.cbIntervalodeCOO.Text = "Intervalo de COO";
            this.cbIntervalodeCOO.Click += new System.EventHandler(this.cbIntervalodeCOO_Click);
            // 
            // editFim
            // 
            this.editFim.Location = new System.Drawing.Point(120, 28);
            this.editFim.Name = "editFim";
            this.editFim.Size = new System.Drawing.Size(80, 20);
            this.editFim.TabIndex = 1;
            this.editFim.Leave += new System.EventHandler(this.editFim_Leave);
            // 
            // mkeDataFim
            // 
            this.mkeDataFim.Location = new System.Drawing.Point(114, 28);
            this.mkeDataFim.Mask = "##/##/####";
            this.mkeDataFim.Name = "mkeDataFim";
            this.mkeDataFim.Size = new System.Drawing.Size(75, 20);
            this.mkeDataFim.TabIndex = 5;
            this.mkeDataFim.Leave += new System.EventHandler(this.mkeDataFim_Leave);
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label4.ForeColor = System.Drawing.Color.Black;
            this.Label4.Location = new System.Drawing.Point(117, 12);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(50, 13);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "Ultimo:";
            // 
            // botaoCancela
            // 
            this.botaoCancela.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botaoCancela.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoCancela.ForeColor = System.Drawing.Color.Black;
            this.botaoCancela.Image = global::PafEcf.Properties.Resources.cancelar16;
            this.botaoCancela.Location = new System.Drawing.Point(414, 149);
            this.botaoCancela.Name = "botaoCancela";
            this.botaoCancela.Size = new System.Drawing.Size(120, 25);
            this.botaoCancela.TabIndex = 10;
            this.botaoCancela.Text = "Ca&ncela (ESC)";
            this.botaoCancela.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoCancela.Click += new System.EventHandler(this.botaoCancela_Click);
            // 
            // mkeDataIni
            // 
            this.mkeDataIni.Location = new System.Drawing.Point(14, 28);
            this.mkeDataIni.Mask = "##/##/####";
            this.mkeDataIni.Name = "mkeDataIni";
            this.mkeDataIni.Size = new System.Drawing.Size(75, 20);
            this.mkeDataIni.TabIndex = 4;
            // 
            // Image1
            // 
            this.Image1.Image = global::PafEcf.Properties.Resources.telaRegistradora01;
            this.Image1.Location = new System.Drawing.Point(13, 11);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(48, 48);
            this.Image1.TabIndex = 5;
            this.Image1.TabStop = false;
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(16, 12);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(61, 13);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Primeiro:";
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(11, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(77, 13);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Data Inicial:";
            // 
            // editInicio
            // 
            this.editInicio.Location = new System.Drawing.Point(19, 28);
            this.editInicio.Name = "editInicio";
            this.editInicio.Size = new System.Drawing.Size(80, 20);
            this.editInicio.TabIndex = 0;
            // 
            // panPeriodo
            // 
            this.panPeriodo.Controls.Add(this.mkeDataFim);
            this.panPeriodo.Controls.Add(this.mkeDataIni);
            this.panPeriodo.Controls.Add(this.Label1);
            this.panPeriodo.Controls.Add(this.Label2);
            this.panPeriodo.Location = new System.Drawing.Point(81, 75);
            this.panPeriodo.Name = "panPeriodo";
            this.panPeriodo.Size = new System.Drawing.Size(217, 57);
            this.panPeriodo.TabIndex = 7;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label2.ForeColor = System.Drawing.Color.Black;
            this.Label2.Location = new System.Drawing.Point(111, 12);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(69, 13);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Data Final:";
            // 
            // panCOO
            // 
            this.panCOO.Controls.Add(this.editInicio);
            this.panCOO.Controls.Add(this.Label3);
            this.panCOO.Controls.Add(this.editFim);
            this.panCOO.Controls.Add(this.Label4);
            this.panCOO.Enabled = false;
            this.panCOO.Location = new System.Drawing.Point(313, 75);
            this.panCOO.Name = "panCOO";
            this.panCOO.Size = new System.Drawing.Size(217, 57);
            this.panCOO.TabIndex = 8;
            // 
            // botaoConfirma
            // 
            this.botaoConfirma.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoConfirma.ForeColor = System.Drawing.Color.Black;
            this.botaoConfirma.Image = global::PafEcf.Properties.Resources.confirmar16;
            this.botaoConfirma.Location = new System.Drawing.Point(280, 149);
            this.botaoConfirma.Name = "botaoConfirma";
            this.botaoConfirma.Size = new System.Drawing.Size(120, 25);
            this.botaoConfirma.TabIndex = 9;
            this.botaoConfirma.Text = "&Confirma (F12)";
            this.botaoConfirma.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoConfirma.Click += new System.EventHandler(this.botaoConfirma_Click);
            // 
            // FEspelhoMfd
            // 
            this.ClientSize = new System.Drawing.Size(546, 184);
            this.Controls.Add(this.RadioGroup2);
            this.Controls.Add(this.botaoCancela);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.panPeriodo);
            this.Controls.Add(this.panCOO);
            this.Controls.Add(this.botaoConfirma);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(245, 307);
            this.Name = "FEspelhoMfd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Espelho MFD - Memoria Fita Detalhe";
            this.Activated += new System.EventHandler(this.FEspelhoMfd_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FEspelhoMfd_KeyDown);
            this.RadioGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            this.panPeriodo.ResumeLayout(false);
            this.panPeriodo.PerformLayout();
            this.panCOO.ResumeLayout(false);
            this.panCOO.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.GroupBox RadioGroup2;
        internal System.Windows.Forms.RadioButton cbPeriododeData;
        internal System.Windows.Forms.RadioButton cbIntervalodeCOO;
        internal System.Windows.Forms.TextBox editFim;
        private System.Windows.Forms.MaskedTextBox mkeDataFim;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Button botaoCancela;
        private System.Windows.Forms.MaskedTextBox mkeDataIni;
        private System.Windows.Forms.PictureBox Image1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox editInicio;
        private System.Windows.Forms.Panel panPeriodo;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Panel panCOO;
        private System.Windows.Forms.Button botaoConfirma;

	}
}
