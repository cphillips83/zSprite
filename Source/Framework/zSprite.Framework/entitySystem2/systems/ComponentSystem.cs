/*
 * Copyright 2013 MovingBlocks
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace org.terasology.entitySystem.systems
{

	/// <summary>
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public interface ComponentSystem
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