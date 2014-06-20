using System.Text;

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

	using Objects = com.google.common.@base.Objects;
	using AssetUri = org.terasology.asset.AssetUri;
	using NetworkComponent = org.terasology.network.NetworkComponent;

	/// <summary>
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public class PojoEntityRef : BaseEntityRef
	{
		private int id;
		private bool exists_Renamed = true;

		internal PojoEntityRef(LowLevelEntityManager manager, int id) : base(manager)
		{
			this.id = id;
		}

		public override int Id
		{
			get
			{
				return id;
			}
		}

		public override EntityRef copy()
		{
			if (exists_Renamed)
			{
				entityManager.create(entityManager.copyComponents(this).values());
			}
			return NULL;
		}

		public override bool exists()
		{
			return exists_Renamed;
		}

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o is PojoEntityRef)
			{
				PojoEntityRef other = (PojoEntityRef) o;
				return !exists() && !other.exists() || Id == other.Id;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Objects.GetHashCode(id);
		}

		public override string ToString()
		{
			AssetUri prefabUri = PrefabURI;
			StringBuilder builder = new StringBuilder();
			builder.Append("EntityRef{id = ");
			builder.Append(id);
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

		public override void invalidate()
		{
			base.invalidate();
			exists_Renamed = false;
		}
	}

}