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

	using ImmutableList = com.google.common.collect.ImmutableList;
	using ImmutableMap = com.google.common.collect.ImmutableMap;
	using Lists = com.google.common.collect.Lists;
	using AssetUri = org.terasology.asset.AssetUri;


	/// <summary>
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public class PojoPrefab : Prefab
	{

		private Prefab parent;
		private IDictionary<Type, Component> componentMap;
		private IList<Prefab> children = Lists.newArrayList();
		private bool persisted;
		private bool alwaysRelevant = true;

		public PojoPrefab(AssetUri uri, PrefabData data) : base(uri)
		{
			reload(data);
		}

		public override Prefab Parent
		{
			get
			{
				return parent;
			}
		}

		public override IList<Prefab> Children
		{
			get
			{
				return ImmutableList.copyOf(children);
			}
		}

		public override bool Persisted
		{
			get
			{
				return persisted;
			}
		}

		public override bool AlwaysRelevant
		{
			get
			{
				return alwaysRelevant;
			}
		}

		public override bool exists()
		{
			return true;
		}

		public override bool hasComponent(Type component)
		{
			return componentMap.ContainsKey(component);
		}

		public override T getComponent<T>(Type componentClass) where T : org.terasology.entitySystem.Component
		{
			return componentClass.cast(componentMap[componentClass]);
		}

		public override IEnumerable<Component> iterateComponents()
		{
			return ImmutableList.copyOf(componentMap.Values);
		}

		public override void dispose()
		{
		}

		public override void reload(PrefabData data)
		{
			this.componentMap = ImmutableMap.copyOf(data.Components);
			this.persisted = data.Persisted;
			this.alwaysRelevant = data.AlwaysRelevant;
			this.parent = data.Parent;
			if (parent != null && parent is PojoPrefab)
			{
				((PojoPrefab) parent).children.Add(this);
			}
		}

		public override bool Disposed
		{
			get
			{
				return false;
			}
		}
	}

}