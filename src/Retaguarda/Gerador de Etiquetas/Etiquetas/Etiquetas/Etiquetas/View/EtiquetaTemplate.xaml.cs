using FastReport;
using System.Windows.Controls;

namespace Etiquetas.View
{
    /// <summary>
    /// Interaction logic for EtiquetaTemplate.xaml
    /// </summary>
    public partial class EtiquetaTemplate : UserControl
    {
        public EtiquetaTemplate()
        {
            InitializeComponent();
        }


        /// EXERCICIO
        ///  Fa�a uso do campo TABELA para trazer dados de outas tabelas do banco de dados

        /// EXERCICIO
        ///  Fa�a uso do campo CAMPO para informar qual campo deseja a impress�o
        ///  Ser� preciso inserir outros campos desse tipo na tabela Template?

        /// EXERCICIO
        ///  O relat�rio est� configurado estaticamente para imprimir EAN
        ///  Pesquise como imprimir QRCode no FastReports e implemente a solu��o

        /// EXERCICIO
        ///  Implemente o campo QUANTIDADE_REPETICOES. Ele � �til quando o usu�rio
        ///  quer imprimir uma mesma etiqueta v�rias vezes
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Report rpt = new Report();

            /// EXERCICIO
            ///  Carregue o arquivo de acordo com o ID do Layout.
            ///  Pesquise uma maneira de criar o arquivo dinamicamente de acordo
            ///  com os dados cadastrados na tabela de layout.
            //rpt.Load("c:\\testes\\1.frx");


            rpt.Load("c:\\testes\\EMBALAGEM.frx");
            rpt.SetParameterValue("XEmitente", "T2Ti.COM");
            rpt.SetParameterValue("XCodigo", "Chave de Acesso");
            rpt.SetParameterValue("XDescricao", "Produtos Diversos");
            rpt.SetParameterValue("XSSCC", "123456789123456789");
            rpt.SetParameterValue("XConteudo", "NumeroNfe");
            rpt.SetParameterValue("XQuantidade", "10");


            /// EXERCICIO
            ///  Vincule os dados aqui exibidos com os dados do relat�rio
            rpt.Prepare();
            rpt.ShowPrepared();
        }
    }
}
