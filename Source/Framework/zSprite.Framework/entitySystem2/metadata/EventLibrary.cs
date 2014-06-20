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
namespace org.terasology.entitySystem.metadata
{

	using Logger = org.slf4j.Logger;
	using LoggerFactory = org.slf4j.LoggerFactory;
	using AbstractClassLibrary = org.terasology.reflection.metadata.AbstractClassLibrary;
	using ClassMetadata = org.terasology.reflection.metadata.ClassMetadata;
	using CopyStrategyLibrary = org.terasology.reflection.copy.CopyStrategyLibrary;
	using ReflectFactory = org.terasology.reflection.reflect.ReflectFactory;
	using SimpleUri = org.terasology.engine.SimpleUri;
	using Event = org.terasology.entitySystem.@event.Event;

	/// <summary>
	/// The library for metadata about events (and their fields).
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public class EventLibrary : AbstractClassLibrary<Event>
	{

		private static readonly Logger logger = LoggerFactory.getLogger(typeof(EventLibrary));

		public EventLibrary(ReflectFactory reflectFactory, CopyStrategyLibrary copyStrategies) : base(reflectFactory, copyStrategies)
		{
		}

//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: @Override protected <C extends org.terasology.entitySystem.event.Event> org.terasology.reflection.metadata.ClassMetadata<C, ?> createMetadata(Class type, org.terasology.reflection.reflect.ReflectFactory factory, org.terasology.reflection.copy.CopyStrategyLibrary copyStrategies, org.terasology.engine.SimpleUri uri)
		protected internal override ClassMetadata<C, ?> createMetadata<C>(Type type, ReflectFactory factory, CopyStrategyLibrary copyStrategies, SimpleUri uri) where C : org.terasology.entitySystem.@event.Event
		{
			try
			{
				return new EventMetadata<>(type, copyStrategies, factory, uri);
			}
			catch (NoSuchMethodException e)
			{
				logger.error("Unable to register class {}: Default Constructor Required", type.Name, e);
				return null;
			}
		}


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override @SuppressWarnings("unchecked") public <T extends org.terasology.entitySystem.event.Event> EventMetadata<T> getMetadata(Class clazz)
		public override EventMetadata<T> getMetadata<T>(Type clazz) where T : org.terasology.entitySystem.@event.Event
		{
			return (EventMetadata<T>) base.getMetadata(clazz);
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override @SuppressWarnings("unchecked") public <T extends org.terasology.entitySystem.event.Event> EventMetadata<T> getMetadata(T object)
		public override EventMetadata<T> getMetadata<T>(T @object) where T : org.terasology.entitySystem.@event.Event
		{
			return (EventMetadata<T>) base.getMetadata(@object);
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override @SuppressWarnings("unchecked") public EventMetadata<? extends org.terasology.entitySystem.event.Event> getMetadata(org.terasology.engine.SimpleUri uri)
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override @SuppressWarnings("unchecked") public EventMetadata<? extends org.terasology.entitySystem.event.Event> getMetadata(org.terasology.engine.SimpleUri uri)
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
		public override EventMetadata<?> getMetadata(SimpleUri uri) where ? : org.terasology.entitySystem.@event.Event
		{
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: return (EventMetadata<? extends org.terasology.entitySystem.event.Event>) base.getMetadata(uri);
			return (EventMetadata<?>) base.getMetadata(uri);
		}

	}

}