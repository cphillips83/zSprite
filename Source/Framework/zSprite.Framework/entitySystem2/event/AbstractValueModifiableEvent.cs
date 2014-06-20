using System;

/*
 * Copyright 2014 MovingBlocks
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
namespace org.terasology.entitySystem.@event
{

	using TFloatIterator = gnu.trove.iterator.TFloatIterator;
	using TFloatList = gnu.trove.list.TFloatList;
	using TFloatArrayList = gnu.trove.list.array.TFloatArrayList;

	/// <summary>
	/// @author Marcin Sciesinski <marcins78@gmail.com>
	/// </summary>
	public abstract class AbstractValueModifiableEvent : Event
	{
		private float baseValue;

		private TFloatList modifiers = new TFloatArrayList();
		private TFloatList multipliers = new TFloatArrayList();
		private TFloatList postModifiers = new TFloatArrayList();

		protected internal AbstractValueModifiableEvent(float baseValue)
		{
			this.baseValue = baseValue;
		}

		public virtual float BaseValue
		{
			get
			{
				return baseValue;
			}
		}

		public virtual void multiply(float amount)
		{
			this.multipliers.add(amount);
		}

		public virtual void add(float amount)
		{
			modifiers.add(amount);
		}

		public virtual void addPostMultiply(float amount)
		{
			postModifiers.add(amount);
		}

		public virtual float ResultValue
		{
			get
			{
				// For now, add all modifiers and multiply by all multipliers. Negative modifiers cap to zero, but negative
				// multipliers remain.
    
				float result = baseValue;
				TFloatIterator modifierIter = modifiers.GetEnumerator();
				while (modifierIter.hasNext())
				{
					result += modifierIter.next();
				}
				result = Math.Max(0, result);
    
				TFloatIterator multiplierIter = multipliers.GetEnumerator();
				while (multiplierIter.hasNext())
				{
					result *= multiplierIter.next();
				}
    
	//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
	//ORIGINAL LINE: final gnu.trove.iterator.TFloatIterator postModifierIter = postModifiers.iterator();
				TFloatIterator postModifierIter = postModifiers.GetEnumerator();
				while (postModifierIter.hasNext())
				{
					result += postModifierIter.next();
				}
				return result;
			}
		}
	}

}