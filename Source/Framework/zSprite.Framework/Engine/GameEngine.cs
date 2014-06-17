#region GPLv3 License

/*
zSprite
Copyright © 2014 zSprite Project Team

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License V3
as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
General Public License V3 for more details.

You should have received a copy of the GNU General Public License V3
along with this library; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
*/

#endregion

#region Namespace Declarations

#endregion Namespace Declarations

namespace zSprite
{
    public interface GameEngine
    {
        /// <summary>
        /// Initialises the engine
        /// </summary>
        void init();

        /// <summary>
        /// Runs the engine, which will block the thread.
        /// Invalid for a disposed engine
        /// </summary>
        void run(GameState initialState);

        /// <summary>
        /// Request the engine to stop running
        /// </summary>
        void shutdown();

        /// <summary>
        /// Cleans up the engine. Can only be called after shutdown.
        /// </summary>
        void dispose();

        /// <summary>
        /// Whether the engine is running
        /// </summary>
        bool isRunning();

        /// <summary>
        /// Whether the engine has been disposed
        /// </summary>
        bool isDisposed();

        /// <summary>
        /// @return The current state of the engine
        /// </summary>
        GameState getState();

        /// <summary>
        /// Clears all states, replacing them with newState
        ///
        /// </summary>
        void changeState(GameState newState);

        //// TODO: Move task system elsewhere? Need to support saving queued/unfinished tasks too, when the world
        //// shuts down

        ///// <summary>
        ///// Submits a task to be run concurrent with the main thread
        /////
        ///// </summary>
        //void submitTask(String name, Runnable task);

        bool isHibernationAllowed();

        void setHibernationAllowed(bool allowed);

        // TODO: This probably should be elsewhere?

        /// <summary>
        /// Whether the game window currently has focus
        /// </summary>
        bool hasFocus();

        /// <summary>
        /// Whether the game window controls if the mouse is captured.
        /// </summary>
        bool hasMouseFocus();

        void setFocus(bool focused);

        void subscribeToStateChange(StateChangeSubscriber subscriber);

        void unsubscribeToStateChange(StateChangeSubscriber subscriber);
    }
}
