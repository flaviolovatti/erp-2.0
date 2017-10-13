using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PafEcf.Util
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>

	/// <summary>
	/// An array that can have a lower bound (offset) other than 0, and which does
	/// not require casting in order to be used.  Enumeration also works.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TSArray<T> : ICollection
	{
		// You can inherit this class and specify a type to make for prettier syntax for common
		// cases like so:
		//		public class TSStringArray : TSArray<string> {...}
		//When you create an array like this: new TSArray<string>[5] it will default to have a lower bound of iDefaultLowerBound
		//If you want to change this, then just set TSArray.iDefaultLowerBound once at the beginning of your code.
		private static int iDefaultLowerBound = 0;

		//the actual lowerbound of the array
		private int iLowerBound = iDefaultLowerBound;
        //the actual upper bound of the array
        private int iUpperBound = iDefaultLowerBound;

		//the internal array used to actually hold the data.  It is an actual offset array.
		private Array array;

		//Both constructors use this to create the internal array
		private static Array createInternalArray(int iLowerBound, int iLength)
		{
			string[] fred;

			return Array.CreateInstance(
				typeof(T),
				new int[] { iLength },
				new int[] { iLowerBound }
			);
		}

		//Construct using default lowerbound and this length
		public TSArray(int iLength)
		{
			array = createInternalArray(iDefaultLowerBound, iLength);
		}
		//Construct by specifying lower & upper bounds of the array
		public TSArray(int iLowerBound, int iUpperBound)
		{
			this.iLowerBound = iLowerBound;
            this.iUpperBound = iUpperBound;
			array = createInternalArray(iLowerBound, 1 + iUpperBound - iLowerBound);
		}

		public T this[int i]
		{
			get { return GetValue(i); }
			set { SetValue(value, i); }
		}

		public void SetValue(T val, int i) { array.SetValue(val, i); }

		public T GetValue(int i) { return (T)array.GetValue(i); }

		public void CopyTo(Array _array, int i) { array.CopyTo(_array, i); }

		public int Count { get { return array.GetLength(0); } }

        public void Clear()
        {
            array = createInternalArray(iLowerBound, 1 + iUpperBound - iLowerBound);
        }
        
		public object SyncRoot
		{
			get { return array.SyncRoot; }
			set { object temp = array.SyncRoot; }
		}

        public int LBound {
            get { return iLowerBound; }
        }
        
        public int UBound {
            get { return iUpperBound; }
        }
        
		public bool IsSynchronized { get { return array.IsSynchronized; } }

		public IEnumerator GetEnumerator() { return array.GetEnumerator(); }

		public int Length { get { return array.GetLength(0); } }

		public int GetLength() { return Length; }

		public void Initialize() { array.Initialize(); }

		public new string ToString() { return array.ToString(); }

	}

	
	public class tsUtils
	{

		public tsUtils()
		{
		}

		[DllImport("kernel32.dll")]
		public static extern bool Beep(int freq,int duration);


		public string XMLparser(string sSource, string sTag, string sDefault )
		{
			try
			{
				if( sSource.IndexOf(sTag) > 0 )
				{
					sSource = sSource.Substring( sSource.IndexOf(sTag) + sTag.Length + 1 );
						//sSource.Length-sSource.IndexOf(sTag)-sTag.Length-2);
					return sSource.Substring(0, sSource.IndexOf(sTag)-2);
				}
				else
					return sDefault;
			
			}
			catch (Exception)
			{
				return sDefault;
			}
		}
		
		public StringBuilder XMLcreate(StringBuilder sbXML, string sTag, string sValue)
		{
			try
			{
				sbXML.Append("<").Append(sTag);
				sbXML.Append(">").Append(sValue).Append("</");
				sbXML.Append(sTag).Append(">");
				
				return sbXML;

			}
			catch (XmlException)
			{
				sbXML.Append("<").Append(sTag);
				sbXML.Append("></").Append(sTag).Append(">");
				
				return sbXML;
			}
		}

		public string XMLreplace(string sSource, string sTag, string sValue )
		{
			string sPre;

			try
			{
				if( sSource.IndexOf(sTag) > 0 )
				{
					sPre = sSource.Substring( 0, sSource.IndexOf(sTag) + sTag.Length + 1 );
					return sPre + sValue + sSource.Substring( sSource.IndexOf( "</" + sTag ) );
				}
				else
					return sSource;
			}
			catch (Exception)
			{
				return sSource;
			}
		}
		
		public string EncryptPassword(string sPassword) 
		{
			UnicodeEncoding encoding = new UnicodeEncoding(); 
			byte[] hashBytes = encoding.GetBytes( sPassword ); 

			// compute SHA-1 hash. 
			SHA1 sha1 = new SHA1CryptoServiceProvider(); 
			byte[] cryptPassword = sha1.ComputeHash ( hashBytes ); 

			return Convert.ToBase64String(cryptPassword);
		} 


		/// <summary>
		/// double all single apostrophes for storing in db
		/// </summary>
		/// <param name="sString"></param>
		/// <returns>string</returns>
		
		public string sqlFixup( string sString )
		{
			try
			{
				sString = "'" + sString + "'";	//Delphi QuotedStr did this 

				//Remove double and triple apostrophes so we make sure we can exactly double the apostrophe when saving sql statements.
				if( sString.IndexOf( "''" ) > 0 )
				{
					//int iFindChar = sString.IndexOf( "''" );
					int iLoopCount = 0;
					while( sString.IndexOf( "''" ) > 0 && iLoopCount++ < 40 )
					{
						sString = sString.Remove( sString.IndexOf( "''" ), 1 );
					}
				}
				
				//Now double the apostrophes so we can same the sql statement
				if( sString.IndexOf( "'", 0, sString.Length ) > 0 )
				{
					string sTemp = sString;
					sString = "";
					while( sTemp.IndexOf( "'", 0, sTemp.Length ) > 0 )
					{
						if( sTemp.IndexOf( "'", 0, sTemp.Length ) > 0 )
						{
							sString = sString + sTemp.Substring( 0, sTemp.IndexOf( "'", 0, sTemp.Length )+1 ) + "'";
							sTemp = sTemp.Substring( sTemp.IndexOf( "'", 0 )+1, sTemp.Length - sTemp.IndexOf( "'", 0 ) -1 );
						}
							
						if( sTemp.IndexOf( "'", 0, sTemp.Length ) < 1 )
							sString = sString + sTemp;
					}
				}
			}
			catch( Exception )
			{
			}
			
			return sString;
		}

		public void logError(string sPath, string sError)
		{
			try
			{
				StreamWriter w = File.AppendText(sPath + "log.txt");
				w.Write("Log Entry : ");
				w.WriteLine("{0} - {1}", DateTime.Now.ToLongDateString(),
					DateTime.Now.ToLongTimeString());
				w.WriteLine("  :{0}", sError);
				w.WriteLine ("-------------------------------");
				// Update the underlying file.
				w.Flush(); 
				w.Close();
				
			}
			catch( Exception ){}
		}

		public bool IsDate(string sDate) 
		{
			try 
			{
				if( sDate.Equals("1/1/1900") || sDate.Trim().Equals("") )
					return false;
				DateTime.Parse(sDate);
				return true;
			}
			catch 
			{
				return false;
			} 
		} 

		public static bool isNumeric(string sText)
		{
			return (System.Text.RegularExpressions.Regex.IsMatch(sText, "^\\-?\\d*(\\.\\d*)?$"));
		} 

		public bool IsDouble(string sNumber)
		{
			try 
			{
				Convert.ToDouble( sNumber );
				return true;
			} 
			catch(Exception) 
			{
				return false;
			}
		}

		public void enableStuff( System.Collections.ICollection outerControl, bool bEnableIt )
		{
			foreach( Control c in outerControl )
			{
				if( c.GetType().Name.Equals("Label") )
					((Label)c).Enabled = bEnableIt;
				else if( c.GetType().Name.Equals("TextBox") )
					((TextBox)c).Enabled = bEnableIt;
				else if( c.GetType().Name.Equals("LinkButton") )
					((Button)c).Enabled = bEnableIt;
			}
		}

		public string[] getMonthNames( DateTime myDate )
		{
			string[] sMonthNames = new string[12];
			string[] sDefaultNames = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

			for( int iCount = 0; iCount < 12; iCount++ )
			{
				sMonthNames[iCount] = sDefaultNames[ myDate.Month-1 ];
				myDate = myDate.AddMonths(1);
			}

			return sMonthNames;
			//return new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
		}

		public string getMonth( int iMonth )
		{
			string[] sDefaultNames = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

			if( (iMonth > 11) || (iMonth < 1) )
				return sDefaultNames[0];
			else
				return sDefaultNames[iMonth];
		}

		public DateTime addToDateTime( string sPeriodType, DateTime dMyDate, int iStartDay )
		{
			if( sPeriodType.Equals("yyyy") )
				return dMyDate.AddYears(1).Subtract( new TimeSpan(1,0,0,0) );
			else if( sPeriodType.Equals("y2") )
				return dMyDate.AddMonths(6).Subtract( new TimeSpan(1,0,0,0) );
			else if( sPeriodType.Equals("y4") )
				return dMyDate.AddMonths(3).Subtract( new TimeSpan(1,0,0,0) );
			else if( sPeriodType.Equals("m") )
			{
				dMyDate = dMyDate.AddMonths(1);
				if( dMyDate.Day == iStartDay+1)
					return dMyDate;
				else if( dMyDate.Day < iStartDay+1)
				{
					for( int iCount = 1; iCount < 5; iCount++ )
					{
						DateTime dTest = dMyDate.AddDays(iStartDay-dMyDate.Day-iCount);
						if( dTest.Month == dMyDate.Month && dTest.Year == dMyDate.Year )
						{
							dTest = dMyDate.AddDays(iStartDay-dMyDate.Day-iCount+1);
							if( dTest.Month != dMyDate.Month )
								return dMyDate.AddDays(iStartDay-dMyDate.Day-iCount-1);
							else
								return dMyDate.AddDays(iStartDay-dMyDate.Day-iCount);
						}
					}
					return dMyDate.Subtract( new TimeSpan(1,0,0,0) );
				}
				else
					return dMyDate.Subtract( new TimeSpan(1,0,0,0) );
			}
			else if( sPeriodType.Equals("m2") )
				return dMyDate.AddDays(27);
			else if( sPeriodType.Equals("m3") )
				return dMyDate.AddDays(14);
			else if( sPeriodType.Equals("ww") )
				return dMyDate.AddDays(6);
			else if( sPeriodType.Equals("d") )
				return dMyDate;
			else
				return dMyDate;
		}

		public bool exportToExcel(string sPathFile, string sFileContents)
		{
			try
			{
				StreamWriter w = File.CreateText(sPathFile);
				w.WriteLine("{0}", sFileContents);
				// Update the underlying file.
				w.Flush(); 
				w.Close();
				
				return true;
			}
			catch( Exception eError )
			{
				MessageBox.Show("Error Exporting: " + eError.Message);
				return false;
			}
		}

		public RichTextBox richTextDeleteLine( RichTextBox rtbOrig, int iItem )
		{
			RichTextBox rtb = new RichTextBox();
			int iLoop = 0;
			foreach( String Line in rtbOrig.Lines )
			{
				if( iLoop++ != iItem )
					rtb.Text += Line;
			}

			return rtb;
		}

	}

	public class MyListBoxItem
	{
		public MyListBoxItem(string sValue, string sKey)
		{
			_sValue = sValue;
			_sKey = sKey;
		}

		public string sKey
		{
			get
			{
				return _sKey;
			}
			set
			{
				_sKey = value;
			}
		}

		public override string ToString()
		{
			return _sValue;
		}

		protected string _sValue;
		protected string _sKey;
	}

}
