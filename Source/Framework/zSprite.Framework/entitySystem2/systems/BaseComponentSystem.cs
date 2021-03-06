﻿/*
 * Copyright 2013 MovingBlocks
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace org.terasology.entitySystem.systems
{

	/// <summary>
	/// @author Immortius
	/// </summary>
	public abstract class BaseComponentSystem : ComponentSystem
	{

		public virtual void initialise()
		{
		}

		public virtual void preBegin()
		{
		}

		public virtual void postBegin()
		{
		}

		public virtual void preSave()
		{
		}

		public virtual void postSave()
		{
		}

		public virtual void shutdown()
		{
		}
	}

}