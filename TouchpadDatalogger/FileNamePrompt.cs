using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchpadDatalogger
{
    public partial class FileNamePrompt : Form
    {
        public FileNamePrompt()
        {
            InitializeComponent();
            label1.Text = "Record Name:"; 
        }

        private void btnStartRec_Click( object sender, EventArgs e )
        {
            string input = tbRecordName.Text;
            input = input.Replace( ' ', '_' );

            if ( isValid(input) )
            {
                // execute start recording process
            }
            else
            {
                // update label with error
                label1.Text = "Record Name: ERROR - INVALID INPUT *No Special Chars*"; 
            }
        }

        private bool isValid(string input)
        {
            return Regex.IsMatch( input, @"^[a-zA-Z0-9]+(?:[\w -]*[a-zA-Z0-9]+)*$" );
        }
    }
}
