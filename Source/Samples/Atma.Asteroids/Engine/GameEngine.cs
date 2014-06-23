using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Asteroids.Core;
using Atma.Core;
using Atma.Engine;
using Atma.Events;

namespace Atma.Asteroids.Engine
{
    public sealed class GameEngine : IGameEngine
    {
        private static readonly Logger logger = Logger.getLogger(typeof(GameEngine));

        public event Events.OnStateChangeEvent onStateChange;

        private StopwatchTime time;

        private bool _initialised = false;
        private bool _isRunning = false;
        private bool _isDisposed = false;
        private bool _hasFocus = false;
        private bool _hasMouseFocus = false;

        private IGameState _state = null;
        private IGameState _changeState = null;

        private void init()
        {
            if (_initialised)
                return;

            _initialised = true;

            logger.info("Initializing Atma Asteroids...");
            logger.info("Version: 0.1 ALPHA");
            //logger.info("Home path: {}", PathManager.getInstance().getHomePath());
            //logger.info("Install path: {}", PathManager.getInstance().getInstallPath());
            //logger.info("Java: {} in {}", System.getProperty("java.version"), System.getProperty("java.home"));
            //logger.info("Java VM: {}, version: {}", System.getProperty("java.vm.name"), System.getProperty("java.vm.version"));
            //logger.info("OS: {}, arch: {}, version: {}", System.getProperty("os.name"), System.getProperty("os.arch"), System.getProperty("os.version"));
            //logger.info("Max. Memory: {} MB", Runtime.getRuntime().maxMemory() / (1024 * 1024));
            //logger.info("Processors: {}", Runtime.getRuntime().availableProcessors());

            time = CoreRegistry.putPermanently("core:time", new StopwatchTime());
        }

        public void run(IGameState initialState)
        {
            CoreRegistry.putPermanently("core:engine", this);

            init();

            changeState(initialState);

            mainLoop();

            cleanUp();


        }

        private void mainLoop()
        {
            logger.info("running");

        }

        private void cleanUp()
        {
            logger.info("clean up");

        }

        public void shutdown()
        {
            logger.info("shutting down");
            CoreRegistry.shutdown();
        }

        public bool isRunning { get { return _isRunning; } }

        public bool isDisposed { get { return _isDisposed; } }

        public bool hasFocus { get { return _hasFocus; } }

        public bool hasMouseFocus { get { return _hasMouseFocus; } }

        public IGameState currentState { get { return _state; } }

        public void changeState(IGameState state)
        {
            logger.info("changing state {0}", state.ToString());

            if (_changeState != null)
                _changeState = state;
            else
                switchState(state);
        }

        private void switchState(IGameState state)
        {
            if (_state != null)
            {
                _state.Dispose();
                _state = null;
            }

            _state = state;
            _state.init();

            if (onStateChange != null)
                onStateChange(new StateChangeEvent(state));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
