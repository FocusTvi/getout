
using Microsoft.Xna.Framework;
using System;

namespace Android
{


    public class CreationController
    {
        public static double startBefore = 1.0;
        public static double spawnTime = 3.0;
        static Random random = new Random();


        public static void Update(GameTime gametime, SpriteAnimation spriteAnimation)
        {
            //it give me few second before the creation of enemies
            startBefore -= gametime.ElapsedGameTime.TotalSeconds;

            //once the time is less than 0 then lets start to create the enemies
            if (startBefore <= 0)
            {
                //this ensure to not create enemies inside the movement stage
                int x = random.Next(1, 3) == 1 ? random.Next((int)SharedVars.movementStage.X - 500, (int)SharedVars.movementStage.X - 100) : random.Next((int)SharedVars.movementStage.Width + 500, (int)SharedVars.movementStage.Width + 800);
                int y = random.Next(1, 3) == 1 ? random.Next((int)SharedVars.movementStage.Y - 500, (int)SharedVars.movementStage.Y - 100) : random.Next((int)SharedVars.movementStage.Height + 500, (int)SharedVars.movementStage.Height + 800);
                Enemy.Enemies.Add(new Enemy(new Vector2(x, y), spriteAnimation));
                startBefore = spawnTime;
                
                
                //it gives me a delay to create the next enemies
                if (spawnTime > 0.5)
                {
                    spawnTime -= 0.05;
                }
            }


        }
    }
}
