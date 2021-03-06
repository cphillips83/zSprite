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
namespace org.terasology.input
{


	/// <summary>
	/// This annotation is used to declare categories of inputs to display in any input binding screens.
	/// @author Immortius
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false]
	public class InputCategory : System.Attribute
	{
		/// <returns> The id of the category, used within binds belonging to this category </returns>
		string id();

		/// <returns> The displayable name for this category </returns>
		string displayName();

		/// <returns> The ordering of binds within this category. Any binds not listed will appear in alphabetical order (by display name) at the end. </returns>
		string[] ordering() default
		{
		};
	}

}