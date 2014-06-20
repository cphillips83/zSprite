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

	using Charsets = com.google.common.@base.Charsets;
	using Lists = com.google.common.collect.Lists;
	using AssetLoader = org.terasology.asset.AssetLoader;
	using Module = org.terasology.engine.module.Module;
	using EngineEntityManager = org.terasology.entitySystem.entity.@internal.EngineEntityManager;
	using EntityDataJSONFormat = org.terasology.persistence.serializers.EntityDataJSONFormat;
	using PrefabSerializer = org.terasology.persistence.serializers.PrefabSerializer;
	using EntityData = org.terasology.protobuf.EntityData;
	using CoreRegistry = org.terasology.registry.CoreRegistry;


	/// <summary>
	/// @author Immortius
	/// </summary>
	public class PrefabLoader : AssetLoader<PrefabData>
	{

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public org.terasology.entitySystem.prefab.PrefabData load(org.terasology.engine.module.Module module, java.io.InputStream stream, java.util.List<java.net.URL> urls, java.util.List<java.net.URL> deltas) throws java.io.IOException
		public override PrefabData load(Module module, InputStream stream, IList<URL> urls, IList<URL> deltas)
		{
			BufferedReader reader = new BufferedReader(new InputStreamReader(stream, Charsets.UTF_8));
			EntityData.Prefab prefabData = EntityDataJSONFormat.readPrefab(reader);
			if (prefabData != null)
			{
				EngineEntityManager entityManager = CoreRegistry.get(typeof(EngineEntityManager));
				IList<EntityData.Prefab> deltaData = Lists.newArrayListWithCapacity(deltas.Count);
				foreach (URL deltaUrl in deltas)
				{
					using (BufferedReader deltaReader = new BufferedReader(new InputStreamReader(deltaUrl.openStream(), Charsets.UTF_8)))
					{
						EntityData.Prefab delta = EntityDataJSONFormat.readPrefab(deltaReader);
						deltaData.Add(delta);
					}
				}
				PrefabSerializer serializer = new PrefabSerializer(entityManager.ComponentLibrary, entityManager.TypeSerializerLibrary);
				return serializer.deserialize(prefabData, deltaData);
			}
			return null;
		}
	}

}