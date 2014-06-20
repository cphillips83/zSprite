using System.Collections;
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
namespace org.terasology.entitySystem.entity.@internal
{

	using Sets = com.google.common.collect.Sets;
	using ComponentFieldMetadata = org.terasology.entitySystem.metadata.ComponentFieldMetadata;
	using ComponentLibrary = org.terasology.entitySystem.metadata.ComponentLibrary;
	using ComponentMetadata = org.terasology.entitySystem.metadata.ComponentMetadata;


	/// <summary>
	/// @author Immortius
	/// </summary>
	public sealed class OwnershipHelper
	{
		private ComponentLibrary componentLibrary;

		public OwnershipHelper(ComponentLibrary componentLibrary)
		{
			this.componentLibrary = componentLibrary;
		}

		/// <summary>
		/// Produces a collection of entities that are owned by the provided entity.
		/// This is immediate ownership only - it does not recursively follow ownership.
		/// </summary>
		/// <param name="entity"> The owning entity </param>
		/// <returns> A collection of owned entities of the given entity </returns>
		public ICollection<EntityRef> listOwnedEntities(EntityRef entity)
		{
			Set<EntityRef> entityRefList = Sets.newHashSet();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: for (org.terasology.entitySystem.metadata.ComponentMetadata<?> componentMetadata : componentLibrary.iterateComponentMetadata())
			foreach (ComponentMetadata<?> componentMetadata in componentLibrary.iterateComponentMetadata())
			{
				if (componentMetadata.ReferenceOwner)
				{
					Component comp = entity.getComponent(componentMetadata.Type);
					if (comp != null)
					{
						addOwnedEntitiesFor(comp, componentMetadata, entityRefList);
					}
				}
			}
			return entityRefList;
		}

		public ICollection<EntityRef> listOwnedEntities(Component component)
		{
			Set<EntityRef> entityRefList = Sets.newHashSet();
			addOwnedEntitiesFor(component, componentLibrary.getMetadata(component.GetType()), entityRefList);
			return entityRefList;
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") private void addOwnedEntitiesFor(org.terasology.entitySystem.Component comp, org.terasology.entitySystem.metadata.ComponentMetadata<?> componentMetadata, java.util.Collection<org.terasology.entitySystem.entity.EntityRef> outEntityList)
		private void addOwnedEntitiesFor<T1>(Component comp, ComponentMetadata<T1> componentMetadata, ICollection<EntityRef> outEntityList)
		{
			foreach (ComponentFieldMetadata field in componentMetadata.Fields)
			{
				if (field.OwnedReference)
				{
					object value = field.getValue(comp);
					if (value is ICollection)
					{
						foreach (EntityRef @ref in ((ICollection<EntityRef>) value))
						{
							if (@ref.exists())
							{
								outEntityList.Add(@ref);
							}
						}
					}
					else if (value is IDictionary)
					{
						foreach (EntityRef @ref in ((IDictionary<object, EntityRef>) value).Values)
						{
							if (@ref.exists())
							{
								outEntityList.Add(@ref);
							}
						}
					}
					else if (value is EntityRef)
					{
						EntityRef @ref = (EntityRef) value;
						if (@ref.exists())
						{
							outEntityList.Add(@ref);
						}
					}
				}
			}
		}

	}

}