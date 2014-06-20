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
namespace org.terasology.entitySystem.prefab
{

	using Objects = com.google.common.@base.Objects;
	using AbstractAsset = org.terasology.asset.AbstractAsset;
	using Asset = org.terasology.asset.Asset;
	using AssetUri = org.terasology.asset.AssetUri;
	using NullPrefab = org.terasology.entitySystem.prefab.@internal.NullPrefab;

	/// <summary>
	/// An entity prefab describes the recipe for creating an entity.
	/// Like an entity it groups a collection of components.
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public abstract class Prefab : AbstractAsset<PrefabData>, ComponentContainer, Asset<PrefabData>
	{

		public static readonly Prefab NULL = new NullPrefab();

		public Prefab(AssetUri uri) : base(uri)
		{
		}

		public string Name
		{
			get
			{
				return URI.toSimpleString();
			}
		}

		/// <summary>
		/// Return parents prefabs
		/// 
		/// @return
		/// </summary>
		public abstract Prefab Parent {get;}

		public abstract IList<Prefab> Children {get;}

		public abstract bool Persisted {get;}

		public abstract bool AlwaysRelevant {get;}

		public abstract bool exists();

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o is Prefab)
			{
				return Objects.equal(URI, ((Prefab) o).URI);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Objects.GetHashCode(URI);
		}

		public override string ToString()
		{
			return "Prefab(" + URI + "){ components: " + this.iterateComponents() + ", parent: " + this.Parent + " }";
		}

	}

}