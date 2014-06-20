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
namespace org.terasology.entitySystem.prefab
{

	using Maps = com.google.common.collect.Maps;
	using AssetData = org.terasology.asset.AssetData;

	/// <summary>
	/// @author Immortius
	/// </summary>
	public class PrefabData : AssetData, MutableComponentContainer
	{

		private IDictionary<Type, Component> components = Maps.newHashMap();
		private bool persisted = true;
		private Prefab parent;
		private bool alwaysRelevant;

		public static PrefabData createFromPrefab(Prefab prefab)
		{
			PrefabData result = new PrefabData();
			foreach (Component component in prefab.iterateComponents())
			{
				result.addComponent(component);
			}

			result.AlwaysRelevant = prefab.AlwaysRelevant;
			result.Parent = prefab.Parent;
			result.Persisted = prefab.Persisted;
			return result;
		}

		public override T addComponent<T>(T component) where T : org.terasology.entitySystem.Component
		{
			components[component.GetType()] = component;
			return component;
		}

		public override void removeComponent(Type componentClass)
		{
			components.Remove(componentClass);
		}

		public override void saveComponent(Component component)
		{
		}

		public override bool hasComponent(Type component)
		{
			return components.ContainsKey(component);
		}

		public override T getComponent<T>(Type componentClass) where T : org.terasology.entitySystem.Component
		{
			return componentClass.cast(components[componentClass]);
		}

		public override IEnumerable<Component> iterateComponents()
		{
			return components.Values;
		}

		public virtual IDictionary<Type, Component> Components
		{
			get
			{
				return components;
			}
		}

		public virtual bool Persisted
		{
			get
			{
				return persisted;
			}
			set
			{
				this.persisted = value;
			}
		}


		public virtual Prefab Parent
		{
			set
			{
				this.parent = value;
			}
			get
			{
				return parent;
			}
		}


		public virtual bool AlwaysRelevant
		{
			get
			{
				return alwaysRelevant;
			}
			set
			{
				this.alwaysRelevant = value;
			}
		}


	}

}