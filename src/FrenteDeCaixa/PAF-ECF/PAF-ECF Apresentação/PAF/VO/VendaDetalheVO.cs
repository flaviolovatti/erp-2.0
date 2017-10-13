/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela ECF_VENDA_DETALHE

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

namespace PafEcf.VO
{


    public class VendaDetalheVO
    {


        private int FID;
        private int FID_ECF_PRODUTO;
        private int FID_ECF_VENDA_CABECALHO;
        private int FCFOP;
        private int FCOO;
        private string FSERIE_ECF;
        private int FCCF;
        private int FITEM;
        private decimal FQUANTIDADE;
        private decimal FVALOR_UNITARIO;
        private decimal FVALOR_TOTAL;
        private decimal FTOTAL_ITEM;
        private decimal FBASE_ICMS;
        private decimal FTAXA_ICMS;
        private decimal FICMS;
        private decimal FTAXA_DESCONTO;
        private decimal FDESCONTO;
        private decimal FTAXA_ISSQN;
        private decimal FISSQN;
        private decimal FTAXA_PIS;
        private decimal FPIS;
        private decimal FTAXA_COFINS;
        private decimal FCOFINS;
        private decimal FTAXA_ACRESCIMO;
        private decimal FACRESCIMO;
        private decimal FACRESCIMO_RATEIO;
        private decimal FDESCONTO_RATEIO;
        private string FTOTALIZADOR_PARCIAL;
        private string FECF_ICMS_ST;
        private string FCST;
        private string FCANCELADO;
        private string FMOVIMENTA_ESTOQUE;
        private string FHASH_TRIPA;
        private int FHASH_INCREMENTO;

        private string FGtin;
        private string FUnidadeProduto;
        private string FDescricaoPDV;
        private string FECFICMS;
        private string FIdentificacaoCliente;



        public int Id
        {
            get
            {
                return FID;
            }
            set
            {
                FID = value;
            }
        }


        public int IdProduto
        {
            get
            {
                return FID_ECF_PRODUTO;
            }
            set
            {
                FID_ECF_PRODUTO = value;
            }
        }


        public int IdVendaCabecalho
        {
            get
            {
                return FID_ECF_VENDA_CABECALHO;
            }
            set
            {
                FID_ECF_VENDA_CABECALHO = value;
            }
        }


        public int CFOP
        {
            get
            {
                return FCFOP;
            }
            set
            {
                FCFOP = value;
            }
        }


        public int Coo
        {
            get
            {
                return FCOO;
            }
            set
            {
                FCOO = value;
            }
        }


        public String SerieECF
        {
            get
            {
                return FSERIE_ECF;
            }
            set
            {
                FSERIE_ECF = value;
            }
        }


        public int Ccf
        {
            get
            {
                return FCCF;
            }
            set
            {
                FCCF = value;
            }
        }


        public int Item
        {
            get
            {
                return FITEM;
            }
            set
            {
                FITEM = value;
            }
        }


        public decimal Quantidade
        {
            get
            {
                return FQUANTIDADE;
            }
            set
            {
                FQUANTIDADE = value;
            }
        }


        public decimal ValorUnitario
        {
            get
            {
                return FVALOR_UNITARIO;
            }
            set
            {
                FVALOR_UNITARIO = value;
            }
        }


        public decimal ValorTotal
        {
            get
            {
                return FVALOR_TOTAL;
            }
            set
            {
                FVALOR_TOTAL = value;
            }
        }


        public decimal TotalItem
        {
            get
            {
                return FTOTAL_ITEM;
            }
            set
            {
                FTOTAL_ITEM = value;
            }
        }


        public decimal BaseICMS
        {
            get
            {
                return FBASE_ICMS;
            }
            set
            {
                FBASE_ICMS = value;
            }
        }


        public decimal TaxaICMS
        {
            get
            {
                return FTAXA_ICMS;
            }
            set
            {
                FTAXA_ICMS = value;
            }
        }


