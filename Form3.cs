using FruitBurstGame;
using FruitBurstGameGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FruitBurstGame
{
    public partial class Form3 : Form
    {
        private Player player;
        private int score = 0;
        private int health = 100;
        private System.Collections.Generic.List<Berry> berriesList = new System.Collections.Generic.List<Berry>();
        private System.Collections.Generic.List<Arrow> arrowsList = new System.Collections.Generic.List<Arrow>();
        private Random random = new Random();
        private int berrySpawnTimer = 0; // Yeh count karega ke kab nayi berry lani hai

        public Form3()
        {
            InitializeComponent();

            picPlayer.BackColor = Color.Transparent; // Player ke picture box ka background transparent kar dein taake wo achi lage
            // Player object create karain gay (Speed rakh rahe hain 12)
            player = new Player(picPlayer, 12);
        }

        // Timer har 20ms baad chalay ga aur game ko update karega
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            player.Move(); // Player ko move karwao

            // 2. Berries ko spawn (create) karne ka time check karain
            berrySpawnTimer++;
            if (berrySpawnTimer >= 40) // Har 40 ticks (almost 1 second) baad aik nayi berry aaye gi
            {
                berrySpawnTimer = 0;
                CreateNewBerry();
            }

            // 3. Har berry ko neeche move karwao
            for (int i = berriesList.Count - 1; i >= 0; i--)
            {
                Berry b = berriesList[i];
                b.Move();

                // Agar berry screen ke bilkul neeche touch ho jaye (Player miss kar de)
                if (b.Sprite.Top > this.Height)
                {
                    health -= 10; // Health kam ho jaye gi
                    lblHealth.Text = "Health: " + health;

                    this.Controls.Remove(b.Sprite); // Screen se delete karo
                    b.Sprite.Dispose();
                    berriesList.RemoveAt(i); // List se nikalo

                    // GAME OVER CHECK: Agar health 0 ya us se kam ho jaye
                    if (health <= 0)
                    {
                        gameTimer.Stop(); // Game ka timer rok do taake berries girna band ho jain

                        // Yeh line Form 4 ko create karegi aur aap ka score usay transfer kar degi
                        Form4 gameOverWindow = new Form4(score);

                        // Yeh line Form 4 ko screen par khol degi (Show karwa degi)
                        gameOverWindow.Show();

                        // Yeh line aap ki chalne wali game window (Form 3) ko band kar degi
                        this.Hide();
                        this.Close();

                        break; // Loop se baahar nikal jao
                    }
                }
            }

            // 4. Har arrow ko upar move karwao
            for (int i = arrowsList.Count - 1; i >= 0; i--)
            {
                Arrow arrow = arrowsList[i];
                arrow.Move();

                // Agar arrow screen se upar bahar nikal jaye, to delete kar do
                if (arrow.Sprite.Top < 0)
                {
                    this.Controls.Remove(arrow.Sprite);
                    arrow.Sprite.Dispose();
                    arrowsList.RemoveAt(i);
                }
            }

            // 5. Collision Detection: Check karain kya teer berry ko laga?
            for (int i = arrowsList.Count - 1; i >= 0; i--)
            {
                for (int j = berriesList.Count - 1; j >= 0; j--)
                {
                    // Agar i ya j kisi wajah se list se pehle hi remove ho chuka ho to crash na ho
                    if (i >= arrowsList.Count || j >= berriesList.Count) continue;

                    Arrow arrow = arrowsList[i];
                    Berry berry = berriesList[j];

                    // Rubrics: Collision detection system working well
                    if (arrow.Sprite.Bounds.IntersectsWith(berry.Sprite.Bounds))
                    {
                        // Rubrics: Scoring and state management
                        // Agar Golden Berry ko hit kiya to 30 points, normal ko kiya to 10 points
                        if (berry.Type == GameObjectType.GoldenBerry)
                        {
                            score += 30;
                        }
                        else
                        {
                            score += 10;
                        }

                        // Score label ko update karain
                        lblScore.Text = "Score: " + score;

                        // Screen se dono cheezon ko mita do
                        this.Controls.Remove(arrow.Sprite);
                        this.Controls.Remove(berry.Sprite);

                        arrow.Sprite.Dispose();
                        berry.Sprite.Dispose();

                        // Lists se bhi nikal do
                        arrowsList.RemoveAt(i);
                        berriesList.RemoveAt(j);

                        // Kyun ke yeh teer ab khatam ho chuka hai, andar wale loop se baahar nikal jao
                        break;
                    }
                }
            }
        }

        private void CreateNewBerry()
        {
            PictureBox berryPic = new PictureBox();
            berryPic.Size = new System.Drawing.Size(45, 45); // Thora bada size taake picture pyari lagay
            berryPic.SizeMode = PictureBoxSizeMode.StretchImage;
            berryPic.BackColor = System.Drawing.Color.Transparent; // Background khali taake box nazar na aaye

            // Screen par horizontal (Left/Right) kis jagah se giregi (Random position)
            int randomX = random.Next(10, this.Width - 60);
            berryPic.Location = new System.Drawing.Point(randomX, -50);

            // 20% chance hai ke golden berry banay
            bool isGolden = random.Next(1, 6) == 1;
            Berry newBerry;

            try
            {
                if (isGolden)
                {
                    // Yahan apni golden berry ki picture ka poora path (address) likhein
                    // Note: @ lagana zaroori hai aur end par file ka naam (.png ya .jpg) check kar lein

                    string userPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    berryPic.Image = Image.FromFile(System.IO.Path.Combine(userPictures, "saved pictures", "golden-berry-isolated-transparent-background_1028290-17533.jpg"));

                    newBerry = new Berry(berryPic, 6, GameObjectType.GoldenBerry, true);
                }
                else
                {
                    // Yahan apni normal strawberry/blueberry ki picture ka path likhein
                    string userPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    berryPic.Image = Image.FromFile(System.IO.Path.Combine(userPictures, "saved pictures", "8dbcc21f6e017b4fe1a94e9a17817803.jpg"));

                    newBerry = new Berry(berryPic, 4, GameObjectType.NormalBerry, false);
                }
            }
            catch (Exception)
            {
                // Agar picture load na ho sake (path ghalat ho), to game crash na ho, balkay rang-birangay boxes ban jain
                if (isGolden)
                {
                    berryPic.BackColor = System.Drawing.Color.Gold;
                    newBerry = new Berry(berryPic, 6, GameObjectType.GoldenBerry, true);
                }
                else
                {
                    berryPic.BackColor = System.Drawing.Color.Red;
                    newBerry = new Berry(berryPic, 4, GameObjectType.NormalBerry, false);
                }
            }

            this.Controls.Add(berryPic); // Form par show karo
            berriesList.Add(newBerry); // List mein add kar dein
        }

        private void FireArrow()
        {
            PictureBox arrowPic = new PictureBox();
            arrowPic.Size = new System.Drawing.Size(10, 30); // Chota aur lamba teer
            arrowPic.BackColor = System.Drawing.Color.Yellow; // Shuruat mein yellow rang ka teer rakhte hain testing ke liye
            arrowPic.SizeMode = PictureBoxSizeMode.StretchImage;

            // Teer bilkul bow (player) ke center aur upar se nikalna chahiye
            int arrowX = player.Sprite.Left + (player.Sprite.Width / 2) - (arrowPic.Width / 2);
            int arrowY = player.Sprite.Top - arrowPic.Height;
            arrowPic.Location = new System.Drawing.Point(arrowX, arrowY);

            this.Controls.Add(arrowPic); // Form par show karo

            // Naya Arrow object create karain (Speed rakh rahe hain 15 taake teer tez jaye)
            Arrow newArrow = new Arrow(arrowPic, 15);
            arrowsList.Add(newArrow); // List mein add kar dein
        }

        //Jb Keyboard ki key press karain
        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                player.Direction = -1; // Left jao
            }
            if (e.KeyCode == Keys.Right)
            {
                player.Direction = 1; // Right jao
            }
            if (e.KeyCode == Keys.Space)
            {
                FireArrow(); // Space bar press karne par arrow fire karo
            }
        }
        //Jb keyboard ki key chorrein
        private void Form3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                player.Direction = 0; // Stop ho jao
            }
        }
    }
}
