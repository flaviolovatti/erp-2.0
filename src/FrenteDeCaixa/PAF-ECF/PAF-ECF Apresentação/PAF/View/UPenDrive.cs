

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PafEcf.View
{

    public partial class FPenDrive : Form
    {

        public static string Rotina;

        public FPenDrive()
        {
            InitializeComponent();

            if (Rotina == "IMPORTA")
                Text = "Rotina de importacao de dados do Pen-Drive";
            else if (Rotina == "EXPORTA")
                Text = "Rotina de exportacao de dados para Pen-Drive";

            editPath.Text = "";
        }


        private void botaoCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FPenDrive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }


        private void SpeedButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog.ShowDialog();

            editPath.Text = OpenFileDialog.FileName;

            //TODO:  Verifique se o procedimento está funcional. Corrija se necessário.
            if (Rotina == "IMPORTA")
            {
                if (File.Exists(editPath.Text))
                {
                    FCargaPDV FCargaPDV = new FCargaPDV();
                    FCargaPDV.Tipo = "importa";
                    FCargaPDV.ShowDialog();
                }
            }
            if (Rotina == "EXPORTA")
            {
                //TODO:  Arrume essa rotina para exportar todos os arquivos de uma pasta
                if (File.Exists(editPath.Text))
                {
                    FCargaPDV FCargaPDV = new FCargaPDV();
                    FCargaPDV.Tipo = "exporta";
                    FCargaPDV.ShowDialog();
                    MessageBox.Show("Arquivos copiados para o Pen-Drive");
                }
            }

        }

    }

}
