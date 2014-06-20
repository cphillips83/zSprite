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
namespace org.terasology.entitySystem.@event.@internal
{

	using SimpleUri = org.terasology.engine.SimpleUri;
	using EntityRef = org.terasology.entitySystem.entity.EntityRef;
	using ComponentSystem = org.terasology.entitySystem.systems.ComponentSystem;

	/// <summary>
	/// Event system propagates events to registered handlers
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public interface EventSystem
	{

		/// <summary>
		/// Process all pending events
		/// </summary>
		void process();

		/// <summary>
		/// Registers an event
		/// </summary>
		/// <param name="uri"> </param>
		/// <param name="eventType"> </param>
		void registerEvent(SimpleUri uri, Type eventType);

		/// <summary>
		/// Registers an object as an event handler - all methods with the <seealso cref="org.terasology.entitySystem.event.ReceiveEvent"/> annotation will be registered
		/// </summary>
		/// <param name="handler"> </param>
		void registerEventHandler(ComponentSystem handler);

		/// <summary>
		/// Unregister an object as an event handler. </summary>
		/// <param name="handler"> </param>
		void unregisterEventHandler(ComponentSystem handler);

		/// <summary>
		/// Registers an event receiver object
		/// </summary>
		/// <param name="eventReceiver"> </param>
		/// <param name="eventClass"> </param>
		/// <param name="componentTypes"> </param>
		/// @param <T> </param>
		void registerEventReceiver<T>(EventReceiver<T> eventReceiver, Type eventClass, params Type[] componentTypes) where T : org.terasology.entitySystem.@event.Event;

		/// <param name="eventReceiver"> </param>
		/// <param name="eventClass"> </param>
		/// <param name="priority"> </param>
		/// <param name="componentTypes"> </param>
		/// @param <T> </param>
		void registerEventReceiver<T>(EventReceiver<T> eventReceiver, Type eventClass, int priority, params Type[] componentTypes) where T : org.terasology.entitySystem.@event.Event;

		void unregisterEventReceiver<T>(EventReceiver<T> eventReceiver, Type eventClass, params Type[] componentTypes) where T : org.terasology.entitySystem.@event.Event;

		/// <summary>
		/// Sends an event to all handlers for an entity's components
		/// </summary>
		/// <param name="entity"> </param>
		/// <param name="event"> </param>
		void send(EntityRef entity, Event @event);

		/// <summary>
		/// Sends an event to a handlers for a specific component of an entity
		/// </summary>
		/// <param name="entity"> </param>
		/// <param name="event"> </param>
		/// <param name="component"> </param>
		void send(EntityRef entity, Event @event, Component component);
	}

}