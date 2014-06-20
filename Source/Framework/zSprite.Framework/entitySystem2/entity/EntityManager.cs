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

	using EventSystem = org.terasology.entitySystem.@event.@internal.EventSystem;
	using ComponentLibrary = org.terasology.entitySystem.metadata.ComponentLibrary;
	using Prefab = org.terasology.entitySystem.prefab.Prefab;
	using PrefabManager = org.terasology.entitySystem.prefab.PrefabManager;


	/// <summary>
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public interface EntityManager
	{

		/// <summary>
		/// Creates an EntityBuilder.
		/// </summary>
		/// <returns> A new entity builder </returns>
		EntityBuilder newBuilder();

		/// <summary>
		/// Creates an EntityBuilder, from a prefab
		/// </summary>
		/// <returns> A new entity builder </returns>
		EntityBuilder newBuilder(string prefabName);

		/// <summary>
		/// Creates an EntityBuilder, from a prefab
		/// </summary>
		/// <returns> A new entity builder </returns>
		EntityBuilder newBuilder(Prefab prefab);

		/// <returns> A references to a new, unused entity </returns>
		EntityRef create();

		/// <returns> A references to a new, unused entity with the desired components </returns>
		EntityRef create(params Component[] components);

		/// <returns> A references to a new, unused entity with the desired components </returns>
		EntityRef create(IEnumerable<Component> components);

		/// <param name="prefabName"> The name of the prefab to create. </param>
		/// <returns> A new entity, based on the the prefab of the given name. If the prefab doesn't exist, just a new entity. </returns>
		EntityRef create(string prefabName);

		/// <param name="prefab"> </param>
		/// <returns> A new entity, based on the given prefab </returns>
		EntityRef create(Prefab prefab);

		// TODO: Review. Probably better to move these into a static helper

		/// <param name="prefab"> </param>
		/// <param name="position"> </param>
		/// <returns> A new entity, based on the given prefab, at the desired position </returns>
		EntityRef create(string prefab, Vector3f position);

		/// <param name="prefab"> </param>
		/// <param name="position"> </param>
		/// <returns> A new entity, based on the given prefab, at the desired position </returns>
		EntityRef create(Prefab prefab, Vector3f position);

		/// <param name="prefab"> </param>
		/// <param name="position"> </param>
		/// <param name="rotation">
		/// @return </param>
		EntityRef create(Prefab prefab, Vector3f position, Quat4f rotation);

		/// <param name="id"> </param>
		/// <returns> The entity with the given id, or the null entity </returns>
		EntityRef getEntity(int id);

		/// <param name="other"> </param>
		/// <returns> A new entity with a copy of each of the other entity's components </returns>
		/// @deprecated Use EntityRef.copy() instead. 
		[Obsolete("Use EntityRef.copy() instead.")]
		EntityRef copy(EntityRef other);

		/// <summary>
		/// Creates a copy of the components of an entity.
		/// </summary>
		/// <param name="original"> </param>
		/// <returns> A map of components types to components copied from the target entity. </returns>
		// TODO: Remove? A little dangerous due to ownership
		IDictionary<Type, Component> copyComponents(EntityRef original);

		/// <returns> An iterable over all entities </returns>
		IEnumerable<EntityRef> AllEntities {get;}

		/// <param name="componentClasses"> </param>
		/// <returns> An iterable over all entities with the provided component types. </returns>
		IEnumerable<EntityRef> getEntitiesWith(params Type[] componentClasses);

		/// <param name="componentClasses"> </param>
		/// <returns> A count of entities with the provided component types </returns>
		int getCountOfEntitiesWith(params Type[] componentClasses);

		/// <returns> The event system being used by the entity manager </returns>
		EventSystem EventSystem {get;}

		/// <returns> The prefab manager being used by the entity manager </returns>
		PrefabManager PrefabManager {get;}

		/// <returns> The component library being used by the entity manager </returns>
		ComponentLibrary ComponentLibrary {get;}

		/// <returns> A count of currently active entities </returns>
		int ActiveEntityCount {get;}
	}

}