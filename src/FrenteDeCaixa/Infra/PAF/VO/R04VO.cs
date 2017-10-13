/********************************************************************************
Title: T2TiPDV
Description: VO transiente. Montará os dados necessários para o registro R04.

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


    public class R04VO
    {



        private int FId;
        private int FIdOperador;
        private string FSERIE_ECF;
        private int FCCF;
        private int FCOO;
        private DateTime FDataEmissao;
        private decimal FSubTotal;
        private decimal FDesconto;
        private string FIndicadorDesconto;
        private decimal FAcrescimo;
        private string FIndicadorAcrescimo;
        private decimal FValorLiquido;

        private decimal FCancelamentoAcrescimo;//  FCancelado: String;
        private string FOrdemDescontoAcrescimo;
        private string FCliente;
        private string FCPFCNPJ;
        private string FHASH_TRIPA;
        private int FHASH_INCREMENTO;
        private string FCUPOM_CANCELADO;
        private string FSTATUS_VENDA;


        private decimal FPIS;// Utilizados pelo Sped Fiscal
        private decimal FCOFINS;



        public int Id
        {
            get
            {
                return FId;
            }
            set
            {
                FId = value;
            }
        }


        public int IdOperador
        {
            get
            {
                return FIdOperador;
            }
            set
            {
                FIdOperador = value;
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


        public decimal SubTotal
        {
            get
            {
                return FSubTotal;
            }
            set
            {
                FSubTotal = value;
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


        public String IndicadorDesconto
        {
            get
            {
                return FIndicadorDesconto;
            }
            set
            {
                FIndicadorDesconto = value;
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


        public String IndicadorAcrescimo
        {
            get
            {
                return FIndicadorAcrescimo;
            }
            set
            {
                FIndicadorAcrescimo = value;
            }
        }


        public decimal ValorLiquido
        {
            get
            {
                return FValorLiquido;
            }
            set
            {
                FValorLiquido = value;
            }
        }


        public String Cancelado
        {
            get
            {
                return FCUPOM_CANCELADO;
            }
            set
            {
                FCUPOM_CANCELADO = value;
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


        public String OrdemDescontoAcrescimo
        {
            get
            {
                return FOrdemDescontoAcrescimo;
            }
            set
            {
                FOrdemDescontoAcrescimo = value;
            }
        }


        public String Cliente
        {
            get
            {
                return FCliente;
            }
            set
            {
                FCliente = value;
            }
        }


        public String CPFCNPJ
        {
            get
            {
                return FCPFCNPJ;
            }
            set
            {
                FCPFCNPJ = value;
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


        public String StatusVenda
        {
            get
            {
                return FSTATUS_VENDA;
            }
            set
            {
                FSTATUS_VENDA = value;
            }
        }



        // Utilizados pelo Sped Fiscal
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
