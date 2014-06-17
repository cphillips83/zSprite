#region GPLv3 License

/*
zSprite
Copyright © 2014 zSprite Project Team

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License V3
as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
General Public License V3 for more details.

You should have received a copy of the GNU General Public License V3
along with this library; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
*/

#endregion

#region Namespace Declarations



#endregion Namespace Declarations

namespace zSprite
{
    public abstract class AbstractBaseUri : Uri
    {
        /// <summary>
        /// The character(s) use to separate the module name from other parts of the Uri
        /// </summary>    
        public static readonly char MODULE_SEPARATOR = ':';

        #region Properties

        public string moduleName { get; protected set; }

        public string normalisedModuleName { get; protected set; }

        #endregion Properties

        #region Methods
        public int CompareTo(Uri other)
        {
            return string.Compare(toNormalisedString(), other.toNormalisedString());
        }

        public bool Equals(Uri other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return toNormalisedString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Uri)
                return Equals((Uri)obj);

            return false;
        }

        public abstract string toNormalisedString();

        public abstract bool isValid();

        #endregion Methods
    }
}
