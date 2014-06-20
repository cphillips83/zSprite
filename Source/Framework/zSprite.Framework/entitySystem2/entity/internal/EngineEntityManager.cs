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

	using TIntSet = gnu.trove.set.TIntSet;
	using EventSystem = org.terasology.entitySystem.@event.@internal.EventSystem;
	using Prefab = org.terasology.entitySystem.prefab.Prefab;
	using TypeSerializationLibrary = org.terasology.persistence.typeHandling.TypeSerializationLibrary;

	/// <summary>
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public interface EngineEntityManager : LowLevelEntityManager
	{

		RefStrategy EntityRefStrategy {set;}

		/// <summary>
		/// Creates an entity but doesn't send any lifecycle events.
		/// <p/>
		/// This is used by the block entity system to give an illusion of permanence to temporary block entities.
		/// </summary>
		/// <param name="components"> </param>
		/// <returns> The newly created entity ref. </returns>
		EntityRef createEntityWithoutLifecycleEvents(IEnumerable<Component> components);

		/// <summary>
		/// Creates an entity but doesn't send any lifecycle events.
		/// <p/>
		/// This is used by the block entity system to give an illusion of permanence to temporary block entities.
		/// </summary>
		/// <param name="prefab"> </param>
		/// <returns> The newly created entity ref. </returns>
		EntityRef createEntityWithoutLifecycleEvents(string prefab);

		EntityRef createEntityWithoutLifecycleEvents(Prefab prefab);

		/// <summary>
		/// Destroys an entity without sending lifecycle events.
		/// <p/>
		/// This is used by the block entity system to give an illusion of permanence to temporary block entities.
		/// </summary>
		/// <param name="entity"> </param>
		void destroyEntityWithoutEvents(EntityRef entity);

		/// <summary>
		/// Allows the creation of an entity with a given id - this is used
		/// when loading persisted entities
		/// </summary>
		/// <param name="id"> </param>
		/// <param name="components"> </param>
		/// <returns> The entityRef for the newly created entity </returns>
		EntityRef createEntityWithId(int id, IEnumerable<Component> components);

		/// <summary>
		/// Creates an entity ref with the given id. This is used when loading components with references.
		/// </summary>
		/// <param name="id"> </param>
		/// <returns> The entityRef for the given id </returns>
		EntityRef createEntityRefWithId(int id);

		/// <summary>
		/// This is used to persist the entity manager's state
		/// </summary>
		/// <returns> The id that will be used for the next entity (after freed ids are used) </returns>
		int NextId {get;set;}


		/// <summary>
		/// A list of freed ids. This is used when persisting the entity manager's state
		/// </summary>
		/// <returns> A list of freed ids that are available for reuse. </returns>
		TIntSet FreedIds {get;}

		/// <summary>
		/// Removes all entities from the entity manager and resets its state.
		/// </summary>
		void clear();

		/// <summary>
		/// Removes an entity while keeping its id in use - this allows it to be stored
		/// </summary>
		/// <param name="entity"> </param>
		void deactivateForStorage(EntityRef entity);

		/// <summary>
		/// Subscribes to all changes related to entities. Used by engine systems.
		/// </summary>
		/// <param name="subscriber"> </param>
		void subscribe(EntityChangeSubscriber subscriber);

		/// <summary>
		/// Subscribe for notification the destruction of entities.
		/// </summary>
		/// <param name="subscriber"> </param>
		void subscribe(EntityDestroySubscriber subscriber);

		/// <summary>
		/// Unsubscribes from changes relating to entities. Used by engine systems.
		/// </summary>
		/// <param name="subscriber"> </param>
		void unsubscribe(EntityChangeSubscriber subscriber);

		/// <summary>
		/// Sets the event system the entity manager will use to propagate life cycle events.
		/// </summary>
		/// <param name="system"> </param>
		EventSystem EventSystem {set;}

		/// <returns> The default serialization library to use for serializing components </returns>
		TypeSerializationLibrary TypeSerializerLibrary {get;}
	}

}