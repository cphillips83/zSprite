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
namespace org.terasology.entitySystem.entity
{

	using Maps = com.google.common.collect.Maps;
	using EngineEntityManager = org.terasology.entitySystem.entity.@internal.EngineEntityManager;
	using EntityInfoComponent = org.terasology.entitySystem.entity.@internal.EntityInfoComponent;

	/// <summary>
	/// An entity builder provides the ability to set up an entity before creating it. This prevents events being sent
	/// for components being added or modified before it is fully set up.
	/// 
	/// @author Immortius
	/// </summary>
	public class EntityBuilder : MutableComponentContainer
	{

		private IDictionary<Type, Component> components = Maps.newHashMap();
		private EngineEntityManager manager;

		public EntityBuilder(EngineEntityManager manager)
		{
			this.manager = manager;
		}

		/// <summary>
		/// Produces an entity with the components contained in this entity builder
		/// </summary>
		/// <returns> The built entity. </returns>
		public virtual EntityRef build()
		{
			return manager.create(components.Values);
		}

		public virtual EntityRef buildWithoutLifecycleEvents()
		{
			return manager.createEntityWithoutLifecycleEvents(components.Values);
		}

		public override bool hasComponent(Type component)
		{
			return components.Keys.contains(component);
		}

		public override T getComponent<T>(Type componentClass) where T : org.terasology.entitySystem.Component
		{
			return componentClass.cast(components[componentClass]);
		}

		public override T addComponent<T>(T component) where T : org.terasology.entitySystem.Component
		{
			components[component.GetType()] = component;
			return component;
		}

		public override void removeComponent(Type componentClass)
		{
			components.Remove(componentClass);
		}

		public override void saveComponent(Component component)
		{
			components[component.GetType()] = component;
		}

		public override IEnumerable<Component> iterateComponents()
		{
			return components.Values;
		}

		public virtual bool Persistent
		{
			get
			{
				return EntityInfo.persisted;
			}
			set
			{
				EntityInfo.persisted = value;
			}
		}


		public virtual bool AlwaysRelevant
		{
			get
			{
				return EntityInfo.alwaysRelevant;
			}
			set
			{
				EntityInfo.alwaysRelevant = value;
			}
		}


		public virtual EntityRef Owner
		{
			set
			{
				EntityInfo.owner = value;
			}
			get
			{
				return EntityInfo.owner;
			}
		}


		private EntityInfoComponent EntityInfo
		{
			get
			{
				EntityInfoComponent entityInfo = getComponent(typeof(EntityInfoComponent));
				if (entityInfo == null)
				{
					entityInfo = addComponent(new EntityInfoComponent());
				}
				return entityInfo;
			}
		}

	}

}