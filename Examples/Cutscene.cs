// https://github.com/EverestAPI/Resources/wiki/Creating-Custom-Events

using Celeste.Mod.Entities;
using Monocle;
using System.Collections;

namespace Celeste.Mod.Example {
    [CustomEvent("ExampleMod/CustomEvent")]
    public class ExampleCutscene : CutsceneEntity {

        private Player player;

        public ExampleCutscene(EventTrigger trigger, Player player, string eventID) {
            this.player = player;
        }

        public override void OnBegin(Level level) {
            Add(new Coroutine(Cutscene(level)));
        }

        // Makes use of Iterator Methods:
        // https://docs.microsoft.com/en-us/dotnet/csharp/iterators#enumeration-sources-with-iterator-methods
        private IEnumerator Cutscene(Level level) {
            player.StateMachine.State = Player.StDummy;
            player.StateMachine.Locked = true;
            yield return null;
            // returning another IEnumerator will cause that routine to run before resuming this one.
            yield return player.DummyWalkTo(CutsceneNode.Find("ExampleMod/CustomEventNode1").X, true);
            // returning an int or float will cause the Coroutine to pause for that many seconds before resuming.
            yield return 2;
            bool test = false;
            if (test)
                // Use yield break to exit from an Iterator Method early.
                yield break;
            player.StateMachine.State = Player.StNormal;
            // yield break is automatically inserted at the end of IEnumerator functions.
        }

        public override void OnEnd(Level level) {
            if (WasSkipped) {
                // Go to end state of cutscene.
            }

        }
    }
}
