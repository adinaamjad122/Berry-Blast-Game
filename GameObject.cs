using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FruitBurstGame
{
    // Rubrics: Logical inheritance relationships and Abstraction
    public abstract class GameObject : IMovable
    {
        public PictureBox Sprite { get; set; }
        public int Speed { get; set; }
        public GameObjectType Type { get; set; }

        public GameObject(PictureBox sprite, int speed, GameObjectType type)
        {
            Sprite = sprite;
            Speed = speed;
            Type = type;
        }

        // Abstract method: Har object (Player, Berry, Arrow) apni marzi se move karega
        public abstract void Move();
    }
}
