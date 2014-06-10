using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSprite.Samples
{
    public class Sample : GameEngine
    {
        private Root root;
        private bool isDone = false;

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                this.Exit();

        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }

    public class SampleSet : List<Sample>
    {
    }
}
