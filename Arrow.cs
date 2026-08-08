using FruitBurstGame;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace FruitBurstGameGame
{
    // Rubrics: Inheritance (Arrow inherits from GameObject)
    public class Arrow : GameObject
    {
        public Arrow(PictureBox sprite, int speed)
            : base(sprite, speed, GameObjectType.Arrow)
        {
        }

        // Rubrics: Movements (Arrow moves vertically UPWARDS)
        public override void Move()
        {
            // Teer hamesha upar ki taraf jaye ga, is liye Top minus hoga
            Sprite.Top -= Speed;
        }
    }
}