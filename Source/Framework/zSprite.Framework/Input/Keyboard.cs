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
using System.Collections.Generic;
using System.Linq;
#endregion Namespace Declarations

namespace zSprite
{
    public class Keyboard
    {

        #region Keys

        public static readonly Key NONE = new Key(KeyCodes.NONE, "KEY_NONE", "");
        public static readonly Key ESCAPE = new Key(KeyCodes.ESCAPE, "KEY_ESCAPE", "Escape");
        public static readonly Key KEY_1 = new Key(KeyCodes.KEY_1, "KEY_1", "1");
        public static readonly Key KEY_2 = new Key(KeyCodes.KEY_2, "KEY_2", "2");
        public static readonly Key KEY_3 = new Key(KeyCodes.KEY_3, "KEY_3", "3");
        public static readonly Key KEY_4 = new Key(KeyCodes.KEY_4, "KEY_4", "4");
        public static readonly Key KEY_5 = new Key(KeyCodes.KEY_5, "KEY_5", "5");
        public static readonly Key KEY_6 = new Key(KeyCodes.KEY_6, "KEY_6", "6");
        public static readonly Key KEY_7 = new Key(KeyCodes.KEY_7, "KEY_7", "7");
        public static readonly Key KEY_8 = new Key(KeyCodes.KEY_8, "KEY_8", "8");
        public static readonly Key KEY_9 = new Key(KeyCodes.KEY_9, "KEY_9", "9");
        public static readonly Key KEY_0 = new Key(KeyCodes.KEY_0, "KEY_0", "0");
        public static readonly Key MINUS = new Key(KeyCodes.MINUS, "KEY_MINUS", "-");
        public static readonly Key EQUALS = new Key(KeyCodes.EQUALS, "KEY_EQUALS", "=");
        public static readonly Key BACKSPACE = new Key(KeyCodes.BACKSPACE, "KEY_BACK", "Backspace");
        public static readonly Key TAB = new Key(KeyCodes.TAB, "KEY_TAB", "Tab");
        public static readonly Key Q = new Key(KeyCodes.Q, "KEY_Q", "Q");
        public static readonly Key W = new Key(KeyCodes.W, "KEY_W", "W");
        public static readonly Key E = new Key(KeyCodes.E, "KEY_E", "E");
        public static readonly Key R = new Key(KeyCodes.R, "KEY_R", "R");
        public static readonly Key T = new Key(KeyCodes.T, "KEY_T", "T");
        public static readonly Key Y = new Key(KeyCodes.Y, "KEY_Y", "Y");
        public static readonly Key U = new Key(KeyCodes.U, "KEY_U", "U");
        public static readonly Key I = new Key(KeyCodes.I, "KEY_I", "I");
        public static readonly Key O = new Key(KeyCodes.O, "KEY_O", "O");
        public static readonly Key P = new Key(KeyCodes.P, "KEY_P", "P");
        public static readonly Key LEFT_BRACKET = new Key(KeyCodes.LEFT_BRACKET, "KEY_LBRACKET", "[");
        public static readonly Key RIGHT_BRACKET = new Key(KeyCodes.RIGHT_BRACKET, "KEY_RBRACKET", "]");
        public static readonly Key ENTER = new Key(KeyCodes.ENTER, "KEY_RETURN", "Enter");
        public static readonly Key LEFT_CTRL = new Key(KeyCodes.LEFT_CTRL, "KEY_LCONTROL", "Left Ctrl");
        public static readonly Key A = new Key(KeyCodes.A, "KEY_A", "A");
        public static readonly Key S = new Key(KeyCodes.S, "KEY_S", "S");
        public static readonly Key D = new Key(KeyCodes.D, "KEY_D", "D");
        public static readonly Key F = new Key(KeyCodes.F, "KEY_F", "F");
        public static readonly Key G = new Key(KeyCodes.G, "KEY_G", "G");
        public static readonly Key H = new Key(KeyCodes.H, "KEY_H", "H");
        public static readonly Key J = new Key(KeyCodes.J, "KEY_J", "J");
        public static readonly Key K = new Key(KeyCodes.K, "KEY_K", "K");
        public static readonly Key L = new Key(KeyCodes.L, "KEY_L", "L");
        public static readonly Key SEMICOLON = new Key(KeyCodes.SEMICOLON, "KEY_SEMICOLON", ";");
        public static readonly Key APOSTROPHE = new Key(KeyCodes.APOSTROPHE, "KEY_APOSTROPHE", "'");
        public static readonly Key GRAVE = new Key(KeyCodes.GRAVE, "KEY_GRAVE", "Grave");
        public static readonly Key LEFT_SHIFT = new Key(KeyCodes.LEFT_SHIFT, "KEY_LSHIFT", "Left Shift");
        public static readonly Key BACKSLASH = new Key(KeyCodes.BACKSLASH, "KEY_BACKSLASH", "\\");
        public static readonly Key Z = new Key(KeyCodes.Z, "KEY_Z", "Z");
        public static readonly Key X = new Key(KeyCodes.X, "KEY_X", "X");
        public static readonly Key C = new Key(KeyCodes.C, "KEY_C", "C");
        public static readonly Key V = new Key(KeyCodes.V, "KEY_V", "V");
        public static readonly Key B = new Key(KeyCodes.B, "KEY_B", "B");
        public static readonly Key N = new Key(KeyCodes.N, "KEY_N", "N");
        public static readonly Key M = new Key(KeyCodes.M, "KEY_M", "M");
        public static readonly Key COMMA = new Key(KeyCodes.COMMA, "KEY_COMMA", ",");
        public static readonly Key PERIOD = new Key(KeyCodes.PERIOD, "KEY_PERIOD", ".");
        public static readonly Key SLASH = new Key(KeyCodes.SLASH, "KEY_SLASH", "/");
        public static readonly Key RIGHT_SHIFT = new Key(KeyCodes.RIGHT_SHIFT, "KEY_RSHIFT", "Right Shift");
        public static readonly Key NUMPAD_MULTIPLY = new Key(KeyCodes.NUMPAD_MULTIPLY, "KEY_MULTIPLY", "Numpad *");
        public static readonly Key LEFT_ALT = new Key(KeyCodes.LEFT_ALT, "KEY_LMENU", "Left Alt");
        public static readonly Key SPACE = new Key(KeyCodes.SPACE, "KEY_SPACE", "Space");
        public static readonly Key CAPS_LOCK = new Key(KeyCodes.CAPS_LOCK, "KEY_CAPITAL", "Caps Lock");
        public static readonly Key F1 = new Key(KeyCodes.F1, "KEY_F1", "F1");
        public static readonly Key F2 = new Key(KeyCodes.F2, "KEY_F2", "F2");
        public static readonly Key F3 = new Key(KeyCodes.F3, "KEY_F3", "F3");
        public static readonly Key F4 = new Key(KeyCodes.F4, "KEY_F4", "F4");
        public static readonly Key F5 = new Key(KeyCodes.F5, "KEY_F5", "F5");
        public static readonly Key F6 = new Key(KeyCodes.F6, "KEY_F6", "F6");
        public static readonly Key F7 = new Key(KeyCodes.F7, "KEY_F7", "F7");
        public static readonly Key F8 = new Key(KeyCodes.F8, "KEY_F8", "F8");
        public static readonly Key F9 = new Key(KeyCodes.F9, "KEY_F9", "F9");
        public static readonly Key F10 = new Key(KeyCodes.F10, "KEY_F10", "F10");
        public static readonly Key NUM_LOCK = new Key(KeyCodes.NUM_LOCK, "KEY_NUMLOCK", "Num Lock");
        public static readonly Key SCROLL_LOCK = new Key(KeyCodes.SCROLL_LOCK, "KEY_SCROLL", "Scroll Lock");
        public static readonly Key NUMPAD_7 = new Key(KeyCodes.NUMPAD_7, "KEY_NUMPAD7", "Numpad 7");
        public static readonly Key NUMPAD_8 = new Key(KeyCodes.NUMPAD_8, "KEY_NUMPAD8", "Numpad 8");
        public static readonly Key NUMPAD_9 = new Key(KeyCodes.NUMPAD_9, "KEY_NUMPAD9", "Numpad 9");
        public static readonly Key NUMPAD_MINUS = new Key(KeyCodes.NUMPAD_MINUS, "KEY_SUBTRACT", "Numpad -");
        public static readonly Key NUMPAD_4 = new Key(KeyCodes.NUMPAD_4, "KEY_NUMPAD4", "Numpad 4");
        public static readonly Key NUMPAD_5 = new Key(KeyCodes.NUMPAD_5, "KEY_NUMPAD5", "Numpad 5");
        public static readonly Key NUMPAD_6 = new Key(KeyCodes.NUMPAD_6, "KEY_NUMPAD6", "Numpad 6");
        public static readonly Key NUMPAD_PLUS = new Key(KeyCodes.NUMPAD_PLUS, "KEY_ADD", "Numpad +");
        public static readonly Key NUMPAD_1 = new Key(KeyCodes.NUMPAD_1, "KEY_NUMPAD1", "Numpad 1");
        public static readonly Key NUMPAD_2 = new Key(KeyCodes.NUMPAD_2, "KEY_NUMPAD2", "Numpad 2");
        public static readonly Key NUMPAD_3 = new Key(KeyCodes.NUMPAD_3, "KEY_NUMPAD3", "Numpad 3");
        public static readonly Key NUMPAD_0 = new Key(KeyCodes.NUMPAD_0, "KEY_NUMPAD0", "Numpad 0");
        public static readonly Key NUMPAD_PERIOD = new Key(KeyCodes.NUMPAD_PERIOD, "KEY_DECIMAL", "Numpad .");
        public static readonly Key F11 = new Key(KeyCodes.F11, "KEY_F11", "F11");
        public static readonly Key F12 = new Key(KeyCodes.F12, "KEY_F12", "F12");
        public static readonly Key F13 = new Key(KeyCodes.F13, "KEY_F13", "F13");
        public static readonly Key F14 = new Key(KeyCodes.F14, "KEY_F14", "F14");
        public static readonly Key F15 = new Key(KeyCodes.F15, "KEY_F15", "F15");
        public static readonly Key F16 = new Key(KeyCodes.F16, "KEY_F16", "F16");
        public static readonly Key F17 = new Key(KeyCodes.F17, "KEY_F17", "F17");
        public static readonly Key F18 = new Key(KeyCodes.F18, "KEY_F18", "F18");
        public static readonly Key KANA = new Key(KeyCodes.KANA, "KEY_KANA", "Kana"); // Japanese Keyboard key = new Key(for switching between roman and japanese characters?
        public static readonly Key F19 = new Key(KeyCodes.F19, "KEY_F19", "F19");
        public static readonly Key CONVERT = new Key(KeyCodes.CONVERT, "KEY_CONVERT", "Convert"); // Japanese Keyboard key = new Key(for converting Hiragana characters to Kanji?)
        public static readonly Key NOCONVERT = new Key(KeyCodes.NOCONVERT, "KEY_NOCONVERT", "No Convert"); // Japanese Keyboard key
        public static readonly Key YEN = new Key(KeyCodes.YEN, "KEY_YEN", "¥"); // Japanese keyboard key for yen
        public static readonly Key NUMPAD_EQUALS = new Key(KeyCodes.NUMPAD_EQUALS, "KEY_NUMPADEQUALS", "Numpad =");
        public static readonly Key CIRCUMFLEX = new Key(KeyCodes.CIRCUMFLEX, "KEY_CIRCUMFLEX", "^"); // Japanese keyboard
        public static readonly Key AT = new Key(KeyCodes.AT, "KEY_AT", "@"); // = new Key(NEC PC98)
        public static readonly Key COLON = new Key(KeyCodes.COLON, "KEY_COLON", ":"); // = new Key(NEC PC98)
        public static readonly Key UNDERLINE = new Key(KeyCodes.UNDERLINE, "KEY_UNDERLINE", "_"); // = new Key(NEC PC98)
        public static readonly Key KANJI = new Key(KeyCodes.KANJI, "KEY_KANJI", "Kanji"); // = new Key(Japanese keyboard)
        public static readonly Key STOP = new Key(KeyCodes.STOP, "KEY_STOP", "Stop"); // = new Key(NEC PC98)
        public static readonly Key AX = new Key(KeyCodes.AX, "KEY_AX", "AX"); // = new Key(Japan AX)
        public static readonly Key UNLABELED = new Key(KeyCodes.UNLABELED, "KEY_UNLABELED", "Unlabelled"); // = new Key(J3100) = new Key(a mystery button?)
        public static readonly Key NUMPAD_ENTER = new Key(KeyCodes.NUMPAD_ENTER, "KEY_NUMPADENTER", "Numpad Enter");
        public static readonly Key RIGHT_CTRL = new Key(KeyCodes.RIGHT_CTRL, "KEY_RCONTROL", "Right Ctrl");
        public static readonly Key SECTION = new Key(KeyCodes.SECTION, "KEY_SECTION", "§");
        public static readonly Key NUMPAD_COMMA = new Key(KeyCodes.NUMPAD_COMMA, "KEY_NUMPADCOMMA", "Numpad ,"); // = new Key(NEC PC98)
        public static readonly Key NUMPAD_DIVIDE = new Key(KeyCodes.NUMPAD_DIVIDE, "KEY_DIVIDE", "Numpad /");
        public static readonly Key PRINT_SCREEN = new Key(KeyCodes.PRINT_SCREEN, "KEY_SYSRQ", "Print Screen");
        public static readonly Key RIGHT_ALT = new Key(KeyCodes.RIGHT_ALT, "KEY_RMENU", "Right Alt");
        public static readonly Key FUNCTION = new Key(KeyCodes.FUNCTION, "KEY_FUNCTION", "Function");
        public static readonly Key PAUSE = new Key(KeyCodes.PAUSE, "KEY_PAUSE", "Pause");
        public static readonly Key HOME = new Key(KeyCodes.HOME, "KEY_HOME", "Home");
        public static readonly Key UP = new Key(KeyCodes.UP, "KEY_UP", "Up");
        public static readonly Key PAGE_UP = new Key(KeyCodes.PAGE_UP, "KEY_PRIOR", "Page Up");
        public static readonly Key LEFT = new Key(KeyCodes.LEFT, "KEY_LEFT", "Left");
        public static readonly Key RIGHT = new Key(KeyCodes.RIGHT, "KEY_RIGHT", "Right");
        public static readonly Key END = new Key(KeyCodes.END, "KEY_END", "End");
        public static readonly Key DOWN = new Key(KeyCodes.DOWN, "KEY_DOWN", "Down");
        public static readonly Key PAGE_DOWN = new Key(KeyCodes.PAGE_DOWN, "KEY_NEXT", "Page Down");
        public static readonly Key INSERT = new Key(KeyCodes.INSERT, "KEY_INSERT", "Insert");
        public static readonly Key DELETE = new Key(KeyCodes.DELETE, "KEY_DELETE", "Delete");
        public static readonly Key CLEAR = new Key(KeyCodes.CLEAR, "KEY_CLEAR", "Clear"); // = new Key(Mac)
        public static readonly Key LEFT_META = new Key(KeyCodes.LEFT_META, "KEY_LMETA", "Left Meta"); // Left Windows/Option key
        public static readonly Key RIGHT_META = new Key(KeyCodes.RIGHT_META, "KEY_RMETA", "Right Meta"); // Right Windows/Option key
        public static readonly Key APPS = new Key(KeyCodes.APPS, "KEY_APPS", "Apps");
        public static readonly Key POWER = new Key(KeyCodes.POWER, "KEY_POWER", "Power");
        public static readonly Key SLEEP = new Key(KeyCodes.SLEEP, "KEY_SLEEP", "Sleep");

