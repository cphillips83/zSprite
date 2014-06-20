using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Engine
{
    public interface IGameState : IDisposable
    {
        void init();

        void update(float dt);

        void input(float dt);

        void render();

    }
}
