/********************************************************************************
Title: T2TiPDV
Description: Procedimentos e fun??es da impressora fiscal.

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
using System.Windows.Forms;
using PafEcf.View;
using PafEcf.Util;
using PafEcf.VO;
using PafEcf.Controller;

namespace PafEcf.Infra
{

    public static class UECF
    {

        public static void Suprimento(decimal Valor, string Descricao)
        {
            try
            {
                FDataModule.ACBrECF.Suprimento(Valor, Descricao);
            }
            catch (Exception eError)
            {
                MessageBox.Show("Falha ao Registrar o Suprimento de Caixa! Verifique a impressora e tente novamente!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.write(eError.ToString());
                return;
            }
        }


        public static void Sangria(decimal Valor, string Descricao)
        {
            try
            {
                FDataModule.ACBrECF.Sangria(Valor, Descricao);
            }
            catch (Exception eError)
            {
                MessageBox.Show("Falha ao Registrar a Sangria de Caixa! Verifique a impressora e tente novamente!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.write(eError.ToString());
                return;
            }
        }


        public static void CancelaCupom()
        {
            try
            {
                FDataModule.ACBrECF.CancelaCupom();
            }
            catch (Exception eError)
            {
                MessageBox.Show("Falha ao Cancelar Cupom! Verifique a impressora e tente novamente!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.write(eError.ToString());
                return;
            }
            UPAF.AtualizaGT();
        }


        //TODO:  Realize diversos testes com o método abaixo e corrija os erros encontrados
        public static void ReducaoZ()
        {
            MovimentoController MovimentoController = new MovimentoController();
            ImpressoraController ImpressoraController = new ImpressoraController();
            PreVendaController PreVendaController = new PreVendaController();
            MovimentoVO Movimento = new MovimentoVO();
            ImpressoraVO Impressora = new ImpressoraVO();
            string Estado, DataMovimento;

            if (ImpressoraOK(1))
            {
                DateTime ADate = FDataModule.ACBrECF.DataMovimento;
                Estado = FDataModule.ACBrECF.Estado.ToString();
                if (Estado != "RequerZ")
                {
                    try
                    {
                        Movimento = MovimentoController.VerificaMovimento();
                        if (Movimento != null)
                        {
                            Impressora = ImpressoraController.PegaImpressora(Movimento.IdImpressora);
                            FEncerraMovimento FEncerraMovimento = new FEncerraMovimento();
                            FEncerraMovimento.AbreMovimento = false;

                            if (FEncerraMovimento.ShowDialog() != DialogResult.OK)
                            {
                                MessageBox.Show("É Necessário Encerrar o Movimento Para Emitir a Redução Z!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                PreVendaController.CancelaPreVendasPendentes(ADate);
                            }
                            FCaixa.StatusCaixa = 3;
                        }
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                    }
                }

                FCaixa.LabelMensagens.Text = "Redução Z - Aguarde!";

                FDataModule.ACBrECF.Desativar();
                FDataModule.ACBrECF.Ativar();

                UPAF.GravaR02R03();

                FDataModule.ACBrECF.ReducaoZ();

                Estado = FDataModule.ACBrECF.Estado.ToString();

                if (Estado != "Bloqueada")
                {
                    try
                    {
                        PreVendaController.CancelaPreVendasPendentes(ADate);
                        Movimento = MovimentoController.VerificaMovimento();
                        if (Movimento != null)
                        {
                            Impressora = ImpressoraController.PegaImpressora(Movimento.IdImpressora);
                            Movimento.DataFechamento = FDataModule.ACBrECF.DataHora;
                            Movimento.HoraFechamento = FDataModule.ACBrECF.DataHora.ToString("hh:mm:ss");
                            Movimento.Status = "F";
                            MovimentoController.EncerraMovimento(Movimento);

                            FEncerraMovimento FEncerraMovimento = new FEncerraMovimento();
                            FEncerraMovimento.Movimento = MovimentoController.VerificaMovimento(Movimento.Id);
                            FEncerraMovimento.ImprimeFechamento();

                            FIniciaMovimento FIniciaMovimento = new FIniciaMovimento();
                            FIniciaMovimento.ShowDialog();
                        }
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                    }
                }

                try
                {
                    DataMovimento = ADate.ToString("dd/MM/yyyy");
                    UPAF.GeraMovimentoECF(DataMovimento, DataMovimento, DataMovimento, Impressora);
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }

                if (!FDataModule.ACBrECF.MFD)
                    PrimeiraReducaoDoMes();

                FCaixa.LabelMensagens.Text = "Movimento do ECF Encerrado.";
            }
        }


        public static void LeituraX()
        {
            try
            {
                FDataModule.ACBrECF.LeituraX();
            }
            catch (Exception eError)
            {
                MessageBox.Show("Falha ao Realizar a Leitura X! Verifique a impressora e tente novamente!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.write(eError.ToString());
                return;
            }
        }


        public static void AbreCupom(string CPFouCNPJ, string Nome, string Endereco)
        {
            try
            {
                FDataModule.ACBrECF.AbreCupom(CPFouCNPJ, Nome, Endereco);
            }
            catch (Exception eError)
            {
                MessageBox.Show("Falha ao Abrir o Cupom! Verifique a impressora e tente novamente!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.write(eError.ToString());
                return;
            }
        }


        public static void VendeItem(VendaDetalheVO VendaDetalhe)
        {
            try
            {
                FDataModule.ACBrECF.VendeItem(VendaDetalhe.GTIN, VendaDetalhe.DescricaoPDV, VendaDetalhe.ECFICMS, VendaDetalhe.Quantidade, VendaDetalhe.ValorUnitario);
            }
            catch (Exception eError)
            {
                MessageBox.Show("Falha ao Registrar Item!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.write(eError.ToString());
                return;
            }
        }


        public static void EfetuaFormaPagamento(TotalTipoPagamentoVO TotalTipoPagamento)
        {
            try
            {
                FDataModule.ACBrECF.EfetuaPagamento(TotalTipoPagamento.CodigoPagamento, TotalTipoPagamento.Valor);
            }
            catch (Exception eError)
            {
                MessageBox.Show("Falha ao Efetuar Pagamento!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.write(eError.ToString());
                return;
            }
        }


        public static void SubTotalizaCupom(decimal? AscDesc)
        {
            try
            {
                FDataModule.ACBrECF.SubtotalizaCupom(AscDesc.Value, "");
            }
            catch (Exception eError)
            {
                MessageBox.Show("Falha ao Sub Totalizar o Cupom!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.write(eError.ToString());
                return;
            }
        }


        public static void FechaCupom(string Observacao)
        {
            try
            {
                FDataModule.ACBrECF.FechaCupom(Observacao);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                MessageBox.Show("Falha ao Fechar o Cupom!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UPAF.AtualizaGT();
        }


        public static void CancelaItem(int Item)
        {
            try
            {
                FDataModule.ACBrECF.CancelaItemVendido(Item);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                MessageBox.Show("Falha no Cancelamento do item!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }


        public static void PrimeiraReducaoDoMes()
        {
            int TotalRegistrosR02;
            string DataInicio, DataFim;
            DataInicio = "01" + FDataModule.ACBrECF.DataHora.ToString().Substring(8, 3);
            DataFim = "30" + FDataModule.ACBrECF.DataHora.ToString().Substring(8, 3); //TODO:  Crie uma rotina para pegar o ultimo dia do mês
            TotalRegistrosR02 = new RegistroRController().TotalR02(DataInicio, DataFim);
            if (TotalRegistrosR02 == 1)
                FDataModule.ACBrECF.LeituraMemoriaFiscal(Convert.ToDateTime(DataInicio), Convert.ToDateTime(DataFim), true);
        }


        public static bool ImpressoraOK(int Msg)
        {
            string Mensagem = "";
            string Estado = FDataModule.ACBrECF.Estado.ToString();

            if ((Estado == "Não Inicializada") || (Estado == "Desconhecido") || (Estado == "Bloqueada"))
            {
                if (Msg == 1)
                {
                    Mensagem = "Estado da Impressora: " + Estado + ".";
                }
                else if (Msg == 2)
                {
                    Mensagem = "Não é possível iniciar o movimento pois o estado da impressora é: " + Estado + ".";
                    MessageBox.Show(Mensagem, "Erro do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            else
            {
                return true;
            }
        }

    }

}
