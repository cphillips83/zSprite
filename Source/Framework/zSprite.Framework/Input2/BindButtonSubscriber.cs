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

namespace org.terasology.input
{

	using EntityRef = org.terasology.entitySystem.entity.EntityRef;

	/// <summary>
	/// Interface for subscribing to bind button events
	/// 
	/// @author Immortius
	/// </summary>
	public interface BindButtonSubscriber
	{

		/// <summary>
		/// Called when the bind is activated
		/// </summary>
		/// <param name="delta">  The time passing this frame </param>
		/// <param name="target"> The current camera target </param>
		/// <returns> True if the bind event was consumed </returns>
		bool onPress(float delta, EntityRef target);

		/// <summary>
		/// Called when the bind repeats
		/// </summary>
		/// <param name="delta">  The time this frame (not per repeat) </param>
		/// <param name="target"> The current camera target </param>
		/// <returns> True if the bind event was consumed </returns>
		bool onRepeat(float delta, EntityRef target);

		/// <summary>
		/// Called when the bind is deactivated
		/// </summary>
		/// <param name="delta">  The time passing this frame </param>
		/// <param name="target"> The current camera target </param>
		/// <returns> True if the bind event was consumed </returns>
		bool onRelease(float delta, EntityRef target);
	}

}