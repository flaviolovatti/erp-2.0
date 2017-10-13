/********************************************************************************
Title: T2TiPDV
Description: Classe de controle dos produtos em promoção

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

namespace PafEcf.Controller
{
	
	  
	
	public class ProdutoPromocaoController 
	{
		/*
		
		
		
		public string ConsultaSQL ;
		  public TSQLQuery Query;
		
		 internal bool ConsultaIdProdutoPromocao( int Id)
		{
		
		bool Result;
		
		  ConsultaSQL = "select ID from PRODUTO_PROMOCAO where (ID = ?pID) ";
		  try {
		    try {
		      Query = new TSQLQuery(null);
		      Query.SQLConnection = FDataModule.Conexao;
		      Query. = ConsultaSQL;
		      //Query.ParamByName("pID").Asint=Id;
		      // Query.Open
		      if( ! Query.IsEmpty )
		        Result = true;
		      else
		        Result = false;
		    
		
		    }
		
		    catch(Exception)
		
		    {;
		    }
		  } finally {
		    Query.Dispose();
		  }
		return Result;
		}
		
		
		
		 internal bool GravaCargaProdutoPromocao( string vTupla)
		{
		 int ID;
		bool Result;
		
		  try {
		    try {
		      if( FDataModule.BancoPAF == "FIREBIRD" )
		      {
		        ConsultaSQL = "UPDATE OR INSERT INTO PRODUTO_PROMOCAO " +
		        " (ID, "+
		        "ID_PRODUTO, "+
		        "DATA_INICIO, "+
		        "DATA_FIM, "+
		        "QUANTIDADE_EM_PROMOCAO, "+
		        "QUANTIDADE_MAXIMA_CLIENTE, "+
		        "VALOR )"+
		        " VALUES ("+
		        DevolveConteudoDelimitado("|",vTupla)+", "+  //     ID                         int NOT NULL,
		        DevolveConteudoDelimitado("|",vTupla)+", "+  //     ID_PRODUTO                 int NOT NULL,
		        DevolveConteudoDelimitado("|",vTupla)+", "+  //     DATA_INICIO                DATE,
		        DevolveConteudoDelimitado("|",vTupla)+", "+  //     DATA_FIM                   DATE,
		        DevolveConteudoDelimitado("|",vTupla)+", "+  //     QUANTIDADE_EM_PROMOCAO     DECIMAL(18,6),
		        DevolveConteudoDelimitado("|",vTupla)+", "+  //     QUANTIDADE_MAXIMA_CLIENTE  DECIMAL(18,6),
		        DevolveConteudoDelimitado("|",vTupla)+")";    //     VALOR                      DECIMAL(18,6)
		      }
		      else if( FDataModule.BancoPAF == "MYSQL" )
		      {
		        ID = Convert.ToInt32(DevolveConteudoDelimitado("|",vTupla));   //     ID              int NOT NULL,
		
		        if( ! ConsultaIdProdutoPromocao(ID) )
		          ConsultaSQL = "INSERT INTO PRODUTO_PROMOCAO "+
		          " (ID, "+
		          "ID_PRODUTO, "+
		          "DATA_INICIO, "+
		          "DATA_FIM, "+
		          "QUANTIDADE_EM_PROMOCAO, "+
		          "QUANTIDADE_MAXIMA_CLIENTE, "+
		          "VALOR )"+
		          " VALUES ("+
		          Convert.ToString( ID )+", "+                            //     ID                    int NOT NULL,
		          DevolveConteudoDelimitado("|",vTupla)+", "+  //     ID_PRODUTO                 int NOT NULL,
		          DevolveConteudoDelimitado("|",vTupla)+", "+  //     DATA_INICIO                DATE,
		          DevolveConteudoDelimitado("|",vTupla)+", "+  //     DATA_FIM                   DATE,
		          DevolveConteudoDelimitado("|",vTupla)+", "+  //     QUANTIDADE_EM_PROMOCAO     DECIMAL(18,6),
		          DevolveConteudoDelimitado("|",vTupla)+", "+  //     QUANTIDADE_MAXIMA_CLIENTE  DECIMAL(18,6),
		          DevolveConteudoDelimitado("|",vTupla)+")"    //    VALOR                      DECIMAL(18,6);
		        else
		          ConsultaSQL = " update PRODUTO_PROMOCAO set "+
		          "ID_PRODUTO ="+                DevolveConteudoDelimitado("|",vTupla)+", "+  //     ID_PRODUTO                 int NOT NULL,
		          "DATA_INICIO ="+               DevolveConteudoDelimitado("|",vTupla)+", "+  //     DATA_INICIO                DATE,
		          "DATA_FIM ="+                  DevolveConteudoDelimitado("|",vTupla)+", "+  //     DATA_FIM                   DATE,
		          "QUANTIDADE_EM_PROMOCAO ="+    DevolveConteudoDelimitado("|",vTupla)+", "+  //     QUANTIDADE_EM_PROMOCAO     DECIMAL(18,6),
		          "QUANTIDADE_MAXIMA_CLIENTE ="+ DevolveConteudoDelimitado("|",vTupla)+", "+  //     QUANTIDADE_MAXIMA_CLIENTE  DECIMAL(18,6),
		          "VALOR ="+                     DevolveConteudoDelimitado("|",vTupla)+       //     VALOR                      DECIMAL(18,6)
		          " where ID ="+Convert.ToString( ID );
		      }
		      Query = new TSQLQuery(null);
		      Query.SQLConnection = FDataModule.Conexao;
		      Query. = ConsultaSQL;
		      Query.long lRowsAffected = dbConn.executeSQLget(sSQL);();
		
		      Result = true;
		    
		
		    }
		
		    catch(Exception)
		
		    {
		      Result = false;
		    }
		
		  } finally {
		    Query.Dispose();
		  }
		return Result;
		}
		
*/		
	}
	
}
