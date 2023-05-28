using System;
using System.Drawing;
using System.Drawing.Text;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Mario_Kostina_kn_2_2
{
    public partial class Menu : Form
    {
        private SoundPlayer soundPlayer;
        public Menu()
        {
            InitializeComponent();
            InitializeSoundPlayer();
        }

        private void InitializeSoundPlayer()
        {
            soundPlayer = new SoundPlayer(@"C:\Users\Анастасия\OneDrive\Documents\Универ\Proga\2 семестр\Mario\Mario_Kostina_kn-2-2\Super-Mario-64-Bob-omb-Battlefield.wav");
            soundPlayer.Play();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void Play_Click(object sender, EventArgs e)
        {
           soundPlayer.Stop();
           Form2 g = new Form2();
           g.Show();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}