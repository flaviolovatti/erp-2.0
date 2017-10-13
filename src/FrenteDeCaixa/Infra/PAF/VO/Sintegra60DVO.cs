/********************************************************************************
Title: T2TiPDV
Description: VO transiente que representa o registro 60D do Sintegra.

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


    public class Sintegra60DVO
    {


        private string FGTIN;
        private DateTime FDataEmissao;
        private string FSerieECF;
        private decimal FSomaQuantidade;
        private decimal FSomaValor;
        private decimal FSomaBaseICMS;
        private string FSituacaoTributaria;
        private decimal FSomaValorICMS;



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


        public DateTime DataEmissao
        {
            get
            {
                return FDataEmissao;
            }
            set
            {
                FDataEmissao = value;
            }
        }


        public String SerieECF
        {
            get
            {
                return FSerieECF;
            }
            set
            {
                FSerieECF = value;
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


        public String SituacaoTributaria
        {
            get
            {
                return FSituacaoTributaria;
            }
            set
            {
                FSituacaoTributaria = value;
            }
        }


        public decimal SomaValorICMS
        {
            get
            {
                return FSomaValorICMS;
            }
            set
            {
                FSomaValorICMS = value;
            }
        }


    }


}
