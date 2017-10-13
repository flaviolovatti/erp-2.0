/********************************************************************************
Title: T2TiPDV
Description: VO transiente que representa o registro 321 do Sped Fiscal.

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


    public class SpedFiscalC321VO
    {


        private int FIdProduto;
        private decimal FSomaQuantidade;
        private string FDescricaoUnidade;
        private decimal FSomaValor;
        private decimal FSomaDesconto;
        private decimal FSomaBaseICMS;
        private decimal FSomaICMS;
        private decimal FSomaPIS;
        private decimal FSomaCOFINS;



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


        public decimal SomaQuantidade
        {
            get
            {
                return FSomaQuantidade;
            }
            set
            {
                FSomaQuantidade = value;
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


        public decimal SomaValor
        {
            get
            {
                return FSomaValor;
            }
            set
            {
                FSomaValor = value;
            }
        }


        public decimal SomaDesconto
        {
            get
            {
                return FSomaDesconto;
            }
            set
            {
                FSomaDesconto = value;
            }
        }


        public decimal SomaBaseICMS
        {
            get
            {
                return FSomaBaseICMS;
            }
            set
            {
                FSomaBaseICMS = value;
            }
        }


        public decimal SomaICMS
        {
            get
            {
                return FSomaICMS;
            }
            set
            {
                FSomaICMS = value;
            }
        }


        public decimal SomaPIS
        {
            get
            {
                return FSomaPIS;
            }
            set
            {
                FSomaPIS = value;
            }
        }


        public decimal SomaCOFINS
        {
            get
            {
                return FSomaCOFINS;
            }
            set
            {
                FSomaCOFINS = value;
            }
        }


    }

}
