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
    public struct Keyboard
    {
        #region Methods

        //public static bool isKeyDown(int key)
        //{
        //    //return CoreRegistry.get(InputSystem.class).getKeyboard().isKeyDown(key);
        //    return false;
        //}

        #endregion Methods

        #region KeyId

        public static class KeyId
        {
            public static readonly int NONE = 0x00;
            public static readonly int ESCAPE = 0x01;
            public static readonly int KEY_1 = 0x02;
            public static readonly int KEY_2 = 0x03;
            public static readonly int KEY_3 = 0x04;
            public static readonly int KEY_4 = 0x05;
            public static readonly int KEY_5 = 0x06;
            public static readonly int KEY_6 = 0x07;
            public static readonly int KEY_7 = 0x08;
            public static readonly int KEY_8 = 0x09;
            public static readonly int KEY_9 = 0x0A;
            public static readonly int KEY_0 = 0x0B;
            public static readonly int MINUS = 0x0C;
            public static readonly int EQUALS = 0x0D;
            public static readonly int BACKSPACE = 0x0E;
            public static readonly int TAB = 0x0F;
            public static readonly int Q = 0x10;
            public static readonly int W = 0x11;
            public static readonly int E = 0x12;
            public static readonly int R = 0x13;
            public static readonly int T = 0x14;
            public static readonly int Y = 0x15;
            public static readonly int U = 0x16;
            public static readonly int I = 0x17;
            public static readonly int O = 0x18;
            public static readonly int P = 0x19;
            public static readonly int LEFT_BRACKET = 0x1A;
            public static readonly int RIGHT_BRACKET = 0x1B;
            public static readonly int ENTER = 0x1C;
            public static readonly int LEFT_CTRL = 0x1D;
            public static readonly int A = 0x1E;
            public static readonly int S = 0x1F;
            public static readonly int D = 0x20;
            public static readonly int F = 0x21;
            public static readonly int G = 0x22;
            public static readonly int H = 0x23;
            public static readonly int J = 0x24;
            public static readonly int K = 0x25;
            public static readonly int L = 0x26;
            public static readonly int SEMICOLON = 0x27;
            public static readonly int APOSTROPHE = 0x28;
            public static readonly int GRAVE = 0x29;
            public static readonly int LEFT_SHIFT = 0x2A;
            public static readonly int BACKSLASH = 0x2B;
            public static readonly int Z = 0x2C;
            public static readonly int X = 0x2D;
            public static readonly int C = 0x2E;
            public static readonly int V = 0x2F;
            public static readonly int B = 0x30;
            public static readonly int N = 0x31;
            public static readonly int M = 0x32;
            public static readonly int COMMA = 0x33;
            public static readonly int PERIOD = 0x34;
            public static readonly int SLASH = 0x35;
            public static readonly int RIGHT_SHIFT = 0x36;
            public static readonly int NUMPAD_MULTIPLY = 0x37;
            public static readonly int LEFT_ALT = 0x38;
            public static readonly int SPACE = 0x39;
            public static readonly int CAPS_LOCK = 0x3A;
            public static readonly int F1 = 0x3B;
            public static readonly int F2 = 0x3C;
            public static readonly int F3 = 0x3D;
            public static readonly int F4 = 0x3E;
            public static readonly int F5 = 0x3F;
            public static readonly int F6 = 0x40;
            public static readonly int F7 = 0x41;
            public static readonly int F8 = 0x42;
            public static readonly int F9 = 0x43;
            public static readonly int F10 = 0x44;
            public static readonly int NUM_LOCK = 0x45;
            public static readonly int SCROLL_LOCK = 0x46;
            public static readonly int NUMPAD_7 = 0x47;
            public static readonly int NUMPAD_8 = 0x48;
            public static readonly int NUMPAD_9 = 0x49;
            public static readonly int NUMPAD_MINUS = 0x4A;
            public static readonly int NUMPAD_4 = 0x4B;
            public static readonly int NUMPAD_5 = 0x4C;
            public static readonly int NUMPAD_6 = 0x4D;
            public static readonly int NUMPAD_PLUS = 0x4E;
            public static readonly int NUMPAD_1 = 0x4F;
            public static readonly int NUMPAD_2 = 0x50;
            public static readonly int NUMPAD_3 = 0x51;
            public static readonly int NUMPAD_0 = 0x52;
            public static readonly int NUMPAD_PERIOD = 0x53;
            public static readonly int F11 = 0x57;
            public static readonly int F12 = 0x58;
            public static readonly int F13 = 0x64;
            public static readonly int F14 = 0x65;
            public static readonly int F15 = 0x66;
            public static readonly int F16 = 0x67;
            public static readonly int F17 = 0x68;
            public static readonly int F18 = 0x69;
            public static readonly int KANA = 0x70;
            public static readonly int F19 = 0x71;
            public static readonly int CONVERT = 0x79;
            public static readonly int NOCONVERT = 0x7B;
            public static readonly int YEN = 0x7D;
            public static readonly int NUMPAD_EQUALS = 0x8D;
            public static readonly int CIRCUMFLEX = 0x90;
            public static readonly int AT = 0x91;
            public static readonly int COLON = 0x92;
            public static readonly int UNDERLINE = 0x93;
            public static readonly int KANJI = 0x94;
            public static readonly int STOP = 0x95;
            public static readonly int AX = 0x96;
            public static readonly int UNLABELED = 0x97;
            public static readonly int NUMPAD_ENTER = 0x9C;
            public static readonly int RIGHT_CTRL = 0x9D;
            public static readonly int SECTION = 0xA7;
            public static readonly int NUMPAD_COMMA = 0xB3;
            public static readonly int NUMPAD_DIVIDE = 0xB5;
            public static readonly int PRINT_SCREEN = 0xB7;
            public static readonly int RIGHT_ALT = 0xB8;
            public static readonly int FUNCTION = 0xC4;
            public static readonly int PAUSE = 0xC5;
            public static readonly int HOME = 0xC7;
            public static readonly int UP = 0xC8;
            public static readonly int PAGE_UP = 0xC9;
            public static readonly int LEFT = 0xCB;
            public static readonly int RIGHT = 0xCD;
            public static readonly int END = 0xCF;
            public static readonly int DOWN = 0xD0;
            public static readonly int PAGE_DOWN = 0xD1;
            public static readonly int INSERT = 0xD2;
            public static readonly int DELETE = 0xD3;
            public static readonly int CLEAR = 0xDA;
            public static readonly int LEFT_META = 0xDB;
            public static readonly int RIGHT_META = 0xDC;
            public static readonly int APPS = 0xDD;
            public static readonly int POWER = 0xDE;
            public static readonly int SLEEP = 0xDF;
        }

        #endregion KeyId

        #region Key

        public struct Key : Input
        {
            #region Keys

            public static readonly Key NONE = new Key(KeyId.NONE, "KEY_NONE", "");
            public static readonly Key ESCAPE = new Key(KeyId.ESCAPE, "KEY_ESCAPE", "Escape");
            public static readonly Key KEY_1 = new Key(KeyId.KEY_1, "KEY_1", "1");
            public static readonly Key KEY_2 = new Key(KeyId.KEY_2, "KEY_2", "2");
            public static readonly Key KEY_3 = new Key(KeyId.KEY_3, "KEY_3", "3");
            public static readonly Key KEY_4 = new Key(KeyId.KEY_4, "KEY_4", "4");
            public static readonly Key KEY_5 = new Key(KeyId.KEY_5, "KEY_5", "5");
            public static readonly Key KEY_6 = new Key(KeyId.KEY_6, "KEY_6", "6");
            public static readonly Key KEY_7 = new Key(KeyId.KEY_7, "KEY_7", "7");
            public static readonly Key KEY_8 = new Key(KeyId.KEY_8, "KEY_8", "8");
            public static readonly Key KEY_9 = new Key(KeyId.KEY_9, "KEY_9", "9");
            public static readonly Key KEY_0 = new Key(KeyId.KEY_0, "KEY_0", "0");
            public static readonly Key MINUS = new Key(KeyId.MINUS, "KEY_MINUS", "-");
            public static readonly Key EQUALS = new Key(KeyId.EQUALS, "KEY_EQUALS", "=");
            public static readonly Key BACKSPACE = new Key(KeyId.BACKSPACE, "KEY_BACK", "Backspace");
            public static readonly Key TAB = new Key(KeyId.TAB, "KEY_TAB", "Tab");
            public static readonly Key Q = new Key(KeyId.Q, "KEY_Q", "Q");
            public static readonly Key W = new Key(KeyId.W, "KEY_W", "W");
            public static readonly Key E = new Key(KeyId.E, "KEY_E", "E");
            public static readonly Key R = new Key(KeyId.R, "KEY_R", "R");
            public static readonly Key T = new Key(KeyId.T, "KEY_T", "T");
            public static readonly Key Y = new Key(KeyId.Y, "KEY_Y", "Y");
            public static readonly Key U = new Key(KeyId.U, "KEY_U", "U");
            public static readonly Key I = new Key(KeyId.I, "KEY_I", "I");
            public static readonly Key O = new Key(KeyId.O, "KEY_O", "O");
            public static readonly Key P = new Key(KeyId.P, "KEY_P", "P");
            public static readonly Key LEFT_BRACKET = new Key(KeyId.LEFT_BRACKET, "KEY_LBRACKET", "[");
            public static readonly Key RIGHT_BRACKET = new Key(KeyId.RIGHT_BRACKET, "KEY_RBRACKET", "]");
            public static readonly Key ENTER = new Key(KeyId.ENTER, "KEY_RETURN", "Enter");
            public static readonly Key LEFT_CTRL = new Key(KeyId.LEFT_CTRL, "KEY_LCONTROL", "Left Ctrl");
            public static readonly Key A = new Key(KeyId.A, "KEY_A", "A");
            public static readonly Key S = new Key(KeyId.S, "KEY_S", "S");
            public static readonly Key D = new Key(KeyId.D, "KEY_D", "D");
            public static readonly Key F = new Key(KeyId.F, "KEY_F", "F");
            public static readonly Key G = new Key(KeyId.G, "KEY_G", "G");
            public static readonly Key H = new Key(KeyId.H, "KEY_H", "H");
            public static readonly Key J = new Key(KeyId.J, "KEY_J", "J");
            public static readonly Key K = new Key(KeyId.K, "KEY_K", "K");
            public static readonly Key L = new Key(KeyId.L, "KEY_L", "L");
            public static readonly Key SEMICOLON = new Key(KeyId.SEMICOLON, "KEY_SEMICOLON", ";");
            public static readonly Key APOSTROPHE = new Key(KeyId.APOSTROPHE, "KEY_APOSTROPHE", "'");
            public static readonly Key GRAVE = new Key(KeyId.GRAVE, "KEY_GRAVE", "Grave");
            public static readonly Key LEFT_SHIFT = new Key(KeyId.LEFT_SHIFT, "KEY_LSHIFT", "Left Shift");
            public static readonly Key BACKSLASH = new Key(KeyId.BACKSLASH, "KEY_BACKSLASH", "\\");
            public static readonly Key Z = new Key(KeyId.Z, "KEY_Z", "Z");
            public static readonly Key X = new Key(KeyId.X, "KEY_X", "X");
            public static readonly Key C = new Key(KeyId.C, "KEY_C", "C");
            public static readonly Key V = new Key(KeyId.V, "KEY_V", "V");
            public static readonly Key B = new Key(KeyId.B, "KEY_B", "B");
            public static readonly Key N = new Key(KeyId.N, "KEY_N", "N");
            public static readonly Key M = new Key(KeyId.M, "KEY_M", "M");
            public static readonly Key COMMA = new Key(KeyId.COMMA, "KEY_COMMA", ",");
            public static readonly Key PERIOD = new Key(KeyId.PERIOD, "KEY_PERIOD", ".");
            public static readonly Key SLASH = new Key(KeyId.SLASH, "KEY_SLASH", "/");
            public static readonly Key RIGHT_SHIFT = new Key(KeyId.RIGHT_SHIFT, "KEY_RSHIFT", "Right Shift");
            public static readonly Key NUMPAD_MULTIPLY = new Key(KeyId.NUMPAD_MULTIPLY, "KEY_MULTIPLY", "Numpad *");
            public static readonly Key LEFT_ALT = new Key(KeyId.LEFT_ALT, "KEY_LMENU", "Left Alt");
            public static readonly Key SPACE = new Key(KeyId.SPACE, "KEY_SPACE", "Space");
            public static readonly Key CAPS_LOCK = new Key(KeyId.CAPS_LOCK, "KEY_CAPITAL", "Caps Lock");
            public static readonly Key F1 = new Key(KeyId.F1, "KEY_F1", "F1");
            public static readonly Key F2 = new Key(KeyId.F2, "KEY_F2", "F2");
            public static readonly Key F3 = new Key(KeyId.F3, "KEY_F3", "F3");
            public static readonly Key F4 = new Key(KeyId.F4, "KEY_F4", "F4");
            public static readonly Key F5 = new Key(KeyId.F5, "KEY_F5", "F5");
            public static readonly Key F6 = new Key(KeyId.F6, "KEY_F6", "F6");
            public static readonly Key F7 = new Key(KeyId.F7, "KEY_F7", "F7");
            public static readonly Key F8 = new Key(KeyId.F8, "KEY_F8", "F8");
            public static readonly Key F9 = new Key(KeyId.F9, "KEY_F9", "F9");
            public static readonly Key F10 = new Key(KeyId.F10, "KEY_F10", "F10");
            public static readonly Key NUM_LOCK = new Key(KeyId.NUM_LOCK, "KEY_NUMLOCK", "Num Lock");
            public static readonly Key SCROLL_LOCK = new Key(KeyId.SCROLL_LOCK, "KEY_SCROLL", "Scroll Lock");
            public static readonly Key NUMPAD_7 = new Key(KeyId.NUMPAD_7, "KEY_NUMPAD7", "Numpad 7");
            public static readonly Key NUMPAD_8 = new Key(KeyId.NUMPAD_8, "KEY_NUMPAD8", "Numpad 8");
            public static readonly Key NUMPAD_9 = new Key(KeyId.NUMPAD_9, "KEY_NUMPAD9", "Numpad 9");
            public static readonly Key NUMPAD_MINUS = new Key(KeyId.NUMPAD_MINUS, "KEY_SUBTRACT", "Numpad -");
            public static readonly Key NUMPAD_4 = new Key(KeyId.NUMPAD_4, "KEY_NUMPAD4", "Numpad 4");
            public static readonly Key NUMPAD_5 = new Key(KeyId.NUMPAD_5, "KEY_NUMPAD5", "Numpad 5");
            public static readonly Key NUMPAD_6 = new Key(KeyId.NUMPAD_6, "KEY_NUMPAD6", "Numpad 6");
            public static readonly Key NUMPAD_PLUS = new Key(KeyId.NUMPAD_PLUS, "KEY_ADD", "Numpad +");
            public static readonly Key NUMPAD_1 = new Key(KeyId.NUMPAD_1, "KEY_NUMPAD1", "Numpad 1");
            public static readonly Key NUMPAD_2 = new Key(KeyId.NUMPAD_2, "KEY_NUMPAD2", "Numpad 2");
            public static readonly Key NUMPAD_3 = new Key(KeyId.NUMPAD_3, "KEY_NUMPAD3", "Numpad 3");
            public static readonly Key NUMPAD_0 = new Key(KeyId.NUMPAD_0, "KEY_NUMPAD0", "Numpad 0");
            public static readonly Key NUMPAD_PERIOD = new Key(KeyId.NUMPAD_PERIOD, "KEY_DECIMAL", "Numpad .");
            public static readonly Key F11 = new Key(KeyId.F11, "KEY_F11", "F11");
            public static readonly Key F12 = new Key(KeyId.F12, "KEY_F12", "F12");
            public static readonly Key F13 = new Key(KeyId.F13, "KEY_F13", "F13");
            public static readonly Key F14 = new Key(KeyId.F14, "KEY_F14", "F14");
            public static readonly Key F15 = new Key(KeyId.F15, "KEY_F15", "F15");
            public static readonly Key F16 = new Key(KeyId.F16, "KEY_F16", "F16");
            public static readonly Key F17 = new Key(KeyId.F17, "KEY_F17", "F17");
            public static readonly Key F18 = new Key(KeyId.F18, "KEY_F18", "F18");
            public static readonly Key KANA = new Key(KeyId.KANA, "KEY_KANA", "Kana"); // Japanese Keyboard key = new Key(for switching between roman and japanese characters?
            public static readonly Key F19 = new Key(KeyId.F19, "KEY_F19", "F19");
            public static readonly Key CONVERT = new Key(KeyId.CONVERT, "KEY_CONVERT", "Convert"); // Japanese Keyboard key = new Key(for converting Hiragana characters to Kanji?)
            public static readonly Key NOCONVERT = new Key(KeyId.NOCONVERT, "KEY_NOCONVERT", "No Convert"); // Japanese Keyboard key
            public static readonly Key YEN = new Key(KeyId.YEN, "KEY_YEN", "¥"); // Japanese keyboard key for yen
            public static readonly Key NUMPAD_EQUALS = new Key(KeyId.NUMPAD_EQUALS, "KEY_NUMPADEQUALS", "Numpad =");
            public static readonly Key CIRCUMFLEX = new Key(KeyId.CIRCUMFLEX, "KEY_CIRCUMFLEX", "^"); // Japanese keyboard
            public static readonly Key AT = new Key(KeyId.AT, "KEY_AT", "@"); // = new Key(NEC PC98)
            public static readonly Key COLON = new Key(KeyId.COLON, "KEY_COLON", ":"); // = new Key(NEC PC98)
            public static readonly Key UNDERLINE = new Key(KeyId.UNDERLINE, "KEY_UNDERLINE", "_"); // = new Key(NEC PC98)
            public static readonly Key KANJI = new Key(KeyId.KANJI, "KEY_KANJI", "Kanji"); // = new Key(Japanese keyboard)
            public static readonly Key STOP = new Key(KeyId.STOP, "KEY_STOP", "Stop"); // = new Key(NEC PC98)
            public static readonly Key AX = new Key(KeyId.AX, "KEY_AX", "AX"); // = new Key(Japan AX)
            public static readonly Key UNLABELED = new Key(KeyId.UNLABELED, "KEY_UNLABELED", "Unlabelled"); // = new Key(J3100) = new Key(a mystery button?)
            public static readonly Key NUMPAD_ENTER = new Key(KeyId.NUMPAD_ENTER, "KEY_NUMPADENTER", "Numpad Enter");
            public static readonly Key RIGHT_CTRL = new Key(KeyId.RIGHT_CTRL, "KEY_RCONTROL", "Right Ctrl");
            public static readonly Key SECTION = new Key(KeyId.SECTION, "KEY_SECTION", "§");
            public static readonly Key NUMPAD_COMMA = new Key(KeyId.NUMPAD_COMMA, "KEY_NUMPADCOMMA", "Numpad ,"); // = new Key(NEC PC98)
            public static readonly Key NUMPAD_DIVIDE = new Key(KeyId.NUMPAD_DIVIDE, "KEY_DIVIDE", "Numpad /");
            public static readonly Key PRINT_SCREEN = new Key(KeyId.PRINT_SCREEN, "KEY_SYSRQ", "Print Screen");
            public static readonly Key RIGHT_ALT = new Key(KeyId.RIGHT_ALT, "KEY_RMENU", "Right Alt");
            public static readonly Key FUNCTION = new Key(KeyId.FUNCTION, "KEY_FUNCTION", "Function");
            public static readonly Key PAUSE = new Key(KeyId.PAUSE, "KEY_PAUSE", "Pause");
            public static readonly Key HOME = new Key(KeyId.HOME, "KEY_HOME", "Home");
            public static readonly Key UP = new Key(KeyId.UP, "KEY_UP", "Up");
            public static readonly Key PAGE_UP = new Key(KeyId.PAGE_UP, "KEY_PRIOR", "Page Up");
            public static readonly Key LEFT = new Key(KeyId.LEFT, "KEY_LEFT", "Left");
            public static readonly Key RIGHT = new Key(KeyId.RIGHT, "KEY_RIGHT", "Right");
            public static readonly Key END = new Key(KeyId.END, "KEY_END", "End");
            public static readonly Key DOWN = new Key(KeyId.DOWN, "KEY_DOWN", "Down");
            public static readonly Key PAGE_DOWN = new Key(KeyId.PAGE_DOWN, "KEY_NEXT", "Page Down");
            public static readonly Key INSERT = new Key(KeyId.INSERT, "KEY_INSERT", "Insert");
            public static readonly Key DELETE = new Key(KeyId.DELETE, "KEY_DELETE", "Delete");
            public static readonly Key CLEAR = new Key(KeyId.CLEAR, "KEY_CLEAR", "Clear"); // = new Key(Mac)
            public static readonly Key LEFT_META = new Key(KeyId.LEFT_META, "KEY_LMETA", "Left Meta"); // Left Windows/Option key
            public static readonly Key RIGHT_META = new Key(KeyId.RIGHT_META, "KEY_RMETA", "Right Meta"); // Right Windows/Option key
            public static readonly Key APPS = new Key(KeyId.APPS, "KEY_APPS", "Apps");
            public static readonly Key POWER = new Key(KeyId.POWER, "KEY_POWER", "Power");
            public static readonly Key SLEEP = new Key(KeyId.SLEEP, "KEY_SLEEP", "Sleep");

            #endregion Keys

            #region Members

            private static Dictionary<string, Input> lookupByName;
            private static Dictionary<int, Input> lookupById;

            #endregion Members

            #region Constructor

            static Key()
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

            public int id;
            public string name;
            public string displayName;

            public Key(int id, string name, string displayName)
            {
                this.id = id;
                this.name = name;
                this.displayName = displayName;
            }


            public InputType getType()
            {
                return InputType.KEY;
            }

            public int getId()
            {
                return id;
            }

            public string getName()
            {
                return name;
            }

            public string getDisplayName()
            {
                return displayName;
            }
        }

        #endregion Key
    }
}
