using Microsoft.Xna.Framework;
using Monocle;
using MonoMod;
using Celeste.Mod.Entities;

namespace Celeste.Mod.Example {
    [CustomEntity("ExampleMod/ExtendedDashBlock")]
    // `DashBlock` is tracked, and we want this entity to also be tracked as a "DashBlock".
    // While this could be applied to an entity that does *not* extend DashBlock,
    // it is likely to cause crashes because entities that are tracked as a Type are assumed to also be assignable to that Type
    [TrackedAs(typeof(DashBlock))]
    public class ExtendedDashBlock : DashBlock {

        private bool removeOnBreak;

        public ExtendedDashBlock(EntityData data, Vector2 offset, EntityID id) 
            : base(data, offset, id) {
            removeOnBreak = data.Bool("removeOnBreak", true);
        }

        public override void Awake(Scene scene) {
            // To completely override a `virtual` method like `Awake` or `Update`, *while still calling the `base` method*, use a method with the `MonoModLinkTo` attribute
            base_Awake(scene);
        }

        // `MonoModLinkTo` links one method to another, such that when `base_Awake` is called, it actually calls `Solid.Awake`.
        [MonoModLinkTo("Celeste.Solid", "System.Void Awake(Monocle.Scene)")]
        public void base_Awake(Scene scene) {
            base.Awake(scene);
        }

        internal static void Load() {
            On.Celeste.DashBlock.Break_Vector2_Vector2_bool_bool += DashBlock_Break_Vector2_Vector2_bool_bool;
        }

        internal static void Unload() {
            On.Celeste.DashBlock.Break_Vector2_Vector2_bool_bool -= DashBlock_Break_Vector2_Vector2_bool_bool;
        }

        private static void DashBlock_Break_Vector2_Vector2_bool_bool(On.Celeste.DashBlock.orig_Break_Vector2_Vector2_bool_bool orig, DashBlock self, Vector2 from, Vector2 direction, bool playSound, bool playDebrisSound) {
            // To `override` any method that isn't marked as `virtual` or `abstract`, use an On. hook and check if `self` (the current instance) is an instance of your subclass.
            if (self is ExtendedDashBlock extendedBlock) {
                Logger.Log("ExampleModule", "DashBlock.Break was called on an ExtendedDashBlock entity.");
                if (playSound)
                    Audio.Play(SFX.char_bad_disappear);
                if (extendedBlock.removeOnBreak) {
                    extendedBlock.Collidable = false;
                    extendedBlock.RemoveSelf();
                }
            } else {
                orig(self, from, direction, playSound, playDebrisSound);
            }
        }

    }
}
