#region GPLv3 License

/*
Atma
Copyright © 2014 Atma Project Team

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

using System;
using Atma.Utilities;

#endregion Namespace Declarations

namespace Atma.Core
{
    #region Uri
    /// <summary>
    /// Uris are used to identify resources, like assets and systems introduced by mods. Uris can then be serialized/deserialized to and from Strings.
    /// Uris are case-insensitive. They have a normalised form which is lower-case (using English casing).
    /// Uris are immutable.
    /// 
    /// All uris include a module name as part of their structure.
    /// </summary>
    public interface IUri : IComparable<IUri>, IEquatable<IUri>
    {

        /// <summary>
        /// The name of the module the resource in question resides in.
        /// </summary>
        string moduleName { get; }

        /// <summary>
        /// The normalised form of the module name. Generally this means lower case.
        /// </summary>
        string normalisedModuleName { get; }

        /// <returns>The normalised form of the uri. Generally this means lower case.</returns>
        string toNormalisedString();

        /// <summary>
        /// </summary>
        /// <returns>Whether this uri represents a valid, well formed uri.</returns>
        bool isValid();

    }
    #endregion Uri

    #region GameUri
    public struct GameUri : IUri
    {
        /// <summary>
        /// The character(s) use to separate the module name from other parts of the Uri
        /// </summary>    
        public static readonly char MODULE_SEPARATOR = ':';

        #region Constructors
        /// <summary>
        /// Creates a SimpleUri from a string in the format "module:object". If the string does not match this format, it will be marked invalid
        /// </summary>
        /// <param name="simpleUri">module:object string</param>
        public GameUri(string simpleUri)
            : this()
        {
            string[] split = simpleUri.Split(MODULE_SEPARATOR);
            if (split.Length == 2)
            {
                moduleName = split[0];
                normalisedModuleName = UriUtil.normalise(split[0]);
                objectName = split[1];
                normalisedObjectName = UriUtil.normalise(split[1]);
            }
        }

        /// <summary>
        /// Creates a SimpleUri for the given module:object combo
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="objectName"></param>
        public GameUri(string _moduleName, string _objectName)
            : this()
        {
            Contract.RequiresNotEmpty(moduleName, "moduleName");
            Contract.RequiresNotEmpty(objectName, "objectName");
            moduleName = _moduleName;
            objectName = _objectName;
            normalisedModuleName = UriUtil.normalise(_moduleName);
            normalisedObjectName = UriUtil.normalise(_objectName);
        }
        #endregion Constructors

        #region Properties

        public string moduleName { get; private set; }

        public string normalisedModuleName { get; private set; }

        public string objectName { get; private set; }

        public string normalisedObjectName { get; private set; }

        #endregion Properties

        #region Methods

        public bool isValid()
        {
            return !string.IsNullOrEmpty(normalisedModuleName) && !string.IsNullOrEmpty(normalisedObjectName);
        }

        public string toNormalisedString()
        {
            if (!isValid())
            {
                return string.Empty;
            }
            return normalisedModuleName + MODULE_SEPARATOR + normalisedObjectName;
        }

        public override string ToString()
        {
            if (!isValid())
            {
                return string.Empty;
            }
            return moduleName + MODULE_SEPARATOR + objectName;

        }

        public int CompareTo(IUri other)
        {
            return string.Compare(toNormalisedString(), other.toNormalisedString());
        }

        public bool Equals(IUri other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return toNormalisedString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is IUri)
                return GetHashCode() == ((IUri)obj).GetHashCode();

            return false;
        }

        #endregion Methods

        #region Operators

        public static implicit operator string(GameUri uri)
        {
            return uri.toNormalisedString();
        }

        public static implicit operator GameUri(string val)
        {
            return new GameUri(val);
        }

        #endregion
    }
    #endregion GameUri
}
