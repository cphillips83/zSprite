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

	using CopyStrategyLibrary = org.terasology.reflection.copy.CopyStrategyLibrary;
	using ReflectFactory = org.terasology.reflection.reflect.ReflectFactory;
	using TypeSerializationLibrary = org.terasology.persistence.typeHandling.TypeSerializationLibrary;

	/// <summary>
	/// The set of metadata libraries used by the entity system
	/// 
	/// @author Immortius
	/// </summary>
	public class EntitySystemLibrary
	{

		private readonly TypeSerializationLibrary typeSerializationLibrary;
		private readonly ComponentLibrary componentLibrary;
		private readonly EventLibrary eventLibrary;

		public EntitySystemLibrary(ReflectFactory reflectFactory, CopyStrategyLibrary copyStrategies, TypeSerializationLibrary typeSerializationLibrary)
		{
			this.typeSerializationLibrary = typeSerializationLibrary;
			this.componentLibrary = new ComponentLibrary(reflectFactory, copyStrategies);
			this.eventLibrary = new EventLibrary(reflectFactory, copyStrategies);
		}

		/// <returns> The library of component metadata </returns>
		public virtual ComponentLibrary ComponentLibrary
		{
			get
			{
				return componentLibrary;
			}
		}

		/// <returns> The library of serializers </returns>
		public virtual TypeSerializationLibrary SerializationLibrary
		{
			get
			{
				return typeSerializationLibrary;
			}
		}

		/// <returns> The library of event metadata </returns>
		public virtual EventLibrary EventLibrary
		{
			get
			{
				return eventLibrary;
			}
		}

	}

}