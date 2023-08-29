using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;

namespace GOAP.Scripts
{
    public class LeftItemsToStealSensor : LocalWorldSensorBase
    {
        public override void Created()
        {
        }

        public override void Update()
        {
        }

        public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
        {
            var client = references.GetCachedComponent<Client>();
            return client.leftItemsToSteal;
        }
    }
}