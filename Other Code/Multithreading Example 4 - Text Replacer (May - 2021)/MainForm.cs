using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Assignment4_Form
{
 
    /// <summary>
    /// The only GUI needed
    /// </summary>
    public partial class MainForm : Form
    {
        BoundedBuffer buffer;

        Writer writer;
        Reader reader;
        Modifier modifier;

        Thread writerThread;
        Thread readerThread;
        Thread modifierThread;

        List<string> fileStrings;

               /// <summary>
        /// Default constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
           
        }

        /// <summary>
        /// Menu exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Ev. stop running threads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (writerThread != null && writerThread.IsAlive)
                writerThread.Abort();
            if (readerThread != null && readerThread.IsAlive)
                readerThread.Abort();
            if (modifierThread != null && modifierThread.IsAlive)
                modifierThread.Abort();

            System.Environment.Exit(System.Environment.ExitCode);
        }

        /// <summary>
        /// Menu file open source file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileToLoad();
        }

        /// <summary>
        /// Save the destination text to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuFileSave_Click(object sender, EventArgs e)
        {
            CreateDestinationFile();
        }

        void OpenFileToLoad()
        { 
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Read text from the file.
                    rtxtSource.Text = File.ReadAllText(dlgOpen.FileName);
                    lblSource.Text = "Source file: " + dlgOpen.SafeFileName;

                    // Split the text into words.
                    fileStrings = new List<string>(rtxtSource.Text.Split(' '));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Could not open source file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {

                }
            }           
        }

        /// <summary>
        /// The final step is to create output
        /// </summary>
        void CreateDestinationFile()
        {
            if (dlgDest.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlgDest.FileName, rtxtTarget.Text);

                rtxtTarget.Clear();

                mnuFileSave.Enabled = false;
            }
        }

        void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileToLoad();
        }

        void replaceButton_Click(object sender, EventArgs e)
        {
            if (fileStrings == null || fileStrings.Count == 0)
                return;

            rtxtTarget.Clear();

            // Initialize all of the objects.
            buffer = new BoundedBuffer(rtxtTarget, 10, txtFind.Text, txtReplace.Text);
            writer = new Writer(buffer, fileStrings);
            reader = new Reader(buffer, fileStrings.Count);
            modifier = new Modifier(buffer, fileStrings.Count);

            // Initialize and run the threads.
            writerThread = new Thread(new ThreadStart(writer.Run));
            readerThread = new Thread(new ThreadStart(reader.Run));
            modifierThread = new Thread(new ThreadStart(modifier.Run));

            writerThread.Start();
            readerThread.Start();
            modifierThread.Start();

            // Only enable the file save button after all of the threads are finished.
            while (writerThread.IsAlive || readerThread.IsAlive || modifierThread.IsAlive)
            {
                Application.DoEvents();
            }

            mnuFileSave.Enabled = true;
        }
    }
}
