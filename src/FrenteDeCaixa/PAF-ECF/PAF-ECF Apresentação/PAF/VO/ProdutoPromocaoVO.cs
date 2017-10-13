/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela PRODUTO_PROMOCAO

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


    public class ProdutoPromocaoVO
    {


        private int FID;
        private int FID_PRODUTO;
        private DateTime FDATA_INICIO;
        private DateTime FDATA_FIM;
        private decimal FQUANTIDADE_EM_PROMOCAO;
        private decimal QUANTIDADE_MAXIMA_CLIENTE;
        private decimal FVALOR;



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
                return FID_PRODUTO;
            }
            set
            {
                FID_PRODUTO = value;
            }
        }


        public DateTime DataInicio
        {
            get
            {
                return FDATA_INICIO;
            }
            set
            {
                FDATA_INICIO = value;
            }
        }


        public DateTime DataFim
        {
            get
            {
                return FDATA_FIM;
            }
            set
            {
                FDATA_FIM = value;
            }
        }


        public decimal QuantidadeEmPromocao
        {
            get
            {
                return FQUANTIDADE_EM_PROMOCAO;
            }
            set
            {
                FQUANTIDADE_EM_PROMOCAO = value;
            }
        }


        public decimal QuantidadeMaximaPorCliente
        {
            get
            {
                return QUANTIDADE_MAXIMA_CLIENTE;
            }
            set
            {
                QUANTIDADE_MAXIMA_CLIENTE = value;
            }
        }


        public decimal Valor
        {
            get
            {
                return FVALOR;
            }
            set
            {
                FVALOR = value;
            }
        }


    }

}
