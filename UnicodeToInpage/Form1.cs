using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace UnicodeToInpage
{
    public partial class Form1 : Form
    {

        #region " Member Variables and Constructors "

        DataTable _dtLigatures = new DataTable();   
        DataTable _dtUniToInpage = new DataTable(); 
        DataTable _dtInpageToUni = new DataTable(); 
        
        List<char> _endChar = new List<char>();     
        List<char> _englishChar = new List<char>();
        List<string> _henStr = new List<string>();

        PdfReader _reader;
        string[] _lines;
        int _pageNum = 1;        
        string _filePath;

        System.Drawing.Font _font;
        System.Drawing.Font _font2;

        public Form1()
        {
            InitializeComponent();

            FillEndChar();
            FillEnglishChar();
            FillHenStr();
            
            /* Method to convert excel data to xml to avoid excel database driver issue on 64bit machine.
            _dtLigatures = GetExcelData("NooriN");
            _dtLigatures.TableName = "Ligatures";
            _dtLigatures.WriteXmlSchema("NastLigSchema.xml");
            _dtLigatures.WriteXml("NastLig.xml");              
            */

            StringReader stringStream = new StringReader(GetResourceTextFile("NastLigSchema.xml"));
            _dtLigatures.ReadXmlSchema(stringStream);
            stringStream = new StringReader(GetResourceTextFile("NastLig.xml"));
            _dtLigatures.ReadXml(stringStream);
            
            stringStream = new StringReader(GetResourceTextFile("InpageToUniSchema.xml"));
            _dtInpageToUni.ReadXmlSchema(stringStream);
            stringStream = new StringReader(GetResourceTextFile("InpageToUni.xml"));
            _dtInpageToUni.ReadXml(stringStream);

            stringStream = new StringReader(GetResourceTextFile("UniToInpageSchema.xml"));
            _dtUniToInpage.ReadXmlSchema(stringStream);
            stringStream = new StringReader(GetResourceTextFile("UniToInpage.xml"));
            _dtUniToInpage.ReadXml(stringStream);            

        }

        #endregion



        #region " Events "

        #region " Form Events "

        private void Form1_Load(object sender, EventArgs e)
        {
            SetScreenResolution();            
            rtbxPDFText.Font = _font;            
            lblHelp1.Font = _font2;
            lblHelp.Font = _font2;            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            axAcroPDF1.Dispose();
            axAcroPDF1 = null;
            */
        }

        #endregion

        #region " Button Events "

        private void btnUniToInpageExcel_Click(object sender, EventArgs e)
        {
            UnicodeToInpageExcel();
        }

        private void btnInpageToUni_Click(object sender, EventArgs e)
        {
            InpageToUnicodeExcel();
        }

        private void btnTextWithFont_Click(object sender, EventArgs e)
        {
            if (_reader == null)
            {
                if (_filePath == null || _filePath == String.Empty)
                {
                    string msg = "پہلے پی ڈی ایف فائل منتخب کریں۔";
                    NaseemMessageBox(msg);
                }
                else
                {
                    string msg = "فائل کرپٹ لگ رہی ہے۔" + Environment.NewLine;
                    msg += "سافٹوئیر اسے کھولنے سے قاصر ہے۔";
                    NaseemMessageBox(msg);
                }
                return;
            }

            rtbxPDFText.SelectionStart = 0;
            rtbxPDFText.SelectionLength = 1;
            rtbxPDFText.ScrollToCaret();            
            GetPdfText();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                _reader = GetPdfReader();
                numCounter.Minimum = 1;
                numCounter.Maximum = _reader.NumberOfPages;
            }
            catch (Exception ex)
            { 
            }
        }

        #endregion

        #region " Label Events "

        private void lblHelp_Click(object sender, EventArgs e)
        {
            lblHelp.Visible = false;
        }

        private void lblHelp_MouseLeave(object sender, EventArgs e)
        {
            lblHelp.Visible = false;
        }


        #endregion

        #region " PictureBox Events "

        private void pbxHelp_Click(object sender, EventArgs e)
        {
            if (lblHelp.Visible == true) lblHelp.Visible = false;
            else lblHelp.Visible = true;

            PictureBox pbx = sender as PictureBox;
            switch (pbx.Name)
            {
                case "pbxHelpRepairText":
                    lblHelp.Left = pbx.Left;
                    lblHelp.Top = pbx.Top + pbx.Height;
                    lblHelp.Text = "بعض دفعہ ایسا ہوتا ہے کہ جس سطر میں ’’ﷺ‘‘ کا ترسیمہ آتا ہے اس سطر کے الفاظ آگے پیچھے ہوجاتے ہیں۔ ایسی صورت میں یہ چیک لگا کر دوبارہ کوشش کریں۔";
                    lblHelp.AutoSize = true;                    
                    break;

                case "pbxHelpShowTextLineByLine":
                    lblHelp.Left = pbx.Left;
                    lblHelp.Top = pbx.Top + pbx.Height;
                    lblHelp.Text = "اگر حاصل کردہ متن میں لائن بریک نہیں چاہییں اور تسلسل کے ساتھ متن حاصل کرنا چاہیں تو اس چیک کو ہٹا دیں";
                    lblHelp.AutoSize = true;                    
                    break;

                case "pbxSkipEnglishWords":
                    lblHelp.Left = pbx.Left;
                    lblHelp.Top = pbx.Top + pbx.Height;
                    lblHelp.Text = "اگر متن میں سے انگریزی الفاظ بھی اخذ کرنا چاہیں تو یہ چیک ہٹا دیں۔";
                    lblHelp.AutoSize = true;
                    break;
            }
        }

        #endregion

        #region " TextBox Events "

        private void rtbxPDFText_TextChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region " Menu Events "

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void userGuidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("HelpPdfTexter.pdf");
            }
            catch (Exception ex)
            {
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout ofrmAbout = new frmAbout();
            ofrmAbout.ShowDialog();
        }

        #endregion

        #region " CheckBox Events "

        private void chkLineFeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLineFeed.Checked) Gen.IsLineFeed = true;
            else Gen.IsLineFeed = false;
        }

        #endregion

        #region " WebBrowser Events "

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                //string text = webBrowser1.Document.Body.InnerText;
                //string txt = webBrowser1.DocumentText;
            }
            catch (Exception ex)
            { 
            }
        }

        #endregion

        #region " RadioButton Events "

        private void rbEncoding_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion



        #region " Conversion Tools Functions "

        private bool UnicodeToInpageExcel()
        {
            try
            {
                /*
                string fullPathToExcel = "_UniToInpage.xlsx";
                string conString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=yes'", fullPathToExcel);
                OleDbConnection con = new OleDbConnection(conString);
                con.Open();
                string query = "SELECT * from [Sheet1$]";
                //string query = "SELECT * from [NooriN$]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                _dtUniToInpage = dt;
                */

                pb1.Value = 0;
                pb1.Minimum = 0;
                pb1.Maximum = Clipboard.GetText().Length;
                StringBuilder sb = new StringBuilder();
                char[] charArray = Clipboard.GetText().ToCharArray();
                string chExtra = String.Empty;
                char ch = ' ';
                char chEndTransmission = Convert.ToChar(4);
                bool endTransmission = false;
                bool ignore = false;
                bool str = false;
                bool isNumber = false;
                bool isEnglishWord = false;
                string numbers = String.Empty;
                string englishWords = String.Empty;
                DataRow[] drr;
                for (int i = 0; i < charArray.Length; i++)
                {
                    ch = charArray[i];

                    #region " Reverse Numbers "
                    try
                    {
                        if (ch >= 1776 && ch <= 1785)
                        {
                            isNumber = true;
                            numbers += Convert.ToInt32(ch).ToString() + ",";
                            continue;
                        }
                        else if (isNumber)
                        {
                            isNumber = false;
                            string[] allNumbers = numbers.Split(',');
                            numbers = String.Empty;
                            for (int j = allNumbers.Length - 1; j >= 0; j--)
                            {
                                if (allNumbers[j].Trim() == String.Empty) continue;
                                drr = _dtUniToInpage.Select("UnicodeDec=" + Convert.ToInt32(allNumbers[j]));
                                int chCode = Convert.ToInt32(drr[0]["InpageDec"].ToString());
                                ch = GetChar(chCode, 1252);
                                sb.Append(chEndTransmission);
                                sb.Append(ch);
                            }
                        }
                    }
                    catch (Exception ex) { }
                    #endregion

                    #region " Reverse English Words "

                    try
                    {
                        if (ch >= 65 && ch <= 90)
                        {
                            isEnglishWord = true;
                            englishWords += ch.ToString();
                            //continue;
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    #endregion

                    drr = _dtUniToInpage.Select("UnicodeDec=" + (int)charArray[i]);
                    if (drr.Length > 0)
                    {
                        int chCode = Convert.ToInt32(drr[0]["InpageDec"].ToString());
                        int codePage = Convert.ToInt32(drr[0]["CodePage"].ToString());
                        endTransmission = (drr[0]["EndTrans"].ToString() == "T") ? true : false;
                        ignore = (drr[0]["Ignore"].ToString() == "T") ? true : false;
                        str = (drr[0]["Type"].ToString() == "S") ? true : false;
                        if (ignore) continue;
                        ch = GetChar(chCode, codePage);
                        if (isEnglishWord) endTransmission = false;
                        if (str)
                        {
                            string st = drr[0]["String1"].ToString();
                            string[] sCharDec = drr[0]["String1"].ToString().Split(',');
                            foreach (string s in sCharDec)
                            {
                                if (s.Trim() == String.Empty) continue;
                                chCode = Convert.ToInt32(s);
                                ch = GetChar(chCode, codePage);
                                sb.Append(chEndTransmission);
                                sb.Append(ch);
                            }
                        }
                        else
                        {
                            if (endTransmission) sb.Append(chEndTransmission);
                            sb.Append(ch);
                            isEnglishWord = false;
                        }
                    }
                    else
                    {
                        if (endTransmission) sb.Append(chEndTransmission);
                        sb.Append(ch);
                    }
                    try
                    {
                        if ((int)charArray[i] == 1569 && (int)charArray[i + 1] == 1575)
                        {
                            sb.Append(chEndTransmission);
                            sb.Append(' ');
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        if ((int)charArray[i] == 1746 && (int)charArray[i + 1] != 32)
                        {
                            sb.Append(chEndTransmission);
                            sb.Append(' ');
                        }
                    }
                    catch (Exception ex) { }
                    endTransmission = false;
                    ignore = false;
                    str = false;
                    pb1.Value++;
                }
                Clipboard.SetText(sb.ToString());
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private char GetChar(int chCode, int codePage)
        {
            try
            {
                //sb.Append("\u0627");
                Encoding iso = Encoding.GetEncoding(codePage);
                byte[] byt = new byte[] { (byte)chCode };
                char[] characters = iso.GetChars(byt);
                return characters[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private int GetChar(char chCode)
        {
            try
            {
                //sb.Append("\u0627");                
                Encoding iso = Encoding.GetEncoding(1252);
                byte[] byt = iso.GetBytes(chCode.ToString());
                int bytDec = Convert.ToInt32(byt[0]);
                return bytDec;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool InpageToUnicodeExcel()
        {
            try
            {
                /*
                string fullPathToExcel = "InpageToUni.xlsx";
                string conString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=yes'", fullPathToExcel);
                OleDbConnection con = new OleDbConnection(conString);
                con.Open();
                string query = "SELECT * from [Sheet1$]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                */

                pb1.Value = 0;
                pb1.Minimum = 0;
                pb1.Maximum = Clipboard.GetText().Length;
                StringBuilder sb = new StringBuilder();
                string allText = Clipboard.GetText();
                char[] charArray = Clipboard.GetText().ToCharArray();
                char ch = ' ';
                bool ignore = false;
                bool isNumber = false;
                string numbers = String.Empty;
                DataRow[] drr;
                for (int i = 0; i < charArray.Length; i++)
                {
                    if ((int)charArray[i] == 4) continue;
                    int chInp = GetChar(charArray[i]);

                    #region " Reverse Numbers "
                    try
                    {
                        if (chInp >= 208 && chInp <= 217)
                        {
                            isNumber = true;
                            numbers += chInp.ToString() + ",";
                            continue;
                        }
                        else if (isNumber)
                        {
                            isNumber = false;
                            string[] allNumbers = numbers.Split(',');
                            numbers = String.Empty;
                            for (int j = allNumbers.Length - 1; j >= 0; j--)
                            {
                                if (allNumbers[j].Trim() == String.Empty) continue;
                                drr = _dtInpageToUni.Select("InpageDec=" + Convert.ToInt32(allNumbers[j]));
                                int chCode = Convert.ToInt32(drr[0]["UnicodeDec"].ToString());
                                ch = (char)chCode;
                                sb.Append(ch);
                            }
                        }
                    }
                    catch (Exception ex) { }
                    #endregion

                    try
                    {
                        if (chInp == 162 && GetChar(charArray[i - 1]) == 4 && GetChar(charArray[i - 2]) == 163)
                        {
                            continue;
                        }
                    }
                    catch (Exception ex) { }
                    try
                    {
                        if (chInp == 165 && GetChar(charArray[i + 1]) == 4 && (GetChar(charArray[i + 2]) >= 129 && GetChar(charArray[i + 2]) <= 167))
                        {
                            chInp = 164;
                        }
                    }
                    catch (Exception ex) { }
                    try
                    {
                        if (chInp == 163 && GetChar(charArray[i + 1]) != 4)
                        {
                            chInp = 183;
                        }
                    }
                    catch (Exception ex) { }
                    try
                    {
                        if (chInp == 163 && GetChar(charArray[i + 1]) == 4 && GetChar(charArray[i + 2]) == 162)
                        {
                            chInp = 182;
                        }
                    }
                    catch (Exception ex) { }
                    try
                    {
                        if (chInp == 163 && GetChar(charArray[i + 1]) == 4 && charArray[i + 2] == 32 && GetChar(charArray[i + 3]) == 4 && GetChar(charArray[i + 4]) == 129)
                        {
                            chInp = 183;
                        }
                    }
                    catch (Exception ex) { }
                    try
                    {
                        if (chInp == 163 && (charArray.Length == i + 1 || ((GetChar(charArray[i + 1]) == 4) && GetChar(charArray[i + 2]) == 32)))
                        {
                            chInp = 183;
                        }
                    }
                    catch (Exception ex) { }
                    try
                    {
                        if (chInp == 163 && GetChar(charArray[i + 1]) == 4 && (GetChar(charArray[i + 2]) < 129 || GetChar(charArray[i + 2]) > 167))
                        {
                            chInp = 183;
                        }
                    }
                    catch (Exception ex) { }
                    try
                    {
                        /*
                        else if (chInp == 163 && (GetChar(charArray[i + 1]) == 13 || GetChar(charArray[i + 1]) == 9))
                        {
                            chInp = 183;
                        }
                        */
                    }
                    catch (Exception ex) { }

                    ch = charArray[i];
                    drr = _dtInpageToUni.Select("InpageDec=" + chInp);
                    if (drr.Length > 0)
                    {
                        int chCode = Convert.ToInt32(drr[0]["UnicodeDec"].ToString());
                        ignore = (drr[0]["Ignore"].ToString() == "T") ? true : false;
                        if (ignore) continue;
                        ch = (char)chCode;
                        sb.Append(ch);
                        try
                        {
                            if (chInp == 161 && GetChar(charArray[i + 1]) == 4 && GetChar(charArray[i + 2]) != 32)
                            {
                                ch = (char)32;
                                sb.Append(ch);
                            }
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                    pb1.Value++;
                }
                pb1.Value = pb1.Maximum;
                Clipboard.SetText(sb.ToString());
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }        

        #endregion

        

        #region " PDF Functions "

        #region " Fill List<> Functions "

        private void FillHenStr()
        {
            _henStr.Add("ے");
            _henStr.Add("ی");
            //_henStr.Add("یعنی");
            //_henStr.Add("۔");
            //_henStr.Add(" ");
            //_henStr.Add("");
        }

        private void FillEndChar()
        {
            _endChar.Add('ب');
            _endChar.Add('پ');
            _endChar.Add('ت');
            _endChar.Add('ٹ');
            _endChar.Add('ث');
            _endChar.Add('ج');
            _endChar.Add('چ');
            _endChar.Add('ح');
            _endChar.Add('خ');
            _endChar.Add('س');
            _endChar.Add('ش');
            _endChar.Add('ص');
            _endChar.Add('ض');
            _endChar.Add('ط');
            _endChar.Add('ظ');
            _endChar.Add('ع');
            _endChar.Add('غ');
            _endChar.Add('ف');
            _endChar.Add('ق');
            _endChar.Add('ک');
            _endChar.Add('گ');
            _endChar.Add('ل');
            _endChar.Add('م');           
            _endChar.Add('ن');
            _endChar.Add('ں');
            _endChar.Add('ہ');
            _endChar.Add('ھ');
            _endChar.Add('ئ');
            _endChar.Add('ی');
            _endChar.Add('ّ');
            _endChar.Add('ِ');
            _endChar.Add('َ');
            _endChar.Add('ٰ');
            _endChar.Add('؂');            
        }

        private void FillEnglishChar()
        {
            _englishChar.Add(' ');
            _englishChar.Add('A');
            _englishChar.Add('B');
            _englishChar.Add('C');
            _englishChar.Add('D');
            _englishChar.Add('E');
            _englishChar.Add('F');
            _englishChar.Add('G');
            _englishChar.Add('H');
            _englishChar.Add('I');
            _englishChar.Add('J');
            _englishChar.Add('K');
            _englishChar.Add('L');
            _englishChar.Add('M');
            _englishChar.Add('N');
            _englishChar.Add('O');
            _englishChar.Add('P');
            _englishChar.Add('Q');
            _englishChar.Add('R');
            _englishChar.Add('S');
            _englishChar.Add('T');
            _englishChar.Add('U');
            _englishChar.Add('V');
            _englishChar.Add('W');
            _englishChar.Add('X');
            _englishChar.Add('Y');
            _englishChar.Add('Z');
            _englishChar.Add('a');
            _englishChar.Add('b');
            _englishChar.Add('c');
            _englishChar.Add('d');
            _englishChar.Add('e');
            _englishChar.Add('f');
            _englishChar.Add('g');
            _englishChar.Add('h');
            _englishChar.Add('i');
            _englishChar.Add('j');
            _englishChar.Add('k');
            _englishChar.Add('l');
            _englishChar.Add('m');
            _englishChar.Add('n');
            _englishChar.Add('o');
            _englishChar.Add('p');
            _englishChar.Add('q');
            _englishChar.Add('r');
            _englishChar.Add('s');
            _englishChar.Add('t');
            _englishChar.Add('u');
            _englishChar.Add('v');
            _englishChar.Add('w');
            _englishChar.Add('x');
            _englishChar.Add('y');
            _englishChar.Add('z');
            _englishChar.Add('(');
            _englishChar.Add(')');
            _englishChar.Add('[');
            _englishChar.Add(']');
            _englishChar.Add('{');
            _englishChar.Add('}');
            _englishChar.Add('.');
            _englishChar.Add('/');
            _englishChar.Add('*');
            _englishChar.Add('-');
            _englishChar.Add('+');
            _englishChar.Add('=');
            _englishChar.Add('!');
            _englishChar.Add('@');
            _englishChar.Add('#');
            _englishChar.Add('$');
            _englishChar.Add('%');
            _englishChar.Add('&');
            _englishChar.Add('_');
            _englishChar.Add('<');
            _englishChar.Add('>');
            _englishChar.Add('?');
            _englishChar.Add('\\');
            _englishChar.Add('|');
            _englishChar.Add(';');
            _englishChar.Add(':');
            _englishChar.Add('\'');
            _englishChar.Add('"');
            _englishChar.Add('0');
            _englishChar.Add('1');
            _englishChar.Add('2');
            _englishChar.Add('3');
            _englishChar.Add('4');
            _englishChar.Add('5');
            _englishChar.Add('6');
            _englishChar.Add('7');
            _englishChar.Add('8');
            _englishChar.Add('9');

        }

        private void FillEnglishCharOnly()
        {
            _englishChar.Add(' ');
            _englishChar.Add('A');
            _englishChar.Add('B');
            _englishChar.Add('C');
            _englishChar.Add('D');
            _englishChar.Add('E');
            _englishChar.Add('F');
            _englishChar.Add('G');
            _englishChar.Add('H');
            _englishChar.Add('I');
            _englishChar.Add('J');
            _englishChar.Add('K');
            _englishChar.Add('L');
            _englishChar.Add('M');
            _englishChar.Add('N');
            _englishChar.Add('O');
            _englishChar.Add('P');
            _englishChar.Add('Q');
            _englishChar.Add('R');
            _englishChar.Add('S');
            _englishChar.Add('T');
            _englishChar.Add('U');
            _englishChar.Add('V');
            _englishChar.Add('W');
            _englishChar.Add('X');
            _englishChar.Add('Y');
            _englishChar.Add('Z');
            _englishChar.Add('a');
            _englishChar.Add('b');
            _englishChar.Add('c');
            _englishChar.Add('d');
            _englishChar.Add('e');
            _englishChar.Add('f');
            _englishChar.Add('g');
            _englishChar.Add('h');
            _englishChar.Add('i');
            _englishChar.Add('j');
            _englishChar.Add('k');
            _englishChar.Add('l');
            _englishChar.Add('m');
            _englishChar.Add('n');
            _englishChar.Add('o');
            _englishChar.Add('p');
            _englishChar.Add('q');
            _englishChar.Add('r');
            _englishChar.Add('s');
            _englishChar.Add('t');
            _englishChar.Add('u');
            _englishChar.Add('v');
            _englishChar.Add('w');
            _englishChar.Add('x');
            _englishChar.Add('y');
            _englishChar.Add('z');
            _englishChar.Add('(');
            _englishChar.Add(')');
            _englishChar.Add('[');
            _englishChar.Add(']');
            _englishChar.Add('{');
            _englishChar.Add('}');
            _englishChar.Add('.');
            _englishChar.Add('/');
            _englishChar.Add('*');
            _englishChar.Add('-');
            _englishChar.Add('+');
            _englishChar.Add('=');
            _englishChar.Add('!');
            _englishChar.Add('@');
            _englishChar.Add('#');
            _englishChar.Add('$');
            _englishChar.Add('%');
            _englishChar.Add('&');
            _englishChar.Add('_');
            _englishChar.Add('<');
            _englishChar.Add('>');
            _englishChar.Add('?');
            _englishChar.Add('\\');
            _englishChar.Add('|');
            _englishChar.Add(';');
            _englishChar.Add(':');
            _englishChar.Add('\'');
            _englishChar.Add('"');
            _englishChar.Add('0');
            _englishChar.Add('1');
            _englishChar.Add('2');
            _englishChar.Add('3');
            _englishChar.Add('4');
            _englishChar.Add('5');
            _englishChar.Add('6');
            _englishChar.Add('7');
            _englishChar.Add('8');
            _englishChar.Add('9');

        }

        #endregion

        #region " Supporting Functions "

        private int SkipIt(int chCode, string font, int preChar, string preFont, int i)
        {
            switch (chCode)
            {
                case 46:
                    if (font == "NOORIN86" && preChar == 125 && preFont == "NOORIN81") return 1;
                    if (font == "NOORIN86" && preChar == 93 && preFont == "NOORIN81") return 1;
                    if (font == "NOORIN86" && preChar == 95 && preFont == "NOORIN81") return 1;
                    if (font == "NOORIN86" && preChar == 126 && preFont == "NOORIN81") return 1; 
                    break;
                case 47:
                    if (font == "NOORIN86" && preChar == 154 && preFont == "NOORIN82") return 1;
                    if (font == "NOORIN86" && preChar == 156 && preFont == "NOORIN82") return 1;                    
                    if (font == "NOORIN86" && preChar == 164 && preFont == "NOORIN82") return 1;
                    if (font == "NOORIN86" && preChar == 165 && preFont == "NOORIN82") return 1;
                    if (font == "NOORIN86" && preChar == 166 && preFont == "NOORIN82") return 1;                    
                    break;
                case 56:
                    if (font == "NOORIN86" && preChar == 162 && preFont == "NOORIN28") return 1;
                    if (font == "NOORIN86" && preChar == 166 && preFont == "NOORIN28") return 1; 
                    if (font == "NOORIN86" && preChar == 167 && preFont == "NOORIN28") return 1; 
                    break;
                case 252:
                    if (font == "NOORIN14" && preChar == 90 && preFont == "NOORIN01") return 247;
                    if (font == "NOORIN14" && _henStr.Contains(NextCharWithDiffFont(i))) return 247;
                    //if (font == "NOORIN14" && NextCharWithDiffFont(i) == 'ا') return 247;
                    break;
                case 111:
                    if (font == "NOORIN86" && preChar == 92 && preFont == "NOORIN32") return 1; 
                    break;
                case 112:
                    if (font == "NOORIN86" && preChar == 112 && preFont == "NOORIN32") return 1; 
                    break;
                case 215:
                    if (font == "NOORIN83" && preChar == 39 && preFont == "NOORIN82") return 1; 
                    break;
                case 219: 
                    if (preChar == 140 && preFont == "NOORIN82") return 1; 
                    if (font == "NOORIN83" && preChar == 129 && preFont == "NOORIN82") return 1;
                    if (font == "NOORIN83" && preChar == 130 && preFont == "NOORIN82") return 1;                    
                    break;
                case 225:
                    if (font == "NOORIN83" && preChar == 247 && preFont == "NOORIN81") return 127;
                    if (font == "NOORIN83" && preChar == 144 && preFont == "NOORIN81") return 1; 
                    break;
            }
            return 0;
        }

        private char GetCharDefault(int chCode, string font)
        {
            try
            {                                    
                if (chCode > 255)
                {
                    switch ((int)chCode)
                    {                        
                        case 257: chCode = (char)158; break;  
                        case 338: chCode = (char)140; break;
                        case 339: chCode = (char)156; break;
                        case 352: chCode = (char)138; break;
                        case 353: chCode = (char)154; break;
                        case 376: chCode = (char)159; break;
                        case 381: chCode = (char)142; break;
                        case 382: chCode = (char)158; break;
                        case 402: chCode = (char)131; break;
                        case 710: chCode = (char)136; break;
                        case 732: chCode = (char)152; break;
                        case 956: chCode = (char)181; break;
                        case 8211: chCode = (char)150; break;
                        case 8212: chCode = (char)151; break;
                        case 8216: chCode = (char)145; break;
                        case 8217: chCode = (char)146; break;
                        case 8218: chCode = (char)130; break;
                        case 8220: chCode = (char)147; break;
                        case 8221: chCode = (char)148; break;
                        case 8222: chCode = (char)132; break;
                        case 8224: chCode = (char)134; break;
                        case 8225: chCode = (char)135; break;
                        case 8226: chCode = (char)149; break;
                        case 8230: chCode = (char)133; break;
                        case 8240: chCode = (char)137; break;
                        case 8249: chCode = (char)139; break;
                        case 8250: chCode = (char)155; break;
                        case 8364: chCode = (char)128; break;
                        case 8482: chCode = (char)153; break;
                        case 8722: chCode = (char)173; break;
                    }
                    return (char)chCode;
                }

                /*
                Encoding iso = Encoding.GetEncoding(1252);                
                //byte[] bytes = Encoding.GetEncoding(1252).GetBytes(chCode.ToString());
                byte[] byt = new byte[] { (byte)chCode };
                char[] characters = iso.GetChars(byt);
                char ch = characters[0];                
                */

                char ch = (char)chCode;

                switch (font)
                {
                    case "NOORIN06":
                        if (chCode == 8) ch = (char)157;
                        break;
                    case "NOORIN09":
                        if (chCode == 247) ch = (char)144;
                        break;
                    case "NOORIN11":
                        if (chCode == 219) ch = (char)158;
                        if (chCode == 247) ch = (char)217;
                        break;
                    case "NOORIN14":
                        if (chCode == 5) ch = (char)127;                        
                        if (chCode == 203) ch = (char)173;
                        if (chCode == 215) ch = (char)144;
                        if (chCode == 216) ch = (char)127;
                        if (chCode == 247) ch = (char)252; 
                        break;
                    case "NOORIN15":
                        if (chCode == 247) ch = (char)254;
                        break;
                    case "NOORIN21":
                        if (chCode == 2) ch = (char)121;
                        break;
                    case "NOORIN49":                         
                        if (chCode == 212) ch = (char)127; 
                        break;
                    case "NOORIN53":
                        if (chCode == 213) ch = (char)142;
                        break;
                    case "NOORIN81": 
                        if (chCode == 163) ch = (char)142;
                        if (chCode == 63) ch = (char)142;
                        //if (chCode == 164) ch = (char)127;
                        if (chCode == 166) ch = (char)158; 
                        break;
                    case "NOORIN82":
                        if (chCode == 141) ch = (char)20;
                        if (chCode == 97) ch = (char)158;
                        if (chCode == 147) ch = (char)158;
                        if (chCode == 138) ch = (char)173; 
                        break;
                }

                if (chCode == 142) ch = (char)142; //or 192

                return ch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private char GetCharSpecial(int charCode, string font)
        {
            try
            {                
                Encoding iso = Encoding.GetEncoding(1252);                
                //byte[] bytes = Encoding.GetEncoding(1252).GetBytes(chCode.ToString());
                byte[] byt = new byte[] { (byte)charCode };
                char[] characters = iso.GetChars(byt);
                char ch = characters[0];

                int chCode = (int)ch;
                if ((int)ch > 255)
                {
                    switch ((int)chCode)
                    {
                        case 257: chCode = (char)158; break; 
                        case 338: chCode = (char)140; break;
                        case 339: chCode = (char)156; break;
                        case 352: chCode = (char)138; break;
                        case 353: chCode = (char)154; break;
                        case 376: chCode = (char)159; break;
                        case 381: chCode = (char)142; break;
                        case 382: chCode = (char)158; break;
                        case 402: chCode = (char)131; break;
                        case 710: chCode = (char)136; break;
                        case 732: chCode = (char)152; break;
                        case 956: chCode = (char)181; break;
                        case 8211: chCode = (char)150; break;
                        case 8212: chCode = (char)151; break;
                        case 8216: chCode = (char)145; break;
                        case 8217: chCode = (char)146; break;
                        case 8218: chCode = (char)130; break;
                        case 8220: chCode = (char)147; break;
                        case 8221: chCode = (char)148; break;
                        case 8222: chCode = (char)132; break;
                        case 8224: chCode = (char)134; break;
                        case 8225: chCode = (char)135; break;
                        case 8226: chCode = (char)149; break;
                        case 8230: chCode = (char)133; break;
                        case 8240: chCode = (char)137; break;
                        case 8249: chCode = (char)139; break;
                        case 8250: chCode = (char)155; break;
                        case 8364: chCode = (char)128; break;
                        case 8482: chCode = (char)153; break;
                        case 8722: chCode = (char)173; break;
                    }
                    return (char)chCode;
                }

                return ch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        private string NextCharWithDiffFont(int i)
        {
            if (_lines[i].Trim() == String.Empty) return "0";
            if (!_lines[i].Contains("NOORI")) return "0";
            char[] allChars;
            try
            {
                string[] sepch = { "<=;=>" };
                string[] lineInfo = _lines[i + 1].Split(sepch, StringSplitOptions.None); 
                allChars = lineInfo[2].ToCharArray();
                string font = GetFontNOORI(_lines[i + 1]);
                if (allChars.Length == 0) return "0";
                //return allChars[0];
                int ch = (int)allChars[0];
                DataRow[] drr = _dtLigatures.Select("FontName='" + font + "' AND UnicodeDec=" + ch);
                string lig = (drr.Length == 0) ? "0" : drr[0]["Ligature"].ToString();
                return lig;
            }
            catch (Exception ex)
            { return "0"; }
        }

        private string CleanString(string str)
        {
            string cleanStr = String.Empty; 
            cleanStr = str.Replace("طاسا", "شا");
            cleanStr = cleanStr.Replace("لنہید", "لہٰذ");            
            cleanStr = cleanStr.Replace("سحثیم", "مستقیم");
            cleanStr = cleanStr.Replace("پتھ م بر", "پیغمبر");
            cleanStr = cleanStr.Replace("بیار", "بطور");
            cleanStr = cleanStr.Replace("پتہ غ", "بلوغ");
            cleanStr = cleanStr.Replace("آشمع ن", "آسمان");
            cleanStr = cleanStr.Replace("ماظاً", "ماتحت ");
            cleanStr = cleanStr.Replace("عالم نٹھ", "عالم گیر");
            cleanStr = cleanStr.Replace("ضدانوں", "خزانوں");
            cleanStr = cleanStr.Replace("ضدانے", "خزانے");
            cleanStr = cleanStr.Replace("ضدانہ", "خزانہ");
            cleanStr = cleanStr.Replace("فرماہیں ں", "کارفرمائیاں");
            cleanStr = cleanStr.Replace("ابیکر", "اٹھیں");
            cleanStr = cleanStr.Replace("خاپنکھیاں", "خاصیتوں");
            cleanStr = cleanStr.Replace("اسارہ", "اشارہ");
            cleanStr = cleanStr.Replace("رٹیکیں", "رہتیں");
            cleanStr = cleanStr.Replace("رئنگ", "رہنے");
            cleanStr = cleanStr.Replace("صفت ات", "حشرات");
            cleanStr = cleanStr.Replace("مریبہ ئ", "مرتبۂ");
            cleanStr = cleanStr.Replace("اسارے", "اشارے");
            cleanStr = cleanStr.Replace("شمع ویہ", "سماویہ");
            cleanStr = cleanStr.Replace("براہیں ں", "برائیاں");
            cleanStr = cleanStr.Replace("نقدت", "نجات");
            cleanStr = cleanStr.Replace("لہٹ ائیوں", "گہرائیوں");
            cleanStr = cleanStr.Replace("ہمہ نٹھ", "ہمہ گیر");
            cleanStr = cleanStr.Replace("رنکوں", "رنگوں");
            cleanStr = cleanStr.Replace("خالقب", "خالقیت");            
            cleanStr = cleanStr.Replace("ٹگےطعا", "بہکے چلے");
            cleanStr = cleanStr.Replace("طعاجا", "چلے جا");
            cleanStr = cleanStr.Replace("اجیام", "اجسام");
            cleanStr = cleanStr.Replace("ساداب", "شاداب");
            cleanStr = cleanStr.Replace("براہبو", "براہین");
            cleanStr = cleanStr.Replace("میہر", "مضمر");
            cleanStr = cleanStr.Replace("پھگرپن", "پھکڑپن");
            cleanStr = cleanStr.Replace("متکثرم", "میکنزم");
            cleanStr = cleanStr.Replace("لتلہ القدر", "لیلۃ القدر");
            cleanStr = cleanStr.Replace("بدطیتاں", "بدظنیاں");
            cleanStr = cleanStr.Replace("حلقہ المسح", "خلیفۃالمسیح");
            cleanStr = cleanStr.Replace("غیرمباعیں", "غیرمبائعین");
            cleanStr = cleanStr.Replace("ہیں ا ", "میرا");
            cleanStr = cleanStr.Replace("میراور", "ہیں اور");
            cleanStr = cleanStr.Replace("انقص اف", "انحراف");
            cleanStr = cleanStr.Replace("لہٹ ا", "گہرا");
            cleanStr = cleanStr.Replace("ہیں ں", "ئیاں");
            cleanStr = cleanStr.Replace("شمع ئ", "سمائی");
            cleanStr = cleanStr.Replace("تدرفظت", "تدریجی");
            cleanStr = cleanStr.Replace("نالنہ نی", "ناگہانی");
            cleanStr = cleanStr.Replace("آبطوری", "آبیاری");
            cleanStr = cleanStr.Replace("ظلہ س", "پچاس");
            cleanStr = cleanStr.Replace("رہےمیر", "رہے ہیں");
            cleanStr = cleanStr.Replace("تےمیر", "تے ہیں");
            cleanStr = cleanStr.Replace("میر۔", "ہیں۔");
            cleanStr = cleanStr.Replace("رکھی میر", "رکھی ہیں");
            cleanStr = cleanStr.Replace("ہےمیر", "ہے ہیں");
            cleanStr = cleanStr.Replace("تممیر", "تمہیں");
            cleanStr = cleanStr.Replace("صدفہ", "صدقہ");
            cleanStr = cleanStr.Replace("راسیتاز", "راستباز");
            cleanStr = cleanStr.Replace("فملثر", "فیملیز");
            cleanStr = cleanStr.Replace("میران", "ہیں ان");
            cleanStr = cleanStr.Replace("حتیتاں", "چینیاں");
            cleanStr = cleanStr.Replace("متیتک", "میٹیک");
            cleanStr = cleanStr.Replace("سکتیتک", "سکیننگ");
            cleanStr = cleanStr.Replace("مصاحتیں", "مصاحبین");
            cleanStr = cleanStr.Replace("تی میر", "تی ہیں");
            cleanStr = cleanStr.Replace("المسح", "المسیح");
            cleanStr = cleanStr.Replace("حضرت حلقہ", "حضرت خلیفۃ");
            cleanStr = cleanStr.Replace("لدت", "لذت");
            cleanStr = cleanStr.Replace("مسیعد", "مستعد");
            cleanStr = cleanStr.Replace("ہیںا", "ہیں ا");
            cleanStr = cleanStr.Replace("صطردلی", "سنگ دلی");
            cleanStr = cleanStr.Replace("ضہیں", "ضمیر");
            cleanStr = cleanStr.Replace("بادشاساہ", "بادشاہ");
            cleanStr = cleanStr.Replace("اشاشارہ", "اشارہ");            
            cleanStr = cleanStr.Replace("ہیں ی", "میری");
            cleanStr = cleanStr.Replace("ہیں ے", "میرے");
            cleanStr = cleanStr.Replace("ہیں ا", "میرا");
            //cleanStr = cleanStr.Replace("", "");
            //cleanStr = cleanStr.Replace("", "");
            //cleanStr = cleanStr.Replace("", "");
            //cleanStr = cleanStr.Replace("", "");
            //cleanStr = cleanStr.Replace("", "");
            //cleanStr = cleanStr.Replace("", "");
            //cleanStr = cleanStr.Replace("", "");

            cleanStr = RemoveBoldness(cleanStr);
            return cleanStr;
        }

        private string RemoveBoldness(string str)
        {
            string regularText = String.Empty;
            string[] sep = { "۞٭" };
            string[] lines = str.Split(sep, StringSplitOptions.None);            
            string ln = String.Empty;
            int offSet = 0;
            bool lineFeed = false;
            string line = String.Empty;

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0)
                {
                    regularText = lines[i];
                    string firstBold = Gen.FirstBoldLigature + Gen.FirstBoldLigature + Gen.FirstBoldLigature;
                    regularText = regularText.Replace(firstBold, "");
                    firstBold = Gen.FirstBoldLigature + " " + Gen.FirstBoldLigature + " " + Gen.FirstBoldLigature;
                    regularText = regularText.Replace(firstBold, "");
                }
                else if (i == lines.Length - 1)
                {
                    regularText += lines[i];
                }
                else if (lines[i].Length > 3)
                {
                    line = lines[i];
                    if (line.Contains('۩'))
                    {
                        line = line.Replace("۩", "");
                        lineFeed = true;
                    }
                    offSet = line.Length / 4;
                    ln = line.Remove(offSet, line.Length - offSet);
                    if (lineFeed) regularText += "۩" + ln;
                    else regularText += ln;
                    lineFeed = false;
                }
                else
                {
                    regularText += lines[i];
                }

                if(regularText.Contains("رٹرٹرتعارف"))
                {

                }
            }

            regularText = CleanLines(regularText);
            return regularText;
        }

        private string CleanEnglishWords(string line)
        {
            string cleanLine = String.Empty;
            try
            {                
                StringBuilder engWord = new StringBuilder(String.Empty);
                bool ifNotContainEngChars = line.Any(x => !char.IsLetter(x));
                int engCharAndDigitCount = Regex.Matches(line, @"[a-zA-Z0-9]").Count;
                //int engCharCount = Regex.Matches(line, @"[a-zA-Z]").Count;
                //int engParenthesis = Regex.Matches(line, @"[()]").Count;
                if (engCharAndDigitCount == 0)
                {
                    cleanLine = line;
                }
                else
                {
                    char[] lineChars = line.ToCharArray();
                    foreach (char ch in lineChars)
                    {
                        if (_englishChar.Contains(ch)) engWord.Append(ch);
                    }
                    string[] sep = { engWord.ToString().Trim() };
                    string[] lineBreak = line.Split(sep, StringSplitOptions.None);
                    if (lineBreak.Length == 1) cleanLine = line;
                    else
                    {
                        //if(engCharCount>0 && 
                        cleanLine = lineBreak[1] + " " + engWord.ToString() + " " + lineBreak[0];
                    }
                }

                //cleanLine = cleanLine.Replace(")", "");
                //cleanLine = cleanLine.Replace("(", "");
                return cleanLine.Trim();
            }
            catch (Exception ex)
            {
                return cleanLine;
            }
        }

        private string CleanLines(string str)
        {
            //str = str.Replace(")", "");
            //str = str.Replace("(", "");
            string cleanText = String.Empty;
            string newLine = String.Empty;
            string[] lines = str.Split('۩');            
            string[] dividedLine;
            
            foreach (string line in lines)
            {
                newLine = line;
                newLine = CleanEnglishWords(line);                
                if (chkSwapText.CheckState == CheckState.Checked && newLine.Contains("ﷺ"))
                {
                    dividedLine = newLine.Trim().Split('ﷺ');
                    string ln = dividedLine[1] + "ﷺ" + dividedLine[0];
                    cleanText += ln + Environment.NewLine;                    
                }
                else
                {
                    cleanText += newLine + Environment.NewLine;                    
                }
            }

            return cleanText;            
        }

        private string SeparateLines(string str)
        {
            string cleanText = String.Empty;
            string[] lines = str.Split('۩');            
            foreach (string line in lines)
            {
                cleanText += line + Environment.NewLine;
            }

            return cleanText;
        }        

        private PdfReader GetPdfReader()
        {
            PdfReader reader;
            try
            {
                _filePath = String.Empty;
                lblPDF.Text = String.Empty;
                OpenFileDialog dlg = new OpenFileDialog();                
                dlg.Filter = "PDF Files(*.PDF)|*.PDF|All Files(*.*)|*.*";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _filePath = dlg.FileName.ToString();
                    lblPDF.Text = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                }
                reader = new PdfReader(_filePath);

                try
                {
                    webBrowser1.AllowNavigation = true;                    
                    webBrowser1.Navigate(_filePath);
                }
                catch (Exception ex)
                {
                    string msg = String.Empty;
                    msg = "براہ کرم یہ چیک کرلیں کہ آپ کے سسٹم پر انٹرنیٹ ایکسپلورر درست کام کررہا ہے؟" + Environment.NewLine;
                    msg += "نیز کیا اس میں پی ڈی ایف فائل کو براوزر کے اندر ہی دکھانے کی سیٹنگز موجود ہیں؟";
                    NaseemMessageBox(msg);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }        

        private string GetFontNOORI(string line)
        {
            string font = String.Empty;
            try
            {
                //string[] lineInfo = line.Split(';');
                string[] sepch = { "<=;=>" };
                string[] lineInfo = line.Split(sepch, StringSplitOptions.None);
                if (lineInfo[0].Contains('+'))
                {
                    string[] strSplits = lineInfo[0].Split('+');
                    if (strSplits[0].Contains("NOORI")) font = strSplits[0].Trim();
                    else if (strSplits[1].Contains("NOORI")) font = strSplits[1].Trim();

                    if (font.Contains('-'))
                    {
                        strSplits = font.Split('-');
                        if (strSplits[0].Contains("NOORI")) font = strSplits[0].Trim();
                        else if (strSplits[1].Contains("NOORI")) font = strSplits[1].Trim();
                    }
                }
                    
                else if (lineInfo[0].Contains('-'))
                {
                    string[] strSplits = lineInfo[0].Split('-');
                    if (strSplits[0].Contains("NOORI")) font = strSplits[0].Trim();
                    else if (strSplits[1].Contains("NOORI")) font = strSplits[1].Trim();

                    if (font.Contains('-'))
                    {
                        strSplits = font.Split('-');
                        if (strSplits[0].Contains("NOORI")) font = strSplits[0].Trim();
                        else if (strSplits[1].Contains("NOORI")) font = strSplits[1].Trim();
                    }
                }
                font = CleanFont(font);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return font;
        }

        private string GetFontJameel(string line)
        {
            string font = String.Empty;
            try
            {
                //string[] lineInfo = line.Split(';');
                string[] sepch = { "<=;=>" };
                string[] lineInfo = line.Split(sepch, StringSplitOptions.None);
                if (lineInfo[0].Contains('+'))
                {
                    string[] strSplits = lineInfo[0].Split('+');
                    if (strSplits[0].Contains("Jameel")) font = strSplits[0].Trim();
                    else if (strSplits[1].Contains("Jameel")) font = strSplits[1].Trim();

                    if (font.Contains('-'))
                    {
                        strSplits = font.Split('-');
                        if (strSplits[0].Contains("Jameel")) font = strSplits[0].Trim();
                        else if (strSplits[1].Contains("Jameel")) font = strSplits[1].Trim();
                    }
                }

                else if (lineInfo[0].Contains('-'))
                {
                    string[] strSplits = lineInfo[0].Split('-');
                    if (strSplits[0].Contains("Jameel")) font = strSplits[0].Trim();
                    else if (strSplits[1].Contains("Jameel")) font = strSplits[1].Trim();

                    if (font.Contains('-'))
                    {
                        strSplits = font.Split('-');
                        if (strSplits[0].Contains("Jameel")) font = strSplits[0].Trim();
                        else if (strSplits[1].Contains("Jameel")) font = strSplits[1].Trim();
                    }
                }
                font = CleanFont(font);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return font;
        }

        private string CleanFont(string fnt)
        {
            string font = fnt;
            try
            {
                switch (fnt)
                {
                    case "NOORI001": font = "NOORIN01"; break;
                    case "NOORI002": font = "NOORIN02"; break;
                    case "NOORI003": font = "NOORIN03"; break;
                    case "NOORI004": font = "NOORIN04"; break;
                    case "NOORI005": font = "NOORIN05"; break;
                    case "NOORI006": font = "NOORIN06"; break;
                    case "NOORI007": font = "NOORIN07"; break;
                    case "NOORI008": font = "NOORIN08"; break;
                    case "NOORI009": font = "NOORIN09"; break;
                    case "NOORI010": font = "NOORIN10"; break;
                    case "NOORI011": font = "NOORIN11"; break;
                    case "NOORI012": font = "NOORIN12"; break;
                    case "NOORI013": font = "NOORIN13"; break;
                    case "NOORI014": font = "NOORIN14"; break;
                    case "NOORI015": font = "NOORIN15"; break;
                    case "NOORI016": font = "NOORIN16"; break;
                    case "NOORI017": font = "NOORIN17"; break;
                    case "NOORI018": font = "NOORIN18"; break;
                    case "NOORI019": font = "NOORIN19"; break;
                    case "NOORI020": font = "NOORIN20"; break;
                    case "NOORI021": font = "NOORIN21"; break;
                    case "NOORI022": font = "NOORIN22"; break;
                    case "NOORI023": font = "NOORIN23"; break;
                    case "NOORI024": font = "NOORIN24"; break;
                    case "NOORI025": font = "NOORIN25"; break;
                    case "NOORI026": font = "NOORIN26"; break;
                    case "NOORI027": font = "NOORIN27"; break;
                    case "NOORI028": font = "NOORIN28"; break;
                    case "NOORI029": font = "NOORIN29"; break;
                    case "NOORI030": font = "NOORIN30"; break;
                    case "NOORI031": font = "NOORIN31"; break;
                    case "NOORI032": font = "NOORIN32"; break;
                    case "NOORI033": font = "NOORIN33"; break;
                    case "NOORI034": font = "NOORIN34"; break;
                    case "NOORI035": font = "NOORIN35"; break;
                    case "NOORI036": font = "NOORIN36"; break;
                    case "NOORI037": font = "NOORIN37"; break;
                    case "NOORI038": font = "NOORIN38"; break;
                    case "NOORI039": font = "NOORIN39"; break;
                    case "NOORI040": font = "NOORIN40"; break;
                    case "NOORI041": font = "NOORIN41"; break;
                    case "NOORI042": font = "NOORIN42"; break;
                    case "NOORI043": font = "NOORIN43"; break;
                    case "NOORI044": font = "NOORIN44"; break;
                    case "NOORI045": font = "NOORIN45"; break;
                    case "NOORI046": font = "NOORIN46"; break;
                    case "NOORI047": font = "NOORIN47"; break;
                    case "NOORI048": font = "NOORIN48"; break;
                    case "NOORI049": font = "NOORIN49"; break;
                    case "NOORI050": font = "NOORIN50"; break;
                    case "NOORI051": font = "NOORIN51"; break;
                    case "NOORI052": font = "NOORIN52"; break;
                    case "NOORI053": font = "NOORIN53"; break;
                    case "NOORI054": font = "NOORIN54"; break;
                    case "NOORI055": font = "NOORIN55"; break;
                    case "NOORI056": font = "NOORIN56"; break;
                    case "NOORI057": font = "NOORIN57"; break;
                    case "NOORI058": font = "NOORIN58"; break;
                    case "NOORI059": font = "NOORIN59"; break;
                    case "NOORI060": font = "NOORIN60"; break;
                    case "NOORI061": font = "NOORIN61"; break;
                    case "NOORI062": font = "NOORIN62"; break;
                    case "NOORI063": font = "NOORIN63"; break;
                    case "NOORI064": font = "NOORIN64"; break;
                    case "NOORI065": font = "NOORIN65"; break;
                    case "NOORI066": font = "NOORIN66"; break;
                    case "NOORI067": font = "NOORIN67"; break;
                    case "NOORI068": font = "NOORIN68"; break;
                    case "NOORI069": font = "NOORIN69"; break;
                    case "NOORI070": font = "NOORIN70"; break;
                    case "NOORI071": font = "NOORIN71"; break;
                    case "NOORI072": font = "NOORIN72"; break;
                    case "NOORI073": font = "NOORIN73"; break;
                    case "NOORI074": font = "NOORIN74"; break;
                    case "NOORI075": font = "NOORIN75"; break;
                    case "NOORI076": font = "NOORIN76"; break;
                    case "NOORI077": font = "NOORIN77"; break;
                    case "NOORI078": font = "NOORIN78"; break;
                    case "NOORI079": font = "NOORIN79"; break;
                    case "NOORI080": font = "NOORIN80"; break;
                    case "NOORI081": font = "NOORIN81"; break;
                    case "NOORI082": font = "NOORIN82"; break;
                    case "NOORI083": font = "NOORIN83"; break;
                    case "NOORI084": font = "NOORIN84"; break;
                    case "NOORI085": font = "NOORIN85"; break;
                    case "NOORI086": font = "NOORIN86"; break;
                    case "NOORI087": font = "NOORIN87"; break;
                    case "NOORI088": font = "NOORIN88"; break;
                    case "NOORI089": font = "NOORIN89"; break;
                    case "NOORI090": font = "NOORIN90"; break;
                    case "NOORI091": font = "NOORIN91"; break;
                    case "NOORI092": font = "NOORIN92"; break;
                    case "NOORI093": font = "NOORIN93"; break;
                    case "NOORI094": font = "NOORIN94"; break;
                    case "NOORI095": font = "NOORIN95"; break;
                    case "NOORI096": font = "NOORIN96"; break;
                    case "NOORI097": font = "NOORIN97"; break;
                    case "NOORI098": font = "NOORIN98"; break;
                    case "NOORI099": font = "NOORIN99"; break;
                    case "NOORIC01": font = "NOORIC01"; break;
                    case "NOORIC02": font = "NOORIC02"; break;
                    case "NOORIC": font = "NOORIC"; break;
                }
            }
            catch (Exception ex)
            {
                return font;
            }
            return font;
        }

        private string UrduNumber(string engNum)
        {
            string urduNumber = String.Empty;
            switch (engNum)
            {
                case "0": urduNumber = "۰"; break;
                case "1": urduNumber = "۱"; break;
                case "2": urduNumber = "۲"; break;
                case "3": urduNumber = "۳"; break;
                case "4": urduNumber = "۴"; break;
                case "5": urduNumber = "۵"; break;
                case "6": urduNumber = "۶"; break;
                case "7": urduNumber = "۷"; break;
                case "8": urduNumber = "۸"; break;
                case "9": urduNumber = "۹"; break;
            }
            return urduNumber;
        }        

        #endregion

        #region " Main Function "

        private void GetPdfText()
        {
            try
            {
                _pageNum = Convert.ToInt32(numCounter.Value);
                TextWithFontExtractionStategy S = new TextWithFontExtractionStategy();
                string allText = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(_reader, _pageNum, S);                
                //_reader.Close();

                if (allText.Trim() == String.Empty)
                {
                    string msg = "منتخب کردہ صفحہ تصویری ہے۔" + Environment.NewLine;
                    msg += "فی الحال تصاویر والے صفحات کو پراسس کرنے کی سہولت موجود نہیں ہے۔";
                    NaseemMessageBox(msg);
                    return;
                }

                #region " Jameel Noori Nastaleeq "
                if (!allText.Contains("NOORIN"))
                {
                    if (allText.Contains("Jameel Noori Nastaleeq")) //This condition has a bug, this must be change to proper validation.
                    {
                        string msg = String.Empty;
                        msg = "منتخب کردہ فائل اردو یونی کوڈ متن پر مبنی ہے۔" + Environment.NewLine;
                        msg += "فی الحال ایسی پی ڈی ایف کو پراسس کرنے کی سہولت موجود نہیں ہے۔";                        
                        NaseemMessageBox(msg);
                        return;
                    }
                    else if (allText.Contains("NOORI0"))
                    {
                        string msg = String.Empty;
                        msg = "منتخب کردہ فائل کی انکوڈنگ بہت زیادہ تبدیل شدہ ہے۔" + Environment.NewLine;
                        msg += "فی الحال ایسی پی ڈی ایف کو پراسس کرنے کی سہولت موجود نہیں ہے۔";
                        //NaseemMessageBox(msg);
                        //return;
                    }
                    else
                    {
                        string msg = String.Empty;
                        msg = "منتخب کردہ فائل کی انکوڈنگ ان پیج کے مطابق نہیں ہے یا متن کی بجائے تصویری پی ڈی ایف ہے۔" + Environment.NewLine;
                        msg += "فی الحال ایسی پی ڈی ایف کو پراسس کرنے کی سہولت موجود نہیں ہے۔";
                        NaseemMessageBox(msg);
                        return;
                    }
                }
                #endregion

                bool firstBoldCaptured = false;
                StringBuilder sb = new StringBuilder();
                string[] sep = { "<=!=>" };
                _lines = allText.Split(sep, StringSplitOptions.None);

                string urduNum = String.Empty;
                string preFont = String.Empty;
                int preChar = 0;
                bool needSpace = false;
                //bool rtl = false;

                for (int i = 0; i < _lines.Length; i++)
                {
                    if (_lines[i].Trim() == String.Empty) continue;
                    if (chkSkipEnglishWords.Checked && !_lines[i].Contains("NOORI")) continue;
                    if (_lines[i].Contains("ASW")) continue;
                    string font;                    
                    char[] allChars;
                    try
                    {
                        if (_lines[i].Contains('۩')) sb.Append('۩');
                        string[] sepch = { "<=;=>" };
                        string[] lineInfo = _lines[i].Split(sepch, StringSplitOptions.None);                        
                        font = GetFontNOORI(_lines[i]);
                        allChars = lineInfo[2].ToCharArray();
                    }
                    catch (Exception)
                    { continue; }

                    foreach (char ch in allChars)
                    {
                        int chCode = (int)ch;
                        if (rbEncodingDefault.Checked) chCode = GetCharDefault(chCode, font);
                        else if (rbSpecialMethod.Checked) chCode = GetCharSpecial(chCode, font);
                        //chCode = GetCharSpecial(chCode, font);
                        int skipIt = SkipIt(chCode, font, preChar, preFont, i);
                        if (skipIt == 1) continue;
                        else if (skipIt > 1) chCode = skipIt;
                        preFont = font;
                        preChar = chCode;

                        if (i == 770)
                        {
                        }

                        char chrBold = ch;
                        DataRow[] drr = _dtLigatures.Select("FontName='" + font + "' AND UnicodeDec=" + chCode);                        
                        string lig = (drr.Length == 0) ? "EMPTY" : drr[0]["Ligature"].ToString();
                        
                        #region " Reverse Number "
                        if (chCode >= 48 && chCode <= 57 && font == "NOORIN01")
                        {
                            urduNum += UrduNumber(lig);
                            continue;
                        }
                        else if (urduNum.Length > 0)
                        {
                            char[] uNumChars = urduNum.ToCharArray();
                            urduNum = String.Empty;
                            for (int j = uNumChars.Length - 1; j >= 0; j--)
                            {
                                urduNum += uNumChars[j].ToString();
                            }
                            sb.Append(urduNum);
                            sb.Append(" ");
                            urduNum = String.Empty;
                        }
                        #endregion
                        
                        if (drr.Length > 0 && drr[0]["SkipSpace"].ToString() == "Y") needSpace = false;
                        else if (lig.Length > 0 && _endChar.Contains(lig[lig.Length - 1])) needSpace = true;

                        if (drr.Length == 0 && _englishChar.Contains(ch)) sb.Append(ch);
                        else if (drr.Length > 0)
                        {
                            if (drr[0]["Ligature"].ToString().Trim() == String.Empty)
                            {
                                continue;
                            }
                            else
                            {
                                sb.Append(drr[0]["Ligature"].ToString());
                                //sb.Append(lig);
                            }
                        }
                        else if (chrBold == '۞')
                        {
                            firstBoldCaptured = true;
                            sb.Append("۞٭");
                        }
                        if (!firstBoldCaptured) Gen.FirstBoldLigature = lig;
                        if (lig == "(")
                        {
                            //sb.Append('\u200e');
                        }
                        if (needSpace) sb.Append(" ");
                        needSpace = false;                        
                        if (sb.ToString().Contains("وجہ سےمیری"))
                        {
                        }

                    }
                }

                rtbxPDFText.Text = CleanString(sb.ToString());
                if (rtbxPDFText.Text.Trim() == String.Empty)
                {
                    rbSpecialMethod.Checked = true;
                    GetPdfText();
                    rbEncodingDefault.Checked = true;
                }
            }
            catch (Exception ex)
            {
                string msg = String.Empty;
                if (ex.Message == "Could not find image data or EI")
                {                    
                    msg = "منتخب کردہ صفحے میں غالباً تصاویر بھی شامل ہیں۔" + Environment.NewLine;
                    msg += "فی الحال تصاویر والے صفحات کو پراسس کرنے کی سہولت موجود نہیں ہے۔";
                    NaseemMessageBox(msg);
                }
                else if (ex.Message == "String cannot be of zero length.\r\nParameter name: oldValue")
                {                    
                    msg = "منتخب کردہ فائل یا تو یونی کوڈ متن پر مبنی ہے یا متن کی بجائے تصویری پی ڈی ایف ہے۔" + Environment.NewLine;
                    msg += "فی الحال ایسی پی ڈی ایف کو پراسس کرنے کی سہولت موجود نہیں ہے۔";
                    NaseemMessageBox(msg);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Naseem Inpage-PDF Texter");
                }
            }
        }                       

        #endregion

        #endregion



        #region " Other functions "

        private string GetResourceTextFile(string filename)
        {
            string result = string.Empty;

            using (Stream stream = this.GetType().Assembly.
                       GetManifestResourceStream("UnicodeToInpage." + filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

        private void AdjustTextBoxRMargin()
        {
            rtbxPDFText.RightMargin = rtbxPDFText.Size.Width - 35;
        }

        private void SetScreenResolution()
        {
            try
            {
                System.Drawing.Rectangle resolution = Screen.PrimaryScreen.Bounds;

                if (resolution.Height < 700)
                {
                    pnlPDF.Height = panel2.Height;
                    rtbxPDFText.Height = panel4.Height - 12;
                    rtbxPDFText.Top = panel4.Top;
                    _font = new System.Drawing.Font("Jameel Noori Nastaleeq", 11);
                    _font2 = new System.Drawing.Font("Jameel Noori Nastaleeq", 9);
                }
                else if (resolution.Height > 700 && resolution.Width > 1000 && resolution.Width < 1100)
                {
                    _font = new System.Drawing.Font("Jameel Noori Nastaleeq", 15);
                    _font2 = new System.Drawing.Font("Jameel Noori Nastaleeq", 12);
                    AdjustTextBoxRMargin();
                }
                else if (resolution.Height > 700 && resolution.Width > 1100 && resolution.Width < 1200)
                {
                    _font = new System.Drawing.Font("Jameel Noori Nastaleeq", 16);
                    _font2 = new System.Drawing.Font("Jameel Noori Nastaleeq", 13);
                    AdjustTextBoxRMargin();
                }
                else if (resolution.Height > 700 && resolution.Width > 1200)
                {
                    _font = new System.Drawing.Font("Jameel Noori Nastaleeq", 16);
                    _font2 = new System.Drawing.Font("Jameel Noori Nastaleeq", 14);
                    AdjustTextBoxRMargin();
                }


            }
            catch (Exception ex)
            { 

            }
        }

        private void NaseemMessageBox(string msg)
        {
            try
            {
                frmMessageBox msgBox = new frmMessageBox(msg);
                msgBox.ShowInTaskbar = false;
                msgBox.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Naseem MsgBox");
            }
        }

        #endregion



        #region " Functions Not In Use "        

        private DataTable GetExcelData(string sheet)
        {
            try
            {
                string fullPathToExcel = @"C:\Users\Rana\Desktop\TexterBin\_UniPDF.xlsx";
                //string fullPathToExcel = @"_UniPDF.xlsx";
                string conString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=yes'", fullPathToExcel);
                OleDbConnection con = new OleDbConnection(conString);
                con.Open();
                string query = "SELECT * from [" + sheet + "$]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private char GetCharUni(int chCode)
        {
            try
            {
                //sb.Append("\u0627");
                Encoding iso = Encoding.Unicode;
                byte[] byt = new byte[] { (byte)chCode };
                char[] characters = iso.GetChars(byt);
                return characters[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        #endregion

    }
}
