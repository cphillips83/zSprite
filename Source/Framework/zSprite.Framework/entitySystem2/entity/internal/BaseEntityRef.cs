using System;
using System.Text;

/*
 * Copyright 2014 MovingBlocks
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

	using AssetType = org.terasology.asset.AssetType;
	using AssetUri = org.terasology.asset.AssetUri;
	using Event = org.terasology.entitySystem.@event.Event;
	using Prefab = org.terasology.entitySystem.prefab.Prefab;
	using NetworkComponent = org.terasology.network.NetworkComponent;

	/// <summary>
	/// @author Immortius
	/// </summary>
	public abstract class BaseEntityRef : EntityRef
	{

		protected internal LowLevelEntityManager entityManager;

		public BaseEntityRef(LowLevelEntityManager entityManager)
		{
			this.entityManager = entityManager;
		}

		public override bool Persistent
		{
			get
			{
				return exists() && (!Active || EntityInfo.persisted);
			}
			set
			{
				if (exists())
				{
					EntityInfoComponent info = EntityInfo;
					if (info.persisted != value)
					{
						info.persisted = value;
						saveComponent(info);
					}
				}
			}
		}


		public override bool AlwaysRelevant
		{
			get
			{
				return Active && EntityInfo.alwaysRelevant;
			}
			set
			{
				if (exists())
				{
					EntityInfoComponent info = EntityInfo;
					if (info.alwaysRelevant != value)
					{
						info.alwaysRelevant = value;
						saveComponent(info);
					}
				}
			}
		}


		public override EntityRef Owner
		{
			get
			{
				if (exists())
				{
					return EntityInfo.owner;
				}
				return EntityRef.NULL;
			}
			set
			{
				if (exists())
				{
					EntityInfoComponent info = EntityInfo;
					if (!info.owner.Equals(value))
					{
						info.owner = value;
						saveComponent(info);
					}
				}
			}
		}


		public override Prefab ParentPrefab
		{
			get
			{
				if (exists())
				{
					EntityInfoComponent info = getComponent(typeof(EntityInfoComponent));
					if (info != null)
					{
						return entityManager.PrefabManager.getPrefab(info.parentPrefab);
					}
				}
				return null;
			}
		}

		public override AssetUri PrefabURI
		{
			get
			{
				if (exists())
				{
					EntityInfoComponent info = getComponent(typeof(EntityInfoComponent));
					if (info != null && info.parentPrefab.Length > 0)
					{
						return new AssetUri(AssetType.PREFAB, info.parentPrefab);
					}
				}
				return null;
			}
		}

		public override bool Active
		{
			get
			{
				return exists() && entityManager.isActiveEntity(Id);
			}
		}

		public override T getComponent<T>(Type componentClass) where T : org.terasology.entitySystem.Component
		{
			if (exists())
			{
				return entityManager.getComponent(Id, componentClass);
			}
			return null;
		}

		public override T addComponent<T>(T component) where T : org.terasology.entitySystem.Component
		{
			if (Active)
			{
				return entityManager.addComponent(Id, component);
			}
			return component;
		}

		public override void removeComponent(Type componentClass)
		{
			if (Active)
			{
				entityManager.removeComponent(Id, componentClass);
			}
		}

		public override void saveComponent(Component component)
		{
			if (Active)
			{
				entityManager.saveComponent(Id, component);
			}
		}

		public override IEnumerable<Component> iterateComponents()
		{
			if (exists())
			{
				return entityManager.iterateComponents(Id);
			}
			return Collections.emptyList();
		}

		public override void destroy()
		{
			if (Active)
			{
				entityManager.destroy(Id);
			}
		}

		public override T send<T>(T @event) where T : org.terasology.entitySystem.@event.Event
		{
			if (exists())
			{
				entityManager.EventSystem.send(this, @event);
			}
			return @event;
		}

		public override bool hasComponent(Type component)
		{
			return exists() && entityManager.hasComponent(Id, component);
		}

		public override string ToString()
		{
			AssetUri prefabUri = PrefabURI;
			StringBuilder builder = new StringBuilder();
			builder.Append("EntityRef{id = ");
			builder.Append(Id);
			NetworkComponent networkComponent = getComponent(typeof(NetworkComponent));
			if (networkComponent != null)
			{
				builder.Append(", netId = ");
				builder.Append(networkComponent.NetworkId);
			}
			if (prefabUri != null)
			{
				builder.Append(", prefab = '");
				builder.Append(prefabUri.toSimpleString());
				builder.Append("'");
			}
			builder.Append("}");
			return builder.ToString();
		}

		public virtual void invalidate()
		{
			entityManager = null;
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