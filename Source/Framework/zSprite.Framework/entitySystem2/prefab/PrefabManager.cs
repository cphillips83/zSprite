using System;
using System.Collections.Generic;

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
namespace org.terasology.entitySystem.prefab
{


	/// <summary>
	/// A PrefabManager keep Prefabs organized and available to the game engine.
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// @author Rasmus 'Cervator' Praestholm <cervator@gmail.com>
	/// </summary>
	// TODO: This is basically unnecessary now, remove and just use Assets?
	public interface PrefabManager
	{

		/// <summary>
		/// Returns the named Prefab or null if it doesn't exist.
		/// </summary>
		/// <param name="name"> The name of the desired Prefab </param>
		/// <returns> Prefab requested or null if it doesn't exist </returns>
		Prefab getPrefab(string name);

		/// <summary>
		/// Tests whether a named Prefab exists or not.
		/// </summary>
		/// <param name="name"> The name of the Prefab to look for </param>
		/// <returns> True if found, false if not </returns>
		bool exists(string name);

		/// <summary>
		/// Returns all loaded prefabs.
		/// </summary>
		/// <returns> Collection containing all prefabs </returns>
		IEnumerable<Prefab> listPrefabs();

		/// <summary>
		/// Returns all loaded prefabs that include the supplied Component (which may result in an empty set).
		/// </summary>
		/// <param name="withComponent"> a Component to filter by </param>
		/// <returns> Collection containing all prefabs that include the supplied Component </returns>
		ICollection<Prefab> listPrefabs(Type withComponent);

	}

}