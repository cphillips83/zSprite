using System;
using System.Collections.Generic;

/*
 * Copyright 2014 MovingBlocks
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace org.terasology.entitySystem.entity.lifecycleEvents
{

	using Maps = com.google.common.collect.Maps;
	using Sets = com.google.common.collect.Sets;
	using Event = org.terasology.entitySystem.@event.Event;
	using Prefab = org.terasology.entitySystem.prefab.Prefab;


	/// @deprecated Use the prefab delta system instead (create a json file under /deltas/moduleName/prefabs/prefabName.prefab with the desired changes)
	/// @author Marcin Sciesinski <marcins78@gmail.com> 
	[Obsolete("Use the prefab delta system instead (create a json file under /deltas/moduleName/prefabs/prefabName.prefab with the desired changes)")]
	public class BeforeEntityCreated : Event
	{
		private Prefab prefab;
		private IEnumerable<Component> components;
		private Set<Type> componentsToRemove = Sets.newLinkedHashSet();
		private IDictionary<Type, Component> componentsToAdd = Maps.newLinkedHashMap();

		public BeforeEntityCreated(Prefab prefab, IEnumerable<Component> components)
		{
			this.prefab = prefab;
			this.components = components;
		}

		public virtual Prefab Prefab
		{
			get
			{
				return prefab;
			}
		}

		public virtual IEnumerable<Component> OriginalComponents
		{
			get
			{
				return components;
			}
		}

		public virtual void addComponent(Component component)
		{
			if (componentsToAdd.ContainsKey(component.GetType()))
			{
				throw new System.ArgumentException("Tried adding the same component multiple times");
			}
			componentsToAdd[component.GetType()] = component;
		}

		public virtual void prohibitComponent(Type componentClass)
		{
			componentsToRemove.add(componentClass);
		}

		public virtual IEnumerable<Component> ResultComponents
		{
			get
			{
				return new IterableAnonymousInnerClassHelper(this);
			}
		}

		private class IterableAnonymousInnerClassHelper : IEnumerable<Component>
		{
			private readonly BeforeEntityCreated outerInstance;

			public IterableAnonymousInnerClassHelper(BeforeEntityCreated outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public virtual IEnumerator<Component> GetEnumerator()
			{
				return new IteratorImpl(outerInstance);
			}
		}

		private sealed class IteratorImpl : IEnumerator<Component>
		{
			private readonly BeforeEntityCreated outerInstance;

			internal IEnumerator<Component> sourceIterator;
			internal IEnumerator<Component> addedIterator;

			internal Component next_Renamed;

			internal IteratorImpl(BeforeEntityCreated outerInstance)
			{
				this.outerInstance = outerInstance;
				sourceIterator = outerInstance.components.GetEnumerator();
				addedIterator = outerInstance.componentsToAdd.Values.GetEnumerator();
				next_Renamed = Next;
			}

			public override bool hasNext()
			{
				return next_Renamed != null;
			}

			public override Component next()
			{
				Component result = next_Renamed;
				next_Renamed = Next;
				return result;
			}

			internal Component Next
			{
				get
				{
					while (sourceIterator.MoveNext())
					{
	//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
	//ORIGINAL LINE: final org.terasology.entitySystem.Component result = sourceIterator.Current;
						Component result = sourceIterator.Current;
						if (outerInstance.componentsToAdd.ContainsKey(result.GetType()))
						{
							throw new IllegalStateException("Requested to add component that was already defined for this entity");
						}
						if (outerInstance.componentsToRemove.contains(result.GetType()))
						{
							continue;
						}
						return result;
					}
					while (addedIterator.MoveNext())
					{
	//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
	//ORIGINAL LINE: final org.terasology.entitySystem.Component result = addedIterator.Current;
						Component result = addedIterator.Current;
						if (outerInstance.componentsToRemove.contains(result.GetType()))
						{
							continue;
						}
						return result;
					}
					return null;
				}
			}

			public override void remove()
			{
				throw new System.NotSupportedException("Remove not supported for read-only iterator");
			}
		}
	}

}