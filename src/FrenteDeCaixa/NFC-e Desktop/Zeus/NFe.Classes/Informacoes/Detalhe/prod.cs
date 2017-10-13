﻿/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using NFe.Classes.Informacoes.Detalhe.DeclaracaoImportacao;
using NFe.Classes.Informacoes.Detalhe.Exportacao;
using NFe.Classes.Informacoes.Detalhe.Produto_Específico;

namespace NFe.Classes.Informacoes.Detalhe
{
    public class prod
    {
        private string _nRecopi;
        private ProdutoEspecifico _produtoEspecifico;
        private decimal _qcom;
        private decimal _qtrib;
        private decimal _vprod;

        /// <summary>
        ///     I02 - Código do produto ou serviço
        /// </summary>
        public string cProd { get; set; }

        /// <summary>
        ///     I03 - GTIN (Global Trade Item Number) do produto, antigo código EAN ou código de barras
        /// </summary>
        public string cEAN { get; set; }

        /// <summary>
        ///     I04 - Descrição do produto ou serviço
        /// </summary>
        public string xProd { get; set; }

        /// <summary>
        ///     I05 - Código NCM (8 posições). Em caso de item de serviço ou item que não tenham produto (Ex. transferência de
        ///     crédito, crédito do ativo imobilizado, etc.), informar o código 00 (zeros) (v2.0)
        /// </summary>
        public string NCM { get; set; }

        /// <summary>
        ///     105a - Nomenclatura de Valor aduaneio e Estatístico
        /// </summary>
        public string NVE { get; set; }

        /// <summary>
        ///     I06 - Código EX TIPI (3 posições)
        /// </summary>
        public string EXTIPI { get; set; }

        /// <summary>
        ///     I08 - Código Fiscal de Operações e Prestações
        /// </summary>
        public int CFOP { get; set; }

        /// <summary>
        ///     I09 - Unidade comercial
        /// </summary>
        public string uCom { get; set; }

        /// <summary>
        ///     I10 - Quantidade Comercial  do produto, alterado para aceitar de 0 a 4 casas decimais e 11 inteiros.
        /// </summary>
        public decimal qCom
        {
            get { return _qcom; }
            set { _qcom = decimal.Parse(value.ToString("F4")); }
        }

        /// <summary>
        ///     I10a - Valor Unitário de Comercialização
        /// </summary>
        public decimal vUnCom { get; set; }

        /// <summary>
        ///     I11 - Valor Total Bruto dos Produtos ou Serviços
        /// </summary>
        public decimal vProd
        {
            get { return _vprod; }
            set { _vprod = decimal.Parse(value.ToString("F2")); }
        }

        /// <summary>
        ///     I12 - GTIN (Global Trade Item Number) do produto, antigo código EAN ou código de barras
        /// </summary>
        public string cEANTrib { get; set; }

        /// <summary>
        ///     I13 - Unidade Tributável
        /// </summary>
        public string uTrib { get; set; }

        /// <summary>
        ///     I14 - Quantidade Tributável
        /// </summary>
        public decimal qTrib
        {
            get { return _qtrib; }
            set { _qtrib = decimal.Parse(value.ToString("F4")); }
        }

        /// <summary>
        ///     I14a - Valor Unitário de tributação
        /// </summary>
        public decimal vUnTrib { get; set; }

        /// <summary>
        ///     I15 - Valor Total do Frete
        /// </summary>
        public decimal? vFrete { get; set; }

        /// <summary>
        ///     I16 - Valor Total do Seguro
        /// </summary>
        public decimal? vSeg { get; set; }

        /// <summary>
        ///     I17 - Valor do Desconto
        /// </summary>
        public decimal? vDesc { get; set; }

        /// <summary>
        ///     I17a - Outras despesas acessórias
        /// </summary>
        public decimal? vOutro { get; set; }

        /// <summary>
        ///     I17b - Indica se valor do Item (vProd) entra no valor total da NF-e (vProd)
        /// </summary>
        public IndicadorTotal indTot { get; set; }

        /// <summary>
        ///     I18 - Declaração de Importação
        /// </summary>
        [XmlElement("DI")]
        public List<DI> DI { get; set; }

        /// <summary>
        ///     I50 - Grupo de informações de exportação para o item
        /// </summary>
        [XmlElement("detExport")]
        public List<detExport> detExport { get; set; }

        /// <summary>
        ///     I60 - Número do Pedido de Compra
        /// </summary>
        public string xPed { get; set; }

        /// <summary>
        ///     I61 - Item do Pedido de Compra
        /// </summary>
        public int nItemPed { get; set; }

        /// <summary>
        ///     I70 - Número de controle da FCI - Ficha de Conteúdo de Importação
        /// </summary>
        public string nFCI { get; set; }

        /// <summary>
        ///     <para>129 (veicProd) - Detalhamento de Veículos novos</para>
        ///     <para>K01 (med) - Detalhamento de Medicamentos e de matérias-primas farmacêuticas</para>
        ///     <para>L01 (arma) - Detalhamento de Armamento</para>
        ///     <para>162a (comb) - Informações específicas para combustíveis líquidos e lubrificantes</para>
        /// </summary>
        [XmlElement("veicProd", typeof (veicProd))]
        [XmlElement("med", typeof (med))]
        [XmlElement("arma", typeof (arma))]
        [XmlElement("comb", typeof (comb))]
        public ProdutoEspecifico ProdutoEspecifico
        {
            get { return _produtoEspecifico; }
            set
            {
                nRECOPI = null; //ProdutoEspecifico e nRECOPI são mutuamente exclusivos
                _produtoEspecifico = value;
            }
        }

        /// <summary>
        ///     LB01 - Número do RECOPI
        /// </summary>
        public string nRECOPI
        {
            get { return _nRecopi; }
            set
            {
                if (String.IsNullOrEmpty(value)) return;
                ProdutoEspecifico = null; //ProdutoEspecifico e nRECOPI são mutuamente exclusivos
                _nRecopi = value;
            }
        }

        public bool ShouldSerializevFrete()
        {
            return vFrete.HasValue && vFrete > 0;
        }

        public bool ShouldSerializevSeg()
        {
            return vSeg.HasValue && vSeg > 0;
        }

        public bool ShouldSerializevDesc()
        {
            return vDesc.HasValue && vDesc > 0;
        }

        public bool ShouldSerializevOutro()
        {
            return vOutro.HasValue && vOutro > 0;
        }
    }
}