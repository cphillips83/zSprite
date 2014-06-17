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

public class InputConfig
{

    private BindsConfig binds = new BindsConfig();
    private float mouseSensitivity = 0.075f;
    private bool mouseYAxisInverted;

    public BindsConfig getBinds()
    {
        return binds;
    }

    public float getMouseSensitivity()
    {
        return mouseSensitivity;
    }

    public void setMouseSensitivity(float mouseSensitivity)
    {
        this.mouseSensitivity = mouseSensitivity;
    }

    public void reset()
    {
        binds.setBinds(BindsConfig.createDefault());
        InputConfig defaultConfig = new InputConfig();
        setMouseSensitivity(defaultConfig.mouseSensitivity);
        setMouseYAxisInverted(defaultConfig.mouseYAxisInverted);
    }

    public bool isMouseYAxisInverted()
    {
        return mouseYAxisInverted;
    }

    public void setMouseYAxisInverted(bool mouseYAxisInverted)
    {

        this.mouseYAxisInverted = mouseYAxisInverted;

    }

}
