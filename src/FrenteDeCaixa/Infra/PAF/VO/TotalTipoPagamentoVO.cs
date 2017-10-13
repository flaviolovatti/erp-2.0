/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela ECF_TOTAL_TIPO_PGTO

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


    public class TotalTipoPagamentoVO
    {


        private int FID;
        private int FID_ECF_VENDA_CABECALHO;
        private int FID_ECF_TIPO_PAGAMENTO;
        private string FSERIE_ECF;
        private int FCOO;
        private int FCCF;
        private int FGNF;
        private decimal FVALOR;
        private string FNSU;
        private string FESTORNO;
        private string FREDE;
        private string FCARTAO_DC;
        private string FSINCRONIZADO;
        private string FHASH_TRIPA;
        private int FHASH_INCREMENTO;

        private string FCodigoPagamento;
        private string FTef;
        private string FVinculado;
        private string FDescricao;
        private DateTime FDataHoraTransacao;
        private string FFinalizacao;



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


        public int IdVenda
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


        public int IdTipoPagamento
        {
            get
            {
                return FID_ECF_TIPO_PAGAMENTO;
            }
            set
            {
                FID_ECF_TIPO_PAGAMENTO = value;
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


        public int Gnf
        {
            get
            {
                return FGNF;
            }
            set
            {
                FGNF = value;
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


        public String NSU
        {
            get
            {
                return FNSU;
            }
            set
            {
                FNSU = value;
            }
        }


        public String Estorno
        {
            get
            {
                return FESTORNO;
            }
            set
            {
                FESTORNO = value;
            }
        }


        public String Rede
        {
            get
            {
                return FREDE;
            }
            set
            {
                FREDE = value;
            }
        }


        public String CartaoDebitoOuCredito
        {
            get
            {
                return FCARTAO_DC;
            }
            set
            {
                FCARTAO_DC = value;
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



        public String CodigoPagamento
        {
            get
            {
                return FCodigoPagamento;
            }
            set
            {
                FCodigoPagamento = value;
            }
        }


        public String TemTEF
        {
            get
            {
                return FTef;
            }
            set
            {
                FTef = value;
            }
        }


        public String ImprimeVinculado
        {
            get
            {
                return FVinculado;
            }
            set
            {
                FVinculado = value;
            }
        }


        public String Descricao
        {
            get
            {
                return FDescricao;
            }
            set
            {
                FDescricao = value;
            }
        }


        public DateTime DataHoraTransacao
        {
            get
            {
                return FDataHoraTransacao;
            }
            set
            {
                FDataHoraTransacao = value;
            }
        }


        public String Finalizacao
        {
            get
            {
                return FFinalizacao;
            }
            set
            {
                FFinalizacao = value;
            }
        }


    }


}
