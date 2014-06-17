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
    public class PropertyChangeSupport
    {
        private object _sender;

        public event Action<object, string, bool, bool> _boolevent;
        public event Action<object, string, int, int> _intevent;
        public event Action<object, string, string, string> _stringevent;
        public event Action<object, string, float, float> _floatevent;
        public event Action<object, string, object, object> _objectevent;

        public PropertyChangeSupport(object sender)
        {
            this._sender = sender;
        }

        public void firePropertyChange(string propertyName, bool oldValue, bool newValue)
        {
            if (_boolevent != null)
                _boolevent(_sender, propertyName, oldValue, newValue);
        }

        public void firePropertyChange(string propertyName, int oldValue, int newValue)
        {
            if (_intevent != null)
                _intevent(_sender, propertyName, oldValue, newValue);
        }

        public void firePropertyChange(string propertyName, string oldValue, string newValue)
        {
            if (_stringevent != null)
                _stringevent(_sender, propertyName, oldValue, newValue);
        }

        public void firePropertyChange(string propertyName, float oldValue, float newValue)
        {
            if (_floatevent != null)
                _floatevent(_sender, propertyName, oldValue, newValue);
        }

        public void firePropertyChange(string propertyName, object oldValue, object newValue)
        {
            if (_objectevent != null)
                _objectevent(_sender, propertyName, oldValue, newValue);
        }
    }
}
