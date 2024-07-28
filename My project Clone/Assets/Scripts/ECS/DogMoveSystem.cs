using UnityEngine;
using Unity.Entities;

namespace DefultNamespace
{
    public class DogMoveSystem : ComponentSystem
    {
        private EntityQuery _query;
        protected override void OnCreate()
        {
            _query = GetEntityQuery(ComponentType.ReadOnly<DogMoveComponent>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_query).ForEach((Entity entity, Transform transform, DogMoveComponent dogMove) =>
            {
                var _position = transform.position;
                _position.y += (dogMove._moveSpeed / 1000);
                transform.position = _position;

            });
        }
    }

}
