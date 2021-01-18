using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.Example {
    [CustomEntity("ExampleMod/ExampleEntity")]
    public class ExampleEntity : Entity {

        public ExampleEntity(EntityData data, Vector2 offset) 
            : base(data.Position + offset) {

        }

        // Not all entities are guaranteed to have been added to the scene
        public override void Added(Scene scene) {
            base.Added(scene);
        }

        // All entities have been added to the scene (except those added in other Awake methods)
        public override void Awake(Scene scene) {
            base.Awake(scene);
            // A good place to see what other entities are present in the level
            if (scene.Tracker.GetEntity<DreamBlock>() != null)
                Logger.Log("ExampleModule", "DreamBlock detected in level with ExampleEntity, doing things...");

        }

        public override void Update() {
            base.Update();
        }

        public override void Render() {
            base.Render();
        }

        public override void Removed(Scene scene) {
            base.Removed(scene);
        }

    }
}
