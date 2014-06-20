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

	using AssetUri = org.terasology.asset.AssetUri;
	using CoreRegistry = org.terasology.registry.CoreRegistry;
	using EngineEntityManager = org.terasology.entitySystem.entity.@internal.EngineEntityManager;
	using NullEntityRef = org.terasology.entitySystem.entity.@internal.NullEntityRef;
	using Event = org.terasology.entitySystem.@event.Event;
	using Prefab = org.terasology.entitySystem.prefab.Prefab;
	using EntityDataJSONFormat = org.terasology.persistence.serializers.EntityDataJSONFormat;
	using EntitySerializer = org.terasology.persistence.serializers.EntitySerializer;

	/// <summary>
	/// A wrapper around an entity id providing access to common functionality
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public abstract class EntityRef : MutableComponentContainer
	{

		public static readonly EntityRef NULL = NullEntityRef.Instance;

		/// <summary>
		/// Copies this entity, creating a new entity with identical components.
		/// Note: You will need to be careful when copying entities, particularly around ownership - this method does nothing to prevent you ending up
		/// with multiple entities owning the same entities. </summary>
		/// <returns> A copy of this entity. </returns>
		public abstract EntityRef copy();

		/// <returns> Does this entity exist - that is, is not deleted. </returns>
		public abstract bool exists();

		/// <returns> Whether this entity is currently loaded (not stored) </returns>
		public abstract bool Active {get;}

		/// <summary>
		/// Removes all components and destroys it
		/// </summary>
		public abstract void destroy();

		/// <summary>
		/// Transmits an event to this entity
		/// </summary>
		/// <param name="event"> </param>
		public abstract T send<T>(T @event) where T : org.terasology.entitySystem.@event.Event;

		/// <returns> The identifier of this entity. Should be avoided where possible and the EntityRef
		///         used instead to allow it to be invalidated if the entity is destroyed. </returns>
		public abstract int Id {get;}

		/// <returns> Whether this entity should be saved </returns>
		public abstract bool Persistent {get;set;}


		/// <returns> Whether this entity should remain active even when the part of the world/owner of the entity is not
		///         relevant </returns>
		public abstract bool AlwaysRelevant {get;set;}


		/// <returns> The owning entity of this entity </returns>
		public abstract EntityRef Owner {get;set;}


		/// <returns> The prefab this entity is based off of </returns>
		public abstract Prefab ParentPrefab {get;}

		/// <returns> The AssetUri of this entity's prefab, or null if it isn't based on an entity. </returns>
		public abstract AssetUri PrefabURI {get;}

		/// <returns> A full, json style description of the entity. </returns>
		public virtual string toFullDescription()
		{
			EntitySerializer serializer = new EntitySerializer((EngineEntityManager) CoreRegistry.get(typeof(EntityManager)));
			serializer.UsingFieldIds = false;
			return EntityDataJSONFormat.write(serializer.serialize(this));
		}

	}

}