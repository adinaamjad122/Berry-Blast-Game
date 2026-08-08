using FruitBurstGame;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace FruitBurstGame

{
    // Rubrics: Inheritance (Player inherits from GameObject)
    public class Player : GameObject
    {
        // Direction variable: -1 for Left, 1 for Right, 0 for Stop
        public int Direction { get; set; }

        public Player(PictureBox sprite, int speed)
            : base(sprite, speed, GameObjectType.Player)
        {
            Direction = 0;
        }

        // Rubrics: Movements implementation
        public override void Move()
        {
            // Player sirf horizontal (Left/Right) move kar sakta hai
            int newX = Sprite.Left + (Direction * Speed);

            // Screen ki boundary check (takay player screen se bahar na nikal jaye)
            if (newX >= 0 && newX <= Sprite.Parent.Width - Sprite.Width)
            {
                Sprite.Left = newX;
            }
        }
    }
}