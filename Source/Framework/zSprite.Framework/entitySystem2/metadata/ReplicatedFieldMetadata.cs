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
	using FieldMetadata = org.terasology.reflection.metadata.FieldMetadata;
	using CopyStrategy = org.terasology.reflection.copy.CopyStrategy;
	using InaccessibleFieldException = org.terasology.reflection.reflect.InaccessibleFieldException;
	using ReflectFactory = org.terasology.reflection.reflect.ReflectFactory;
	using NoReplicate = org.terasology.network.NoReplicate;
	using Replicate = org.terasology.network.Replicate;

	/// <summary>
	/// An extended FieldMetadata that provides information on whether a the field should be replicated, and under what conditions
	/// 
	/// @author Immortius
	/// </summary>
	public class ReplicatedFieldMetadata<T, U> : FieldMetadata<T, U>
	{

		private bool replicated;
		private Replicate replicationInfo;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ReplicatedFieldMetadata(org.terasology.reflection.metadata.ClassMetadata<T, ?> owner, Field field, org.terasology.reflection.copy.CopyStrategy<U> copyStrategy, org.terasology.reflection.reflect.ReflectFactory factory, boolean replicatedByDefault) throws org.terasology.reflection.reflect.InaccessibleFieldException
		public ReplicatedFieldMetadata<T1>(ClassMetadata<T1> owner, Field field, CopyStrategy<U> copyStrategy, ReflectFactory factory, bool replicatedByDefault) : base(owner, field, copyStrategy, factory)
		{
			this.replicated = replicatedByDefault;
			if (field.getAnnotation(typeof(NoReplicate)) != null)
			{
				replicated = false;
			}
			if (field.getAnnotation(typeof(Replicate)) != null)
			{
				replicated = true;
			}
			this.replicationInfo = field.getAnnotation(typeof(Replicate));
		}

		/// <returns> Whether this field should be replicated on the network </returns>
		public virtual bool Replicated
		{
			get
			{
				return replicated;
			}
		}

		/// <returns> The replication information for this field, or null if it isn't marked with the Replicate annotation </returns>
		public virtual Replicate ReplicationInfo
		{
			get
			{
				return replicationInfo;
			}
		}
	}

}