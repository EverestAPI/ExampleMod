// https://github.com/EverestAPI/Resources/wiki/Your-First-Code-Mod#modifying-the-games-code

using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using System.Reflection;

namespace Celeste.Mod.Example {
    public static class Hooks {

        private static IDetour hook_Player_get_MaxDashes;
        private static IDetour hook_Player_DashCoroutine;

        public static void Load() {
            On.Celeste.Player.Jump += Player_Jump;
            // Manually construct an On. hook
            // This is useful when the method you're trying to hook isn't parsed by MonoMod.RuntimeDetour.HookGen (it isn't available using the regular method)
            // This applies to Property accessors (as shown here), Compiler generated methods (as shown further down) or methods defined in other assemblies.
            hook_Player_get_MaxDashes = new Hook(
                typeof(Player).GetProperty("MaxDashes").GetGetMethod(),
                typeof(Hooks).GetMethod("Player_get_MaxDashes", BindingFlags.NonPublic | BindingFlags.Static)
            );

            IL.Celeste.Player.Duck += Player_Duck;

            // This constructs a new IL. hook similarly to the manually constructed On. hook above.
            // Since DashCoroutine is an iterator method, there are some behind-the-scenes things done that make it so we can't directly modify Player.DashCoroutine
            // Instead we are modifying a compiler generated method, which can be easily retrieved using GetStateMachineTarget (provided by MonoMod)
            // To explore these methods and learn how they work, enable "View -> Show all types and members" (ILSpy) OR
            // disable "View -> Options -> Decompiler -> Show hidden compiler generated types and methods" (DnSpy).
            hook_Player_DashCoroutine = new ILHook(
                typeof(Player).GetMethod("DashCoroutine", BindingFlags.NonPublic | BindingFlags.Instance).GetStateMachineTarget(),
                Player_DashCoroutine
            );
        }

        public static void Unload() {
            On.Celeste.Player.Jump -= Player_Jump;
            hook_Player_get_MaxDashes.Dispose();

            IL.Celeste.Player.Duck -= Player_Duck;
            hook_Player_DashCoroutine.Dispose();
        }

        private static void Player_Jump(On.Celeste.Player.orig_Jump orig, Player self, bool particles, bool playSfx) {
            orig(self, particles, playSfx);
        }

        // Not required if the orig method is not needed
        private delegate int orig_Player_get_MaxDashes(Player self);
        private static int Player_get_MaxDashes(orig_Player_get_MaxDashes orig, Player self) {
            return orig(self);
        }

        private static void Player_Duck(ILContext il) {
        }

        private static void Player_DashCoroutine(ILContext il) {

        }

    }
}
