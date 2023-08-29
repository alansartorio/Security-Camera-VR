using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.References;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
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

        private void FaceTarget(Transform transform, Vector3 destination, float deltaTime)
        {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);  
        }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            FaceTarget(agent.transform, data.Target.Position, context.DeltaTime);
            data.Timer -= context.DeltaTime;

            if (data.Timer > 0)
                return ActionRunState.Continue;

            data.client.itemsInHand++;

            return ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, Data data)
        {
        }
    }
}