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

	using AssetType = org.terasology.asset.AssetType;
	using AssetUri = org.terasology.asset.AssetUri;
	using TerasologyConstants = org.terasology.engine.TerasologyConstants;


	/// <summary>
	/// @author Immortius
	/// </summary>
	public class NullPrefab : Prefab
	{

		public NullPrefab() : base(new AssetUri(AssetType.PREFAB, TerasologyConstants.ENGINE_MODULE, "null"))
		{
		}

		public override Prefab Parent
		{
			get
			{
				return null;
			}
		}

		public override IList<Prefab> Children
		{
			get
			{
				return Collections.emptyList();
			}
		}

		public override bool Persisted
		{
			get
			{
				return true;
			}
		}

		public override bool AlwaysRelevant
		{
			get
			{
				return false;
			}
		}

		public override bool exists()
		{
			return false;
		}

		public override void reload(PrefabData data)
		{
		}

		public override void dispose()
		{
		}

		public override bool Disposed
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

		public override IEnumerable<Component> iterateComponents()
		{
			return Collections.emptyList();
		}
	}

}