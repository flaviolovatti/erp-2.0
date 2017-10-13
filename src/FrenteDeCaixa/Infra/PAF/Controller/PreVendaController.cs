/********************************************************************************
Title: T2TiPDV
Description: Classe de controle da pre-venda

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
using PafEcf.View;

namespace PafEcf.Controller
{

    public class PreVendaController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public PreVendaController()
        {
            conexao = dbConnection.conectarRetaguarda();
        }


        public List<PreVendaDetalheVO> CarregaPreVenda(int pId)
        {
            // verifica se existe a pre-venda solicitada
            ConsultaSQL =
              "select * from PRE_VENDA_CABECALHO where " +
              "SITUACAO <> " + Biblioteca.QuotedStr("E") + " and SITUACAO <> " + Biblioteca.QuotedStr("M") + " and ID=" + Convert.ToString(pId);

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                // caso exista a pre-venda, procede com a importacao da mesma
                if (leitor.HasRows)
                {
                    leitor.Close();
                    // verifica se existem itens para a pre-venda
                    ConsultaSQL =
                      "select * from PRE_VENDA_DETALHE where ID_PRE_VENDA_CABECALHO=" + Convert.ToString(pId);
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    leitor = comando.ExecuteReader();
                    // caso existam itens no detalhe
                    if (leitor.HasRows)
                    {
                        leitor.Close();

                        ConsultaSQL =
                          "select * from PRE_VENDA_CABECALHO where ID=" + Convert.ToString(pId);
                        comando = new MySqlCommand(ConsultaSQL, conexao);
                        leitor = comando.ExecuteReader();
                        leitor.Read();

                        if (FCaixa.Cliente == null)
                            FCaixa.Cliente = new ClienteVO();

                        FCaixa.Cliente.Id = Convert.ToInt32(leitor["ID_PESSOA"]);
                        FCaixa.Cliente.Nome = Convert.ToString(leitor["NOME_DESTINATARIO"]);
                        FCaixa.Cliente.CpfOuCnpj = Convert.ToString(leitor["CPF_CNPJ_DESTINATARIO"]);
                        FCaixa.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                        FCaixa.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);

                        leitor.Close();
                        ConsultaSQL =
                          "select * from PRE_VENDA_DETALHE where ID_PRE_VENDA_CABECALHO=" + Convert.ToString(pId);
                        comando = new MySqlCommand(ConsultaSQL, conexao);
                        leitor = comando.ExecuteReader();

                        List<PreVendaDetalheVO> ListaVenda = new List<PreVendaDetalheVO>();
                        while (leitor.Read())
                        {
                            PreVendaDetalheVO PreVendaDetalhe = new PreVendaDetalheVO();
                            PreVendaDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                            PreVendaDetalhe.IdPreVenda = pId;
                            PreVendaDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                            PreVendaDetalhe.Item = Convert.ToInt32(leitor["ITEM"]);
                            PreVendaDetalhe.Quantidade = Convert.ToInt32(leitor["QUANTIDADE"]);
                            PreVendaDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                            PreVendaDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                            PreVendaDetalhe.Cancelado = Convert.ToString(leitor["CANCELADO"]);
                            PreVendaDetalhe.GtinProduto = Convert.ToString(leitor["GTIN_PRODUTO"]);
                            PreVendaDetalhe.NomeProduto = Convert.ToString(leitor["NOME_PRODUTO"]);
                            PreVendaDetalhe.UnidadeProduto = Convert.ToString(leitor["UNIDADE_PRODUTO"]);
                            PreVendaDetalhe.ECFICMS = Convert.ToString(leitor["ECF_ICMS_ST"]);
                            ListaVenda.Add(PreVendaDetalhe);
                        }
                        return ListaVenda;
                    }
                    else
                        return null;
                }
                // caso não exista a pre-venda, retorna um ponteiro nulo
                else
                    return null;
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


        public void FechaPreVenda(int? Id, int CCF)
        {
            ConsultaSQL =
              "update PRE_VENDA_CABECALHO set " +
              "SITUACAO=?pSituacao, " +
              "CCF=?pCCF " +
              " where ID = ?pId";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pId", Id);
                comando.Parameters.AddWithValue("?pCCF", CCF);
                comando.Parameters.AddWithValue("?pSituacao", "E");
                comando.ExecuteNonQuery();
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public void MesclaPreVenda(List<PreVendaCabecalhoVO> ListaPreVendaCabecalho, List<PreVendaDetalheVO> ListaPreVendaDetalhe)
        {
            decimal TotalNovaPreVenda;
            // inicia e configura a nova Pre-Venda
            PreVendaCabecalhoVO NovaPreVenda = new PreVendaCabecalhoVO();
            NovaPreVenda.IdPessoa = ListaPreVendaCabecalho[0].IdPessoa;
            NovaPreVenda.NomeDestinatario = ListaPreVendaCabecalho[0].NomeDestinatario;
            NovaPreVenda.CpfCnpjDestinatario = ListaPreVendaCabecalho[0].CpfCnpjDestinatario;
            NovaPreVenda.DataEmissao = DateTime.Now;
            NovaPreVenda.HoraEmissao = DateTime.Now.ToString("HH:mm:ss");
            NovaPreVenda.Situacao = "P";
            TotalNovaPreVenda = 0;

            // atualiza a tabela de cabecalho
            for (int i = 0; i <= ListaPreVendaCabecalho.Count - 1; i++)
            {
                // altera a situacao da PV selecionada para M de mesclada
                ConsultaSQL =
                  "update PRE_VENDA_CABECALHO set " +
                  "SITUACAO=?pSituacao " +
                  " where ID = ?pId";

                TotalNovaPreVenda = Biblioteca.TruncaValor(TotalNovaPreVenda, Constantes.DECIMAIS_VALOR) +
                                    Biblioteca.TruncaValor(ListaPreVendaCabecalho[i].SubTotal, Constantes.DECIMAIS_VALOR);
                try
                {
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pId", ListaPreVendaCabecalho[i].Id);
                    comando.Parameters.AddWithValue("?pSituacao", "M");
                    comando.ExecuteNonQuery();
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }
            }

            // cria uma nova PV
            ConsultaSQL =
              "insert into PRE_VENDA_CABECALHO (" +
              "NOME_DESTINATARIO," +
              "CPF_CNPJ_DESTINATARIO," +
              "SUBTOTAL," +
              "DATA_PV," +
              "HORA_PV," +
              "SITUACAO, ID_PESSOA) values (" +
              "?pDestinatario," +
              "?pCPFCNPJ," +
              "?pSubTotal," +
              "?pDataEmissao," +
              "?pHoraEmissao," +
              "?psituacao, ?pIdPessoa)";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pDestinatario", NovaPreVenda.NomeDestinatario);
                comando.Parameters.AddWithValue("?pCPFCNPJ", NovaPreVenda.CpfCnpjDestinatario);
                comando.Parameters.AddWithValue("?pDataEmissao", NovaPreVenda.DataEmissao);
                comando.Parameters.AddWithValue("?pHoraEmissao", NovaPreVenda.HoraEmissao);
                comando.Parameters.AddWithValue("?pSituacao", NovaPreVenda.Situacao);
                comando.Parameters.AddWithValue("?pSubTotal", TotalNovaPreVenda);
                comando.Parameters.AddWithValue("?pIdPessoa", NovaPreVenda.IdPessoa);
                comando.ExecuteNonQuery();

                ConsultaSQL = "select max(ID) as ID from PRE_VENDA_CABECALHO";
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                NovaPreVenda.Id = Convert.ToInt32(leitor["ID"]);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }

            // atualiza a tabela de detalhes
            ConsultaSQL =
              "insert into PRE_VENDA_DETALHE (" +
              "ID_PRODUTO," +
              "ID_PRE_VENDA_CABECALHO," +
              "ITEM," +
              "QUANTIDADE," +
              "CANCELADO," +
              "GTIN_PRODUTO," +
              "NOME_PRODUTO," +
              "UNIDADE_PRODUTO," +
              "ECF_ICMS_ST," +
              "VALOR_UNITARIO," +
              "VALOR_TOTAL) " +
              " values (" +
              "?pID_PRODUTO," +
              "?pID_PRE_VENDA_CABECALHO," +
              "?pITEM," +
              "?pQUANTIDADE," +
              "?pCANCELADO," +
              "?pGTIN_PRODUTO," +
              "?pNOME_PRODUTO," +
              "?pUNIDADE_PRODUTO," +
              "?pECF_ICMS_ST," +
              "?pVALOR_UNITARIO," +
              "?pVALOR_TOTAL)";
            try
            {
                for (int i = 0; i <= ListaPreVendaDetalhe.Count - 1; i++)
                {
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pID_PRODUTO", ListaPreVendaDetalhe[i].IdProduto);
                    comando.Parameters.AddWithValue("?pID_PRE_VENDA_CABECALHO", NovaPreVenda.Id);
                    comando.Parameters.AddWithValue("?pITEM", ListaPreVendaDetalhe[i].Item);
                    comando.Parameters.AddWithValue("?pQUANTIDADE", ListaPreVendaDetalhe[i].Quantidade);
                    comando.Parameters.AddWithValue("?pCANCELADO", ListaPreVendaDetalhe[i].Cancelado);
                    comando.Parameters.AddWithValue("?pGTIN_PRODUTO", ListaPreVendaDetalhe[i].GtinProduto);
                    comando.Parameters.AddWithValue("?pNOME_PRODUTO", ListaPreVendaDetalhe[i].NomeProduto);
                    comando.Parameters.AddWithValue("?pUNIDADE_PRODUTO", ListaPreVendaDetalhe[i].UnidadeProduto);
                    comando.Parameters.AddWithValue("?pECF_ICMS_ST", ListaPreVendaDetalhe[i].ECFICMS);
                    comando.Parameters.AddWithValue("?pVALOR_UNITARIO", ListaPreVendaDetalhe[i].ValorUnitario);
                    comando.Parameters.AddWithValue("?pVALOR_TOTAL", ListaPreVendaDetalhe[i].ValorTotal);
                    comando.ExecuteNonQuery();
                }
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }

            CancelaPreVendasPendentes(ListaPreVendaCabecalho, ListaPreVendaDetalhe);
            
            FCaixa.CarregaPreVenda(Convert.ToString( NovaPreVenda.Id ));
        }


        public void CancelaPreVendasPendentes(System.DateTime pDataMovimento)
        {
            MySqlDataReader leitorInterno;

            // verifica se existem PV pendentes
            ConsultaSQL =
              "select * from PRE_VENDA_CABECALHO where " +
              "SITUACAO = " + Biblioteca.QuotedStr("P") + " and DATA_PV < ?pData ";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pData", pDataMovimento);
                leitor = comando.ExecuteReader();

                // caso existam PV pendentes procede com o processo de cancelamento de pre-vendas
                if (leitor.HasRows)
                {
                    leitor.Close();
                    List<PreVendaCabecalhoVO> ListaPreVendaCabecalho = new List<PreVendaCabecalhoVO>();
                    List<PreVendaDetalheVO> ListaPreVendaDetalhe = new List<PreVendaDetalheVO>();
                    // 
                    ConsultaSQL = "select * from PRE_VENDA_CABECALHO where " +
                    "SITUACAO = " + Biblioteca.QuotedStr("P") + " and DATA_PV < ?pData";

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pData", pDataMovimento);
                    leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        PreVendaCabecalhoVO PreVendaCabecalho = new PreVendaCabecalhoVO();
                        PreVendaCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                        PreVendaCabecalho.Valor = Convert.ToDecimal(leitor["VALOR"]);
                        ListaPreVendaCabecalho.Add(PreVendaCabecalho);

                        comando = new MySqlCommand("SELECT * FROM PRE_VENDA_DETALHE WHERE ID_PRE_VENDA_CABECALHO=" + Convert.ToString(PreVendaCabecalho.Id), conexao);
                        leitorInterno = comando.ExecuteReader();

                        while (leitorInterno.Read())
                        {
                            PreVendaDetalheVO PreVendaDetalhe = new PreVendaDetalheVO();
                            PreVendaDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                            PreVendaDetalhe.IdPreVenda = Convert.ToInt32(leitor["ID_PRE_VENDA_CABECALHO"]);
                            PreVendaDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                            PreVendaDetalhe.Item = Convert.ToInt32(leitor["ITEM"]);
                            PreVendaDetalhe.Quantidade = Convert.ToDecimal(leitor["QUANTIDADE"]);
                            PreVendaDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                            PreVendaDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                            PreVendaDetalhe.Cancelado = Convert.ToString(leitor["CANCELADO"]);
                            PreVendaDetalhe.GtinProduto = Convert.ToString(leitor["GTIN_PRODUTO"]);
                            PreVendaDetalhe.NomeProduto = Convert.ToString(leitor["NOME_PRODUTO"]);
                            PreVendaDetalhe.UnidadeProduto = Convert.ToString(leitor["UNIDADE_PRODUTO"]);
                            PreVendaDetalhe.ECFICMS = Convert.ToString(leitor["ECF_ICMS_ST"]);
                            ListaPreVendaDetalhe.Add(PreVendaDetalhe);
                        }
                    }

                    // atualiza no banco de dados
                    ConsultaSQL =
                      "update PRE_VENDA_CABECALHO set " +
                      "SITUACAO = " + Biblioteca.QuotedStr("C") +
                      " where SITUACAO = " + Biblioteca.QuotedStr("P") + " and DATA_PV < ?pData";
                    comando.Parameters.AddWithValue("?pData", pDataMovimento);
                    comando.ExecuteNonQuery();

                    CancelaPreVendasPendentes(ListaPreVendaCabecalho, ListaPreVendaDetalhe);
                }

            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public void CancelaPreVendasPendentes(List<PreVendaCabecalhoVO> ListaPreVendaCabecalho, List<PreVendaDetalheVO> ListaPreVendaDetalhe)
        {
            string Mensagem;
            int IdMovimento, ItemCupom;
            string Id;

            IdMovimento = FCaixa.Movimento.Id;

            for (int i = 0; i <= ListaPreVendaCabecalho.Count - 1; i++)
            {
                VendaCabecalhoVO VendaCabecalho = new VendaCabecalhoVO();
                List<VendaDetalheVO> ListaVendaDetalhe = new List<VendaDetalheVO>();
                VendaController VendaController = new VendaController();

                ItemCupom = 0;

                FDataModule.ACBrECF.AbreCupom();

                VendaCabecalho.IdMovimento = IdMovimento;
                VendaCabecalho.DataVenda = FDataModule.ACBrECF.DataHora;
                VendaCabecalho.HoraVenda = FDataModule.ACBrECF.DataHora.ToString("hh:mm:ss");
                VendaCabecalho.StatusVenda = "C";
                VendaCabecalho.CFOP = FCaixa.Configuracao.CFOPECF;
                VendaCabecalho.COO = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);
                VendaCabecalho.CCF = Convert.ToInt32(FDataModule.ACBrECF.NumCCF);
                VendaCabecalho.ValorVenda = ListaPreVendaCabecalho[i].Valor;
                VendaCabecalho.IdPreVenda = ListaPreVendaCabecalho[i].Id;
                VendaCabecalho = VendaController.IniciaVenda(VendaCabecalho);

                Id = Convert.ToString(ListaPreVendaCabecalho[i].Id);

                Mensagem = FCaixa.MD5 + "PV" + new string('0', 10 - Id.Length) + Id + "\r" + "\n";

                for (int j = 0; j <= ListaPreVendaDetalhe.Count - 1; j++)
                {
                    if (ListaPreVendaDetalhe[j].IdPreVenda == ListaPreVendaCabecalho[i].Id)
                    {
                        ItemCupom++;
                        VendaDetalheVO VendaDetalhe = new VendaDetalheVO();
                        ProdutoVO Produto = new ProdutoController().ConsultaId(ListaPreVendaDetalhe[j].IdProduto);
                        VendaDetalhe.IdProduto = ListaPreVendaDetalhe[j].IdProduto;
                        VendaDetalhe.CFOP = FCaixa.Configuracao.CFOPECF;
                        VendaDetalhe.IdVendaCabecalho = VendaCabecalho.Id;
                        VendaDetalhe.DescricaoPDV = Produto.DescricaoPDV;
                        VendaDetalhe.UnidadeProduto = Produto.UnidadeProduto;
                        VendaDetalhe.CST = Produto.Cst;
                        VendaDetalhe.ECFICMS = Produto.ECFICMS;
                        VendaDetalhe.TaxaICMS = Produto.AliquotaICMS;
                        VendaDetalhe.TotalizadorParcial = Produto.TotalizadorParcial;
                        VendaDetalhe.Quantidade = ListaPreVendaDetalhe[j].Quantidade;
                        VendaDetalhe.ValorUnitario = ListaPreVendaDetalhe[j].ValorUnitario;
                        VendaDetalhe.ValorTotal = ListaPreVendaDetalhe[j].ValorTotal;
                        if (Produto.GTIN.Trim() == "")
                            VendaDetalhe.GTIN = Convert.ToString(Produto.Id);
                        else
                            VendaDetalhe.GTIN = Produto.GTIN;

                        VendaDetalhe.Item = ItemCupom;
                        if (Produto.IPPT == "T")
                            VendaDetalhe.MovimentaEstoque = "S";
                        else
                            VendaDetalhe.MovimentaEstoque = "N";

                        ListaVendaDetalhe.Add(VendaDetalhe);
                        VendaController.InserirItem(VendaDetalhe);

                        FDataModule.ACBrECF.VendeItem(ListaPreVendaDetalhe[j].GtinProduto, ListaPreVendaDetalhe[j].NomeProduto, ListaPreVendaDetalhe[j].ECFICMS, Convert.ToDecimal(ListaPreVendaDetalhe[j].Quantidade), Convert.ToDecimal(ListaPreVendaDetalhe[j].ValorUnitario));
                    }
                }// for j = 0 to ListaPreVendaDetalhe.Count - 1 do

                FDataModule.ACBrECF.SubtotalizaCupom();
                FDataModule.ACBrECF.EfetuaPagamento(FDataModule.ACBrECF.FormasPagamento[0].Indice, Convert.ToDecimal(ListaPreVendaCabecalho[i].Valor));

                try
                {
                    EmpresaVO Empresa = new EmpresaController().PegaEmpresa(FCaixa.Configuracao.IdEmpresa);
                    if (Empresa.Uf == "MG")
                    {
                        Mensagem = Mensagem + "MINAS LEGAL:" +
                                 Empresa.Cnpj.Substring(8, 1) + FDataModule.ACBrECF.DataHora.ToString("ddmmyyyy");
                        if (FCaixa.VendaCabecalho.ValorFinal >= 1)
                        {
                            Mensagem = Mensagem + FCaixa.VendaCabecalho.ValorFinal.ToString();
                        }
                        Mensagem = Mensagem + "\r" + "\n";
                    }
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }

                UECF.FechaCupom(Mensagem + FCaixa.Configuracao.MensagemCupom);
                UECF.CancelaCupom();
                VendaController.CancelaVenda(VendaCabecalho, ListaVendaDetalhe);
            }// for i := 0 to ListaPreVendaCabecalho.Count - 1 do

            FCaixa.Movimento = new MovimentoController().VerificaMovimento();
        }


        public void CancelaPreVendasPendentes(int pId)
        {
            PreVendaCabecalhoVO PreVendaCabecalho = new PreVendaCabecalhoVO();
            try
            {
                List<PreVendaCabecalhoVO> ListaPreVendaCabecalho = new List<PreVendaCabecalhoVO>();
                List<PreVendaDetalheVO> ListaPreVendaDetalhe = new List<PreVendaDetalheVO>();
                // 
                ConsultaSQL = "select * from PRE_VENDA_CABECALHO where ID=" + Convert.ToString(pId);
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                {
                    PreVendaCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                    PreVendaCabecalho.Valor = Convert.ToDecimal(leitor["VALOR"]);
                    ListaPreVendaCabecalho.Add(PreVendaCabecalho);
                    leitor.Close();

                    ConsultaSQL = "SELECT * FROM PRE_VENDA_DETALHE WHERE ID_PRE_VENDA_CABECALHO=" + Convert.ToString(PreVendaCabecalho.Id);
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        PreVendaDetalheVO PreVendaDetalhe = new PreVendaDetalheVO();
                        PreVendaDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                        PreVendaDetalhe.IdPreVenda = Convert.ToInt32(leitor["ID_PRE_VENDA_CABECALHO"]);
                        PreVendaDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                        PreVendaDetalhe.Item = Convert.ToInt32(leitor["ITEM"]);
                        PreVendaDetalhe.Quantidade = Convert.ToDecimal(leitor["QUANTIDADE"]);
                        PreVendaDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                        PreVendaDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                        PreVendaDetalhe.Cancelado = Convert.ToString(leitor["CANCELADO"]);
                        PreVendaDetalhe.GtinProduto = Convert.ToString(leitor["GTIN_PRODUTO"]);
                        PreVendaDetalhe.NomeProduto = Convert.ToString(leitor["NOME_PRODUTO"]);
                        PreVendaDetalhe.UnidadeProduto = Convert.ToString(leitor["UNIDADE_PRODUTO"]);
                        PreVendaDetalhe.ECFICMS = Convert.ToString(leitor["ECF_ICMS_ST"]);
                        ListaPreVendaDetalhe.Add(PreVendaDetalhe);
                    }
                }

                if (leitor != null)
                    leitor.Close();

                // atualiza no banco de dados
                ConsultaSQL =
                  "update PRE_VENDA_CABECALHO set " +
                  "SITUACAO = " + Biblioteca.QuotedStr("C") +
                  " where ID=" + Convert.ToString(PreVendaCabecalho.Id);

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.ExecuteNonQuery();

                CancelaPreVendasPendentes(ListaPreVendaCabecalho, ListaPreVendaDetalhe);
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public List<PreVendaCabecalhoVO> TabelaPreVendaCabecalho()
        {
            List<PreVendaCabecalhoVO> ListaPreVendaCabecalho = new List<PreVendaCabecalhoVO>();

            ConsultaSQL = " SELECT * FROM PRE_VENDA_CABECALHO WHERE SITUACAO = 'P' ORDER BY ID";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    PreVendaCabecalhoVO PreVendaCabecalho = new PreVendaCabecalhoVO();

                    PreVendaCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                    PreVendaCabecalho.DataEmissao = Convert.ToDateTime(leitor["DATA_PV"]);
                    PreVendaCabecalho.HoraEmissao = leitor["HORA_PV"].ToString();
                    PreVendaCabecalho.Valor = Convert.IsDBNull(leitor["VALOR"]) ? 0 : Convert.ToDecimal(leitor["VALOR"]);

                    ListaPreVendaCabecalho.Add(PreVendaCabecalho);
                }
                return ListaPreVendaCabecalho;
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


        public List<PreVendaDetalheVO> TabelaPreVendaDetalhe(string pIdCabecalho)
        {
            List<PreVendaDetalheVO> ListaPreVendaDetalhe = new List<PreVendaDetalheVO>();

            ConsultaSQL = " SELECT * FROM PRE_VENDA_DETALHE WHERE ID_PRE_VENDA_CABECALHO = " + pIdCabecalho;

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    PreVendaDetalheVO PreVendaDetalhe = new PreVendaDetalheVO();

                    PreVendaDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                    PreVendaDetalhe.NomeProduto = leitor["NOME_PRODUTO"].ToString();
                    PreVendaDetalhe.Quantidade = Convert.ToDecimal(leitor["QUANTIDADE"]);
                    PreVendaDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                    PreVendaDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);

                    ListaPreVendaDetalhe.Add(PreVendaDetalhe);
                }
                return ListaPreVendaDetalhe;
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
    }

}