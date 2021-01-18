using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.Example {
    [CustomEntity("ExampleMod/ExampleTrigger")]
    public class ExampleTrigger : Trigger {
        public ExampleTrigger(EntityData data, Vector2 offset) 
            : base(data, offset) {
        }
    }
}
