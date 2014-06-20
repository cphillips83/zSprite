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
namespace org.terasology.entitySystem.prefab.@internal
{

	using Sets = com.google.common.collect.Sets;
	using AssetManager = org.terasology.asset.AssetManager;
	using AssetType = org.terasology.asset.AssetType;
	using Assets = org.terasology.asset.Assets;
	using CoreRegistry = org.terasology.registry.CoreRegistry;

	/// <summary>
	/// Basic implementation of PrefabManager.
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// @author Rasmus 'Cervator' Praestholm <cervator@gmail.com> </summary>
	/// <seealso cref= PrefabManager </seealso>
	public class PojoPrefabManager : PrefabManager
	{

		/// <summary>
		/// {@inheritDoc}
		/// </summary>
		public override Prefab getPrefab(string name)
		{
			if (name.Length > 0)
			{
				return Assets.getPrefab(name);
			}
			return null;

		}

		/// <summary>
		/// {@inheritDoc}
		/// </summary>
		public override bool exists(string name)
		{
			return Assets.getPrefab(name) != null;
		}

		/// <summary>
		/// {@inheritDoc}
		/// </summary>
		public override IEnumerable<Prefab> listPrefabs()
		{
			return CoreRegistry.get(typeof(AssetManager)).listLoadedAssets(AssetType.PREFAB, typeof(Prefab));
		}

		/// <summary>
		/// {@inheritDoc}
		/// </summary>
		public override ICollection<Prefab> listPrefabs(Type comp)
		{
			ICollection<Prefab> prefabs = Sets.newHashSet();

			foreach (Prefab p in CoreRegistry.get(typeof(AssetManager)).listLoadedAssets(AssetType.PREFAB, typeof(Prefab)))
			{
				if (p.getComponent(comp) != null)
				{
					prefabs.Add(p);
				}
			}

			return prefabs;
		}
	}

}