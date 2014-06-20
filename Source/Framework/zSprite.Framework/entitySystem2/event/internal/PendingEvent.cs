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

	using EntityRef = org.terasology.entitySystem.entity.EntityRef;

	/// <summary>
	/// @author Immortius
	/// </summary>
	internal class PendingEvent
	{
		private EntityRef entity;
		private Event @event;
		private Component component;

		public PendingEvent(EntityRef entity, Event @event)
		{
			this.@event = @event;
			this.entity = entity;
		}

		public PendingEvent(EntityRef entity, Event @event, Component component)
		{
			this.entity = entity;
			this.@event = @event;
			this.component = component;
		}

		public virtual EntityRef Entity
		{
			get
			{
				return entity;
			}
		}

		public virtual Event Event
		{
			get
			{
				return @event;
			}
		}

		public virtual Component Component
		{
			get
			{
				return component;
			}
		}
	}

}