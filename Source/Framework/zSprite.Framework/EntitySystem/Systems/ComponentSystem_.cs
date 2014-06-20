using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zSprite.EntitySystem.Systems
{
    public interface ComponentSystem_
    {
        /// <summary>
        /// Called to initialise the system. This occurs after injection, but before other systems are necessarily initialised, so they should not be interacted with
        /// </summary>
        void initialise();

        /// <summary>
        /// Called after all systems are initialised, but before the game is loaded
        /// </summary>
        void preBegin();

        /// <summary>
        /// Called after the game is loaded, right before first frame
        /// </summary>
        void postBegin();

        /// <summary>
        /// Called before the game is saved (this may be after shutdown)
        /// </summary>
        void preSave();

        /// <summary>
        /// Called after the game is saved
        /// </summary>
        void postSave();

        /// <summary>
        /// Called right before the game is shut down
        /// </summary>
        void shutdown();
    }
}
