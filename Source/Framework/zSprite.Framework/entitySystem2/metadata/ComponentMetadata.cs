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
namespace org.terasology.entitySystem.metadata
{

	using Predicates = com.google.common.@base.Predicates;
	using ClassMetadata = org.terasology.reflection.metadata.ClassMetadata;
	using CopyStrategy = org.terasology.reflection.copy.CopyStrategy;
	using CopyStrategyLibrary = org.terasology.reflection.copy.CopyStrategyLibrary;
	using InaccessibleFieldException = org.terasology.reflection.reflect.InaccessibleFieldException;
	using ReflectFactory = org.terasology.reflection.reflect.ReflectFactory;
	using SimpleUri = org.terasology.engine.SimpleUri;
	using Replicate = org.terasology.network.Replicate;
	using ForceBlockActive = org.terasology.world.block.ForceBlockActive;
	using RequiresBlockLifecycleEvents = org.terasology.world.block.RequiresBlockLifecycleEvents;

	/// <summary>
	/// Metadata on a component class and its fields.
	/// 
	/// @author Immortius
	/// </summary>
	public class ComponentMetadata<T> : ClassMetadata<T, ComponentFieldMetadata<T, JavaToDotNetGenericWildcard>> where T : org.terasology.entitySystem.Component
	{

		private bool replicated;
		private bool replicatedFromOwner;
		private bool referenceOwner;
		private bool forceBlockActive;
		private bool retainUnalteredOnBlockChange;
		private bool blockLifecycleEventsRequired;

		/// <param name="uri">            The uri to identify the component with. </param>
		/// <param name="type">           The type to create the metadata for </param>
		/// <param name="factory">        A reflection library to provide class construction and field get/set functionality </param>
		/// <param name="copyStrategies"> A copy strategy library </param>
		/// <exception cref="NoSuchMethodException"> If the component has no default constructor </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ComponentMetadata(org.terasology.engine.SimpleUri uri, Class type, org.terasology.reflection.reflect.ReflectFactory factory, org.terasology.reflection.copy.CopyStrategyLibrary copyStrategies) throws NoSuchMethodException
		public ComponentMetadata(SimpleUri uri, Type type, ReflectFactory factory, CopyStrategyLibrary copyStrategies) : base(uri, type, factory, copyStrategies, Predicates.alwaysTrue())
		{
			replicated = type.getAnnotation(typeof(Replicate)) != null;
			blockLifecycleEventsRequired = type.getAnnotation(typeof(RequiresBlockLifecycleEvents)) != null;
			ForceBlockActive forceBlockActiveAnnotation = type.getAnnotation(typeof(ForceBlockActive));
			if (forceBlockActiveAnnotation != null)
			{
				forceBlockActive = true;
				retainUnalteredOnBlockChange = forceBlockActiveAnnotation.retainUnalteredOnBlockChange();
			}

//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: for (ComponentFieldMetadata<T, ?> field : getFields())
			foreach (ComponentFieldMetadata<T, ?> field in Fields)
			{
				if (field.Replicated)
				{
					replicated = true;
					if (field.ReplicationInfo.value().ReplicateFromOwner)
					{
						replicatedFromOwner = true;
					}
				}
				if (field.OwnedReference)
				{
					referenceOwner = true;
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected <U> ComponentFieldMetadata<T, U> createField(Field field, org.terasology.reflection.copy.CopyStrategy<U> copyStrategy, org.terasology.reflection.reflect.ReflectFactory factory) throws org.terasology.reflection.reflect.InaccessibleFieldException
		protected internal virtual ComponentFieldMetadata<T, U> createField<U>(Field field, CopyStrategy<U> copyStrategy, ReflectFactory factory)
		{
			return new ComponentFieldMetadata<>(this, field, copyStrategy, factory, false);
		}

		/// <returns> Whether this component owns any references </returns>
		public virtual bool ReferenceOwner
		{
			get
			{
				return referenceOwner;
			}
		}

		/// <returns> Whether this component replicates any fields from owner to server </returns>
		public virtual bool ReplicatedFromOwner
		{
			get
			{
				return replicatedFromOwner;
			}
		}

		/// <returns> Whether this component needs to be replicated </returns>
		public virtual bool Replicated
		{
			get
			{
				return replicated;
			}
		}

		/// <returns> Whether this component forces a block active </returns>
		public virtual bool ForceBlockActive
		{
			get
			{
				return forceBlockActive;
			}
		}

		/// <returns> Whether this component should be retained unaltered on block change </returns>
		public virtual bool RetainUnalteredOnBlockChange
		{
			get
			{
				return retainUnalteredOnBlockChange;
			}
		}

		/// <returns> Whether this component makes a block valid for block lifecycle events </returns>
		public virtual bool BlockLifecycleEventsRequired
		{
			get
			{
				return blockLifecycleEventsRequired;
			}
		}
	}

}