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
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using PafEcf.Controller;
using PafEcf.Util;

namespace PafEcf.View
{

    //TODO:  Acompanhe o arquivo de log e corrija os erros gerados nos procedimentos de carga.
    //TODO:  Organize os arquivos do projeto em suas devidas pastas.

    public partial class FCargaPDV : Form
    {

        public static string PathVenda, PathCarga, PathCargaRemoto;
        public static string Tipo;
        public static string NomeArquivo;
        public static ProgressBar Barra;
        public static ImportaController ImportaController;
        public static LogImportacaoController LogImportacaoController;

        public FCargaPDV()
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            ImportaController = new ImportaController();
            LogImportacaoController = new LogImportacaoController();

            try
            {
                XmlDocument ArquivoXML = new XmlDocument();
                ArquivoXML.Load(Application.StartupPath + "\\Conexao.xml");
                PathCargaRemoto = ArquivoXML.GetElementsByTagName("remoteApp").Item(0).InnerText.Trim();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }

            Barra = this.ProgressBar1;
            Timer1.Enabled = true;
        }


        public static Boolean ImportaCarga(string pRemoteApp)
        {
            string Linha = "";
            string LocalApp, Compara, LogTupla = "";
            string[] Tupla;
            int i = 0;

            LocalApp = Application.StartupPath + "\\Script\\carga.txt";
            Barra.Maximum = 100;

            try
            {
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

                        //TODO:  Verifique se todas as cargas estão corretas. Corrija o que for necessário e implemente as demais.

                        if (Compara == "VCB")
                        {
                            //if (!(ImportaController.GravaCargaVendaCabecalho(Linha)))
                            //{
                            //    LogImportacaoController.GravaLogImportacao(LogTupla);
                            //}
                        }
                        else if (Compara == "VDT")
                        {
                            //if (!(ImportaController.GravaCargaVendaDetalhe(Linha)))
                            //{
                            //    LogImportacaoController.GravaLogImportacao(LogTupla);
                            //}
                        }
                        else if (Compara == "TTP")
                        {
                            //if (!(ImportaController.GravaCargaTotalTipoPagamento(Linha)))
                            //{
                            //    LogImportacaoController.GravaLogImportacao(LogTupla);
                            //}
                        }
                        else if (Compara == "SANGRIA")
                        {
                            if (!(ImportaController.GravaCargaSangria(Linha)))
                            {
                                LogImportacaoController.GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "SUPRIMENTO")
                        {
                            if (!(ImportaController.GravaCargaSuprimento(Linha)))
                            {
                                LogImportacaoController.GravaLogImportacao(LogTupla);
                            }
                        }
                        else if (Compara == "CANCELAVCB")
                        {
                        }
                        else if (Compara == "CANCELAVDT")
                        {
                        }
                        else if (Compara == "CANCELATTP")
                        {
                        }
                        else if (Compara == "CANCELANF2CAB")
                        {
                        }
                        else if (Compara == "INSERENF2CAB")
                        {
                        }
                        else if (Compara == "INSERENF2DET")
                        {
                        }
                        else if (Compara == "R02")
                        {
                        }
                        else if (Compara == "R03")
                        {
                        }
                        else if (Compara == "R06")
                        {
                        }
                        else if (Compara == "MOVIMENTOA")
                        {
                        }
                        else if (Compara == "MOVIMENTOF")
                        {
                        }
                        else if (Compara == "FECHAMENTO")
                        {
                        }
                    }

                    objReader.Close();
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


        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(PathCargaRemoto);
                foreach (FileInfo file in dir.GetFiles())
                {
                    ImportaCarga(file.FullName);
                    File.Delete(file.FullName);
                }
                Thread.Sleep(10);
                this.Refresh();
            }
            catch (Exception eError)
            {
                MessageBox.Show("Problemas na aplicação. A mesma será encerrada. Erro: " + eError.Message);
                Application.Exit();
            }
        }

    }

}
