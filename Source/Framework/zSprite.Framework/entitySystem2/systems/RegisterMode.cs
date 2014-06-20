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

namespace org.terasology.entitySystem.systems
{

	using NetworkMode = org.terasology.network.NetworkMode;

	/// <summary>
	/// @author Immortius
	/// </summary>
	public sealed class RegisterMode
	{
		/// <summary>
		/// Always
		/// </summary>
		public static readonly RegisterMode ALWAYS = new RegisterMode("ALWAYS", InnerEnum.ALWAYS, true, true, true);
		/// <summary>
		/// Only if the application is acting as the authority (single player, listen or dedicated server)
		/// </summary>
		public static readonly RegisterMode AUTHORITY = new RegisterMode("AUTHORITY", InnerEnum.AUTHORITY, true, false, true);
		/// <summary>
		/// Only if the application is hosting a player (single player, remote client or listen server)
		/// </summary>
		public static readonly RegisterMode CLIENT = new RegisterMode("CLIENT", InnerEnum.CLIENT, true, true, false);
		/// <summary>
		/// Only if the application is a remote client.
		/// </summary>
		public static readonly RegisterMode REMOTE_CLIENT = new RegisterMode("REMOTE_CLIENT", InnerEnum.REMOTE_CLIENT, false, true, false);

		private static readonly IList<RegisterMode> valueList = new List<RegisterMode>();

		static RegisterMode()
		{
			valueList.Add(ALWAYS);
			valueList.Add(AUTHORITY);
			valueList.Add(CLIENT);
			valueList.Add(REMOTE_CLIENT);
		}

		public enum InnerEnum
		{
			ALWAYS,
			AUTHORITY,
			CLIENT,
			REMOTE_CLIENT
		}

		private readonly string nameValue;
		private readonly int ordinalValue;
		private readonly InnerEnum innerEnumValue;
		private static int nextOrdinal = 0;


		private bool validWhenAuthority;
		private bool validWhenRemote;
		private bool validWhenHeadless;

		private RegisterMode(string name, InnerEnum innerEnum, bool validWhenAuthority, bool validWhenRemote, bool validWhenHeadless)
		{
			this.validWhenAuthority = validWhenAuthority;
			this.validWhenRemote = validWhenRemote;
			this.validWhenHeadless = validWhenHeadless;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public boolean isValidFor(org.terasology.network.NetworkMode mode, bool headless)
		{
			return ((mode.Authority) ? validWhenAuthority : validWhenRemote) && (!headless || validWhenHeadless);
		}

		public static IList<RegisterMode> values()
		{
			return valueList;
		}

		public InnerEnum InnerEnumValue()
		{
			return innerEnumValue;
		}

		public int ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static RegisterMode valueOf(string name)
		{
			foreach (RegisterMode enumInstance in RegisterMode.values())
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}

}