/********************************************************************************
Title: T2TiPDV
Description: VO transiente que representa o registro C425 do Sped Fiscal.

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



namespace PafEcf.VO
{


    public class SpedFiscalC370VO
    {


        private int FID_NF_CABECALHO;

        private int FID_PRODUTO;//     FDATA_EMISSAOdateYES
        private int FITEM;
        private int FID_UNIDADE_PRODUTO;
        private decimal FQUANTIDADE;
        private decimal FVALOR_TOTAL;
        private decimal FDESCONTO;



        public int IdCabecalho
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


        public int IdUnidade
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


        public decimal Valor
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


    }

}
