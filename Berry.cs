using FruitBurstGame;
using System;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace FruitBurstGame
{
    // Rubrics: Inheritance (Berry inherits from GameObject)
    public class Berry : GameObject
    {
        private Random random;
        private bool isRandomMovement;

        public Berry(PictureBox sprite, int speed, GameObjectType type, bool useRandomMove)
            : base(sprite, speed, type)
        {
            random = new Random(Guid.NewGuid().GetHashCode()); // Alag random behavior ke liye
            isRandomMovement = useRandomMove;
        }

        // Rubrics: Movements (Vertical and Random movements)
        public override void Move()
        {
            // 1. Vertical Movement: Berry hamesha neeche giregi
            Sprite.Top += Speed;

            // 2. Random Movement: Agar special berry hai to wo thora left/right zigzag bhi karegi
            if (isRandomMovement)
            {
                // Randomly left (-3) ya right (+3) move karegi girtay huay
                Sprite.Left += random.Next(-3, 4);
            }
        }
    }
}