        public decimal ICMS
        {
            get
            {
                return FICMS;
            }
            set
            {
                FICMS = value;
            }
        }


        public decimal TaxaDesconto
        {
            get
            {
                return FTAXA_DESCONTO;
            }
            set
            {
                FTAXA_DESCONTO = value;
            }
        }


        public decimal Desconto
        {
            get
            {
                return FDESCONTO;
            }
            set
            {
                FDESCONTO = value;
            }
        }


        public decimal TaxaISSQN
        {
            get
            {
                return FTAXA_ISSQN;
            }
            set
            {
                FTAXA_ISSQN = value;
            }
        }


        public decimal ISSQN
        {
            get
            {
                return FISSQN;
            }
            set
            {
                FISSQN = value;
            }
        }


        public decimal TaxaPIS
        {
            get
            {
                return FTAXA_PIS;
            }
            set
            {
                FTAXA_PIS = value;
            }
        }


        public decimal PIS
        {
            get
            {
                return FPIS;
            }
            set
            {
                FPIS = value;
            }
        }


        public decimal TaxaCOFINS
        {
            get
            {
                return FTAXA_COFINS;
            }
            set
            {
                FTAXA_COFINS = value;
            }
        }


        public decimal COFINS
        {
            get
            {
                return FCOFINS;
            }
            set
            {
                FCOFINS = value;
            }
        }


        public decimal TaxaAcrescimo
        {
            get
            {
                return FTAXA_ACRESCIMO;
            }
            set
            {
                FTAXA_ACRESCIMO = value;
            }
        }


        public decimal Acrescimo
        {
            get
            {
                return FACRESCIMO;
            }
            set
            {
                FACRESCIMO = value;
            }
        }


        public decimal AcrescimoRateio
        {
            get
            {
                return FACRESCIMO_RATEIO;
            }
            set
            {
                FACRESCIMO_RATEIO = value;
            }
        }


        public decimal DescontoRateio
        {
            get
            {
                return FDESCONTO_RATEIO;
            }
            set
            {
                FDESCONTO_RATEIO = value;
            }
        }


        public String TotalizadorParcial
        {
            get
            {
                return FTOTALIZADOR_PARCIAL;
            }
            set
            {
                FTOTALIZADOR_PARCIAL = value;
            }
        }


        public String ECFIcmsST
        {
            get
            {
                return FECF_ICMS_ST;
            }
            set
            {
                FECF_ICMS_ST = value;
            }
        }


        public String CST
        {
            get
            {
                return FCST;
            }
            set
            {
                FCST = value;
            }
        }


        public String Cancelado
        {
            get
            {
                return FCANCELADO;
            }
            set
            {
                FCANCELADO = value;
            }
        }


        public String MovimentaEstoque
        {
            get
            {
                return FMOVIMENTA_ESTOQUE;
            }
            set
            {
                FMOVIMENTA_ESTOQUE = value;
            }
        }


        public String HashTripa
        {
            get
            {
                return FHASH_TRIPA;
            }
            set
            {
                FHASH_TRIPA = value;
            }
        }


        public int HashIncremento
        {
            get
            {
                return FHASH_INCREMENTO;
            }
            set
            {
                FHASH_INCREMENTO = value;
            }
        }



        public String GTIN
        {
            get
            {
                return FGtin;
            }
            set
            {
                FGtin = value;
            }
        }


        public String UnidadeProduto
        {
            get
            {
                return FUnidadeProduto;
            }
            set
            {
                FUnidadeProduto = value;
            }
        }


        public String DescricaoPDV
        {
            get
            {
                return FDescricaoPDV;
            }
            set
            {
                FDescricaoPDV = value;
            }
        }


        public String ECFICMS
        {
            get
            {
                return FECFICMS;
            }
            set
            {
                FECFICMS = value;
            }
        }


        public String IdentificacaoCliente
        {
            get
            {
                return FIdentificacaoCliente;
            }
            set
            {
                FIdentificacaoCliente = value;
            }
        }


    }


}