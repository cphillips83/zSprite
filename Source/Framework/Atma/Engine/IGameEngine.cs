using Atma.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atma.Engine
{
    public interface IGameEngine : IDisposable
    {
        void init();
        
        void run(IGameState initialState);

        void shutdown();

        bool isRunning { get; }

        bool isDisposed { get; }

        IGameState currentState { get; }

        void changeState(IGameState state);

        bool hasFocus { get; }

        bool hasMouseFocus { get; }

        event OnStateChangeEvent onStateChange;
    }
}
