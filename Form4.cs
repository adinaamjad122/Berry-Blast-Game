using FruitBurstGame;
using System;
using System.IO;
using System.Windows.Forms;

namespace FruitBurstGame
{
    public partial class Form4 : Form
    {
        private int currentScore;
        // Text file automatically project folder ke andar debug directory mein save hogi
        private string filePath = "highscore.txt";

        public Form4(int score)
        {
            InitializeComponent();
            currentScore = score;

            // Aap ke custom design labels par text update karna
            lblFinalScore.Text = "Your Score: " + currentScore;

            HandleHighScore();
        }

        private void HandleHighScore()
        {
            int highScore = 0;

            // 1. Agar highscore file pehle se maujood hai to purana high score read karo
            if (File.Exists(filePath))
            {
                string savedScore = File.ReadAllText(filePath);
                int.TryParse(savedScore, out highScore);
            }

            // 2. Agar current score purane score se bada hai to naya score save karo
            if (currentScore > highScore)
            {
                highScore = currentScore;
                File.WriteAllText(filePath, highScore.ToString());
                lblHighScore.Text = "Highest Score: " + highScore ;
            }
            else
            {
                lblHighScore.Text = "Highest Score: " + highScore;
            }
        }

        // Back Button (btnBack): Yeh aap ka button hai jo Main Menu (Form1) par wapis le jaye ga
        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 menu = new Form1();
            menu.Show();
            this.Close(); // Current Game Over form ko band kar dega
        }
    }
}