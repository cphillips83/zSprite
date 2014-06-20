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


	/// <summary>
	/// @author Immortius <immortius@gmail.com>
	/// </summary>
	public sealed class MetadataUtil
	{

		private MetadataUtil()
		{
		}

		public static string getComponentClassName(Type componentClass)
		{
			string name = componentClass.Name;
			int index = name.ToLower(Locale.ENGLISH).LastIndexOf("component", StringComparison.Ordinal);
			if (index != -1)
			{
				return name.Substring(0, index);
			}
			return name;
		}
	}

}