        #endregion Keys

        #region Members

        private static Dictionary<string, Input> lookupByName;
        private static Dictionary<int, Input> lookupById;

        #endregion Members

        #region Constructor

        static Keyboard()
        {
            var _values = values().ToArray();

            lookupByName = new Dictionary<string, Input>(_values.Length);
            lookupById = new Dictionary<int, Input>(_values.Length);
            foreach (var key in _values)
            {
                lookupByName.Add(key.getName(), key);
                lookupById.Add(key.getId(), key);
            }
        }

        #endregion Constructor

        #region Methods

        public static IEnumerable<Key> values()
        {
            yield return NONE;
            yield return ESCAPE;
            yield return KEY_1;
            yield return KEY_2;
            yield return KEY_3;
            yield return KEY_4;
            yield return KEY_5;
            yield return KEY_6;
            yield return KEY_7;
            yield return KEY_8;
            yield return KEY_9;
            yield return KEY_0;
            yield return MINUS;
            yield return EQUALS;
            yield return BACKSPACE;
            yield return TAB;
            yield return Q;
            yield return W;
            yield return E;
            yield return R;
            yield return T;
            yield return Y;
            yield return U;
            yield return I;
            yield return O;
            yield return P;
            yield return LEFT_BRACKET;
            yield return RIGHT_BRACKET;
            yield return ENTER;
            yield return LEFT_CTRL;
            yield return A;
            yield return S;
            yield return D;
            yield return F;
            yield return G;
            yield return H;
            yield return J;
            yield return K;
            yield return L;
            yield return SEMICOLON;
            yield return APOSTROPHE;
            yield return GRAVE;
            yield return LEFT_SHIFT;
            yield return BACKSLASH;
            yield return Z;
            yield return X;
            yield return C;
            yield return V;
            yield return B;
            yield return N;
            yield return M;
            yield return COMMA;
            yield return PERIOD;
            yield return SLASH;
            yield return RIGHT_SHIFT;
            yield return NUMPAD_MULTIPLY;
            yield return LEFT_ALT;
            yield return SPACE;
            yield return CAPS_LOCK;
            yield return F1;
            yield return F2;
            yield return F3;
            yield return F4;
            yield return F5;
            yield return F6;
            yield return F7;
            yield return F8;
            yield return F9;
            yield return F10;
            yield return NUM_LOCK;
            yield return SCROLL_LOCK;
            yield return NUMPAD_7;
            yield return NUMPAD_8;
            yield return NUMPAD_9;
            yield return NUMPAD_MINUS;
            yield return NUMPAD_4;
            yield return NUMPAD_5;
            yield return NUMPAD_6;
            yield return NUMPAD_PLUS;
            yield return NUMPAD_1;
            yield return NUMPAD_2;
            yield return NUMPAD_3;
            yield return NUMPAD_0;
            yield return NUMPAD_PERIOD;
            yield return F11;
            yield return F12;
            yield return F13;
            yield return F14;
            yield return F15;
            yield return F16;
            yield return F17;
            yield return F18;
            yield return KANA;
            yield return F19;
            yield return CONVERT;
            yield return NOCONVERT;
            yield return YEN;
            yield return NUMPAD_EQUALS;
            yield return CIRCUMFLEX;
            yield return AT;
            yield return COLON;
            yield return UNDERLINE;
            yield return KANJI;
            yield return STOP;
            yield return AX;
            yield return UNLABELED;
            yield return NUMPAD_ENTER;
            yield return RIGHT_CTRL;
            yield return SECTION;
            yield return NUMPAD_COMMA;
            yield return NUMPAD_DIVIDE;
            yield return PRINT_SCREEN;
            yield return RIGHT_ALT;
            yield return FUNCTION;
            yield return PAUSE;
            yield return HOME;
            yield return UP;
            yield return PAGE_UP;
            yield return LEFT;
            yield return RIGHT;
            yield return END;
            yield return DOWN;
            yield return PAGE_DOWN;
            yield return INSERT;
            yield return DELETE;
            yield return CLEAR;
            yield return LEFT_META;
            yield return RIGHT_META;
            yield return APPS;
            yield return POWER;
            yield return SLEEP;
        }

        public static Input find(string name)
        {
            Input input;
            if (lookupByName.TryGetValue(name.ToUpperInvariant(), out input))
                return input;

            return null;
        }

        public static Input find(int id)
        {
            Input input;
            if (!lookupById.TryGetValue(id, out input))
            {
                input = new UnknownInput(InputType.KEY, id);
                lookupById.Add(id, input);
                lookupByName.Add(input.getName(), input);
            }
            return input;
        }

        //public static bool isKeyDown(int key)
        //{
        //    //return CoreRegistry.get(InputSystem.class).getKeyboard().isKeyDown(key);
        //    return false;
        //}

        #endregion Methods
    }
}
