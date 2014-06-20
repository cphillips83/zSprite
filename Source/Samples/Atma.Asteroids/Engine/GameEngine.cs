using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Engine;

namespace Atma.Asteroids.Engine
{
    public sealed class GameEngine : IGameEngine
    {
        private static readonly Logger logger = Logger.getLogger(typeof(GameEngine));

        public event Events.OnStateChangeEvent onStateChange;

        private bool _isRunning = false;
        private bool _isDisposed = false;
        private bool _hasFocus = false;
        private bool _hasMouseFocus = false;

        private IGameState _state = null;

        public void init()
        {
        }

        public void run(IGameState initialState)
        {
        }

        public void shutdown()
        {
        }

        public bool isRunning { get { return _isRunning; } }

        public bool isDisposed { get { return _isDisposed; } }

        public bool hasFocus { get { return _hasFocus; } }

        public bool hasMouseFocus { get { return _hasMouseFocus; } }

        public IGameState currentState { get { return _state; } }

        public void changeState(IGameState state)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
