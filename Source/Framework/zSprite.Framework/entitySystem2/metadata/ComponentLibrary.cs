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

	using Iterables = com.google.common.collect.Iterables;
	using Logger = org.slf4j.Logger;
	using LoggerFactory = org.slf4j.LoggerFactory;
	using AbstractClassLibrary = org.terasology.reflection.metadata.AbstractClassLibrary;
	using ClassMetadata = org.terasology.reflection.metadata.ClassMetadata;
	using CopyStrategyLibrary = org.terasology.reflection.copy.CopyStrategyLibrary;
	using ReflectFactory = org.terasology.reflection.reflect.ReflectFactory;
	using CoreRegistry = org.terasology.registry.CoreRegistry;
	using SimpleUri = org.terasology.engine.SimpleUri;
	using Module = org.terasology.engine.module.Module;
	using ModuleManager = org.terasology.engine.module.ModuleManager;

	/// <summary>
	/// The library for metadata about components (and their fields).
	/// 
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public class ComponentLibrary : AbstractClassLibrary<Component>
	{

		private static readonly Logger logger = LoggerFactory.getLogger(typeof(ComponentLibrary));
		private ModuleManager moduleManager = CoreRegistry.get(typeof(ModuleManager));

		public ComponentLibrary(ReflectFactory factory, CopyStrategyLibrary copyStrategies) : base(factory, copyStrategies)
		{
		}

//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: @Override protected <C extends org.terasology.entitySystem.Component> org.terasology.reflection.metadata.ClassMetadata<C, ?> createMetadata(Class type, org.terasology.reflection.reflect.ReflectFactory factory, org.terasology.reflection.copy.CopyStrategyLibrary copyStrategies, org.terasology.engine.SimpleUri uri)
		protected internal override ClassMetadata<C, ?> createMetadata<C>(Type type, ReflectFactory factory, CopyStrategyLibrary copyStrategies, SimpleUri uri) where C : org.terasology.entitySystem.Component
		{
			ComponentMetadata<C> info;
			try
			{
				info = new ComponentMetadata<>(uri, type, factory, copyStrategies);
			}
			catch (NoSuchMethodException e)
			{
				logger.error("Unable to register class {}: Default Constructor Required", type.Name, e);
				return null;
			}
			return info;
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override @SuppressWarnings("unchecked") public <T extends org.terasology.entitySystem.Component> ComponentMetadata<T> getMetadata(Class clazz)
		public override ComponentMetadata<T> getMetadata<T>(Type clazz) where T : org.terasology.entitySystem.Component
		{
			return (ComponentMetadata<T>) base.getMetadata(clazz);
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override @SuppressWarnings("unchecked") public <T extends org.terasology.entitySystem.Component> ComponentMetadata<T> getMetadata(T object)
		public override ComponentMetadata<T> getMetadata<T>(T @object) where T : org.terasology.entitySystem.Component
		{
			return (ComponentMetadata<T>) base.getMetadata(@object);
		}

//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: @Override public ComponentMetadata<? extends org.terasology.entitySystem.Component> getMetadata(org.terasology.engine.SimpleUri uri)
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: @Override public ComponentMetadata<? extends org.terasology.entitySystem.Component> getMetadata(org.terasology.engine.SimpleUri uri)
		public override ComponentMetadata<?> getMetadata(SimpleUri uri) where ? : org.terasology.entitySystem.Component
		{
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: return (ComponentMetadata<? extends org.terasology.entitySystem.Component>) base.getMetadata(uri);
			return (ComponentMetadata<?>) base.getMetadata(uri);
		}

//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: @Override public ComponentMetadata<? extends org.terasology.entitySystem.Component> resolve(String name)
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: @Override public ComponentMetadata<? extends org.terasology.entitySystem.Component> resolve(String name)
		public override ComponentMetadata<?> resolve(string name) where ? : org.terasology.entitySystem.Component
		{
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: return (ComponentMetadata<? extends org.terasology.entitySystem.Component>) base.resolve(name);
			return (ComponentMetadata<?>) base.resolve(name);
		}

//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: @Override public ComponentMetadata<?> resolve(String name, String context)
		public override ComponentMetadata<?> resolve(string name, string context)
		{
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: return (ComponentMetadata<?>) base.resolve(name, context);
			return (ComponentMetadata<?>) base.resolve(name, context);
		}

//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: @Override public ComponentMetadata<?> resolve(String name, org.terasology.engine.module.Module context)
		public override ComponentMetadata<?> resolve(string name, Module context)
		{
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: return (ComponentMetadata<?>) base.resolve(name, context);
			return (ComponentMetadata<?>) base.resolve(name, context);
		}

		public virtual IEnumerable<ComponentMetadata> iterateComponentMetadata()
		{
			return Iterables.filter(this, typeof(ComponentMetadata));
		}
	}

}