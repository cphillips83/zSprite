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

    //public delegate void PropertyChangeSupport(object sender, string property, 
    public class AudioConfig
    {
        public static readonly String SOUND_VOLUME = "soundVolume";
        public static readonly String MUSIC_VOLUME = "musicVolume";

        private float soundVolume = 1.0f;
        private float musicVolume = 0.1f;
        private bool disableSound;

        //transient
        private PropertyChangeSupport propertyChangeSupport;

        public AudioConfig()
        {
            propertyChangeSupport = new PropertyChangeSupport(this);
        }

        public float getSoundVolume()
        {
            return soundVolume;
        }

        public void setSoundVolume(float soundVolume)
        {
            float oldValue = this.soundVolume;
            this.soundVolume = soundVolume;
            propertyChangeSupport.firePropertyChange(SOUND_VOLUME, oldValue, soundVolume);
        }

        public float getMusicVolume()
        {
            return musicVolume;
        }

        public void setMusicVolume(float musicVolume)
        {
            float oldValue = this.musicVolume;
            this.musicVolume = musicVolume;
            propertyChangeSupport.firePropertyChange(MUSIC_VOLUME, oldValue, musicVolume);
        }

        public bool isDisableSound()
        {
            return disableSound;
        }

        public void setDisableSound(bool disableSound)
        {
            this.disableSound = disableSound;
        }

        public void subscribe(Action<object, string, bool, bool> changeListener)
        {
            this.propertyChangeSupport._boolevent += changeListener;
        }

        public void unsubscribe(Action<object, string, bool, bool> changeListener)
        {
            this.propertyChangeSupport._boolevent -= changeListener;
        }

        public void subscribe(Action<object, string, string, string> changeListener)
        {
            this.propertyChangeSupport._stringevent += changeListener;
        }

        public void unsubscribe(Action<object, string, string, string> changeListener)
        {
            this.propertyChangeSupport._stringevent -= changeListener;
        }

        public void subscribe(Action<object, string, float, float> changeListener)
        {
            this.propertyChangeSupport._floatevent += changeListener;
        }

        public void unsubscribe(Action<object, string, float, float> changeListener)
        {
            this.propertyChangeSupport._floatevent -= changeListener;
        }

        public void subscribe(Action<object, string, int, int> changeListener)
        {
            this.propertyChangeSupport._intevent += changeListener;
        }

        public void unsubscribe(Action<object, string, int, int> changeListener)
        {
            this.propertyChangeSupport._intevent -= changeListener;
        }

        public void subscribe(Action<object, string, object, object> changeListener)
        {
            this.propertyChangeSupport._objectevent += changeListener;
        }

        public void unsubscribe(Action<object, string, object, object> changeListener)
        {
            this.propertyChangeSupport._objectevent -= changeListener;
        }
    }

}