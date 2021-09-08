using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchpadDatalogger
{
    class RecordMgr
    {
        public List<Tuple<string, string, string>> RecordedFiles { get { return mRecordedFiles; } } 
        private List<Tuple<string, string, string>> mRecordedFiles;             // first string is the path, second is the file name, and third is the date of creation

        public string mRecordDestPath;
        public string mCurrentFilename; 

        public RecordMgr()
        {
            mRecordedFiles = new List<Tuple<string, string, string>>(); 
            string myDocsPath = System.Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
            mRecordDestPath = myDocsPath + @"\Cirque\DataLogger";

            if ( !Directory.Exists( mRecordDestPath ) )
            {
                Directory.CreateDirectory( mRecordDestPath );
            }

            LoadRecordedFiles(); 
        }

        public void LoadRecordedFiles()
        {
            mRecordedFiles.Clear(); 
            string[] fileArray = Directory.GetFiles( mRecordDestPath );

            // Loop through all the files in the log directory.
            foreach(string filePath in fileArray)
            {
                // Verify the file is a log file. 
                if( isRecordedData( filePath ) )
                {
                    // Add each new valid file to the list of valid files. 
                    string creationTime = File.GetCreationTime( filePath ).ToString();
                    string fileName = Path.GetFileNameWithoutExtension( filePath );
                    Tuple<string, string, string> fileMetaData = 
                        new Tuple<string, string, string>(filePath, fileName, creationTime);

                    mRecordedFiles.Add( fileMetaData );
                }
            }
        }

        public bool SaveSampleToFile(SampleUtil sample)
        {
            bool result = false;

            bool noHeader = false; 
            if(!File.Exists(Path.Combine(mRecordDestPath, mCurrentFilename)))
            {
                noHeader = true; 
            }

            using ( StreamWriter outputFile = File.AppendText( Path.Combine( mRecordDestPath, mCurrentFilename ) ) )
            {
                // If this is the first creating this low add the header to the file
                if (noHeader)
                {
                    outputFile.Write( sample.GetHeader() );
                }

                outputFile.Write( sample.ToString() );
            }

            return result; 
        }

        private bool isRecordedData(string filePath)
        {
            // check to see if the file is a *.csv and if the header matches the standard file output
            bool result = false;

            if ( filePath.Contains(".csv") )
            {
                using ( CsvReader csv = new CsvReader( new StreamReader(filePath), true ) )
                {
                    string[] headers = csv.GetFieldHeaders();

                    // Check if length of header is correct before matching header. 
                    if (headers.Length >= 6)
                    {
                        result = true; 
                        result &= headers[0].Contains("TIMESTAMP");
                        result &= headers[1].Contains("MEAS X");
                        result &= headers[2].Contains("MEAS Y");
                        result &= headers[3].Contains("CONFIDENCE");
                        result &= headers[4].Contains("FILTER X");
                        result &= headers[5].Contains("FILTER Y");
                    }
                }
            }

            return result; 
        }
    }
}
