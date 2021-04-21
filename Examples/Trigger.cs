using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.Example {
    [CustomEntity("ExampleMod/ExampleTrigger")]
    public class ExampleTrigger : Trigger {
        public ExampleTrigger(EntityData data, Vector2 offset) 
            : base(data, offset) {
        }

        public override void OnEnter(Player player) {
            base.OnEnter(player);
        }

        public override void OnStay(Player player) {
            base.OnStay(player);

            float lerpX = GetPositionLerp(player, PositionModes.HorizontalCenter);
        }

        public override void OnLeave(Player player) {
            base.OnLeave(player);
        }

    }
}
