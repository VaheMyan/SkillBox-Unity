using UnityEngine;
using Unity.Entities;

namespace DefultNamespace
{
    public class DogMoveSystem : ComponentSystem
    {
        private EntityQuery _query;
        protected override void OnCreate()
        {
            _query = GetEntityQuery(ComponentType.ReadOnly<Transform>());
        }

        protected override void OnUpdate()
        {

        }
    }

}
