using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class DogAnimSystem : ComponentSystem
{
    private EntityQuery _query;

    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<AnimData>(), ComponentType.ReadOnly<Animator>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, ref InputData move, Animator animator, UserInputData inputData) =>
        {
            animator.SetBool(inputData.moveAnimHash, Math.Abs(move.Move.x) > 0.05f || Math.Abs(move.Move.y) > 0.05f);

            if (inputData.moveAnimSpeedHash == String.Empty) return;
            animator.SetFloat(inputData.moveAnimSpeedHash, inputData.speed * math.distancesq(move.Move.x, move.Move.y));
        });
    }
}
