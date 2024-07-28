using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;

public class CharacterMovSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<MoveData>(), ComponentType.ReadOnly<Transform>(), ComponentType.ReadOnly<DogMoveComponent>()); // move
        // (nshan ***** MoveData() )


    }
    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach(
            (Entity entity, DogMoveComponent dogMoveComponent, ref InputData inputData, ref MoveData move) => // move
        {
            if (dogMoveComponent.transform != null)
            {
                if (dogMoveComponent.isTest) // Test
                {
                    inputData.Move = dogMoveComponent.Move;
                }

                var pos = dogMoveComponent.transform?.position;
                pos += new Vector3(inputData.Move.x * move.Speed, y: 0, inputData.Move.y * move.Speed);
                dogMoveComponent.transform.position = (Vector3)pos;

                var dir = new Vector3(inputData.Move.x, 0, inputData.Move.y);
                if (dir == Vector3.zero) return;

                var rot = dogMoveComponent.transform.rotation;
                var newRot = Quaternion.LookRotation(Vector3.Normalize(dir));
                if (newRot == rot) return;
                dogMoveComponent.transform.rotation = Quaternion.Lerp(rot, newRot, Time.DeltaTime * 10);
            }
        });
    }


}
