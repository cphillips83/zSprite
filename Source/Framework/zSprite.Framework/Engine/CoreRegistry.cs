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
using System;
using System.Collections.Generic;
#endregion Namespace Declarations

namespace zSprite
{
    /// <summary>
    /// Registry giving access to major singleton systems, via the interface they fulfil.
    /// </summary>
    public static class CoreRegistry
    {
        #region Members
        private static Dictionary<Type, object> _store = new Dictionary<Type, object>();
        private static HashSet<object> _permStore = new HashSet<object>();
        #endregion Members

        #region Methods
        /// <summary>
        /// Registers an object. These objects will be removed when CoreRegistry.clear() is called (typically when game state changes)
        /// </summary>
        /// <typeparam name="T">The interface which the system fulfils</typeparam>
        /// <typeparam name="U">The system itself</typeparam>
        /// <param name="obj">The system itself</param>
        /// <returns>The system itself</returns>
        public static U put<T, U>(U obj)
            where U : class, T
        {
            var type = typeof(T);
            _store.Add(type, obj);
            return obj;
        }

        /// <summary>
        /// Registers an object. These objects are not removed when CoreRegistry.clear() is called.
        /// </summary>
        /// <typeparam name="T">The interface which the system fulfils</typeparam>
        /// <typeparam name="U">The system itself</typeparam>
        /// <param name="obj">The system itself</param>
        /// <returns>The system itself</returns>
        public static U putPermanently<T, U>(U obj)
            where U : class, T
        {
            var type = typeof(T);
            _store.Add(type, obj);
            _permStore.Add(type);
            return obj;
        }

        /// <summary>
        /// Looks up the system fulfilling the given interface
        /// </summary>
        /// <typeparam name="T">The interface for the system</typeparam>
        /// <returns>The system fulfilling the given interface</returns>
        public static T get<T>()
            where T : class
        {
            object obj;
            if (_store.TryGetValue(typeof(T), out obj))
            {
                T t = obj as T;
                if (t == null)
                {
                    //invalid cast, call logger
                }

                return t;
            }

            return null;
        }

        /// <summary>
        /// Clears all non-permanent objects from the registry.
        /// </summary>
        public static void clear()
        {
            var objsToClear = new List<Type>();
            foreach (var key in _store.Keys)
                if (!_permStore.Contains(key))
                    objsToClear.Add(key);

            foreach (var key in objsToClear)
                _store.Remove(key);
        }

        /// <summary>
        /// Removes the system fulfilling the given interface
        /// </summary>
        /// <typeparam name="T">The interface for the system</typeparam>
        public static void remove<T>()
            where T : class
        {
            var type = typeof(T);
            _store.Remove(type);
        }
        #endregion Methods
    }
}
