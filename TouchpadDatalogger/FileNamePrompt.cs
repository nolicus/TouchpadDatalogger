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
        public static string Filename; 
        public FileNamePrompt()
        {
            InitializeComponent();
            label1.Text = "Record Name:";
            Filename = ""; 
        }

        private void btnStartRec_Click( object sender, EventArgs e )
        {
            Filename = tbRecordName.Text;
            Filename = Filename.Replace( ' ', '_' );

            if ( isValid( Filename ) )
            {
                Filename += ".csv";
                Close(); 
            }
            else
            {
                // update label with error
                label1.Text = "Record Name: ERROR - INVALID INPUT *No Special Chars*";
                Filename = ""; 
            }
        }

        private bool isValid(string input)
        {
            return Regex.IsMatch( input, @"^[a-zA-Z0-9]+(?:[\w -]*[a-zA-Z0-9]+)*$" );
        }
    }
}
