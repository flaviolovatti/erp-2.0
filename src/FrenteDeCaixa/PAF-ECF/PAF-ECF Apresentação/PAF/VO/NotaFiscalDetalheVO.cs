/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela NOTA_FISCAL_DETALHE

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



    public class NotaFiscalDetalheVO
    {


        private int FID;
        private int FID_NF_CABECALHO;
        private int FID_PRODUTO;
        private int FCFOP;
        private int FITEM;
        private decimal FQUANTIDADE;
        private decimal FVALOR_UNITARIO;
        private decimal FVALOR_TOTAL;
        private decimal FBASE_ICMS;
        private decimal FTAXA_ICMS;
        private decimal FICMS;
        private decimal FICMS_OUTRAS;
        private decimal FICMS_ISENTO;
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
        private decimal FTAXA_IPI;
        private decimal FIPI;
        private string FCANCELADO;
        private string FCST;
        private string FMOVIMENTA_ESTOQUE;
        private string FSINCRONIZADO;
        private string FECF_ICMS_ST;
        private string FDescricaoUnidade;
        private string FTotalizadorParcial;


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


        public int IdNfCabecalho
        {
            get
            {
                return FID_NF_CABECALHO;
            }
            set
            {
                FID_NF_CABECALHO = value;
            }
        }


        public int IdProduto
        {
            get
            {
                return FID_PRODUTO;
            }
            set
            {
                FID_PRODUTO = value;
            }
        }


        public int Cfop
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


        public Decimal Quantidade
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


        public Decimal ValorUnitario
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


        public Decimal ValorTotal
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


        public Decimal BaseIcms
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


        public Decimal TaxaIcms
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


        public Decimal Icms
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


        public Decimal IcmsOutras
        {
            get
            {
                return FICMS_OUTRAS;
            }
            set
            {
                FICMS_OUTRAS = value;
            }
        }


        public Decimal IcmsIsento
        {
            get
            {
                return FICMS_ISENTO;
            }
            set
            {
                FICMS_ISENTO = value;
            }
        }


        public Decimal TaxaDesconto
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


        public Decimal Desconto
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


        public Decimal TaxaIssqn
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


        public Decimal Issqn
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


        public Decimal TaxaPis
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


        public Decimal Pis
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


        public Decimal TaxaCofins
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


        public Decimal Cofins
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


        public Decimal TaxaAcrescimo
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


        public Decimal Acrescimo
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


        public Decimal TaxaIpi
        {
            get
            {
                return FTAXA_IPI;
            }
            set
            {
                FTAXA_IPI = value;
            }
        }


        public Decimal Ipi
        {
            get
            {
                return FIPI;
            }
            set
            {
                FIPI = value;
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


        public String Cst
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


        public String Sincronizado
        {
            get
            {
                return FSINCRONIZADO;
            }
            set
            {
                FSINCRONIZADO = value;
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


        public String DescricaoUnidade
        {
            get
            {
                return FDescricaoUnidade;
            }
            set
            {
                FDescricaoUnidade = value;
            }
        }


        public String TotalizadorParcial
        {
            get
            {
                return FTotalizadorParcial;
            }
            set
            {
                FTotalizadorParcial = value;
            }
        }


    }

}

