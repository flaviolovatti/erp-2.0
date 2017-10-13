/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela CONTAS_PARCELAS

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


    public class ContasParcelasVO
    {


        private int FID;
        private int FID_CONTAS_PAGAR_RECEBER;
        private int FID_MEIOS_PAGAMENTO;
        private int FID_CHEQUE_EMITIDO;
        private int FID_CONTA_CAIXA;
        private DateTime FDATA_EMISSAO;
        private DateTime FDATA_VENCIMENTO;
        private DateTime FDATA_PAGAMENTO;
        private int FNUMERO_PARCELA;
        private decimal FVALOR;
        private decimal FTAXA_JUROS;
        private decimal FTAXA_MULTA;
        private decimal FTAXA_DESCONTO;
        private decimal FVALOR_JUROS;
        private decimal FVALOR_MULTA;
        private decimal FVALOR_DESCONTO;
        private decimal FTOTAL_PARCELA;
        private string FHISTORICO;
        private string FSITUACAO;


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


        public int IdContasPagarReceber
        {
            get
            {
                return FID_CONTAS_PAGAR_RECEBER;
            }
            set
            {
                FID_CONTAS_PAGAR_RECEBER = value;
            }
        }


        public int IdMeiosPagamento
        {
            get
            {
                return FID_MEIOS_PAGAMENTO;
            }
            set
            {
                FID_MEIOS_PAGAMENTO = value;
            }
        }


        public int IdChequeEmitido
        {
            get
            {
                return FID_CHEQUE_EMITIDO;
            }
            set
            {
                FID_CHEQUE_EMITIDO = value;
            }
        }


        public int IdContaCaixa
        {
            get
            {
                return FID_CONTA_CAIXA;
            }
            set
            {
                FID_CONTA_CAIXA = value;
            }
        }


        public DateTime DataEmissao
        {
            get
            {
                return FDATA_EMISSAO;
            }
            set
            {
                FDATA_EMISSAO = value;
            }
        }


        public DateTime DataVencimento
        {
            get
            {
                return FDATA_VENCIMENTO;
            }
            set
            {
                FDATA_VENCIMENTO = value;
            }
        }


        public DateTime DataPagamento
        {
            get
            {
                return FDATA_PAGAMENTO;
            }
            set
            {
                FDATA_PAGAMENTO = value;
            }
        }


        public int NumeroParcela
        {
            get
            {
                return FNUMERO_PARCELA;
            }
            set
            {
                FNUMERO_PARCELA = value;
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


        public decimal TaxaJuros
        {
            get
            {
                return FTAXA_JUROS;
            }
            set
            {
                FTAXA_JUROS = value;
            }
        }


        public decimal TaxaMulta
        {
            get
            {
                return FTAXA_MULTA;
            }
            set
            {
                FTAXA_MULTA = value;
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


        public decimal ValorJuros
        {
            get
            {
                return FVALOR_JUROS;
            }
            set
            {
                FVALOR_JUROS = value;
            }
        }


        public decimal ValorMulta
        {
            get
            {
                return FVALOR_MULTA;
            }
            set
            {
                FVALOR_MULTA = value;
            }
        }


        public decimal ValorDesconto
        {
            get
            {
                return FVALOR_DESCONTO;
            }
            set
            {
                FVALOR_DESCONTO = value;
            }
        }


        public decimal TotalParcela
        {
            get
            {
                return FTOTAL_PARCELA;
            }
            set
            {
                FTOTAL_PARCELA = value;
            }
        }


        public String Historico
        {
            get
            {
                return FHISTORICO;
            }
            set
            {
                FHISTORICO = value;
            }
        }


        public String Situacao
        {
            get
            {
                return FSITUACAO;
            }
            set
            {
                FSITUACAO = value;
            }
        }


    }

}
