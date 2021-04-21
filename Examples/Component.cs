using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.Example {
    public class ExampleComponent : Component {
        public ExampleComponent()
            : base(active: true, visible: true) {
        }


    }

    public class EntityWithComponents : Entity {

        public EntityWithComponents() {
            Add(new ExampleComponent());
        }

    }
}
