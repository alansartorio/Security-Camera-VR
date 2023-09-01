using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.References;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GOAP.Scripts
{
    public class ExitAction : ActionBase<ExitAction.Data>
    {
        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            [GetComponent] public Client Client { get; set; }
        }

        public override void Created()
        {
        }

        public override void Start(IMonoAgent agent, Data data)
        {
        }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            data.Client.Exited();
            return ActionRunState.Stop;
        }
        
        public override void End(IMonoAgent agent, Data data)
        {
        }
    }
}