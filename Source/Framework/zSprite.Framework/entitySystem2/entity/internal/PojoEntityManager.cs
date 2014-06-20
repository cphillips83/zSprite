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

	using Preconditions = com.google.common.@base.Preconditions;
	using Lists = com.google.common.collect.Lists;
	using MapMaker = com.google.common.collect.MapMaker;
	using Maps = com.google.common.collect.Maps;
	using Sets = com.google.common.collect.Sets;
	using UnsignedInts = com.google.common.primitives.UnsignedInts;
	using TIntIterator = gnu.trove.iterator.TIntIterator;
	using TIntObjectIterator = gnu.trove.iterator.TIntObjectIterator;
	using TIntList = gnu.trove.list.TIntList;
	using TIntArrayList = gnu.trove.list.array.TIntArrayList;
	using TIntSet = gnu.trove.set.TIntSet;
	using TIntHashSet = gnu.trove.set.hash.TIntHashSet;
	using Logger = org.slf4j.Logger;
	using LoggerFactory = org.slf4j.LoggerFactory;
	using BeforeDeactivateComponent = org.terasology.entitySystem.entity.lifecycleEvents.BeforeDeactivateComponent;
	using BeforeEntityCreated = org.terasology.entitySystem.entity.lifecycleEvents.BeforeEntityCreated;
	using BeforeRemoveComponent = org.terasology.entitySystem.entity.lifecycleEvents.BeforeRemoveComponent;
	using OnActivatedComponent = org.terasology.entitySystem.entity.lifecycleEvents.OnActivatedComponent;
	using OnAddedComponent = org.terasology.entitySystem.entity.lifecycleEvents.OnAddedComponent;
	using OnChangedComponent = org.terasology.entitySystem.entity.lifecycleEvents.OnChangedComponent;
	using EventSystem = org.terasology.entitySystem.@event.@internal.EventSystem;
	using ComponentLibrary = org.terasology.entitySystem.metadata.ComponentLibrary;
	using EntitySystemLibrary = org.terasology.entitySystem.metadata.EntitySystemLibrary;
	using Prefab = org.terasology.entitySystem.prefab.Prefab;
	using PrefabManager = org.terasology.entitySystem.prefab.PrefabManager;
	using LocationComponent = org.terasology.logic.location.LocationComponent;
	using TypeSerializationLibrary = org.terasology.persistence.typeHandling.TypeSerializationLibrary;


	/// <summary>
	/// Prototype entity manager. Not intended for final use, but a stand in for experimentation.
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public class PojoEntityManager : LowLevelEntityManager, EngineEntityManager
	{
		public const int NULL_ID = 0;

		private static readonly Logger logger = LoggerFactory.getLogger(typeof(PojoEntityManager));

		private int nextEntityId = 1;
		private TIntSet loadedIds = new TIntHashSet();
		private TIntSet freedIds = new TIntHashSet();
		private IDictionary<int?, BaseEntityRef> entityCache = new MapMaker().weakValues().concurrencyLevel(4).initialCapacity(1000).makeMap();
		private ComponentTable store = new ComponentTable();

		private Set<EntityChangeSubscriber> subscribers = Sets.newLinkedHashSet();
		private Set<EntityDestroySubscriber> destroySubscribers = Sets.newLinkedHashSet();
		private EventSystem eventSystem;
		private PrefabManager prefabManager;
		private ComponentLibrary componentLibrary;

		private RefStrategy refStrategy = new DefaultRefStrategy();

		private TypeSerializationLibrary typeSerializerLibrary;

		public PojoEntityManager()
		{
		}

		public virtual TypeSerializationLibrary TypeSerializerLibrary
		{
			set
			{
				this.typeSerializerLibrary = value;
			}
			get
			{
				return typeSerializerLibrary;
			}
		}

		public virtual EntitySystemLibrary EntitySystemLibrary
		{
			set
			{
				componentLibrary = value.ComponentLibrary;
			}
		}

		public virtual PrefabManager PrefabManager
		{
			set
			{
				this.prefabManager = value;
			}
			get
			{
				return prefabManager;
			}
		}

		public override void clear()
		{
			foreach (BaseEntityRef entityRef in entityCache.Values)
			{
				entityRef.invalidate();
			}
			store.clear();
			nextEntityId = 1;
			loadedIds.clear();
			freedIds.clear();
			entityCache.Clear();
		}

		public override EntityBuilder newBuilder()
		{
			return new EntityBuilder(this);
		}

		public override EntityBuilder newBuilder(string prefabName)
		{
			if (prefabName != null && prefabName.Length > 0)
			{
				Prefab prefab = prefabManager.getPrefab(prefabName);
				if (prefab == null)
				{
					logger.warn("Unable to instantiate unknown prefab: \"{}\"", prefabName);
					return new EntityBuilder(this);
				}
				return newBuilder(prefab);
			}
			return newBuilder();
		}

		public override EntityBuilder newBuilder(Prefab prefab)
		{
			EntityBuilder builder = new EntityBuilder(this);
			if (prefab != null)
			{
				foreach (Component component in prefab.iterateComponents())
				{
					builder.addComponent(componentLibrary.copy(component));
				}
				builder.addComponent(new EntityInfoComponent(prefab.Name, prefab.Persisted, prefab.AlwaysRelevant));
			}
			return builder;
		}

		public override EntityRef create()
		{
			return createEntityRef(createEntity());
		}

		private int createEntity()
		{
			if (!freedIds.Empty)
			{
				TIntIterator iterator = freedIds.GetEnumerator();
				int id = iterator.next();
				iterator.remove();
				loadedIds.add(id);
				return id;
			}
			if (nextEntityId == NULL_ID)
			{
				nextEntityId++;
			}
			loadedIds.add(nextEntityId);
			return nextEntityId++;
		}

		public override EntityRef create(params Component[] components)
		{
			return create(Arrays.asList(components));
		}

		public override EntityRef create(IEnumerable<Component> components)
		{
			EntityRef entity = createEntity(components);
			if (eventSystem != null)
			{
				eventSystem.send(entity, OnAddedComponent.newInstance());
				eventSystem.send(entity, OnActivatedComponent.newInstance());
			}
			return entity;
		}

		public virtual RefStrategy EntityRefStrategy
		{
			set
			{
				this.refStrategy = value;
			}
		}

		private EntityRef createEntity(IEnumerable<Component> components)
		{
			int entityId = createEntity();

			Prefab prefab = null;
			foreach (Component component in components)
			{
				if (component is EntityInfoComponent)
				{
					EntityInfoComponent comp = (EntityInfoComponent) component;
					prefab = prefabManager.getPrefab(comp.parentPrefab);
					break;
				}
			}

			IEnumerable<Component> finalComponents;
			if (eventSystem != null)
			{
				BeforeEntityCreated @event = new BeforeEntityCreated(prefab, components);
				BaseEntityRef tempRef = refStrategy.createRefFor(entityId, this);
				eventSystem.send(tempRef, @event);
				tempRef.invalidate();
				finalComponents = @event.ResultComponents;
			}
			else
			{
				finalComponents = components;
			}

			foreach (Component c in finalComponents)
			{
				store.put(entityId, c);
			}
			return createEntityRef(entityId);
		}

		public override EntityRef create(string prefabName)
		{
			if (prefabName != null && prefabName.Length > 0)
			{
				Prefab prefab = prefabManager.getPrefab(prefabName);
				if (prefab == null)
				{
					logger.warn("Unable to instantiate unknown prefab: \"{}\"", prefabName);
					return EntityRef.NULL;
				}
				return create(prefab);
			}
			return create();
		}

		public override EntityRef create(string prefabName, Vector3f position)
		{
			if (prefabName != null && prefabName.Length > 0)
			{
				Prefab prefab = prefabManager.getPrefab(prefabName);
				return create(prefab, position);
			}
			return create();
		}

		public override EntityRef create(Prefab prefab, Vector3f position, Quat4f rotation)
		{
			IList<Component> components = Lists.newArrayList();
			foreach (Component component in prefab.iterateComponents())
			{
				Component newComp = componentLibrary.copy(component);
				components.Add(newComp);
				if (newComp is LocationComponent)
				{
					LocationComponent loc = (LocationComponent) newComp;
					loc.WorldPosition = position;
					loc.WorldRotation = rotation;
				}
			}
			components.Add(new EntityInfoComponent(prefab.Name, prefab.Persisted, prefab.AlwaysRelevant));
			return create(components);
		}

		public override EntityRef getEntity(int id)
		{
			return createEntityRef(id);
		}

		public override EntityRef create(Prefab prefab, Vector3f position)
		{
			IList<Component> components = Lists.newArrayList();
			foreach (Component component in prefab.iterateComponents())
			{
				Component newComp = componentLibrary.copy(component);
				components.Add(newComp);
				if (newComp is LocationComponent)
				{
					LocationComponent loc = (LocationComponent) newComp;
					loc.WorldPosition = position;
				}
			}
			components.Add(new EntityInfoComponent(prefab.Name, prefab.Persisted, prefab.AlwaysRelevant));
			return create(components);
		}

		public override EntityRef create(Prefab prefab)
		{
			IList<Component> components = Lists.newArrayList();
			foreach (Component component in prefab.iterateComponents())
			{
				components.Add(componentLibrary.copy(component));
			}
			components.Add(new EntityInfoComponent(prefab.Name, prefab.Persisted, prefab.AlwaysRelevant));
			return create(components);
		}

		public override EntityRef copy(EntityRef other)
		{
			if (!other.exists())
			{
				return EntityRef.NULL;
			}
			IList<Component> newEntityComponents = Lists.newArrayList();
			foreach (Component c in other.iterateComponents())
			{
				newEntityComponents.Add(componentLibrary.copy(c));
			}
			return create(newEntityComponents);
		}

		public override IDictionary<Type, Component> copyComponents(EntityRef other)
		{
			IDictionary<Type, Component> result = Maps.newHashMap();
			foreach (Component c in other.iterateComponents())
			{
				result[c.GetType()] = componentLibrary.copy(c);
			}
			return result;
		}

		public override IEnumerable<EntityRef> AllEntities
		{
			get
			{
				return new IterableAnonymousInnerClassHelper(this);
			}
		}

		private class IterableAnonymousInnerClassHelper : IEnumerable<EntityRef>
		{
			private readonly PojoEntityManager outerInstance;

			public IterableAnonymousInnerClassHelper(PojoEntityManager outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public virtual IEnumerator<EntityRef> GetEnumerator()
			{
				return new EntityIterator(outerInstance, outerInstance.store.entityIdIterator());
			}
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SafeVarargs @Override public final Iterable<org.terasology.entitySystem.entity.EntityRef> getEntitiesWith(Class... componentClasses)
		public override IEnumerable<EntityRef> getEntitiesWith(params Type[] componentClasses)
		{
			if (componentClasses.Length == 0)
			{
				return AllEntities;
			}
			if (componentClasses.Length == 1)
			{
				return iterateEntities(componentClasses[0]);
			}
			TIntList idList = new TIntArrayList();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: gnu.trove.iterator.TIntObjectIterator<? extends org.terasology.entitySystem.Component> primeIterator = store.componentIterator(componentClasses[0]);
			TIntObjectIterator<?> primeIterator = store.componentIterator(componentClasses[0]);
			if (primeIterator == null)
			{
				return Collections.emptyList();
			}

			while (primeIterator.hasNext())
			{
				primeIterator.advance();
				int id = primeIterator.key();
				bool discard = false;
				for (int i = 1; i < componentClasses.Length; ++i)
				{
					if (store.get(id, componentClasses[i]) == null)
					{
						discard = true;
						break;
					}
				}
				if (!discard)
				{
					idList.add(primeIterator.key());
				}
			}
			return new EntityIterable(this, idList);
		}

		private IEnumerable<EntityRef> iterateEntities(Type componentClass)
		{
			TIntList idList = new TIntArrayList();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: gnu.trove.iterator.TIntObjectIterator<? extends org.terasology.entitySystem.Component> primeIterator = store.componentIterator(componentClass);
			TIntObjectIterator<?> primeIterator = store.componentIterator(componentClass);
			if (primeIterator == null)
			{
				return Collections.emptyList();
			}

			while (primeIterator.hasNext())
			{
				primeIterator.advance();
				int id = primeIterator.key();
				idList.add(primeIterator.key());
			}
			return new EntityIterable(this, idList);
		}

		public override int ActiveEntityCount
		{
			get
			{
				return entityCache.Count;
			}
		}

		public override ComponentLibrary ComponentLibrary
		{
			get
			{
				return componentLibrary;
			}
		}

		public override EventSystem EventSystem
		{
			get
			{
				return eventSystem;
			}
			set
			{
				this.eventSystem = value;
			}
		}



		/*
		 * Engine features
		 */

		public override EntityRef createEntityRefWithId(int id)
		{
			if (isExistingEntity(id))
			{
				return createEntityRef(id);
			}
			return EntityRef.NULL;
		}

		public override EntityRef createEntityWithoutLifecycleEvents(IEnumerable<Component> components)
		{
			return createEntity(components);
		}

		public override EntityRef createEntityWithoutLifecycleEvents(string prefabName)
		{
			return createEntityWithoutLifecycleEvents(PrefabManager.getPrefab(prefabName));
		}

		public override EntityRef createEntityWithoutLifecycleEvents(Prefab prefab)
		{
			if (prefab != null)
			{
				IList<Component> components = Lists.newArrayList();
				foreach (Component component in prefab.iterateComponents())
				{
					components.Add(componentLibrary.copy(component));
				}
				components.Add(new EntityInfoComponent(prefab.Name, prefab.Persisted, prefab.AlwaysRelevant));

				return createEntityWithoutLifecycleEvents(components);
			}
			else
			{
				return createEntityWithoutLifecycleEvents(System.Linq.Enumerable.Empty<Component>());
			}
		}

		public override void destroyEntityWithoutEvents(EntityRef entity)
		{
			if (entity.Active)
			{
				destroy(entity);
			}
		}

		public override EntityRef createEntityWithId(int id, IEnumerable<Component> components)
		{
			if (!freedIds.contains(id))
			{
				foreach (Component c in components)
				{
					store.put(id, c);
				}
				loadedIds.add(id);
				EntityRef entity = createEntityRef(id);
				if (eventSystem != null)
				{
					eventSystem.send(entity, OnActivatedComponent.newInstance());
				}
				return entity;
			}
			return EntityRef.NULL;
		}

		public override void subscribe(EntityChangeSubscriber subscriber)
		{
			subscribers.add(subscriber);
		}

		public override void subscribe(EntityDestroySubscriber subscriber)
		{
			destroySubscribers.add(subscriber);
		}

		public override void unsubscribe(EntityChangeSubscriber subscriber)
		{
			subscribers.remove(subscriber);
		}



		public override void deactivateForStorage(EntityRef entity)
		{
			if (entity.exists())
			{
				int entityId = entity.Id;
				if (eventSystem != null)
				{
					eventSystem.send(entity, BeforeDeactivateComponent.newInstance());
				}
				loadedIds.remove(entityId);
				store.remove(entityId);
			}
		}

		public override int NextId
		{
			get
			{
				return nextEntityId;
			}
			set
			{
				nextEntityId = value;
			}
		}


		public override TIntSet FreedIds
		{
			get
			{
				return freedIds;
			}
		}

		/*
		 * For use by Entity Refs
		 */

		/// <param name="entityId"> </param>
		/// <param name="componentClass"> </param>
		/// <returns> Whether the entity has a component of the given type </returns>
		public override bool hasComponent(int entityId, Type componentClass)
		{
			return store.get(entityId, componentClass) != null;
		}

		public override bool isExistingEntity(int id)
		{
			return freedIds.contains(id) || UnsignedInts.toLong(nextEntityId) > UnsignedInts.toLong(id);
		}

		/// <param name="id"> </param>
		/// <returns> Whether the entity is currently active </returns>
		public override bool isActiveEntity(int id)
		{
			return loadedIds.contains(id);
		}

		/// <param name="entityId"> </param>
		/// <returns> An iterable over the components of the given entity </returns>
		public override IEnumerable<Component> iterateComponents(int entityId)
		{
			return store.iterateComponents(entityId);
		}

		/// <summary>
		/// Destroys this entity, sending event
		/// </summary>
		/// <param name="entityId"> </param>
		public override void destroy(int entityId)
		{
			// Don't allow the destruction of unloaded entities.
			if (!loadedIds.contains(entityId))
			{
				return;
			}
			EntityRef @ref = createEntityRef(entityId);
			if (eventSystem != null)
			{
				eventSystem.send(@ref, BeforeDeactivateComponent.newInstance());
				eventSystem.send(@ref, BeforeRemoveComponent.newInstance());
			}
			foreach (Component comp in store.iterateComponents(entityId))
			{
				notifyComponentRemoved(@ref, comp.GetType());
			}
			destroy(@ref);
			foreach (EntityDestroySubscriber destroySubscriber in destroySubscribers)
			{
				destroySubscriber.onEntityDestroyed(entityId);
			}
		}

		private void destroy(EntityRef @ref)
		{
			// Don't allow the destruction of unloaded entities.
			int entityId = @ref.Id;
			entityCache.Remove(entityId);
			loadedIds.remove(entityId);
			freedIds.add(entityId);
			if (@ref is PojoEntityRef)
			{
				((PojoEntityRef) @ref).invalidate();
			}
			store.remove(entityId);
		}

		/// <param name="entityId"> </param>
		/// <param name="componentClass"> </param>
		/// @param <T> </param>
		/// <returns> The component of that type owned by the given entity, or null if it doesn't have that component </returns>
		public override T getComponent<T>(int entityId, Type componentClass) where T : org.terasology.entitySystem.Component
		{
			//return componentLibrary.copy(store.get(entityId, componentClass));
			return store.get(entityId, componentClass);
		}

		/// <summary>
		/// Adds (or replaces) a component to an entity
		/// </summary>
		/// <param name="entityId"> </param>
		/// <param name="component"> </param>
		/// @param <T> </param>
		/// <returns> The added component </returns>
		public override T addComponent<T>(int entityId, T component) where T : org.terasology.entitySystem.Component
		{
			Preconditions.checkNotNull(component);
			Component oldComponent = store.put(entityId, component);
			if (oldComponent != null)
			{
				logger.error("Adding a component ({}) over an existing component for entity {}", component.GetType(), entityId);
			}
			if (eventSystem != null)
			{
				EntityRef entityRef = createEntityRef(entityId);
				if (oldComponent == null)
				{
					eventSystem.send(entityRef, OnAddedComponent.newInstance(), component);
					eventSystem.send(entityRef, OnActivatedComponent.newInstance(), component);
				}
				else
				{
					eventSystem.send(entityRef, OnChangedComponent.newInstance(), component);
				}
			}
			if (oldComponent == null)
			{
				notifyComponentAdded(getEntity(entityId), component.GetType());
			}
			else
			{
				notifyComponentChanged(getEntity(entityId), component.GetType());
			}
			return component;
		}

		/// <summary>
		/// Removes a component from an entity
		/// </summary>
		/// <param name="entityId"> </param>
		/// <param name="componentClass"> </param>
		public override T removeComponent<T>(int entityId, Type componentClass) where T : org.terasology.entitySystem.Component
		{
			T component = store.get(entityId, componentClass);
			if (component != null)
			{
				if (eventSystem != null)
				{
					EntityRef entityRef = createEntityRef(entityId);
					eventSystem.send(entityRef, BeforeDeactivateComponent.newInstance(), component);
					eventSystem.send(entityRef, BeforeRemoveComponent.newInstance(), component);
				}
				notifyComponentRemoved(getEntity(entityId), componentClass);
				store.remove(entityId, componentClass);
			}
			return component;
		}

		/// <summary>
		/// Saves a component to an entity
		/// </summary>
		/// <param name="entityId"> </param>
		/// <param name="component"> </param>
		public override void saveComponent(int entityId, Component component)
		{
			Component oldComponent = store.put(entityId, component);
			if (oldComponent == null)
			{
				logger.error("Saving a component ({}) that doesn't belong to this entity {}", component.GetType(), entityId);
			}
			if (eventSystem != null)
			{
				EntityRef entityRef = createEntityRef(entityId);
				if (oldComponent == null)
				{
					eventSystem.send(entityRef, OnAddedComponent.newInstance(), component);
					eventSystem.send(entityRef, OnActivatedComponent.newInstance(), component);
				}
				else
				{
					eventSystem.send(entityRef, OnChangedComponent.newInstance(), component);
				}
			}
			if (oldComponent == null)
			{
				notifyComponentAdded(getEntity(entityId), component.GetType());
			}
			else
			{
				notifyComponentChanged(getEntity(entityId), component.GetType());
			}
		}

		/*
		 * Implementation
		 */

		private EntityRef createEntityRef(int entityId)
		{
			if (entityId == NULL_ID)
			{
				return EntityRef.NULL;
			}
			BaseEntityRef existing = entityCache[entityId];
			if (existing != null)
			{
				return existing;
			}
			BaseEntityRef newRef = refStrategy.createRefFor(entityId, this);
			entityCache[entityId] = newRef;
			return newRef;
		}

		private void notifyComponentAdded(EntityRef changedEntity, Type component)
		{
			foreach (EntityChangeSubscriber subscriber in subscribers)
			{
				subscriber.onEntityComponentAdded(changedEntity, component);
			}
		}

		private void notifyComponentRemoved(EntityRef changedEntity, Type component)
		{
			foreach (EntityChangeSubscriber subscriber in subscribers)
			{
				subscriber.onEntityComponentRemoved(changedEntity, component);
			}
		}

		private void notifyComponentChanged(EntityRef changedEntity, Type component)
		{
			foreach (EntityChangeSubscriber subscriber in subscribers)
			{
				subscriber.onEntityComponentChange(changedEntity, component);
			}
		}

		// For testing
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override @SafeVarargs public final int getCountOfEntitiesWith(Class... componentClasses)
		public override int getCountOfEntitiesWith(params Type[] componentClasses)
		{
			switch (componentClasses.Length)
			{
				case 0:
					return store.numEntities();
				case 1:
					return store.getComponentCount(componentClasses[0]);
				default:
					return Lists.newArrayList(getEntitiesWith(componentClasses)).size();
			}
		}

		public virtual IEnumerable<KeyValuePair<EntityRef, T>> listComponents<T>(Type componentClass) where T : org.terasology.entitySystem.Component
		{
			TIntObjectIterator<T> iterator = store.componentIterator(componentClass);
			if (iterator != null)
			{
				IList<KeyValuePair<EntityRef, T>> list = new List<KeyValuePair<EntityRef, T>>();
				while (iterator.hasNext())
				{
					iterator.advance();
					list.Add(new EntityEntry<T>(createEntityRef(iterator.key()), iterator.value()));
				}
				return list;
			}
			return Collections.emptyList();
		}

		private class EntityEntry<T> : KeyValuePair<EntityRef, T>
		{
			internal EntityRef key;
			internal T value;

			public EntityEntry(EntityRef @ref, T value)
			{
				this.key = @ref;
				this.value = value;
			}

			public virtual EntityRef Key
			{
				get
				{
					return key;
				}
			}

			public virtual T Value
			{
				get
				{
					return value;
				}
			}

			public virtual T setValue(T newValue)
			{
				throw new System.NotSupportedException();
			}
		}

		private class EntityIterable : IEnumerable<EntityRef>
		{
			private readonly PojoEntityManager outerInstance;

			internal TIntList list;

			public EntityIterable(PojoEntityManager outerInstance, TIntList list)
			{
				this.outerInstance = outerInstance;
				this.list = list;
			}

			public virtual IEnumerator<EntityRef> GetEnumerator()
			{
				return new EntityIterator(outerInstance, list.GetEnumerator());
			}
		}

		private class EntityIterator : IEnumerator<EntityRef>
		{
			private readonly PojoEntityManager outerInstance;

			internal TIntIterator idIterator;

			public EntityIterator(PojoEntityManager outerInstance, TIntIterator idIterator)
			{
				this.outerInstance = outerInstance;
				this.idIterator = idIterator;
			}

			public virtual bool hasNext()
			{
				return idIterator.hasNext();
			}

			public virtual EntityRef next()
			{
				return outerInstance.createEntityRef(idIterator.next());
			}

			public virtual void remove()
			{
				throw new System.NotSupportedException();
			}
		}

	}

}