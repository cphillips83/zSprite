﻿/*
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

namespace org.terasology.entitySystem
{


	/// <summary>
	/// When used in a Component on a EntityRef, List&lt;EntityRef> or Set&lt;EntityRef> field, denotes that the Entity
	/// will assume ownership of the entity or entities contained in that field.
	/// <p/>
	/// This means:
	/// <ul>
	/// <li>The owned entity will be persisted and restored along with its owner.</li>
	/// </ul>
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false]
	public class Owns : System.Attribute
	{
	}

}