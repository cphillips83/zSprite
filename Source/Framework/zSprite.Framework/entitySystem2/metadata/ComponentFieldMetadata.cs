using System;
using System.Collections;

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
namespace org.terasology.entitySystem.metadata
{

	using ClassMetadata = org.terasology.reflection.metadata.ClassMetadata;
	using CopyStrategy = org.terasology.reflection.copy.CopyStrategy;
	using InaccessibleFieldException = org.terasology.reflection.reflect.InaccessibleFieldException;
	using ReflectFactory = org.terasology.reflection.reflect.ReflectFactory;
	using EntityRef = org.terasology.entitySystem.entity.EntityRef;
	using ReflectionUtil = org.terasology.utilities.ReflectionUtil;


	/// <summary>
	/// Field Metadata for the fields of components. In addition to the standard and replication metadata, has information on whether the field declares ownership over an entity.
	/// 
	/// @author Immortius
	/// </summary>
	public class ComponentFieldMetadata<T, U> : ReplicatedFieldMetadata<T, U> where T : org.terasology.entitySystem.Component
	{

		private bool ownedReference;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ComponentFieldMetadata(org.terasology.reflection.metadata.ClassMetadata<T, ?> owner, Field field, org.terasology.reflection.copy.CopyStrategy<U> copyStrategy, org.terasology.reflection.reflect.ReflectFactory factory, boolean replicatedByDefault) throws org.terasology.reflection.reflect.InaccessibleFieldException
		public ComponentFieldMetadata<T1>(ClassMetadata<T1> owner, Field field, CopyStrategy<U> copyStrategy, ReflectFactory factory, bool replicatedByDefault) : base(owner, field, copyStrategy, factory, replicatedByDefault)
		{
			ownedReference = field.getAnnotation(typeof(Owns)) != null && (field.Type.IsSubclassOf(typeof(EntityRef)) || isCollectionOf(typeof(EntityRef), field.GenericType));
		}

		/// <returns> Whether this field is marked with the @Owned annotation </returns>
		public virtual bool OwnedReference
		{
			get
			{
				return ownedReference;
			}
		}

		private bool isCollectionOf(Type targetType, Type genericType)
		{
			return (Type.IsSubclassOf(typeof(ICollection)) && ReflectionUtil.getTypeParameter(genericType, 0).Equals(targetType)) || (Type.IsSubclassOf(typeof(IDictionary)) && ReflectionUtil.getTypeParameter(genericType, 1).Equals(targetType));
		}
	}

}