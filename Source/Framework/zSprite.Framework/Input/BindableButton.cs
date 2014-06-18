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

namespace zSprite
{


	/// <summary>
	/// @author Immortius
	/// </summary>
	public interface BindableButton
	{

		/// <returns> The identifier for this button </returns>
		SimpleUri Id {get;}

		/// <returns> The display name for this button </returns>
		string DisplayName {get;}

		/// <summary>
		/// Set the circumstance under which this button sends events
		/// </summary>
		/// <param name="mode"> </param>
		ActivateMode Mode {set;get;}


		/// <summary>
		/// Sets whether this button sends repeat events while pressed
		/// </summary>
		/// <param name="repeating"> </param>
		bool Repeating {set;get;}


		/// <param name="repeatTimeMs"> The time (in milliseconds) between repeat events being sent </param>
		int RepeatTime {set;get;}


		/// <returns> The current state of this button (either up or down) </returns>
		ButtonState State {get;}

		/// <summary>
		/// Used to directly subscribe to the button's events
		/// </summary>
		/// <param name="subscriber"> </param>
		void subscribe(BindButtonSubscriber subscriber);

		/// <summary>
		/// Used to unsubscribe from the button's event
		/// </summary>
		/// <param name="subscriber"> </param>
		void unsubscribe(BindButtonSubscriber subscriber);

	}

}