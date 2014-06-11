using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSprite.Managers;

namespace zSprite.Samples
{
    public class Sample : GameEngine
    {
        private Root root;
        private InputManager input;
        private bool isDone = false;

        protected override void Initialize()
        {
            input = new InputManager();
            base.Initialize();

        }

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


        protected virtual void keyPressed(Keys key)
        {

        }

        protected virtual void keyReleased(Keys key)
        {

        }

        protected virtual void updateInput(KeyboardState keyboard, MouseState mouse)
        {

        }
    }

    public class SampleSet : List<Sample>
    {
    }
}
