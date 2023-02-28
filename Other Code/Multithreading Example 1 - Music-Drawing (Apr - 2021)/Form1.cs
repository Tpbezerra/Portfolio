using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MulProAssignment1
{
    public partial class Form1 : Form
    {
        PlayMusic music;
        DisplayText textDisplay;
        DisplayShape shapeDisplay;

        Thread musicThread;
        Thread displayThread;
        Thread drawThread;

        Random rand;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rand = new Random();

            music = new PlayMusic();
            textDisplay = new DisplayText(rand, ref Panel_DisplayText);
            shapeDisplay = new DisplayShape(ref Panel_DrawingTriangle);

            musicThread = new Thread(new ThreadStart(music.CreatePlayer));
            musicThread.Start();
        }

        #region Play Music
        private void Btn_OpenMusicFile_Click(object sender, EventArgs e)
        {
            openMusicFileDialog.ShowDialog();
        }

        private void openMusicFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            Label_MusicFilePath.Text = openMusicFileDialog.FileName;
            music.Open(openMusicFileDialog.FileName);
        }

        private void Btn_PlayMusicFile_Click(object sender, EventArgs e)
        {
            music.Play();
        }

        private void Btn_StopMusicFile_Click(object sender, EventArgs e)
        {
            music.Pause();
        }
        #endregion

        #region Text Display
        private void Panel_DisplayText_Paint(object sender, PaintEventArgs e)
        {
            textDisplay.PaintOnDisplayPanel();
        }

        private void Btn_StartDisplay_Click(object sender, EventArgs e)
        {
            if (displayThread != null && displayThread.IsAlive)
                return;

            displayThread = new Thread(new ThreadStart(textDisplay.UpdateTextPosition));
            displayThread.Start();

            Btn_StartDisplay.Enabled = false;
            Btn_StopDisplay.Enabled = true;
        }

        private void Btn_StopDisplay_Click(object sender, EventArgs e)
        {
            if (displayThread.IsAlive)
                displayThread.Abort();

            Btn_StartDisplay.Enabled = true;
            Btn_StopDisplay.Enabled = false;
        }
        #endregion

        #region Draw Display
        private void Panel_DrawingTriangle_Paint(object sender, PaintEventArgs e)
        {
            shapeDisplay.PaintOnDrawPanel();
        }

        private void Btn_StartDrawing_Click(object sender, EventArgs e)
        {
            if (drawThread != null && drawThread.IsAlive)
                return;

            drawThread = new Thread(new ThreadStart(shapeDisplay.UpdateTrianglePosition));
            drawThread.Start();

            Btn_StartDrawing.Enabled = false;
            Btn_StopDrawing.Enabled = true;
        }

        private void Btn_StopDrawing_Click(object sender, EventArgs e)
        {
            if (drawThread.IsAlive)
                drawThread.Abort();

            Btn_StartDrawing.Enabled = true;
            Btn_StopDrawing.Enabled = false;
        }
        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            music.Pause();
            musicThread.Abort();
        }
    }
}
