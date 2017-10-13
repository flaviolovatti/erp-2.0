/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela ECF_VENDA_CABECALHO

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
	
	public class VendaCabecalhoVO 
	{
		    private int FID; 
		    private int? FID_CLIENTE; 
		    private int? FID_ECF_FUNCIONARIO; 
		    private int? FID_ECF_MOVIMENTO; 
		    private int? FID_ECF_DAV; 
		    private int? FID_ECF_PRE_VENDA_CABECALHO; 
		    private string FSERIE_ECF; 
		    private int FCFOP; 
		    private int FCOO; 
		    private int FCCF;
            private DateTime FDATA_VENDA; 
		    private string FHORA_VENDA; 
		    private decimal? FVALOR_VENDA; 
		    private decimal? FTAXA_DESCONTO; 
		    private decimal? FDESCONTO; 
		    private decimal? FTAXA_ACRESCIMO; 
		    private decimal? FACRESCIMO; 
		    private decimal? FVALOR_FINAL; 
		    private decimal? FVALOR_RECEBIDO; 
		    private decimal? FTROCO; 
		    private decimal? FVALOR_CANCELADO; 
		    private string FSINCRONIZADO; 
		    private decimal? FTOTAL_PRODUTOS; 
		    private decimal? FTOTAL_DOCUMENTO; 
		    private decimal? FBASE_ICMS; 
		    private decimal? FICMS; 
		    private decimal? FICMS_OUTRAS; 
		    private decimal? FISSQN; 
		    private decimal? FPIS; 
		    private decimal? FCOFINS; 
		    private decimal? FACRESCIMO_ITENS; 
		    private decimal? FDESCONTO_ITENS; 
		    private string FSTATUS_VENDA; 
		    private string FNOME_CLIENTE; 
		    private string FCPF_CNPJ_CLIENTE; 
		    private string FCUPOM_CANCELADO; 
		    private string FHASH_TRIPA; 
		    private int FHASH_INCREMENTO; 
		
		  
		
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
		    
		
		    public int? IdCliente
		    {
		    	get
		    	{
		    		return FID_CLIENTE;
		    	}
		    	set
		    	{
		    		FID_CLIENTE = value;
		    	}
		    }
		    
		
		    public int? IdVendedor
		    {
		    	get
		    	{
		    		return FID_ECF_FUNCIONARIO;
		    	}
		    	set
		    	{
		    		FID_ECF_FUNCIONARIO = value;
		    	}
		    }
		    
		
		    public int? IdMovimento
		    {
		    	get
		    	{
		    		return FID_ECF_MOVIMENTO;
		    	}
		    	set
		    	{
		    		FID_ECF_MOVIMENTO = value;
		    	}
		    }
		    
		
		    public int? IdDAV
		    {
		    	get
		    	{
		    		return FID_ECF_DAV;
		    	}
		    	set
		    	{
		    		FID_ECF_DAV = value;
		    	}
		    }
		    
		
		    public int? IdPreVenda
		    {
		    	get
		    	{
		    		return FID_ECF_PRE_VENDA_CABECALHO;
		    	}
		    	set
		    	{
		    		FID_ECF_PRE_VENDA_CABECALHO = value;
		    	}
		    }
		    
		
		    public string SerieEcf
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
		    
		
		    public int CFOP
		    {
		    	get
		    	{
		    		return FCFOP;
		    	}
		    	set
		    	{
		    		FCFOP = value;
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


            public DateTime DataVenda
		    {
		    	get
		    	{
		    		return FDATA_VENDA;
		    	}
		    	set
		    	{
		    		FDATA_VENDA = value;
		    	}
		    }
		    
		
		    public String HoraVenda
		    {
		    	get
		    	{
		    		return FHORA_VENDA;
		    	}
		    	set
		    	{
		    		FHORA_VENDA = value;
		    	}
		    }
		    
		
		    public decimal? ValorVenda
		    {
		    	get
		    	{
		    		return FVALOR_VENDA;
		    	}
		    	set
		    	{
		    		FVALOR_VENDA = value;
		    	}
		    }
		    
		
		    public decimal? TaxaDesconto
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
		    
		
		    public decimal? Desconto
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
		    
		
		    public decimal? TaxaAcrescimo
		    {
		    	get
		    	{
		    		return FTAXA_ACRESCIMO;
		    	}
		    	set
		    	{
		    		FTAXA_ACRESCIMO = value;
		    	}
		    }
		    
		
		    public decimal? Acrescimo
		    {
		    	get
		    	{
		    		return FACRESCIMO;
		    	}
		    	set
		    	{
		    		FACRESCIMO = value;
		    	}
		    }
		    
		
		    public decimal? ValorFinal
		    {
		    	get
		    	{
		    		return FVALOR_FINAL;
		    	}
		    	set
		    	{
		    		FVALOR_FINAL = value;
		    	}
		    }
		    
		
		    public decimal? ValorRecebido
		    {
		    	get
		    	{
		    		return FVALOR_RECEBIDO;
		    	}
		    	set
		    	{
		    		FVALOR_RECEBIDO = value;
		    	}
		    }
		    
		
		    public decimal? Troco
		    {
		    	get
		    	{
		    		return FTROCO;
		    	}
		    	set
		    	{
		    		FTROCO = value;
		    	}
		    }
		    
		
		    public decimal? ValorCancelado
		    {
		    	get
		    	{
		    		return FVALOR_CANCELADO;
		    	}
		    	set
		    	{
		    		FVALOR_CANCELADO = value;
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
		    
		
		    public decimal? TotalProdutos
		    {
		    	get
		    	{
		    		return FTOTAL_PRODUTOS;
		    	}
		    	set
		    	{
		    		FTOTAL_PRODUTOS = value;
		    	}
		    }
		    
		
		    public decimal? TotalDocumentos
		    {
		    	get
		    	{
		    		return FTOTAL_DOCUMENTO;
		    	}
		    	set
		    	{
		    		FTOTAL_DOCUMENTO = value;
		    	}
		    }
		    
		
		    public decimal? BaseICMS
		    {
		    	get
		    	{
		    		return FBASE_ICMS;
		    	}
		    	set
		    	{
		    		FBASE_ICMS = value;
		    	}
		    }
		    
		
		    public decimal? ICMS
		    {
		    	get
		    	{
		    		return FICMS;
		    	}
		    	set
		    	{
		    		FICMS = value;
		    	}
		    }
		    
		
		    public decimal? ICMSOutras
		    {
		    	get
		    	{
		    		return FICMS_OUTRAS;
		    	}
		    	set
		    	{
		    		FICMS_OUTRAS = value;
		    	}
		    }
		    
		
		    public decimal? ISSQN
		    {
		    	get
		    	{
		    		return FISSQN;
		    	}
		    	set
		    	{
		    		FISSQN = value;
		    	}
		    }
		    
		
		    public decimal? PIS
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
		    
		
		    public decimal? COFINS
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
		    
		
		    public decimal? AcrescimoItens
		    {
		    	get
		    	{
		    		return FACRESCIMO_ITENS;
		    	}
		    	set
		    	{
		    		FACRESCIMO_ITENS = value;
		    	}
		    }
		    
		
		    public decimal? DescontoItens
		    {
		    	get
		    	{
		    		return FDESCONTO_ITENS;
		    	}
		    	set
		    	{
		    		FDESCONTO_ITENS = value;
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
		    
		
		    public String NomeCliente
		    {
		    	get
		    	{
		    		return FNOME_CLIENTE;
		    	}
		    	set
		    	{
		    		FNOME_CLIENTE = value;
		    	}
		    }
		    
		
		    public String CPFouCNPJCliente
		    {
		    	get
		    	{
		    		return FCPF_CNPJ_CLIENTE;
		    	}
		    	set
		    	{
		    		FCPF_CNPJ_CLIENTE = value;
		    	}
		    }
		    
		
		    public String CupomFoiCancelado
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
		    
		
	}
	
	
}