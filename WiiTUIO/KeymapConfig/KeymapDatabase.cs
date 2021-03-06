﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiiTUIO.Properties;

namespace WiiTUIO
{
    class KeymapDatabase
    {
        public string DisableKey { get; private set; }

        private List<KeymapInput> allInputs;
        private List<KeymapOutput> allOutputs;

        private string DEFAULT_JSON_FILENAME = "default.json";

        private static KeymapDatabase currentInstance;
        public static KeymapDatabase Current
        {
            get
            {
                if (currentInstance == null)
                {
                    currentInstance = new KeymapDatabase();
                }
                return currentInstance;
            }
        }

        private KeymapDatabase()
        {
            this.DisableKey = "disable";

            allInputs = new List<KeymapInput>();
            allInputs.Add(new KeymapInput(KeymapInputSource.IR, "Pointer", "Pointer", false, true));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "A", "A"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "B", "B"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Home", "Home"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Left", "Left"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Right", "Right"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Up", "Up"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Down", "Down"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Plus", "Plus"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Minus", "Minus"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "One", "One"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Two", "Two"));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Tilt X", "AccelX", true, false));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Tilt Y", "AccelY", true, false));
            allInputs.Add(new KeymapInput(KeymapInputSource.WIIMOTE, "Tilt Z", "AccelZ", true, false));

            allInputs.Add(new KeymapInput(KeymapInputSource.NUNCHUK, "C", "Nunchuk.C"));
            allInputs.Add(new KeymapInput(KeymapInputSource.NUNCHUK, "Z", "Nunchuk.Z"));
            allInputs.Add(new KeymapInput(KeymapInputSource.NUNCHUK, "Stick X", "Nunchuk.StickX", true, false));
            allInputs.Add(new KeymapInput(KeymapInputSource.NUNCHUK, "Stick Y", "Nunchuk.StickY", true, false));
            allInputs.Add(new KeymapInput(KeymapInputSource.NUNCHUK, "Stick Up", "Nunchuk.StickUp"));
            allInputs.Add(new KeymapInput(KeymapInputSource.NUNCHUK, "Stick Down", "Nunchuk.StickDown"));
            allInputs.Add(new KeymapInput(KeymapInputSource.NUNCHUK, "Stick Left", "Nunchuk.StickLeft"));
            allInputs.Add(new KeymapInput(KeymapInputSource.NUNCHUK, "Stick Right", "Nunchuk.StickRight"));


            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Left", "Classic.Left"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Right", "Classic.Right"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Up", "Classic.Up"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Down", "Classic.Down"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Left Stick X", "Classic.StickLX", true, false));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Left Stick Y", "Classic.StickLY", true, false));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Right Stick X", "Classic.StickRX", true, false));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Right Stick Y", "Classic.StickRY", true, false));

            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Left Stick Left", "Classic.StickLLeft"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Left Stick Right", "Classic.StickLRight"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Left Stick Up", "Classic.StickLUp"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Left Stick Down", "Classic.StickLDown"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Right Stick Left", "Classic.StickRLeft"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Right Stick Right", "Classic.StickRRight"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Right Stick Up", "Classic.StickRUp"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Right Stick Down", "Classic.StickRDown"));

            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Minus", "Classic.Minus"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Plus", "Classic.Plus"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Home", "Classic.Home"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Y", "Classic.Y"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "X", "Classic.X"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "A", "Classic.A"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "B", "Classic.B"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Left Trigger", "Classic.TriggerL", true, false));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Right Trigger", "Classic.TriggerR", true, false));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Left Trigger Push", "Classic.L"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "Right Trigger Push", "Classic.R"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "ZL", "Classic.ZL"));
            allInputs.Add(new KeymapInput(KeymapInputSource.CLASSIC, "ZR", "Classic.ZR"));

            allOutputs = new List<KeymapOutput>();
            allOutputs.Add(new KeymapOutput(KeymapOutputType.TOUCH, "Touch Cursor", "touch", false, true, false));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.TOUCH, "Touch Main", "touchmaster"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.TOUCH, "Touch Slave", "touchslave"));

            allOutputs.Add(new KeymapOutput(KeymapOutputType.MOUSE, "Mouse Cursor", "mouse", false, true, false));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.MOUSE, "Mouse Left", "mouseleft"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.MOUSE, "Mouse Right", "mouseright"));
            //allOutputs.Add(new KeymapOutput(KeymapOutputType.MOUSE, "Mouse Middle", "mbutton"));
            //allOutputs.Add(new KeymapOutput(KeymapOutputType.MOUSE, "Mouse Extra 1", "xbutton1"));
            //allOutputs.Add(new KeymapOutput(KeymapOutputType.MOUSE, "Mouse Extra 2", "xbutton2"));



            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Tab", "tab"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Backspace", "back"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Return", "return"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Shift", "shift"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Control", "control"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Alt", "menu"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Pause", "pause"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Caps Lock", "capital"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Escape", "escape"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Spacebar", "space"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Page Up", "prior"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Page Down", "next"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "End", "end"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Home", "home"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Left", "left"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Up", "up"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Right", "right"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Down", "down"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Print Screen", "snapshot"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Insert", "ins"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Delete", "delete"));


            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "0", "vk_0"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "1", "vk_1"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "2", "vk_2"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "3", "vk_3"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "4", "vk_4"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "5", "vk_5"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "6", "vk_6"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "7", "vk_7"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "8", "vk_8"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "9", "vk_9"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "A", "vk_a"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "B", "vk_b"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "C", "vk_c"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "D", "vk_d"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "E", "vk_e"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F", "vk_f"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "G", "vk_g"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "H", "vk_h"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "I", "vk_i"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "J", "vk_j"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "K", "vk_k"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "L", "vk_l"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "M", "vk_m"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "N", "vk_n"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "O", "vk_o"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "P", "vk_p"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Q", "vk_q"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "R", "vk_r"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "S", "vk_s"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "T", "vk_t"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "U", "vk_u"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "V", "vk_v"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "W", "vk_w"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "X", "vk_x"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Y", "vk_y"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Z", "vk_z"));

            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Left Win", "lwin"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Right Win", "rwin"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 0", "numpad0"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 1", "numpad1"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 2", "numpad2"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 3", "numpad3"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 4", "numpad4"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 5", "numpad5"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 6", "numpad6"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 7", "numpad7"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 8", "numpad8"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numpad 9", "numpad9"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "*", "multiply"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "+", "add"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "-", "subtract"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, ",", "decimal"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "/", "divide"));


            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F1", "f1"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F2", "f2"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F3", "f3"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F4", "f4"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F5", "f5"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F6", "f6"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F7", "f7"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F8", "f8"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F9", "f9"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F10", "f10"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F11", "f11"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F12", "f12"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F13", "f13"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F14", "f14"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F15", "f15"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F16", "f16"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F17", "f17"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F18", "f18"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F19", "f19"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F20", "f20"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F21", "f21"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F22", "f22"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F23", "f23"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "F24", "f24"));


            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Numlock", "numlock"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Left Shift", "lshift"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Right Shift", "rshift"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Left Control", "lcontrol"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Right Control", "rcontrol"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Left Alt", "lmenu"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Right Alt", "rmenu"));

            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Browser Back", "browser_back"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Browser Forward", "browser_forward"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Browser Refresh", "browser_refresh"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Browser Stop", "browser_stop"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Browser Search", "browser_search"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Browser Favorites", "browser_favorites"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Browser Home", "browser_home"));

            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Volume Mute", "volume_mute"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Volume Up", "volume_up"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Volume Down", "volume_down"));

            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Next Track", "media_next_track"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Previous Track", "media_prev_track"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Stop Media", "media_stop"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Play/Pause Media", "media_play_pause"));

            allOutputs.Add(new KeymapOutput(KeymapOutputType.KEYBOARD, "Zoom", "zoom"));

            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "A", "360.a"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "B", "360.b"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "X", "360.x"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Y", "360.y"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left", "360.left"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right", "360.right"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Up", "360.up"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Down", "360.down"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Back", "360.back"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Start", "360.start"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Guide", "360.guide"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left Stick X", "360.sticklx", true, false, false));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left Stick Y", "360.stickly", true, false, false));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right Stick X", "360.stickrx", true, false, false));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right Stick Y", "360.stickry", true, false, false));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left Stick Up", "360.stickly+"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left Stick Down", "360.stickly-"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left Stick Left", "360.sticklx-"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left Stick Right", "360.sticklx+"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right Stick Up", "360.stickry+"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right Stick Down", "360.stickry-"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right Stick Left", "360.stickrx-"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right Stick Right", "360.stickrx+"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left Trigger", "360.triggerl", true, false, false));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right Trigger", "360.triggerr", true, false, false));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left Trigger Press", "360.triggerl"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right Trigger Press", "360.triggerr"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Left Bumper", "360.bumperl"));
            allOutputs.Add(new KeymapOutput(KeymapOutputType.XINPUT, "Right Bumper", "360.bumperr"));

            allOutputs.Add(new KeymapOutput(KeymapOutputType.DISABLE, "Disable", this.DisableKey));
        }


        public KeymapSettings getKeymapSettings()
        {
            return new KeymapSettings(Settings.Default.keymaps_config);
        }

        public List<Keymap> getAllKeymaps()
        {
            List<Keymap> list = new List<Keymap>();
            string[] files = Directory.GetFiles(Settings.Default.keymaps_path, "*.json");
            string defaultKeymapFilename = this.getKeymapSettings().getDefaultKeymap();

            Keymap defaultKeymap = new Keymap(null, defaultKeymapFilename);
            list.Add(defaultKeymap);

            foreach (string filepath in files)
            {
                string filename = Path.GetFileName(filepath);
                if (filename != Settings.Default.keymaps_config && filename != defaultKeymapFilename)
                {
                    list.Add(new Keymap(defaultKeymap, filename));
                }
            }
            return list;
        }

        public Keymap getKeymap(string filename)
        {
            List<Keymap> list = this.getAllKeymaps();

            foreach (Keymap keymap in list)
            {
                if (keymap.Filename == filename)
                {
                    return keymap;
                }
            }
            return null;
        }

        public Keymap getDefaultKeymap()
        {
            List<Keymap> list = this.getAllKeymaps();
            KeymapSettings settings = this.getKeymapSettings();

            foreach (Keymap keymap in list)
            {
                if (keymap.Filename == settings.getDefaultKeymap())
                {
                    return keymap;
                }
            }
            return null;
        }

        public List<KeymapInput> getAvailableInputs(KeymapInputSource source)
        {
            List<KeymapInput> list = new List<KeymapInput>();
            foreach (KeymapInput input in allInputs)
            {
                if (input.Source == source)
                {
                    list.Add(input);
                }
            }
            return list;
        }

        public List<KeymapOutput> getAvailableOutputs(KeymapOutputType type)
        {
            if (type == KeymapOutputType.ALL)
            {
                return allOutputs;
            }
            List<KeymapOutput> list = new List<KeymapOutput>();
            foreach (KeymapOutput output in allOutputs)
            {
                if (output.Type == type)
                {
                    list.Add(output);
                }
            }
            return list;
        }

        public KeymapInput getInput(string key)
        {
            List<KeymapInput> list = this.allInputs;
            foreach (KeymapInput input in list)
            {
                if (input.Key == key.ToLower())
                {
                    return input;
                }
            }
            return null;
        }

        public KeymapOutput getOutput(string key)
        {
            List<KeymapOutput> list = this.allOutputs;
            foreach (KeymapOutput output in list)
            {
                if (output.Key == key.ToLower())
                {
                    return output;
                }
            }
            return null;
        }

        public KeymapOutput getDisableOutput()
        {
            return this.getAvailableOutputs(KeymapOutputType.DISABLE).First();
        }

        public bool deleteKeymap(Keymap keymap)
        {
            if (keymap.Filename == this.getKeymapSettings().getDefaultKeymap())
            {
                return false;
            }
            this.getKeymapSettings().removeFromLayoutChooser(keymap);
            this.getKeymapSettings().removeFromApplicationSearch(keymap);
            File.Delete(Settings.Default.keymaps_path + keymap.Filename);
            return true;
        }

        public Keymap createNewKeymap()
        {
            List<Keymap> list = new List<Keymap>();
            string[] files = Directory.GetFiles(Settings.Default.keymaps_path, "*.json");

            string suggestedFilename = "z_custom.json";

            bool recheck = false;

            int iterations = 0;

            do
            {
                recheck = false;
                foreach (string filepath in files)
                {
                    string filename = Path.GetFileName(filepath);
                    if (suggestedFilename == filename)
                    {
                        suggestedFilename = "z_custom_" + (++iterations) + ".json";
                        recheck = true;
                    }
                }
            } while (recheck);

            return new Keymap(this.getDefaultKeymap(), suggestedFilename);
        }



        public void CreateDefaultFiles()
        {
            this.createDefaultApplicationsJSON();
            this.createDefaultKeymapJSON();
        }

        private static void MergeJSON(JObject receiver, JObject donor)
        {
            foreach (var property in donor)
            {
                JObject receiverValue = receiver[property.Key] as JObject;
                JObject donorValue = property.Value as JObject;
                if (receiverValue != null && donorValue != null)
                    MergeJSON(receiverValue, donorValue);
                else
                    receiver[property.Key] = property.Value;
            }
        }

        private JObject createDefaultApplicationsJSON()
        {
            JArray layouts = new JArray();
            layouts.Add(new JObject(
                new JProperty("Name", "Default"),
                new JProperty("Keymap", DEFAULT_JSON_FILENAME)
            ));

            JArray applications = new JArray();

            JObject applicationList =
                new JObject(
                    new JProperty("LayoutChooser", layouts),
                    new JProperty("Applications", applications),
                    new JProperty("Default", DEFAULT_JSON_FILENAME)
                );

            JObject union = applicationList;

            if (File.Exists(Settings.Default.keymaps_path + Settings.Default.keymaps_config))
            {
                StreamReader reader = File.OpenText(Settings.Default.keymaps_path + Settings.Default.keymaps_config);
                try
                {
                    JObject existingConfig = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                    reader.Close();

                    MergeJSON(union, existingConfig);
                }
                catch (Exception e)
                {
                    throw new Exception(Settings.Default.keymaps_path + Settings.Default.keymaps_config + " is not valid JSON");
                }
            }

            File.WriteAllText(Settings.Default.keymaps_path + Settings.Default.keymaps_config, union.ToString());
            return union;
        }

        private JObject createDefaultKeymapJSON()
        {
            JObject buttons = new JObject();

            buttons.Add(new JProperty("Pointer", "Touch"));

            buttons.Add(new JProperty("A", "TouchMaster"));

            buttons.Add(new JProperty("B", "TouchSlave"));

            buttons.Add(new JProperty("Home", "LWin"));

            buttons.Add(new JProperty("Left", "Left"));
            buttons.Add(new JProperty("Right", "Right"));
            buttons.Add(new JProperty("Up", "Up"));
            buttons.Add(new JProperty("Down", "Down"));

            buttons.Add(new JProperty("Plus", "Volume_Up"));

            buttons.Add(new JProperty("Minus", "Volume_Down"));

            JArray buttonOne = new JArray();
            buttonOne.Add(new JValue("LWin"));
            buttonOne.Add(new JValue("VK_C"));
            buttons.Add(new JProperty("One", buttonOne));

            JArray buttonTwo = new JArray();
            buttonTwo.Add(new JValue("LWin"));
            buttonTwo.Add(new JValue("Tab"));
            buttons.Add(new JProperty("Two", buttonTwo));

            buttons.Add(new JProperty("Nunchuk.StickX", "360.StickLX"));
            buttons.Add(new JProperty("Nunchuk.StickY", "360.StickLY"));
            buttons.Add(new JProperty("Nunchuk.StickUp", "disable"));
            buttons.Add(new JProperty("Nunchuk.StickDown", "disable"));
            buttons.Add(new JProperty("Nunchuk.StickLeft", "disable"));
            buttons.Add(new JProperty("Nunchuk.StickRight", "disable"));
            buttons.Add(new JProperty("Nunchuk.C", "360.TriggerL"));
            buttons.Add(new JProperty("Nunchuk.Z", "360.TriggerR"));

            buttons.Add(new JProperty("Classic.Left", "360.Left"));
            buttons.Add(new JProperty("Classic.Right", "360.Right"));
            buttons.Add(new JProperty("Classic.Up", "360.Up"));
            buttons.Add(new JProperty("Classic.Down", "360.Down"));
            buttons.Add(new JProperty("Classic.StickLX", "360.StickLX"));
            buttons.Add(new JProperty("Classic.StickLY", "360.StickLY"));
            buttons.Add(new JProperty("Classic.StickRX", "360.StickRX"));
            buttons.Add(new JProperty("Classic.StickRY", "360.StickRY"));
            buttons.Add(new JProperty("Classic.StickLUp", "disable"));
            buttons.Add(new JProperty("Classic.StickLDown", "disable"));
            buttons.Add(new JProperty("Classic.StickLLeft", "disable"));
            buttons.Add(new JProperty("Classic.StickLRight", "disable"));
            buttons.Add(new JProperty("Classic.StickRUp", "disable"));
            buttons.Add(new JProperty("Classic.StickRDown", "disable"));
            buttons.Add(new JProperty("Classic.StickRLeft", "disable"));
            buttons.Add(new JProperty("Classic.StickRRight", "disable"));
            buttons.Add(new JProperty("Classic.Minus", "360.Back"));
            buttons.Add(new JProperty("Classic.Plus", "360.Start"));
            buttons.Add(new JProperty("Classic.Home", "360.Guide"));
            buttons.Add(new JProperty("Classic.Y", "360.Y"));
            buttons.Add(new JProperty("Classic.X", "360.X"));
            buttons.Add(new JProperty("Classic.A", "360.A"));
            buttons.Add(new JProperty("Classic.B", "360.B"));
            buttons.Add(new JProperty("Classic.TriggerL", "360.TriggerL"));
            buttons.Add(new JProperty("Classic.TriggerR", "360.TriggerR"));
            buttons.Add(new JProperty("Classic.L", "360.L"));
            buttons.Add(new JProperty("Classic.R", "360.R"));
            buttons.Add(new JProperty("Classic.ZL", "360.BumperL"));
            buttons.Add(new JProperty("Classic.ZR", "360.BumperR"));

            JObject union = new JObject();

            union.Add(new JProperty("Title", "Default"));

            union.Add(new JProperty("All", buttons));

            if (File.Exists(Settings.Default.keymaps_path + Settings.Default.keymaps_config))
            {
                StreamReader reader = File.OpenText(Settings.Default.keymaps_path + DEFAULT_JSON_FILENAME);
                try
                {
                    JObject existingConfig = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                    reader.Close();

                    MergeJSON(union, existingConfig);
                }
                catch (Exception e)
                {
                    throw new Exception(Settings.Default.keymaps_path + DEFAULT_JSON_FILENAME + " is not valid JSON");
                }
            }
            File.WriteAllText(Settings.Default.keymaps_path + DEFAULT_JSON_FILENAME, union.ToString());
            return union;
        }
    }

    public enum KeymapInputSource
    {
        IR,
        WIIMOTE,
        NUNCHUK,
        CLASSIC
    }

    public class KeymapInput
    {
        public string Name { get; private set; }
        public string Key { get; private set; }
        public KeymapInputSource Source { get; private set; }
        public bool Continous { get; private set; }
        public bool Cursor { get; private set; }

        public KeymapInput(KeymapInputSource source, string name, string key)
            : this(source, name, key, false, false)
        {

        }

        public KeymapInput(KeymapInputSource source, string name, string key, bool continous, bool cursor)
        {
            this.Source = source;
            this.Name = name;
            this.Key = key;
            this.Continous = continous;
            this.Cursor = cursor;
        }

        public bool canHandle(KeymapOutput output)
        {
            return (this.Continous == output.Continous && this.Cursor == output.Cursor) || output.Type == KeymapOutputType.DISABLE;
        }
    }


    public enum KeymapOutputType
    {
        ALL, //Only used for search
        TOUCH,
        MOUSE,
        XINPUT,
        KEYBOARD,
        DISABLE
    }
    public class KeymapOutput
    {
        public string Name { get; private set; }
        public string Key { get; private set; }
        public KeymapOutputType Type { get; private set; }
        public bool Continous { get; private set; }
        public bool Cursor { get; private set; }
        public bool Stackable { get; private set; }

        public KeymapOutput(KeymapOutputType type, string name, string key)
            : this(type, name, key, false, false, true)
        {

        }

        public KeymapOutput(KeymapOutputType type, string name, string key, bool continous, bool cursor, bool stackable)
        {
            this.Type = type;
            this.Name = name;
            this.Key = key;
            this.Continous = continous;
            this.Cursor = cursor;
        }

        public bool canStack(KeymapOutput other)
        {
            return this.Stackable && other.Stackable;
        }
    }


    public class KeymapOutputComparer : IComparer<KeymapOutput>
    {
        StringComparer comparer = StringComparer.CurrentCulture;

        public int Compare(KeymapOutput x, KeymapOutput y)
        {
            if (x.Type - y.Type == 0)
            {
                return comparer.Compare(x.Name, y.Name);
            }
            return x.Type - y.Type;
        }
    }
}
