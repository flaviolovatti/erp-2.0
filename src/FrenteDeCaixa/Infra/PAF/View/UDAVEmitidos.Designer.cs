namespace PafEcf.View
{
	partial class FDavEmitidos
    {
		private System.Windows.Forms.GroupBox RadioGroup1;
		internal System.Windows.Forms.RadioButton cbRelatorioGerencial;
        internal System.Windows.Forms.RadioButton cbGeracaodeArquivo;
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
            this.RadioGroup1 = new System.Windows.Forms.GroupBox();
            this.cbRelatorioGerencial = new System.Windows.Forms.RadioButton();
            this.cbGeracaodeArquivo = new System.Windows.Forms.RadioButton();
            this.panPeriodo = new System.Windows.Forms.Panel();
            this.mkeDataFim = new System.Windows.Forms.MaskedTextBox();
            this.mkeDataIni = new System.Windows.Forms.MaskedTextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.botaoCancela = new System.Windows.Forms.Button();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.botaoConfirma = new System.Windows.Forms.Button();
            this.RadioGroup1.SuspendLayout();
            this.panPeriodo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            this.SuspendLayout();
            // 
            // RadioGroup1
            // 
            this.RadioGroup1.Controls.Add(this.cbRelatorioGerencial);
            this.RadioGroup1.Controls.Add(this.cbGeracaodeArquivo);
            this.RadioGroup1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.RadioGroup1.ForeColor = System.Drawing.Color.Black;
            this.RadioGroup1.Location = new System.Drawing.Point(80, 12);
            this.RadioGroup1.Name = "RadioGroup1";
            this.RadioGroup1.Size = new System.Drawing.Size(175, 63);
            this.RadioGroup1.TabIndex = 0;
            this.RadioGroup1.TabStop = false;
            this.RadioGroup1.Text = "Selecione uma Opera��o:";
            // 
            // cbRelatorioGerencial
            // 
            this.cbRelatorioGerencial.AutoSize = true;
            this.cbRelatorioGerencial.Location = new System.Drawing.Point(12, 18);
            this.cbRelatorioGerencial.Name = "cbRelatorioGerencial";
            this.cbRelatorioGerencial.Size = new System.Drawing.Size(133, 17);
            this.cbRelatorioGerencial.TabIndex = 0;
            this.cbRelatorioGerencial.Text = "Relat�rio Gerencial";
            // 
            // cbGeracaodeArquivo
            // 
            this.cbGeracaodeArquivo.AutoSize = true;
            this.cbGeracaodeArquivo.Location = new System.Drawing.Point(12, 38);
            this.cbGeracaodeArquivo.Name = "cbGeracaodeArquivo";
            this.cbGeracaodeArquivo.Size = new System.Drawing.Size(136, 17);
            this.cbGeracaodeArquivo.TabIndex = 1;
            this.cbGeracaodeArquivo.Text = "Gera��o de Arquivo";
            // 
            // panPeriodo
            // 
            this.panPeriodo.Controls.Add(this.mkeDataFim);
            this.panPeriodo.Controls.Add(this.mkeDataIni);
            this.panPeriodo.Controls.Add(this.Label1);
            this.panPeriodo.Controls.Add(this.Label2);
            this.panPeriodo.Location = new System.Drawing.Point(270, 18);
            this.panPeriodo.Name = "panPeriodo";
            this.panPeriodo.Size = new System.Drawing.Size(217, 57);
            this.panPeriodo.TabIndex = 19;
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
            // botaoCancela
            // 
            this.botaoCancela.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botaoCancela.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoCancela.ForeColor = System.Drawing.Color.Black;
            this.botaoCancela.Image = global::PafEcf.Properties.Resources.cancelar16;
            this.botaoCancela.Location = new System.Drawing.Point(367, 90);
            this.botaoCancela.Name = "botaoCancela";
            this.botaoCancela.Size = new System.Drawing.Size(120, 25);
            this.botaoCancela.TabIndex = 21;
            this.botaoCancela.Text = "Ca&ncela (ESC)";
            this.botaoCancela.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoCancela.Click += new System.EventHandler(this.botaoCancela_Click);
            // 
            // Image1
            // 
            this.Image1.Image = global::PafEcf.Properties.Resources.telaRegistradora01;
            this.Image1.Location = new System.Drawing.Point(12, 12);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(48, 48);
            this.Image1.TabIndex = 18;
            this.Image1.TabStop = false;
            // 
            // botaoConfirma
            // 
            this.botaoConfirma.Font = new System.Drawing.Font("Tahoma", 8F);
            this.botaoConfirma.ForeColor = System.Drawing.Color.Black;
            this.botaoConfirma.Image = global::PafEcf.Properties.Resources.confirmar16;
            this.botaoConfirma.Location = new System.Drawing.Point(233, 90);
            this.botaoConfirma.Name = "botaoConfirma";
            this.botaoConfirma.Size = new System.Drawing.Size(120, 25);
            this.botaoConfirma.TabIndex = 20;
            this.botaoConfirma.Text = "&Confirma (F12)";
            this.botaoConfirma.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botaoConfirma.Click += new System.EventHandler(this.botaoConfirma_Click);
            // 
            // FDavEmitidos
            // 
            this.ClientSize = new System.Drawing.Size(502, 127);
            this.Controls.Add(this.botaoCancela);
            this.Controls.Add(this.RadioGroup1);
            this.Controls.Add(this.panPeriodo);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.botaoConfirma);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(278, 302);
            this.Name = "FDavEmitidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DAV Emitidos";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FDavEmitidos_KeyDown);
            this.RadioGroup1.ResumeLayout(false);
            this.RadioGroup1.PerformLayout();
            this.panPeriodo.ResumeLayout(false);
            this.panPeriodo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.Button botaoCancela;
        private System.Windows.Forms.Panel panPeriodo;
        private System.Windows.Forms.MaskedTextBox mkeDataFim;
        private System.Windows.Forms.MaskedTextBox mkeDataIni;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.PictureBox Image1;
        private System.Windows.Forms.Button botaoConfirma;

	}
}
