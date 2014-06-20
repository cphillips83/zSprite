﻿using System;

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


	/// <summary>
	/// Primarily for internal use, is informed of all component lifecycle events for all components.
	/// 
	/// @author Immortius
	/// </summary>
	public interface EntityChangeSubscriber
	{

		void onEntityComponentAdded(EntityRef entity, Type component);

		void onEntityComponentChange(EntityRef entity, Type component);

		void onEntityComponentRemoved(EntityRef entity, Type component);


	}

}