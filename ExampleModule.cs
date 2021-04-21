// https://github.com/EverestAPI/Resources/wiki/Your-First-Code-Mod#module-class

using Monocle;
using System;

namespace Celeste.Mod.Example {
    public class ExampleModule : EverestModule {
        // Only one alive module instance can exist at any given time.
        public static ExampleModule Instance;

        public ExampleModule() {
            Instance = this;
        }

        // Settings, SaveData, and Session are optional
        // If you need to store settings:
        public override Type SettingsType => typeof(ExampleModuleSettings);
        public static ExampleModuleSettings Settings => (ExampleModuleSettings) Instance._Settings;

        // If you need to store save data:
        public override Type SaveDataType => typeof(ExampleModuleSaveData);
        public static ExampleModuleSaveData SaveData => (ExampleModuleSaveData) Instance._SaveData;

        // If you need to store session data:
        public override Type SessionType => typeof(ExampleModuleSession);
        public static ExampleModuleSession Session => (ExampleModuleSession) Instance._Session;

        // Initialized in LoadContent, after graphics and other assets have been loaded.
        public SpriteBank ExampleSpriteBank;

        // Set up any hooks, event handlers and your mod in general here.
        // Load runs before Celeste itself has initialized properly.
        public override void Load() {
            Hooks.Load();
        }

        // Optional, initialize anything after Celeste has initialized itself properly.
        public override void Initialize() {
        }

        // Optional, do anything requiring either the Celeste or mod content here.
        public override void LoadContent(bool firstLoad) {
            // This creates a new spritebank from an xml file.
            // This particular one uses textures from the Gameplay atlas, which is stored in GFX.Game
            ExampleSpriteBank = new SpriteBank(GFX.Game, "ExampleMod/ExampleSprites.xml");
        }

        // Unload the entirety of your mod's content. Free up any native resources.
        public override void Unload() {
            Hooks.Unload();
        }

    }
}
