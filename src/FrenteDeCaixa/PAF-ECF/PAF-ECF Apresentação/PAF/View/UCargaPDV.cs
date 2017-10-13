/********************************************************************************
Title: T2TiPDV
Description: Controle de importacao e exportacao de arquivos

The MIT License

Copyright: Copyright (C) 2012 T2Ti.COM

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

       The author may be contacted at:
           alberteije@gmail.com

@author T2Ti.COM
@version 1.0
********************************************************************************/


using System;
using System.IO;
using System.Windows.Forms;
using PafEcf.Util;
using PafEcf.Controller;
using PafEcf.VO;

namespace PafEcf.View
{

    //TODO:  Acompanhe o arquivo de log e corrija os erros gerados nos procedimentos de carga.

    public partial class FCargaPDV : Form
    {

        public static string PathVenda, PathCarga;
        public static string Tipo;
        public static string NomeArquivo;
        public static ProgressBar Barra;

        public FCargaPDV()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            Barra = this.ProgressBar1;
            Timer1.Enabled = true;
        }

        public static bool ImportaCarga(string pRemoteApp)
        {
            try
            {
                string Linha = "";
                string LocalApp, Compara, LogTupla = "";
                String[] Tupla;
                int i = 0;
                LocalApp = Application.StartupPath + "\\Script\\carga.txt";
                Barra.Maximum = 100;

                if (File.Exists(pRemoteApp))
                {
                    Application.DoEvents();
                    File.Copy(pRemoteApp, LocalApp, true);

                    StreamReader objReader = new StreamReader(LocalApp);

                    while ((Linha = objReader.ReadLine()) != null)
                    {
                        i++;
                        if (Barra.Value == 100)
                        {
                            i = 0;
                        }
                        Barra.Value = i;
                        Tupla = Linha.Split('|');
                        LogTupla = Linha;
                        Compara = Tupla[0];

                        //TODO:  Verifique se todas as cargas estão corretas. Corrija o que for necessário. Implemente o que estiver faltando.

                        if (Compara == "BANCO")
                        {
                            if (!(new BancoController().GravaCargaBanco(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "UNIDADE")
                        {
                            if (!(new UnidadeController().GravaCargaUnidadeProduto(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "PRODUTO")
                        {
                            if (!(new ProdutoController().GravaCargaProduto(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "SITUACAO_CLI")
                        {
                            if (!(new SituacaoClienteController().GravaCargaSituacaoCliente(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "CLIENTE")
                        {
                            if (!(new ClienteController().GravaCargaCliente(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "EMPRESA")
                        {
                            if (!(new EmpresaController().GravaCargaEmpresa(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "CONTADOR")
                        {
                            if (!(new ContadorController().GravaCargaContador(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "TURNO")
                        {
                            if (!(new TurnoController().GravaCargaTurno(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "FUNCIONARIO")
                        {
                            if (!(new VendedorController().GravaCargaFuncionario(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "OPERADOR")
                        {
                            if (!(new OperadorController().GravaCargaOperador(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "CFOP")
                        {
                            if (!(new CFOPController().GravaCargaCfop(Linha)))
                            {
                                new LogImportacaoController().GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "FICHA")
                        {
                            //if (!(new FichaTecnicaController().GravaCargaFichaTecnica(Linha)))
                            //{
                            //    new LogImportacaoController().GravaLogImportacao(LogTupla);
                            //}
                        }
                        else if (Compara == "PROMOCAO")
                        {
                            //if (!(new ProdutoPromocaoController().GravaCargaProdutoPromocao(Linha))) {
                            //    new LogImportacaoController().GravaLogImportacao(LogTupla);
                            //}
                        }
                        else if (Compara == "RESOLUCAO")
                        {
                            //if (!(new ResolucaoController().GravaCargaResolucao(Linha))) {
                            //TLogImportacaoController.GravaLogImportacao(LogTupla);
                            //}
                        }
                        else if (Compara == "COMPONENTES")
                        {
                            //if (!(new ComponentesController().GravaCargaComponentes(Linha)))
                            //{
                            //    new LogImportacaoController().GravaLogImportacao(LogTupla);
                            //}
                        }
                        else if (Compara == "IMPRESSORA")
                        {
                            //if (!(new ImpressoraController().GravaCargaImpressora(Linha)))
                            //{
                            //    new LogImportacaoController().GravaLogImportacao(LogTupla);
                            //}
                        }
                        else if (Compara == "CONFIGURACAO")
                        {
                            //if (!(new ConfiguracaoController().GravaCargaConfiguracao(Linha))) {
                            //    new LogImportacaoController().GravaLogImportacao(LogTupla);
                            //}
                        }
                    }
                    objReader.Close();
                    File.Delete(pRemoteApp);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return false;
            }
        }


        public static Boolean ExportaCargaSuprimento(String pRemoteApp)
        {
            Barra.Maximum = 100;
            Barra.Value = 5;

            SuprimentoVO Suprimento = new MovimentoController().UltimoSuprimento();

            string strData = DateTime.Now.ToString("ddmmyyyyHHMMss");

            string Identificacao = "E" + FCaixa.Configuracao.IdEmpresa + "X" + FCaixa.Configuracao.NomeCaixa + "S" + Suprimento.Id + "M" + Suprimento.IdMovimento + "D" + strData;
            string Arquivo = "SUPRIMENTO_C" + FCaixa.Configuracao.IdCaixa + "E" + FCaixa.Configuracao.IdEmpresa + "-" + strData + ".txt";
            string PathSuprimento = Application.StartupPath + "\\script\\" + Arquivo;

            try
            {
                string Linha = "SUPRIMENTO|'"
                        + Identificacao + "'|'"
                        + FCaixa.Configuracao.NomeCaixa + "'|'"
                        + Suprimento.Id + "'|'"
                        + Suprimento.IdMovimento + "'|'"
                        + Suprimento.DataSuprimento.ToString("yyyy-MM-dd") + "'|'"
                        + Suprimento.Valor + "'|";

                System.IO.File.WriteAllText(PathSuprimento, Linha);

                File.Copy(PathSuprimento, pRemoteApp + Arquivo, true);

                Barra.Value = 100;

                return true;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return false;
            }
        }


        public static Boolean ExportaCargaSangria(String pRemoteApp)
        {
            Barra.Maximum = 100;
            Barra.Value = 5;

            SangriaVO Sangria = new MovimentoController().UltimaSangria();

            string strData = DateTime.Now.ToString("ddmmyyyyHHMMss");

            string Identificacao = "E" + FCaixa.Configuracao.IdEmpresa + "X" + FCaixa.Configuracao.NomeCaixa + "S" + Sangria.Id + "M" + Sangria.IdMovimento + "D" + strData;
            string Arquivo = "SANGRIA_C" + FCaixa.Configuracao.IdCaixa + "E" + FCaixa.Configuracao.IdEmpresa + "-" + strData + ".txt";
            string PathSangria = Application.StartupPath + "\\script\\" + Arquivo;

            try
            {
                string Linha = "SANGRIA|'"
                        + Identificacao + "'|'"
                        + FCaixa.Configuracao.NomeCaixa + "'|'"
                        + Sangria.Id + "'|'"
                        + Sangria.IdMovimento + "'|'"
                        + Sangria.DataSangria.ToString("yyyy-MM-dd") + "'|'"
                        + Sangria.Valor + "'|";

                System.IO.File.WriteAllText(PathSangria, Linha);

                File.Copy(PathSangria, pRemoteApp + Arquivo, true);

                Barra.Value = 100;

                return true;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return false;
            }
        }
        

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            if (Tipo == "importa")
            {
                ImportaCarga(FCaixa.PathCargaRemoto + "Caixa" + FCaixa.Configuracao.IdCaixa + "\\carga.txt");
            }
            if (Tipo == "suprimento")
            {
                ExportaCargaSuprimento(FCaixa.PathCargaRemoto);
            }
            if (Tipo == "sangria")
            {
                ExportaCargaSangria(FCaixa.PathCargaRemoto);
            }

            //TODO:  Implemente a carga para os procedimentos abaixo e os demais que achar necessário.
            /*
            if (Tipo == "venda")
            {
                ExportaCargaVenda(Caixa.pathCargaRemoto);
            }
            if (Tipo == "fechamento")
            {
                ExportaCargaFechamento(Caixa.pathCargaRemoto);
            }
            if (Tipo == "movimento")
            {
                ExportaCargaMovimento(Caixa.pathCargaRemoto);
            }
            if (Tipo == "registroR")
            {
                ExportaCargaRegistroR(Caixa.pathCargaRemoto);
            }
             */
            this.Close();
        }

    }

}
