using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;

public class CharacterMovSystem : ComponentSystem
{
    private EntityQuery _moveQuery;
    private EntityQuery _runQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<MoveData>(), ComponentType.ReadOnly<Transform>()); // move
        // (nshan ***** MoveData() )
        _runQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<MoveData>()); // run


    }
    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach(
            (Entity entity, Transform transform, ref InputData inputData, ref MoveData move) => // move
        {
            if (transform != null)
            {
                var pos = transform?.position;
                pos += new Vector3(inputData.Move.x * move.Speed, y: 0, inputData.Move.y * move.Speed);
                transform.position = (Vector3)pos;

                var dir = new Vector3(inputData.Move.x, 0, inputData.Move.y);
                if (dir == Vector3.zero) return;

                var rot = transform.rotation;
                var newRot = Quaternion.LookRotation(Vector3.Normalize(dir));
                if (newRot == rot) return;
                transform.rotation = Quaternion.Lerp(rot, newRot, Time.DeltaTime * 10);
            }
        });

        Entities.With(_runQuery).ForEach((Entity entity, ref MoveData move) => // run
        {
            //move.Speed += 10; 

        });



    }


}
