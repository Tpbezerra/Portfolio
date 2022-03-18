using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Assignment2_Form
{
    public partial class MainForm : Form
    {
        CharacterBuffer buffer;
        Reader reader;
        Writer writer;
        Thread readerThread;
        Thread writerThread;

        bool useSync;

        Random rand = new Random();

        public MainForm()
        {
            InitializeComponent();

            buffer = new CharacterBuffer(ListBox_Writer, ListBox_Reader);
        }

        void Btn_Run_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox_String.Text))
                return;

            writer = new Writer(buffer, rand);
            writer.SetStringToWrite(TextBox_String.Text);
            writer.SetSyncMode(useSync);
            reader = new Reader(buffer, rand);
            reader.SetStringToRead(TextBox_String.Text);
            reader.SetSyncMode(useSync);

            writerThread = new Thread(new ThreadStart(writer.Run));
            readerThread = new Thread(new ThreadStart(reader.Run));

            writerThread.Start();
            readerThread.Start();

            Btn_Run.Enabled = false;

            while (writerThread.IsAlive || readerThread.IsAlive)
            {
                Application.DoEvents();
            }

            if (reader.StringRead == TextBox_String.Text)
                Label_Status.Text = "Success";
            else
                Label_Status.Text = "Failure";

            Label_WriterResult.Text = writer.StringWritten;
            Label_ReaderResult.Text = reader.StringRead;

            Btn_Clear.Enabled = true;
        }

        void Btn_Clear_Click(object sender, EventArgs e)
        {
            TextBox_String.Clear();
            ListBox_Writer.Items.Clear();
            ListBox_Reader.Items.Clear();
            Label_WriterResult.Text = "";
            Label_ReaderResult.Text = "";
            Btn_Run.Enabled = true;

            Btn_Clear.Enabled = false;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        void rbtnSync_CheckedChanged(object sender, EventArgs e)
        {
            rbtnAsync.CheckedChanged -= rbtnAsync_CheckedChanged;
            rbtnSync.CheckedChanged -= rbtnSync_CheckedChanged;

            rbtnSync.Checked = true;
            rbtnAsync.Checked = false;

            useSync = true;
            rbtnAsync.CheckedChanged += rbtnAsync_CheckedChanged;
            rbtnSync.CheckedChanged += rbtnSync_CheckedChanged;
        }

        void rbtnAsync_CheckedChanged(object sender, EventArgs e)
        {
            rbtnAsync.CheckedChanged -= rbtnAsync_CheckedChanged;
            rbtnSync.CheckedChanged -= rbtnSync_CheckedChanged;

            rbtnAsync.Checked = true;
            rbtnSync.Checked = false;

            useSync = false;
            rbtnAsync.CheckedChanged += rbtnAsync_CheckedChanged;
            rbtnSync.CheckedChanged += rbtnSync_CheckedChanged;
        }
    }
 }
