// https://github.com/EverestAPI/Resources/wiki/Custom-Entities-and-Triggers#tracked
// https://github.com/EverestAPI/Resources/wiki/Monocle-Reference#Tracker

using Microsoft.Xna.Framework;
using Monocle;
using System.Linq;

namespace Celeste.Mod.Example {
    // Track this entity in the Tracker, do not track child classes.
    [Tracked(inherited:false)]
    // Track this entity in the Tracker AS A DreamBlock, include child classes.
    [TrackedAs(typeof(DreamBlock), inherited:true)]
    public class TrackedExample : DreamBlock {
        public TrackedExample(EntityData data, Vector2 offset) 
            : base(data, offset) {
        }

        public override void Awake(Scene scene) {
            base.Awake(scene);

            // Will only return entities of type TrackedExample
            scene.Tracker.GetEntities<TrackedExample>();
            // Will return any entities of type TrackedChildExample and child classes
            scene.Tracker.GetEntities<TrackedChildExample>();

            // Will return entities of type DreamBlock, TrackedExample, AND TrackedChildExample
            scene.Tracker.GetEntities<DreamBlock>();

            // Retrieve untracked entities from the scene. Much slower than using the Tracker
            scene.Entities.Select(e => e is UnTrackedExample);
        }
    }

    // Track this entity in the Tracker, include child classes.
    [Tracked(inherited:true)]
    public class TrackedChildExample : TrackedExample {
        public TrackedChildExample(EntityData data, Vector2 offset) 
            : base(data, offset) {
        }
    }

    public class UnTrackedExample : Entity {

    }

}
