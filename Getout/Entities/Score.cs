using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Getout.Models
{
    public class Score
    {
        public int Name { get; set; }
        public int Kills { get; set; }
        public int Time { get; set; }
        public int Rage { get; set; }
        public int EnemiesAlive { get; set; }
    }
}
