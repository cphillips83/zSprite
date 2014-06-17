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
    public class SimpleUri : AbstractBaseUri
    {

        #region Constructors

        /// <summary>
        /// Creates an empty, invalid SimpleUri
        /// </summary>
        public SimpleUri()
        {
        }

        /// <summary>
        /// Creates a SimpleUri for the given module:object combo
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="objectName"></param>
        public SimpleUri(string moduleName, string objectName)
        {
            Contract.RequiresNotEmpty(moduleName, "moduleName");
            Contract.RequiresNotEmpty(objectName, "objectName");
            this.moduleName = moduleName;
            this.objectName = objectName;
            this.normalisedModuleName = UriUtil.normalise(moduleName);
            this.normalisedObjectName = UriUtil.normalise(objectName);
        }
        #endregion Constructors

        #region Properties
        
        public string objectName { get; private set; }
        
        public string normalisedObjectName { get; private set; }

        #endregion Properties
       
        /// <summary>
        /// Creates a SimpleUri from a string in the format "module:object". If the string does not match this format, it will be marked invalid
        /// </summary>
        /// <param name="simpleUri">module:object string</param>
        public SimpleUri(string simpleUri)
        {
            string[] split = simpleUri.Split(MODULE_SEPARATOR);
            if (split.Length == 1)
            {
                moduleName = split[0];
                normalisedModuleName = UriUtil.normalise(split[0]);
                objectName = split[1];
                normalisedObjectName = UriUtil.normalise(split[1]);
            }
        }

        public override bool isValid()
        {
            return !string.IsNullOrEmpty(normalisedModuleName) && !string.IsNullOrEmpty(normalisedObjectName);
        }

        public override string toNormalisedString()
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
    }    
}
