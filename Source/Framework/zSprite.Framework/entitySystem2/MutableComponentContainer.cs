using System;

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
namespace org.terasology.entitySystem
{

	/// <summary>
	/// @author Immortius
	/// </summary>
	public interface MutableComponentContainer : ComponentContainer
	{

		/// <summary>
		/// Adds a component. If this already has a component of the same class it is replaced.
		/// </summary>
		/// <param name="component"> </param>
		T addComponent<T>(T component) where T : Component;

		/// <param name="componentClass"> </param>
		void removeComponent(Type componentClass);

		/// <summary>
		/// Saves changes made to a component
		/// </summary>
		/// <param name="component"> </param>
		void saveComponent(Component component);
	}

}