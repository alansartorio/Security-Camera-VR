using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.References;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using Random = UnityEngine.Random;

namespace GOAP.Scripts
{
    public class GrabItemAction : ActionBase<GrabItemAction.Data>
    {
        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            public float Timer { get; set; }

            [GetComponent] public Client client { get; set; }
        }

        public override void Created()
        {
        }

        public override void Start(IMonoAgent agent, Data data)
        {
            data.Timer = Random.Range(2f, 4f);
        }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            data.client.leftItemsToGrab--;

            return ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, Data data)
        {
        }
    }
}