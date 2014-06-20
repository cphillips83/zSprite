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
namespace org.terasology.entitySystem.entity.@internal
{

	using AssetUri = org.terasology.asset.AssetUri;
	using Event = org.terasology.entitySystem.@event.Event;
	using Prefab = org.terasology.entitySystem.prefab.Prefab;

	/// <summary>
	/// Null entity implementation - acts the same as an empty entity, except you cannot add anything to it.
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public sealed class NullEntityRef : EntityRef
	{
		private static NullEntityRef instance = new NullEntityRef();

		private NullEntityRef()
		{
		}

		public static NullEntityRef Instance
		{
			get
			{
				return instance;
			}
		}

		public override EntityRef copy()
		{
			return this;
		}

		public override bool exists()
		{
			return false;
		}

		public override bool Active
		{
			get
			{
				return false;
			}
		}

		public override bool hasComponent(Type component)
		{
			return false;
		}

		public override T getComponent<T>(Type componentClass) where T : org.terasology.entitySystem.Component
		{
			return null;
		}

		public override T addComponent<T>(T component) where T : org.terasology.entitySystem.Component
		{
			return null;
		}

		public override void removeComponent(Type componentClass)
		{
		}

		public override void saveComponent(Component component)
		{
		}

		public override IEnumerable<Component> iterateComponents()
		{
			return Collections.emptyList();
		}

		public override void destroy()
		{
		}

		public override T send<T>(T @event) where T : org.terasology.entitySystem.@event.Event
		{
			return @event;
		}

		public override int Id
		{
			get
			{
				return PojoEntityManager.NULL_ID;
			}
		}

		public override bool Persistent
		{
			get
			{
				return false;
			}
			set
			{
			}
		}


		public override bool AlwaysRelevant
		{
			get
			{
				return false;
			}
			set
			{
			}
		}


		public override EntityRef Owner
		{
			get
			{
				return EntityRef.NULL;
			}
			set
			{
			}
		}


		public override Prefab ParentPrefab
		{
			get
			{
				return null;
			}
		}

		public override AssetUri PrefabURI
		{
			get
			{
				return null;
			}
		}


		public override string ToString()
		{
			return "EntityRef{" + "id=" + PojoEntityManager.NULL_ID + '}';
		}

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o is EntityRef)
			{
				return !((EntityRef) o).exists();
			}
			return o == null;
		}

		public override int GetHashCode()
		{
			return 0;
		}

	}

}