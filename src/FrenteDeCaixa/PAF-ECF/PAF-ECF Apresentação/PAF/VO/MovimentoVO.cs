/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela ECF_MOVIMENTO

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


    public class MovimentoVO
    {


        private int FID;
        private int FID_ECF_EMPRESA;
        private int FID_ECF_TURNO;
        private int FID_ECF_IMPRESSORA;
        private int FID_ECF_OPERADOR;
        private int FID_ECF_CAIXA;
        private int FID_GERENTE_SUPERVISOR;
        private DateTime? FDATA_ABERTURA;
        private string FHORA_ABERTURA;
        private DateTime? FDATA_FECHAMENTO;
        private string FHORA_FECHAMENTO;
        private decimal? FTOTAL_SUPRIMENTO;
        private decimal? FTOTAL_SANGRIA;
        private decimal? FTOTAL_NAO_FISCAL;
        private decimal? FTOTAL_VENDA;
        private decimal? FTOTAL_DESCONTO;
        private decimal? FTOTAL_ACRESCIMO;
        private decimal? FTOTAL_FINAL;
        private decimal? FTOTAL_RECEBIDO;
        private decimal? FTOTAL_TROCO;
        private decimal? FTOTAL_CANCELADO;
        private string FSTATUS_MOVIMENTO;
        private string FSINCRONIZADO;

        private string FIdentificacaoImpressora;
        private string FDescricaoTurno;
        private string FLoginOperador;
        private string FNomeCaixa;



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


        public int IdEmpresa
        {
            get
            {
                return FID_ECF_EMPRESA;
            }
            set
            {
                FID_ECF_EMPRESA = value;
            }
        }


        public int IdTurno
        {
            get
            {
                return FID_ECF_TURNO;
            }
            set
            {
                FID_ECF_TURNO = value;
            }
        }


        public int IdImpressora
        {
            get
            {
                return FID_ECF_IMPRESSORA;
            }
            set
            {
                FID_ECF_IMPRESSORA = value;
            }
        }


        public int IdOperador
        {
            get
            {
                return FID_ECF_OPERADOR;
            }
            set
            {
                FID_ECF_OPERADOR = value;
            }
        }


        public int IdCaixa
        {
            get
            {
                return FID_ECF_CAIXA;
            }
            set
            {
                FID_ECF_CAIXA = value;
            }
        }


        public int IdGerenteSupervisor
        {
            get
            {
                return FID_GERENTE_SUPERVISOR;
            }
            set
            {
                FID_GERENTE_SUPERVISOR = value;
            }
        }


        public DateTime? DataAbertura
        {
            get
            {
                return FDATA_ABERTURA;
            }
            set
            {
                FDATA_ABERTURA = value;
            }
        }


        public String HoraAbertura
        {
            get
            {
                return FHORA_ABERTURA;
            }
            set
            {
                FHORA_ABERTURA = value;
            }
        }


        public DateTime? DataFechamento
        {
            get
            {
                return FDATA_FECHAMENTO;
            }
            set
            {
                FDATA_FECHAMENTO = value;
            }
        }


        public String HoraFechamento
        {
            get
            {
                return FHORA_FECHAMENTO;
            }
            set
            {
                FHORA_FECHAMENTO = value;
            }
        }


        public decimal? TotalSuprimento
        {
            get
            {
                return FTOTAL_SUPRIMENTO;
            }
            set
            {
                FTOTAL_SUPRIMENTO = value;
            }
        }


        public decimal? TotalSangria
        {
            get
            {
                return FTOTAL_SANGRIA;
            }
            set
            {
                FTOTAL_SANGRIA = value;
            }
        }


        public decimal? TotalNaoFiscal
        {
            get
            {
                return FTOTAL_NAO_FISCAL;
            }
            set
            {
                FTOTAL_NAO_FISCAL = value;
            }
        }


        public decimal? TotalVenda
        {
            get
            {
                return FTOTAL_VENDA;
            }
            set
            {
                FTOTAL_VENDA = value;
            }
        }


        public decimal? TotalDesconto
        {
            get
            {
                return FTOTAL_DESCONTO;
            }
            set
            {
                FTOTAL_DESCONTO = value;
            }
        }


        public decimal? TotalAcrescimo
        {
            get
            {
                return FTOTAL_ACRESCIMO;
            }
            set
            {
                FTOTAL_ACRESCIMO = value;
            }
        }


        public decimal? TotalFinal
        {
            get
            {
                return FTOTAL_FINAL;
            }
            set
            {
                FTOTAL_FINAL = value;
            }
        }


        public decimal? TotalRecebido
        {
            get
            {
                return FTOTAL_RECEBIDO;
            }
            set
            {
                FTOTAL_RECEBIDO = value;
            }
        }


        public decimal? TotalTroco
        {
            get
            {
                return FTOTAL_TROCO;
            }
            set
            {
                FTOTAL_TROCO = value;
            }
        }


        public decimal? TotalCancelado
        {
            get
            {
                return FTOTAL_CANCELADO;
            }
            set
            {
                FTOTAL_CANCELADO = value;
            }
        }


        public String Status
        {
            get
            {
                return FSTATUS_MOVIMENTO;
            }
            set
            {
                FSTATUS_MOVIMENTO = value;
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



        public String IdentificacaoImpressora
        {
            get
            {
                return FIdentificacaoImpressora;
            }
            set
            {
                FIdentificacaoImpressora = value;
            }
        }


        public String DescricaoTurno
        {
            get
            {
                return FDescricaoTurno;
            }
            set
            {
                FDescricaoTurno = value;
            }
        }


        public String LoginOperador
        {
            get
            {
                return FLoginOperador;
            }
            set
            {
                FLoginOperador = value;
            }
        }


        public String NomeCaixa
        {
            get
            {
                return FNomeCaixa;
            }
            set
            {
                FNomeCaixa = value;
            }
        }


    }

}