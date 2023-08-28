using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

namespace GOAP.Scripts
{
    public class WanderTargetSensor : LocalTargetSensorBase
    {
        public override void Created()
        {
        }

        public override void Update()
        {
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var random = GetRandomPosition(agent);
            return new PositionTarget(random);
        }

        private Vector3 GetRandomPosition(IMonoAgent agent)
        {
            var gos = GameObject.FindGameObjectsWithTag("Gondola");
            var index = Random.Range(0, gos.Length);
            var position = gos[index].transform.position;
            return position;
        }
    }
}