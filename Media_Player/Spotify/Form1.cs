using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxWMPLib;
using PlaylistSharpLib;

namespace Spotify
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }
        
        string[] paths, files;
        int startindex =0;
        string[] filename, filepath;
        Boolean playnext = false;
        bool _playing = false;
        public bool isplay
        {
            get
            {
                return _playing;
            }
            set
            {
                _playing = value;
                if (_playing)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.pause();bunifuImageButton20.Image = bunifuImageButton1.Image;
                }
                else
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play(); bunifuImageButton20.Image = bunifuImageButton19.Image;
                }
            }
        }
        public void stoplayer()
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }
        public void playfile(int playlistindex)
        {
            if (listBox1.Items.Count<=0)
            {
                return;

            }
            if (playlistindex<0 )
            {
                return;
            }
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.URL = filepath[playlistindex];
            axWindowsMediaPlayer1.Ctlcontrols.next();
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Focus();
            
            
        }
        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            if (this.WindowState==FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            startindex = listBox1.SelectedIndex;
            playfile(startindex);
            bunifuCustomLabel1.Text = listBox1.Text;
           // pictureBox1.Image == listBox1.SelectedItem;
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            startindex = 0;
            playnext = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "(mp3,wav,mp4,mov,wmv,mpg,avi,3gp,flv)|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mov;*.mpg;*.avi;*.3gp;*.flv|all files|*.* ";
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                filename = openFileDialog.SafeFileNames;
                filepath = openFileDialog.FileNames;
                for (int i = 0; i <= filename.Length -1; i++)
                {
                    listBox1.Items.Add(filename[i]);
                }
                startindex = 0;
                playfile(0);
                
            }
        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        public EventHandler onActon = null;
        private void bunifuImageButton20_Click(object sender, EventArgs e)
        {
            isplay = !isplay;
            if (onActon!=null)
            {
                onActon.Invoke(this,e);
            }
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {
            bunifuCustomLabel1.Text = listBox1.Text;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (startindex==listBox1.Items.Count-1)
            {
                startindex = listBox1.Items.Count - 1;
            }
            else if (startindex<listBox1.Items.Count)
            {
                startindex = startindex + 1;
            }
            playfile(startindex);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (startindex>0)
            {
                startindex = startindex - 1;
            }
            playfile(startindex);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label13.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
           // label14.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString.ToString();
            if (axWindowsMediaPlayer1.playState==WMPLib.WMPPlayState.wmppsPlaying)
            {
                bunifuSlider1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            }
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState==WMPLib.WMPPlayState.wmppsPlaying)
            {
                bunifuSlider1.MaximumValue = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                timer1.Start();
            }
            else if (axWindowsMediaPlayer1.playState==WMPLib.WMPPlayState.wmppsStopped)
            {
                timer1.Stop();
                bunifuSlider1.Value = 0;
            }

        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                bunifuSlider1.MaximumValue = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                timer1.Start();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                timer1.Stop();
                bunifuSlider1.Value = 0;
            }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.setMode("shuffle", true);
            


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            startindex = 0;
            playnext = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "(mp3,wav,mp4,mov,wmv,mpg,avi,3gp,flv)|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mov;*.mpg;*.avi;*.3gp;*.flv|all files|*.* ";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog.SafeFileNames;
                filepath = openFileDialog.FileNames;
                for (int i = 0; i <= filename.Length - 1; i++)
                {
                    listBox1.Items.Add(filename[i]);
                }
                startindex = 0;
                playfile(0);

            }
        }

        private void bunifuImageButton10_Click_2(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuSlider2_ValueChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = bunifuSlider2.Value;
            label15.Text = bunifuSlider2.Value.ToString();
            if (bunifuSlider2.Value==0)
            {
                pictureBox3.ImageLocation = "C:\\Users\\KELREN\\Desktop\\1.png";
            }
            else if (bunifuSlider2.Value==100)
            {
                pictureBox3.ImageLocation = "C:\\Users\\KELREN\\Desktop\\yüksek.png";
            }
            else
            {
                pictureBox3.ImageLocation = "C:\\Users\\KELREN\\Desktop\\kısık.png";
            }
            

        }
        private void bunifuImageButton10_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
