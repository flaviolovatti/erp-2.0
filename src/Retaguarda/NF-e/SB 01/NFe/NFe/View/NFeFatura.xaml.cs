using NFe.ServidorReference;
using NFe.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NFe.View
{
    /// <summary>
    /// Interaction logic for NFeDestinatario.xaml
    /// </summary>
    public partial class NFeFatura: UserControl
    {
        public NFeFatura()
        {
            InitializeComponent();
        }


        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    ((NFeViewModel)DataContext).ExcluirDuplicata(dataGrid.SelectedIndex);
                    dataGrid.Items.Refresh();
                }
                else
                    MessageBox.Show("Selecione um cupom fiscal a ser excluído.", "Alerta do sistema");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btIncluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NfeDuplicataDTO Duplicata = new NfeDuplicataDTO();

                Duplicata.DataVencimento = dpDuplicataData.SelectedDate;
                Duplicata.Numero = tbDuplicataNumero.Text;

                decimal aux;
                if (decimal.TryParse(tbDuplicataValor.Text, out aux))
                    Duplicata.Valor = aux;
                else
                    Duplicata.Valor = null;

                ((NFeViewModel)DataContext).IncluirDuplicata(Duplicata);

                tbDuplicataNumero.Clear();
                tbDuplicataValor.Clear();
                dpDuplicataData.SelectedDate = null;

                dataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btExcluir_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    ((NFeViewModel)DataContext).ExcluirDuplicata(dataGrid.SelectedIndex);
                    dataGrid.Items.Refresh();
                }
                else
                    MessageBox.Show("Selecione uma duplicata a ser excluída.", "Alerta do sistema");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }
    }
}
