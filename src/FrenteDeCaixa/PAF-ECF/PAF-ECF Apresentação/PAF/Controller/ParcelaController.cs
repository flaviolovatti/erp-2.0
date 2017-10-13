/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do parcelamento

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
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using PafEcf.Util;
using PafEcf.Infra;
using PafEcf.VO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using PafEcf.View;

namespace PafEcf.Controller
{


    public class ParcelaController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public ParcelaController()
        {
            conexao = dbConnection.conectar();
        }


        public ContasPagarReceberVO InserirCabecalho(ContasPagarReceberVO pParcelaCabecalho)
        {

            ConsultaSQL =
                "insert into CONTAS_PAGAR_RECEBER (" +
                  "ID_ECF_VENDA_CABECALHO, " +
                  "ID_PLANO_CONTAS, " +
                  "ID_TIPO_DOCUMENTO, " +
                  "ID_PESSOA, " +
                  "TIPO, " +
                  "NUMERO_DOCUMENTO, " +
                  "VALOR, " +
                  "DATA_LANCAMENTO, " +
                  "PRIMEIRO_VENCIMENTO, " +
                  "NATUREZA_LANCAMENTO, " +
                  "QUANTIDADE_PARCELA) " +
                "values (" +
                  "?pID_ECF_VENDA_CABECALHO, " +
                  "?pID_PLANO_CONTAS, " +
                  "?pID_TIPO_DOCUMENTO, " +
                  "?pID_PESSOA, " +
                  "?pTIPO, " +
                  "?pNUMERO_DOCUMENTO, " +
                  "?pVALOR, " +
                  "?pDATA_LANCAMENTO, " +
                  "?pPRIMEIRO_VENCIMENTO, " +
                  "?pNATUREZA_LANCAMENTO, " +
                  "?pQUANTIDADE_PARCELA) ";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pID_ECF_VENDA_CABECALHO", pParcelaCabecalho.IdEcfVendaCabecalho);
                comando.Parameters.AddWithValue("?pID_PLANO_CONTAS", pParcelaCabecalho.IdPlanoContas);
                comando.Parameters.AddWithValue("?pID_TIPO_DOCUMENTO", pParcelaCabecalho.IdTipoDocumento);
                comando.Parameters.AddWithValue("?pID_PESSOA", pParcelaCabecalho.IdPessoa);
                comando.Parameters.AddWithValue("?pTIPO", pParcelaCabecalho.Tipo);
                comando.Parameters.AddWithValue("?pNUMERO_DOCUMENTO", pParcelaCabecalho.NumeroDocumento);
                comando.Parameters.AddWithValue("?pVALOR", pParcelaCabecalho.Valor);
                comando.Parameters.AddWithValue("?pDATA_LANCAMENTO", pParcelaCabecalho.DataLancamento);
                comando.Parameters.AddWithValue("?pPRIMEIRO_VENCIMENTO", pParcelaCabecalho.PrimeiroVencimento);
                comando.Parameters.AddWithValue("?pNATUREZA_LANCAMENTO", pParcelaCabecalho.NaturezaLancamento);
                comando.Parameters.AddWithValue("?pQUANTIDADE_PARCELA", pParcelaCabecalho.QuantidadeParcela);
                comando.ExecuteNonQuery();

                ConsultaSQL = "select max(ID) as ID from CONTAS_PAGAR_RECEBER";
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                pParcelaCabecalho.Id = Convert.ToInt32(leitor["ID"]);

                return pParcelaCabecalho;
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return null;
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public void InserirDetalhe(List<ContasParcelasVO> pListaParcelaDetalhe)
        {

            ConsultaSQL = "insert into CONTAS_PARCELAS (" +
                            "ID_CONTAS_PAGAR_RECEBER, " +
                            "ID_MEIOS_PAGAMENTO, " +
                            "ID_CHEQUE_EMITIDO, " +
                            "ID_CONTA_CAIXA, " +
                            "DATA_EMISSAO, " +
                            "DATA_VENCIMENTO, " +
                            "NUMERO_PARCELA, " +
                            "VALOR, " +
                            "TAXA_JUROS, " +
                            "TAXA_MULTA, " +
                            "TAXA_DESCONTO, " +
                            "VALOR_JUROS, " +
                            "VALOR_MULTA, " +
                            "VALOR_DESCONTO, " +
                            "TOTAL_PARCELA, " +
                            "HISTORICO, " +
                            "SITUACAO) " +
                          "values (" +
                            "?pID_CONTAS_PAGAR_RECEBER, " +
                            "?pID_MEIOS_PAGAMENTO, " +
                            "?pID_CHEQUE_EMITIDO, " +
                            "?pID_CONTA_CAIXA, " +
                            "?pDATA_EMISSAO, " +
                            "?pDATA_VENCIMENTO, " +
                            "?pNUMERO_PARCELA, " +
                            "?pVALOR, " +
                            "?pTAXA_JUROS, " +
                            "?pTAXA_MULTA, " +
                            "?pTAXA_DESCONTO, " +
                            "?pVALOR_JUROS, " +
                            "?pVALOR_MULTA, " +
                            "?pVALOR_DESCONTO, " +
                            "?pTOTAL_PARCELA, " +
                            "?pHISTORICO, " +
                            "?pSITUACAO) ";
            try
            {
                ContasParcelasVO ParcelaDetalhe = new ContasParcelasVO();

                for (int i = 0; i <= pListaParcelaDetalhe.Count - 1; i++)
                {
                    ParcelaDetalhe = pListaParcelaDetalhe[i];

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pID_CONTAS_PAGAR_RECEBER", ParcelaDetalhe.IdContasPagarReceber);
                    comando.Parameters.AddWithValue("?pID_MEIOS_PAGAMENTO", ParcelaDetalhe.IdMeiosPagamento);
                    comando.Parameters.AddWithValue("?pID_CHEQUE_EMITIDO", ParcelaDetalhe.IdChequeEmitido);
                    comando.Parameters.AddWithValue("?pID_CONTA_CAIXA", ParcelaDetalhe.IdContaCaixa);
                    comando.Parameters.AddWithValue("?pDATA_EMISSAO", ParcelaDetalhe.DataEmissao);
                    comando.Parameters.AddWithValue("?pDATA_VENCIMENTO", ParcelaDetalhe.DataVencimento);
                    comando.Parameters.AddWithValue("?pNUMERO_PARCELA", ParcelaDetalhe.NumeroParcela);
                    comando.Parameters.AddWithValue("?pVALOR", ParcelaDetalhe.Valor);
                    comando.Parameters.AddWithValue("?pTAXA_JUROS", ParcelaDetalhe.TaxaJuros);
                    comando.Parameters.AddWithValue("?pTAXA_MULTA", ParcelaDetalhe.TaxaMulta);
                    comando.Parameters.AddWithValue("?pTAXA_DESCONTO", ParcelaDetalhe.TaxaDesconto);
                    comando.Parameters.AddWithValue("?pVALOR_JUROS", ParcelaDetalhe.ValorJuros);
                    comando.Parameters.AddWithValue("?pVALOR_MULTA", ParcelaDetalhe.ValorMulta);
                    comando.Parameters.AddWithValue("?pVALOR_DESCONTO", ParcelaDetalhe.ValorDesconto);
                    comando.Parameters.AddWithValue("?pTOTAL_PARCELA", ParcelaDetalhe.TotalParcela);
                    comando.Parameters.AddWithValue("?pHISTORICO", ParcelaDetalhe.Historico);
                    comando.Parameters.AddWithValue("?pSITUACAO", ParcelaDetalhe.Situacao);
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public ContasPagarReceberVO RetornaCabecalhoDaParcela(int pIdVenda)
        {

            ConsultaSQL = "SELECT * FROM CONTAS_PAGAR_RECEBER WHERE ID_ECF_VENDA_CABECALHO = " + Convert.ToString(pIdVenda);

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                if (!leitor.HasRows)
                {
                    pIdVenda = 0;
                    return null;
                }
                else
                {
                    ContasPagarReceberVO ParcelaCabecalho = new ContasPagarReceberVO();

                    ParcelaCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                    ParcelaCabecalho.IdEcfVendaCabecalho = Convert.ToInt32(leitor["ID_ECF_VENDA_CABECALHO"]);
                    ParcelaCabecalho.IdPlanoContas = Convert.ToInt32(leitor["ID_PLANO_CONTAS"]);
                    ParcelaCabecalho.IdTipoDocumento = Convert.ToInt32(leitor["ID_TIPO_DOCUMENTO"]);
                    ParcelaCabecalho.IdPessoa = Convert.ToInt32(leitor["ID_PESSOA"]);
                    ParcelaCabecalho.Tipo = Convert.ToString(leitor["TIPO"]);
                    ParcelaCabecalho.NumeroDocumento = Convert.ToString(leitor["NUMERO_DOCUMENTO"]);
                    ParcelaCabecalho.Valor = Convert.ToDecimal(leitor["VALOR"]);
                    ParcelaCabecalho.DataLancamento = (Convert.ToDateTime(leitor["DATA_LANCAMENTO"]));
                    ParcelaCabecalho.PrimeiroVencimento = (Convert.ToString(leitor["PRIMEIRO_VENCIMENTO"]));
                    ParcelaCabecalho.NaturezaLancamento = Convert.ToString(leitor["NATUREZA_LANCAMENTO"]);
                    ParcelaCabecalho.QuantidadeParcela = Convert.ToInt32(leitor["QUANTIDADE_PARCELA"]);

                    return ParcelaCabecalho;
                }

            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
                pIdVenda = 0;
                return null;
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public List<ContasParcelasVO> RetornaDetalheDaParcela(int pIdContas)
        {
            ConsultaSQL =
              "select " +
              "ID, " +
              "ID_CONTAS_PAGAR_RECEBER, " +
              "ID_MEIOS_PAGAMENTO, " +
              "ID_CHEQUE_EMITIDO, " +
              "ID_CONTA_CAIXA, " +
              "DATA_EMISSAO, " +
              "DATA_VENCIMENTO, " +
              "NUMERO_PARCELA, " +
              "VALOR, " +
              "TAXA_JUROS, " +
              "TAXA_MULTA, " +
              "TAXA_DESCONTO, " +
              "VALOR_JUROS, " +
              "VALOR_MULTA, " +
              "VALOR_DESCONTO, " +
              "TOTAL_PARCELA, " +
              "HISTORICO, " +
              "SITUACAO " +
              " from CONTAS_PARCELAS where ID_CONTAS_PAGAR_RECEBER = " + Convert.ToString(pIdContas);

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                if (!leitor.HasRows)
                {
                    return null;
                }
                else
                {
                    List<ContasParcelasVO> ListaParcela = new List<ContasParcelasVO>();

                    while (leitor.Read())
                    {
                        ContasParcelasVO ParcelaDetalhe = new ContasParcelasVO();

                        ParcelaDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                        ParcelaDetalhe.IdContasPagarReceber = Convert.ToInt32(leitor["ID_CONTAS_PAGAR_RECEBER"]);
                        ParcelaDetalhe.IdMeiosPagamento = Convert.ToInt32(leitor["ID_MEIOS_PAGAMENTO"]);
                        ParcelaDetalhe.IdChequeEmitido = Convert.ToInt32(leitor["ID_CHEQUE_EMITIDO"]);
                        ParcelaDetalhe.IdContaCaixa = Convert.ToInt32(leitor["ID_CONTA_CAIXA"]);
                        ParcelaDetalhe.DataEmissao = Convert.ToDateTime(leitor["DATA_EMISSAO"]);
                        ParcelaDetalhe.DataVencimento = Convert.ToDateTime(leitor["DATA_VENCIMENTO"]);
                        ParcelaDetalhe.NumeroParcela = Convert.ToInt32(leitor["NUMERO_PARCELA"]);
                        ParcelaDetalhe.Valor = Convert.ToDecimal(leitor["VALOR"]);
                        ParcelaDetalhe.TaxaJuros = Convert.ToDecimal(leitor["TAXA_JUROS"]);
                        ParcelaDetalhe.TaxaMulta = Convert.ToDecimal(leitor["TAXA_MULTA"]);
                        ParcelaDetalhe.TaxaDesconto = Convert.ToDecimal(leitor["TAXA_DESCONTO"]);
                        ParcelaDetalhe.ValorJuros = Convert.ToDecimal(leitor["VALOR_JUROS"]);
                        ParcelaDetalhe.ValorMulta = Convert.ToDecimal(leitor["VALOR_MULTA"]);
                        ParcelaDetalhe.ValorDesconto = Convert.ToDecimal(leitor["VALOR_DESCONTO"]);
                        ParcelaDetalhe.TotalParcela = Convert.ToDecimal(leitor["TOTAL_PARCELA"]);
                        ParcelaDetalhe.Historico = Convert.ToString(leitor["HISTORICO"]);
                        ParcelaDetalhe.Situacao = Convert.ToString(leitor["SITUACAO"]);

                        ListaParcela.Add(ParcelaDetalhe);
                    }
                    return ListaParcela;
                }
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
                pIdContas = 0;
                return null;
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public void ImprimeParcelas(string Nome, string CPF, string COO, decimal ValorTotal, List<ContasParcelasVO> pListaParcelaDetalhe)
        {
            int Elementos, Linhas, Adicional;

            string Valor, Parcela, Vencimento;
            string sContrato;
            Application.DoEvents();

            //TODO:  Carregue o texto do contrato de um arquivo externo

            sContrato = "Pelo presente instrumento particular de Confissão e Assunção de " +
           "Dívida que entre si fazem, de um lado, <NomeCliente>, inscrito no " +
           "CPF sob o nº <CPFCliente>, aqui designada simplesmente DEVEDORA e, de outro" +
           "lado, <QualificaEmpresa>, doravante denominada simplesmente CREDORA, " +
           "pactuam a CONFISSÃO E ASSUNÇÃO DE DÍVIDA, segundo as cláusulas e condições abaixo enumeradas:" +
           "01- A CREDORA ajustou com a DEVEDORA venda de mercadoria de acordo com Cupom Fiscal nº <COO>, " +
           "em data de <DataVenda>, no qual esta assumiu débito no valor de <ValorTotalVenda>; " +
           "02- Reconhecendo seu débito - em sua certeza, liquidez e exigibilidade -, a DEVEDORA se " +
           "compromete a pagar a quantia da seguinte forma:";

            sContrato = sContrato.Replace("<NomeCliente>", Nome);
            sContrato = sContrato.Replace("<CPFCliente>", CPF);
            sContrato = sContrato.Replace("<QualificaEmpresa>", new EmpresaController().PegaEmpresa(1).RazaoSocial);
            sContrato = sContrato.Replace("<COO>", COO);
            sContrato = sContrato.Replace("<DataVenda>", FDataModule.ACBrECF.DataHora.ToString("dd/MM/yyyy"));
            sContrato = sContrato.Replace("<ValorTotalVenda>", Convert.ToString(ValorTotal));


            //  INICIO Cabecalho
            FDataModule.ACBrECF.AbreRelatorioGerencial(1);
            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
            FDataModule.ACBrECF.LinhaRelatorioGerencial("        TERMO DE COMPROMISSO CONTRATUAL         ");
            FDataModule.ACBrECF.PulaLinhas(1);
            //  FIM Cabecalho

            //  INICIO CONTRATO PARAMETRIZADO
            Elementos = sContrato.Length;  //  Quantas letras tem o contrato
            Linhas = Elementos / 48;   //  divide pelo numero de colunas, no caso 48
            Adicional = Elementos % 48;   //  Caso sobre algo da divisao, indica que ha mais uma linha a ser impressa.

            if (Adicional > 0)
                Linhas = Linhas + 1;

            Elementos = 1; //  Estou reaproveitando esta variivel para fazer outro controle.

            for (int i = 1; i <= Linhas; i++)
            {
                FDataModule.ACBrECF.LinhaRelatorioGerencial(sContrato.Substring(48, Elementos));
                Elementos = Elementos + 48;
            }

            FDataModule.ACBrECF.PulaLinhas(2);
            //  FIM CONTRATO PARAMETRIZADO

            //  INICIO PARCELAS
            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('_', 48));
            FDataModule.ACBrECF.LinhaRelatorioGerencial("            VALOR       PARCELA    VENCIMENTO   ");
            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('_', 48));

            for (int i = 0; i <= pListaParcelaDetalhe.Count - 1; i++)
            {
                ContasParcelasVO ParcelaDetalhe = pListaParcelaDetalhe[i];

                Valor = ParcelaDetalhe.Valor.ToString("###,##0.00");
                Valor = new string(' ', 17 - Valor.Length) + Valor;
                Parcela = Convert.ToString(ParcelaDetalhe.NumeroParcela);
                Parcela = new string(' ', 11 - Parcela.Length) + Parcela;
                Vencimento = ParcelaDetalhe.DataVencimento.ToString("dd/MM/yyyy");
                Vencimento = new string(' ', 17 - Vencimento.Length) + Vencimento;

                FDataModule.ACBrECF.LinhaRelatorioGerencial(Valor + Parcela + Vencimento);
            }

            FDataModule.ACBrECF.PulaLinhas(4);
            //  FIM PARCELAS

            //  INICIO RODAPÉ
            FDataModule.ACBrECF.LinhaRelatorioGerencial("    ________________________________________    ");
            FDataModule.ACBrECF.LinhaRelatorioGerencial(Nome.Substring(40, 1));
            FDataModule.ACBrECF.LinhaRelatorioGerencial(CPF);
            FDataModule.ACBrECF.PulaLinhas(2);
            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
            FDataModule.ACBrECF.FechaRelatorio();
            UPAF.GravaR06("RG");
        }

    }

}
