using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UnicodeToInpage
{
    public partial class frmMessageBox : Form
    {

        #region " Member Variables and Constructors "

        string _message = String.Empty;
        System.Drawing.Font _font;

        public frmMessageBox()
        {
            InitializeComponent();
        }        

        public frmMessageBox(string msg)
            : this()
        {
            _message = msg;
        }

        #endregion


        #region " Events "

        private void frmMessageBox_Load(object sender, EventArgs e)
        {
            SetScreenResolution();
            lblMessage.Font = _font;            
            lblMessage.Text = _message;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region " Other Functions "

        private void SetScreenResolution()
        {
            try
            {
                System.Drawing.Rectangle resolution = Screen.PrimaryScreen.Bounds;

                if (resolution.Height < 700)
                {                    
                    _font = new System.Drawing.Font("Jameel Noori Nastaleeq", 9);
                }
                else if (resolution.Height > 700 && resolution.Width > 1000 && resolution.Width < 1100)
                {                    
                    _font = new System.Drawing.Font("Jameel Noori Nastaleeq", 12);                    
                }
                else if (resolution.Height > 700 && resolution.Width > 1100 && resolution.Width < 1200)
                {                    
                    _font = new System.Drawing.Font("Jameel Noori Nastaleeq", 13);                    
                }
                else if (resolution.Height > 700 && resolution.Width > 1200)
                {                    
                    _font = new System.Drawing.Font("Jameel Noori Nastaleeq", 14);                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

    }
}
