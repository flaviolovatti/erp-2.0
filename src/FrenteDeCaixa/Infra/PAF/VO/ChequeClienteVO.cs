/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela ECF_CHEQUE_CLIENTE

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
	
	  
	public class ChequeClienteVO 
	{
		
		
		    private int FID; 
		    private int FID_BANCO; 
		    private int FID_CLIENTE; 
		    private int FID_ECF_MOVIMENTO ; 
		    private int FNUMERO_CHEQUE;
            private DateTime FDATA_CHEQUE; 
		    private decimal FVALOR_CHEQUE; 
		    private string FOBSERVACOES; 
		    private string FAGENCIA; 
		    private string FCONTA; 
		    private string FTIPO_CHEQUE ; 
		
		  
		
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
		    
		
		    public int IdBanco
		    {
		    	get
		    	{
		    		return FID_BANCO;
		    	}
		    	set
		    	{
		    		FID_BANCO = value;
		    	}
		    }
		    
		
		    public int IdCliente
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
		    
		
		    public int IdEcfMovimento
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
		    
		
		    public int NumeroCheque
		    {
		    	get
		    	{
		    		return FNUMERO_CHEQUE;
		    	}
		    	set
		    	{
		    		FNUMERO_CHEQUE = value;
		    	}
		    }
		    
		
		    public decimal ValorCheque
		    {
		    	get
		    	{
		    		return FVALOR_CHEQUE;
		    	}
		    	set
		    	{
		    		FVALOR_CHEQUE = value;
		    	}
		    }
		    
		
		    public String Observacoes
		    {
		    	get
		    	{
		    		return FOBSERVACOES;
		    	}
		    	set
		    	{
		    		FOBSERVACOES = value;
		    	}
		    }
		    
		
		    public String Conta
		    {
		    	get
		    	{
		    		return FCONTA;
		    	}
		    	set
		    	{
		    		FCONTA = value;
		    	}
		    }
		    
		
		    public String Agencia
		    {
		    	get
		    	{
		    		return FAGENCIA;
		    	}
		    	set
		    	{
		    		FAGENCIA = value;
		    	}
		    }


            public DateTime DataCheque
		    {
		    	get
		    	{
		    		return FDATA_CHEQUE;
		    	}
		    	set
		    	{
		    		FDATA_CHEQUE = value;
		    	}
		    }
		    
		
		    public String TipoCheque
		    {
		    	get
		    	{
		    		return FTIPO_CHEQUE;
		    	}
		    	set
		    	{
		    		FTIPO_CHEQUE = value;
		    	}
		    }
		    
		
	}
		
}
