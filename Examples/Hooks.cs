// https://github.com/EverestAPI/Resources/wiki/Your-First-Code-Mod#modifying-the-games-code

using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using System;
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

            IL.Celeste.Player.ClimbHop += Player_ClimbHop;

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

        // Any hooks you apply should be undone during your EverestModule's Unload function.
        public static void Unload() {
            On.Celeste.Player.Jump -= Player_Jump;
            hook_Player_get_MaxDashes.Dispose();

            IL.Celeste.Player.ClimbHop -= Player_ClimbHop;
            hook_Player_DashCoroutine.Dispose();
        }

        private static void Player_Jump(On.Celeste.Player.orig_Jump orig, Player self, bool particles, bool playSfx) {
            Logger.Log("ExampleMod", "Just before Player.Jump()");

            // We can call the original method at any point in the hook.
            orig(self, particles, playSfx);

            Logger.Log("ExampleMod", "Just after Player.Jump()");
        }

        // Not required if the orig method is not needed
        private delegate int orig_Player_get_MaxDashes(Player self);
        private static int Player_get_MaxDashes(orig_Player_get_MaxDashes orig, Player self) {
            return orig(self);
        }

        private static void Player_ClimbHop(ILContext il) {
            // The ILCursor is what we use to traverse the method and modify it
            ILCursor cursor = new ILCursor(il);

            // When moving the cursor to the target location, be as specific as reasonably possible.
            // Using cursor.TryGotoNext instead of cursor.GotoNext will prevent a crash if the instruction can't be found
            if (cursor.TryGotoNext(MoveType.After, instr => instr.MatchLdstr("event:/char/madeline/climb_ledge"))) {
                // Using EmitDelegate has a few advantages over inserting only IL code.
                // - Less IL code to write and debug
                // - Greater compatibility with other mods:
                // If another mod adds the same delegate in the same location it will not break, since the string will get passed to each delegate in sequence.
                cursor.EmitDelegate<Func<string, string>>(str => {
                    if (false)
                        return "different event";
                    return str;
                });
            }
        }

        private static void Player_DashCoroutine(ILContext il) {

        }

    }
}
