/********************************************************************************
Title: T2TiPDV
Description: VO transiente para carregar dados do Sintegra

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


    public class SintegraVO
    {


        private int FID;
        private int FID_SINTEGRA_60M;
        private string FSITUACAO_TRIBUTARIA;
        private decimal FOutras;
        private string FSituacao;
        private decimal FAliquota;
        private decimal FIsentas;
        private decimal FIcms;
        private decimal FValorContabil;
        private decimal FBasedeCalculo;
        private string FEmissorDocumento;
        private string FCfop;
        private string FInscricao;
        private string FUF;
        private string FSerie;
        private string FCPFCNPJ;
        private string FModelo;
        private string FNumero;
        private DateTime FDataDocumento;
        private string FString;
        private string FEstado;
        private string FCodigoAntecipacao;
        private decimal FBaseST;
        private string FEmitente;
        private decimal FDespesas;
        private decimal FIcmsRetido;
        private decimal FValorIpi;
        private decimal FValorIsentas;
        private decimal FValorOutras;

        private string FNumeroItem;
        private string FDescricao;
        private string FCST;
        private string FCodigo;
        private decimal FQuantidade;
        private decimal FValor;
        private decimal FValorDescontoDespesa;
        private string FNCM;
        private string FUnidade;
        private decimal FAliquotaIpi;
        private decimal FAliquotaICMS;
        private decimal FReducao;


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


        public int Id60M
        {
            get
            {
                return FID_SINTEGRA_60M;
            }
            set
            {
                FID_SINTEGRA_60M = value;
            }
        }


        public String SituacaoTributaria
        {
            get
            {
                return FSITUACAO_TRIBUTARIA;
            }
            set
            {
                FSITUACAO_TRIBUTARIA = value;
            }
        }


        public string CPFCNPJ
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


        public string Inscricao
        {
            get
            {
                return FInscricao;
            }
            set
            {
                FInscricao = value;
            }
        }


        public DateTime DataDocumento
        {
            get
            {
                return FDataDocumento;
            }
            set
            {
                FDataDocumento = value;
            }
        }


        public string UF
        {
            get
            {
                return FUF;
            }
            set
            {
                FUF = value;
            }
        }


        public string Modelo
        {
            get
            {
                return FModelo;
            }
            set
            {
                FModelo = value;
            }
        }


        public string Serie
        {
            get
            {
                return FSerie;
            }
            set
            {
                FSerie = value;
            }
        }


        public string Numero
        {
            get
            {
                return FNumero;
            }
            set
            {
                FNumero = value;
            }
        }


        public string Cfop
        {
            get
            {
                return FCfop;
            }
            set
            {
                FCfop = value;
            }
        }


        public string EmissorDocumento
        {
            get
            {
                return FEmissorDocumento;
            }
            set
            {
                FEmissorDocumento = value;
            }
        }


        public Decimal ValorContabil
        {
            get
            {
                return FValorContabil;
            }
            set
            {
                FValorContabil = value;
            }
        }


        public Decimal BasedeCalculo
        {
            get
            {
                return FBasedeCalculo;
            }
            set
            {
                FBasedeCalculo = value;
            }
        }


        public Decimal Icms
        {
            get
            {
                return FIcms;
            }
            set
            {
                FIcms = value;
            }
        }


        public Decimal Isentas
        {
            get
            {
                return FIsentas;
            }
            set
            {
                FIsentas = value;
            }
        }


        public Decimal Outras
        {
            get
            {
                return FOutras;
            }
            set
            {
                FOutras = value;
            }
        }


        public Decimal Aliquota
        {
            get
            {
                return FAliquota;
            }
            set
            {
                FAliquota = value;
            }
        }


        public string Situacao
        {
            get
            {
                return FSituacao;
            }
            set
            {
                FSituacao = value;
            }
        }


        public string Emitente
        {
            get
            {
                return FEmitente;
            }
            set
            {
                FEmitente = value;
            }
        }


        public Decimal BaseST
        {
            get
            {
                return FBaseST;
            }
            set
            {
                FBaseST = value;
            }
        }


        public Decimal IcmsRetido
        {
            get
            {
                return FIcmsRetido;
            }
            set
            {
                FIcmsRetido = value;
            }
        }


        public Decimal Despesas
        {
            get
            {
                return FDespesas;
            }
            set
            {
                FDespesas = value;
            }
        }


        public string CodigoAntecipacao
        {
            get
            {
                return FCodigoAntecipacao;
            }
            set
            {
                FCodigoAntecipacao = value;
            }
        }


        public Decimal ValorIpi
        {
            get
            {
                return FValorIpi;
            }
            set
            {
                FValorIpi = value;
            }
        }


        public Decimal ValorOutras
        {
            get
            {
                return FValorOutras;
            }
            set
            {
                FValorOutras = value;
            }
        }


        public Decimal ValorIsentas
        {
            get
            {
                return FValorIsentas;
            }
            set
            {
                FValorIsentas = value;
            }
        }



        public string NumeroItem
        {
            get
            {
                return FNumeroItem;
            }
            set
            {
                FNumeroItem = value;
            }
        }


        public string Descricao
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


        public string CST
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


        public string Codigo
        {
            get
            {
                return FCodigo;
            }
            set
            {
                FCodigo = value;
            }
        }


        public string NCM
        {
            get
            {
                return FNCM;
            }
            set
            {
                FNCM = value;
            }
        }


        public string Unidade
        {
            get
            {
                return FUnidade;
            }
            set
            {
                FUnidade = value;
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


        public decimal Valor
        {
            get
            {
                return FValor;
            }
            set
            {
                FValor = value;
            }
        }


        public decimal AliquotaIpi
        {
            get
            {
                return FAliquotaIpi;
            }
            set
            {
                FAliquotaIpi = value;
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


        public decimal Reducao
        {
            get
            {
                return FReducao;
            }
            set
            {
                FReducao = value;
            }
        }


    }

}
