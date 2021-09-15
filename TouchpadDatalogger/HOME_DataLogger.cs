using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchpadDatalogger
{
    public partial class HOME_DataLogger : Form
    {
        private Blueboard mBlueboard;
        private RecordMgr mRecordMgr;
        private bool mIsRecording;
        SerialPort mComPort; 

        public HOME_DataLogger()
        {
            InitializeComponent();

            mBlueboard = new Blueboard();
            mRecordMgr = new RecordMgr();

            dgvGesturePlaylist.RowHeadersVisible = false;
            dgvGesturePlaylist.AllowUserToAddRows = false;

            setInitButtonState();
            loadRecordedData();

            mIsRecording = false; 
        }

        private void btnRecord_Click( object sender, EventArgs e )
        {
            if ( !mIsRecording )
            {
                using ( FileNamePrompt formFileNamePrompt = new FileNamePrompt() )
                {
                    Visible = false;
                    formFileNamePrompt.StartPosition = StartPosition;
                    formFileNamePrompt.ShowDialog();
                    Visible = true;

                    mIsRecording = true; 
                    btnRecord.Text = "Stop Recording";

                    mRecordMgr.mCurrentFilename = FileNamePrompt.Filename;

                    Thread thread = new Thread( recordData );
                    thread.Start();
                }
            }
            else
            {
                mIsRecording = false;
                btnRecord.Text = "Record";

                loadRecordedData();
            }
        }

        private void HOME_DataLogger_Load( object sender, EventArgs e )
        {
            if(mBlueboard.IsConnected)
            {
                lblComStatus.Text = "Connected";
            }
        }

        private void btnRunPCFilter_Click( object sender, EventArgs e )
        {

        }

        private void btnApplyFilterTeensy_Click( object sender, EventArgs e )
        {

        }

        private void recordData()
        { 
            SampleUtil samp; 

            while(mIsRecording)
            {
                if(mBlueboard.GetSample( out samp ))
                {
                    mRecordMgr.SaveSampleToFile( samp );
                }
            }
        }

        private void setInitButtonState()
        {
            if(mBlueboard.IsConnected)
            {
                btnRecord.Enabled = true; 
            }

            if(mRecordMgr.RecordedFiles.Count != 0)
            {
                //btnRunPCFilter.Enabled = true;
                //btnApplyFilterTeensy.Enabled = true; 
            }
        }

        private void loadRecordedData()
        {
            flushRows();
            mRecordMgr.LoadRecordedFiles(); 
            foreach(Tuple<string, string, string> val in mRecordMgr.RecordedFiles)
            {
                int row = dgvGesturePlaylist.Rows.Add( val.Item2, val.Item3 );
                dgvGesturePlaylist.Rows[row].Cells[0].ToolTipText = val.Item1;
            }
        }
        private void flushRows()
        {
            dgvGesturePlaylist.Visible = false;
            while ( dgvGesturePlaylist.RowCount > 0 )
            {
                dgvGesturePlaylist.Rows.RemoveAt( dgvGesturePlaylist.RowCount - 1 );
            }
            dgvGesturePlaylist.Visible = true;
        }

        private void dgvGesturePlaylist_CellContentClick( object sender, DataGridViewCellEventArgs e )
        {

        }
    }
}
