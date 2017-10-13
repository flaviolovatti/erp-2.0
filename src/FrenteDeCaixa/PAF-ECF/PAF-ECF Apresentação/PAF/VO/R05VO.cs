/********************************************************************************
Title: T2TiPDV
Description: VO transiente. Montará os dados necessários para o registro R05.

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


    public class R05VO
    {



        private int FID;
        private int FCOO;
        private int FCCF;
        private string FSERIE_ECF;
        private int FItem;
        private string FGTIN;
        private string FDescricaoPDV;
        private decimal FQuantidade;
        private string FSiglaUnidade;
        private decimal FValorUnitario;
        private decimal FDesconto;
        private decimal FAcrescimo;
        private decimal FTotalItem;
        private string FTotalizadorParcial;
        private string FIndicadorCancelamento;
        private decimal FQuantidadeCancelada;
        private decimal FValorCancelado;
        private decimal FCancelamentoAcrescimo;
        private string FIAT;
        private string FIPPT;
        private int FCasasDecimaisQuantidade;
        private int FCasasDecimaisValor;
        private string FHASH_TRIPA;
        private int FHASH_INCREMENTO;


        private int FIdProduto;// Utilizados pelo Sped Fiscal
        private int FIdUnidade;
        private string FCST;
        private int FCFOP;
        private decimal FAliquotaICMS;
        private decimal FPIS;
        private decimal FCOFINS;



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


        public int COO
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


        public int CCF
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
                return FItem;
            }
            set
            {
                FItem = value;
            }
        }


        public String SerieEcf
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


        public String GTIN
        {
            get
            {
                return FGTIN;
            }
            set
            {
                FGTIN = value;
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


        public decimal Quantidade
        {
            get
            {
                return FQuantidade;
            }
            set
            {
                FQuantidade = value;
            }
        }


        public String SiglaUnidade
        {
            get
            {
                return FSiglaUnidade;
            }
            set
            {
                FSiglaUnidade = value;
            }
        }


        public decimal ValorUnitario
        {
            get
            {
                return FValorUnitario;
            }
            set
            {
                FValorUnitario = value;
            }
        }


        public decimal Desconto
        {
            get
            {
                return FDesconto;
            }
            set
            {
                FDesconto = value;
            }
        }


        public decimal Acrescimo
        {
            get
            {
                return FAcrescimo;
            }
            set
            {
                FAcrescimo = value;
            }
        }


        public decimal TotalItem
        {
            get
            {
                return FTotalItem;
            }
            set
            {
                FTotalItem = value;
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


        public String IndicadorCancelamento
        {
            get
            {
                return FIndicadorCancelamento;
            }
            set
            {
                FIndicadorCancelamento = value;
            }
        }


        public decimal QuantidadeCancelada
        {
            get
            {
                return FQuantidadeCancelada;
            }
            set
            {
                FQuantidadeCancelada = value;
            }
        }


        public decimal ValorCancelado
        {
            get
            {
                return FValorCancelado;
            }
            set
            {
                FValorCancelado = value;
            }
        }


        public decimal CancelamentoAcrescimo
        {
            get
            {
                return FCancelamentoAcrescimo;
            }
            set
            {
                FCancelamentoAcrescimo = value;
            }
        }


        public String IAT
        {
            get
            {
                return FIAT;
            }
            set
            {
                FIAT = value;
            }
        }


        public String IPPT
        {
            get
            {
                return FIPPT;
            }
            set
            {
                FIPPT = value;
            }
        }


        public int CasasDecimaisQuantidade
        {
            get
            {
                return FCasasDecimaisQuantidade;
            }
            set
            {
                FCasasDecimaisQuantidade = value;
            }
        }


        public int CasasDecimaisValor
        {
            get
            {
                return FCasasDecimaisValor;
            }
            set
            {
                FCasasDecimaisValor = value;
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




        // Utilizados pelo Sped Fiscal
        public int IdProduto
        {
            get
            {
                return FIdProduto;
            }
            set
            {
                FIdProduto = value;
            }
        }


        public int IdUnidade
        {
            get
            {
                return FIdUnidade;
            }
            set
            {
                FIdUnidade = value;
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


        public decimal AliquotaICMS
        {
            get
            {
                return FAliquotaICMS;
            }
            set
            {
                FAliquotaICMS = value;
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


    }


}
