/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela CONTAS_PAGAR_RECEBER

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


    public class ContasPagarReceberVO
    {


        private int FID;
        private int FID_ECF_VENDA_CABECALHO;
        private int FID_PLANO_CONTAS;
        private int FID_TIPO_DOCUMENTO;
        private int FID_PESSOA;
        private string FTIPO;
        private string FNUMERO_DOCUMENTO;
        private decimal FVALOR;
        private DateTime FDATA_LANCAMENTO;
        private string FPRIMEIRO_VENCIMENTO;
        private string FNATUREZA_LANCAMENTO;
        private int FQUANTIDADE_PARCELA;
        private string FDescricaoPlanoConta;
        private string FNomePessoa;
        private string FNomeTipoDocumento;


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


        public int IdEcfVendaCabecalho
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


        public int IdPlanoContas
        {
            get
            {
                return FID_PLANO_CONTAS;
            }
            set
            {
                FID_PLANO_CONTAS = value;
            }
        }


        public int IdTipoDocumento
        {
            get
            {
                return FID_TIPO_DOCUMENTO;
            }
            set
            {
                FID_TIPO_DOCUMENTO = value;
            }
        }


        public int IdPessoa
        {
            get
            {
                return FID_PESSOA;
            }
            set
            {
                FID_PESSOA = value;
            }
        }


        public String Tipo
        {
            get
            {
                return FTIPO;
            }
            set
            {
                FTIPO = value;
            }
        }


        public String NumeroDocumento
        {
            get
            {
                return FNUMERO_DOCUMENTO;
            }
            set
            {
                FNUMERO_DOCUMENTO = value;
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


        public DateTime DataLancamento
        {
            get
            {
                return FDATA_LANCAMENTO;
            }
            set
            {
                FDATA_LANCAMENTO = value;
            }
        }


        public String PrimeiroVencimento
        {
            get
            {
                return FPRIMEIRO_VENCIMENTO;
            }
            set
            {
                FPRIMEIRO_VENCIMENTO = value;
            }
        }


        public String NaturezaLancamento
        {
            get
            {
                return FNATUREZA_LANCAMENTO;
            }
            set
            {
                FNATUREZA_LANCAMENTO = value;
            }
        }


        public int QuantidadeParcela
        {
            get
            {
                return FQUANTIDADE_PARCELA;
            }
            set
            {
                FQUANTIDADE_PARCELA = value;
            }
        }


        public String DescricaoPlanoConta
        {
            get
            {
                return FDescricaoPlanoConta;
            }
            set
            {
                FDescricaoPlanoConta = value;
            }
        }


        public String NomePessoa
        {
            get
            {
                return FNomePessoa;
            }
            set
            {
                FNomePessoa = value;
            }
        }


        public String NomeTipoDocumento
        {
            get
            {
                return FNomeTipoDocumento;
            }
            set
            {
                FNomeTipoDocumento = value;
            }
        }


    }


}
