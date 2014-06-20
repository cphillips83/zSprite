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
namespace org.terasology.entitySystem.@event
{

	using RegisterMode = org.terasology.entitySystem.systems.RegisterMode;


	/// <summary>
	/// This annotation is used to mark up methods that can be registered to receive events through the EventSystem
	/// <p/>
	/// These methods should have the form
	/// <code>public void handlerMethod(EventType event, EntityRef entity)</code>
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false]
	public class ReceiveEvent : System.Attribute
	{
		/// <summary>
		/// What components that the entity must have for this method to be invoked
		/// </summary>
		internal virtual Type[] components() default
		{
		};

		RegisterMode netFilter() default RegisterMode.ALWAYS;

		int priority() default EventPriority.PRIORITY_NORMAL;
	}

}