namespace PafEcf.View
{
	partial class FLmfs
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
            this.Label2 = new System.Windows.Forms.Label();
            this.panCRZ = new System.Windows.Forms.Panel();
            this.editInicio = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.editFim = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.RadioGroup2 = new System.Windows.Forms.GroupBox();
            this.cbPeriododeData = new System.Windows.Forms.RadioButton();
            this.cbIntervaloCRZ = new System.Windows.Forms.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.panPeriodo = new System.Windows.Forms.Panel();
            this.mkeDataFim = new System.Windows.Forms.MaskedTextBox();
            this.mkeDataIni = new System.Windows.Forms.MaskedTextBox();
            this.cbAImpressaododocumentonoECF = new System.Windows.Forms.RadioButton();
            this.cbBGravacaodearquivoeletroniconoformatodeespelho = new System.Windows.Forms.RadioButton();
            this.RadioGroup1 = new System.Windows.Forms.GroupBox();
            this.botaoCancela = new System.Windows.Forms.Button();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.botaoConfirma = new System.Windows.Forms.Button();
            this.panCRZ.SuspendLayout();
            this.RadioGroup2.SuspendLayout();
            this.panPeriodo.SuspendLayout();
            this.RadioGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            this.SuspendLayout();
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
            // panCRZ
            // 
            this.panCRZ.Controls.Add(this.editInicio);
            this.panCRZ.Controls.Add(this.Label3);
            this.panCRZ.Controls.Add(this.editFim);
            this.panCRZ.Controls.Add(this.Label4);
            this.panCRZ.Enabled = false;
            this.panCRZ.Location = new System.Drawing.Point(313, 155);
            this.panCRZ.Name = "panCRZ";
            this.panCRZ.Size = new System.Drawing.Size(217, 57);
            this.panCRZ.TabIndex = 15;
            // 
            // editInicio
            // 
            this.editInicio.Location = new System.Drawing.Point(19, 28);
            this.editInicio.Name = "editInicio";
            this.editInicio.Size = new System.Drawing.Size(80, 20);
            this.editInicio.TabIndex = 0;
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
            // editFim
            // 
            this.editFim.Location = new System.Drawing.Point(120, 28);
            this.editFim.Name = "editFim";
            this.editFim.Size = new System.Drawing.Size(80, 20);
            this.editFim.TabIndex = 1;
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
            // RadioGroup2
            // 
            this.RadioGroup2.Controls.Add(this.cbPeriododeData);
            this.RadioGroup2.Controls.Add(this.cbIntervaloCRZ);
            this.RadioGroup2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.RadioGroup2.ForeColor = System.Drawing.Color.Black;
            this.RadioGroup2.Location = new System.Drawing.Point(81, 91);
            this.RadioGroup2.Name = "RadioGroup2";
            this.RadioGroup2.Size = new System.Drawing.Size(449, 50);
            this.RadioGroup2.TabIndex = 12;
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
            // cbIntervaloCRZ
            // 
            this.cbIntervaloCRZ.Location = new System.Drawing.Point(251, 18);
            this.cbIntervaloCRZ.Name = "cbIntervaloCRZ";
            this.cbIntervaloCRZ.Size = new System.Drawing.Size(127, 24);
            this.cbIntervaloCRZ.TabIndex = 1;
            this.cbIntervaloCRZ.Text = "Intervalo de CRZ";
            this.cbIntervaloCRZ.Click += new System.EventHandler(this.cbIntervaloCRZ_Click);
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
            // panPeriodo
            // 
            this.panPeriodo.Controls.Add(this.mkeDataFim);
            this.panPeriodo.Controls.Add(this.mkeDataIni);
            this.panPeriodo.Controls.Add(this.Label1);
            this.panPeriodo.Controls.Add(this.Label2);
            this.panPeriodo.Location = new System.Drawing.Point(81, 155);
            this.panPeriodo.Name = "panPeriodo";
            this.panPeriodo.Size = new System.Drawing.Size(217, 57);
            this.panPeriodo.TabIndex = 14;
            // 
            // mkeDataFim
            // 
            this.mkeDataFim.Location = new System.Drawing.Point(114, 28);
            this.mkeDataFim.Mask = "##/##/####";
            this.mkeDataFim.Name = "mkeDataFim";
            this.mkeDataFim.Size = new System.Drawing.Size(75, 20);
            this.mkeDataFim.TabIndex = 5;
            // 
            // mkeDataIni
            // 
            this.mkeDataIni.Location = new System.Drawing.Point(14, 28);
            this.mkeDataIni.Mask = "##/##/####";
            this.mkeDataIni.Name = "mkeDataIni";
            this.mkeDataIni.Size = new System.Drawing.Size(75, 20);
            this.mkeDataIni.TabIndex = 4;
            // 
            // cbAImpressaododocumentonoECF
            // 
            this.cbAImpressaododocumentonoECF.AutoSize = true;
            this.cbAImpressaododocumentonoECF.Location = new System.Drawing.Point(12, 18);
            this.cbAImpressaododocumentonoECF.Name = "cbAImpressaododocumentonoECF";
            this.cbAImpressaododocumentonoECF.Size = new System.Drawing.Size(224, 17);
            this.cbAImpressaododocumentonoECF.TabIndex = 0;
            this.cbAImpressaododocumentonoECF.Text = "a) Impressão do documento no ECF";
            // 
            // cbBGravacaodearquivoeletroniconoformatodeespelho
            // 
            this.cbBGravacaodearquivoeletroniconoformatodeespelho.AutoSize = true;
            this.cbBGravacaodearquivoeletroniconoformatodeespelho.Location = new System.Drawing.Point(12, 42);
            this.cbBGravacaodearquivoeletroniconoformatodeespelho.Name = "cbBGravacaodearquivoeletroniconoformatodeespelho";
            this.cbBGravacaodearquivoeletroniconoformatodeespelho.Size = new System.Drawing.Size(347, 17);
            this.cbBGravacaodearquivoeletroniconoformatodeespelho.TabIndex = 1;
            this.cbBGravacaodearquivoeletroniconoformatodeespelho.Text = "b) Gravação de arquivo eletrônico no formato de espelho";
            // 
            // RadioGroup1
            // 
            this.RadioGroup1.Controls.Add(this.cbAImpressaododocumentonoECF);
            this.RadioGroup1.Controls.Add(this.cbBGravacaodearquivoeletroniconoformatodeespelho);
            this.RadioGroup1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.RadioGroup1.ForeColor = System.Drawing.Color.Black;
            this.RadioGroup1.Location = new System.Drawing.Point(80, 10);
            this.RadioGroup1.Name = "RadioGroup1";
            this.RadioGroup1.Size = new System.Drawing.Size(449, 70);
            this.RadioGroup1.TabIndex = 11;
            this.RadioGroup1.TabStop = false;
            this.RadioGroup1.Text = "Selecione uma Operação:";
            // 
            // botaoCancela
            // 
            this.botaoCancela.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botaoCancela.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoCancela.ForeColor = System.Drawing.Color.Black;
            this.botaoCancela.Image = global::PafEcf.Properties.Resources.cancelar16;
            this.botaoCancela.Location = new System.Drawing.Point(414, 229);
            this.botaoCancela.Name = "botaoCancela";
            this.botaoCancela.Size = new System.Drawing.Size(120, 25);
            this.botaoCancela.TabIndex = 17;
            this.botaoCancela.Text = "Ca&ncela (ESC)";
            this.botaoCancela.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoCancela.Click += new System.EventHandler(this.botaoCancela_Click);
            // 
            // Image1
            // 
            this.Image1.Image = global::PafEcf.Properties.Resources.telaRegistradora01;
            this.Image1.Location = new System.Drawing.Point(13, 10);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(48, 48);
            this.Image1.TabIndex = 13;
            this.Image1.TabStop = false;
            // 
            // botaoConfirma
            // 
            this.botaoConfirma.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoConfirma.ForeColor = System.Drawing.Color.Black;
            this.botaoConfirma.Image = global::PafEcf.Properties.Resources.confirmar16;
            this.botaoConfirma.Location = new System.Drawing.Point(280, 229);
            this.botaoConfirma.Name = "botaoConfirma";
            this.botaoConfirma.Size = new System.Drawing.Size(120, 25);
            this.botaoConfirma.TabIndex = 16;
            this.botaoConfirma.Text = "&Confirma (F12)";
            this.botaoConfirma.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoConfirma.Click += new System.EventHandler(this.botaoConfirma_Click);
            // 
            // FLmfs
            // 
            this.ClientSize = new System.Drawing.Size(546, 265);
            this.Controls.Add(this.botaoCancela);
            this.Controls.Add(this.panCRZ);
            this.Controls.Add(this.RadioGroup2);
            this.Controls.Add(this.panPeriodo);
            this.Controls.Add(this.RadioGroup1);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.botaoConfirma);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(308, 368);
            this.Name = "FLmfs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LMFS - Leitura Memoria Fiscal Simplificada";
            this.Activated += new System.EventHandler(this.FLmfs_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FLmfs_KeyDown);
            this.panCRZ.ResumeLayout(false);
            this.panCRZ.PerformLayout();
            this.RadioGroup2.ResumeLayout(false);
            this.panPeriodo.ResumeLayout(false);
            this.panPeriodo.PerformLayout();
            this.RadioGroup1.ResumeLayout(false);
            this.RadioGroup1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Button botaoCancela;
        private System.Windows.Forms.Panel panCRZ;
        internal System.Windows.Forms.TextBox editInicio;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox editFim;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.GroupBox RadioGroup2;
        internal System.Windows.Forms.RadioButton cbPeriododeData;
        internal System.Windows.Forms.RadioButton cbIntervaloCRZ;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Panel panPeriodo;
        private System.Windows.Forms.MaskedTextBox mkeDataFim;
        private System.Windows.Forms.MaskedTextBox mkeDataIni;
        internal System.Windows.Forms.RadioButton cbAImpressaododocumentonoECF;
        internal System.Windows.Forms.RadioButton cbBGravacaodearquivoeletroniconoformatodeespelho;
        private System.Windows.Forms.GroupBox RadioGroup1;
        private System.Windows.Forms.PictureBox Image1;
        private System.Windows.Forms.Button botaoConfirma;

	}
}
