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
namespace org.terasology.entitySystem.entity.@internal
{

	using Lists = com.google.common.collect.Lists;
	using Maps = com.google.common.collect.Maps;
	using TIntIterator = gnu.trove.iterator.TIntIterator;
	using TIntObjectIterator = gnu.trove.iterator.TIntObjectIterator;
	using TIntObjectMap = gnu.trove.map.TIntObjectMap;
	using TIntObjectHashMap = gnu.trove.map.hash.TIntObjectHashMap;
	using TIntSet = gnu.trove.set.TIntSet;
	using TIntHashSet = gnu.trove.set.hash.TIntHashSet;


	/// <summary>
	/// A table for storing entities and components. Focused on allowing iteration across a components of a given type
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	internal class ComponentTable
	{
		private IDictionary<Type, TIntObjectMap<Component>> store = Maps.newConcurrentMap();

		public virtual T get<T>(int entityId, Type componentClass) where T : org.terasology.entitySystem.Component
		{
			TIntObjectMap<Component> entityMap = store[componentClass];
			if (entityMap != null)
			{
				return componentClass.cast(entityMap.get(entityId));
			}
			return null;
		}

		public virtual Component put(int entityId, Component component)
		{
			TIntObjectMap<Component> entityMap = store[component.GetType()];
			if (entityMap == null)
			{
				entityMap = new TIntObjectHashMap<Component>();
				store[component.GetType()] = entityMap;
			}
			return entityMap.put(entityId, component);
		}

		public virtual Component remove<T>(int entityId, Type componentClass) where T : org.terasology.entitySystem.Component
		{
			TIntObjectMap<Component> entityMap = store[componentClass];
			if (entityMap != null)
			{
				return entityMap.remove(entityId);
			}
			return null;
		}

		public virtual void remove(int entityId)
		{
			foreach (TIntObjectMap<Component> entityMap in store.Values)
			{
				entityMap.remove(entityId);
			}
		}

		public virtual void clear()
		{
			store.Clear();
		}

		public virtual int getComponentCount(Type componentClass)
		{
			TIntObjectMap<Component> map = store[componentClass];
			return (map == null) ? 0 : map.size();
		}

		public virtual IEnumerable<Component> iterateComponents(int entityId)
		{
			IList<Component> components = Lists.newArrayList();
			foreach (TIntObjectMap<Component> componentMap in store.Values)
			{
				Component comp = componentMap.get(entityId);
				if (comp != null)
				{
					components.Add(comp);
				}
			}
			return components;
		}

		public virtual TIntObjectIterator<T> componentIterator<T>(Type componentClass) where T : org.terasology.entitySystem.Component
		{
			TIntObjectMap<T> entityMap = (TIntObjectMap<T>) store[componentClass];
			if (entityMap != null)
			{
				return entityMap.GetEnumerator();
			}
			return null;
		}

		/// <summary>
		/// Produces an iterator for iterating over all entities
		/// <p/>
		/// This is not designed to be performant, and in general usage entities should not be iterated over.
		/// </summary>
		/// <returns> An iterator over all entity ids. </returns>
		public virtual TIntIterator entityIdIterator()
		{
			TIntSet idSet = new TIntHashSet();
			foreach (TIntObjectMap<Component> componentMap in store.Values)
			{
				idSet.addAll(componentMap.keys());
			}
			return idSet.GetEnumerator();
		}

		public virtual int numEntities()
		{
			TIntSet idSet = new TIntHashSet();
			foreach (TIntObjectMap<Component> componentMap in store.Values)
			{
				idSet.addAll(componentMap.keys());
			}
			return idSet.size();
		}

	}

}