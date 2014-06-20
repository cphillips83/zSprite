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

	using Predicates = com.google.common.@base.Predicates;
	using ClassMetadata = org.terasology.reflection.metadata.ClassMetadata;
	using CopyStrategy = org.terasology.reflection.copy.CopyStrategy;
	using CopyStrategyLibrary = org.terasology.reflection.copy.CopyStrategyLibrary;
	using InaccessibleFieldException = org.terasology.reflection.reflect.InaccessibleFieldException;
	using ReflectFactory = org.terasology.reflection.reflect.ReflectFactory;
	using SimpleUri = org.terasology.engine.SimpleUri;
	using Event = org.terasology.entitySystem.@event.Event;
	using BroadcastEvent = org.terasology.network.BroadcastEvent;
	using OwnerEvent = org.terasology.network.OwnerEvent;
	using ServerEvent = org.terasology.network.ServerEvent;

	/// <summary>
	/// @author Immortius
	/// </summary>
	public class EventMetadata<T> : ClassMetadata<T, ReplicatedFieldMetadata<T, JavaToDotNetGenericWildcard>> where T : org.terasology.entitySystem.@event.Event
	{

		private NetworkEventType networkEventType = NetworkEventType.NONE;
		private bool lagCompensated;
		private bool skipInstigator;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public EventMetadata(Class simpleClass, org.terasology.reflection.copy.CopyStrategyLibrary copyStrategies, org.terasology.reflection.reflect.ReflectFactory factory, org.terasology.engine.SimpleUri uri) throws NoSuchMethodException
		public EventMetadata(Type simpleClass, CopyStrategyLibrary copyStrategies, ReflectFactory factory, SimpleUri uri) : base(uri, simpleClass, factory, copyStrategies, Predicates.alwaysTrue())
		{
			if (simpleClass.getAnnotation(typeof(ServerEvent)) != null)
			{
				networkEventType = NetworkEventType.SERVER;
				lagCompensated = simpleClass.getAnnotation(typeof(ServerEvent)).lagCompensate();
			}
			else if (simpleClass.getAnnotation(typeof(OwnerEvent)) != null)
			{
				networkEventType = NetworkEventType.OWNER;
			}
			else if (simpleClass.getAnnotation(typeof(BroadcastEvent)) != null)
			{
				networkEventType = NetworkEventType.BROADCAST;
				skipInstigator = simpleClass.getAnnotation(typeof(BroadcastEvent)).skipInstigator();
			}
		}

		/// <returns> Whether this event is a network event. </returns>
		public virtual bool NetworkEvent
		{
			get
			{
				return networkEventType != NetworkEventType.NONE;
			}
		}

		/// <returns> The type of network event this event is. </returns>
		public virtual NetworkEventType NetworkEventType
		{
			get
			{
				return networkEventType;
			}
		}

		/// <returns> Whether this event is compensated for lag. </returns>
		public virtual bool LagCompensated
		{
			get
			{
				return lagCompensated;
			}
		}

		/// <returns> Whether this event should not be replicated to the instigator </returns>
		public virtual bool SkipInstigator
		{
			get
			{
				return skipInstigator;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override protected <V> ReplicatedFieldMetadata<T, ?> createField(Field field, org.terasology.reflection.copy.CopyStrategy<V> copyStrategy, org.terasology.reflection.reflect.ReflectFactory factory) throws org.terasology.reflection.reflect.InaccessibleFieldException
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
		protected internal override ReplicatedFieldMetadata<T, ?> createField<V>(Field field, CopyStrategy<V> copyStrategy, ReflectFactory factory)
		{
			return new ReplicatedFieldMetadata<>(this, field, copyStrategy, factory, true);
		}
	}

}