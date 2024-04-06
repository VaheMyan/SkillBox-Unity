using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class DogAnimSystem : ComponentSystem
{
    private EntityQuery _query;
    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<MoveAnimData>(), ComponentType.ReadOnly<Animator>(), ComponentType.ReadOnly<AttackAnimData>(),
            ComponentType.ReadOnly<GetHitAnimData>(), ComponentType.ReadOnly<DieAnimData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, ref InputData move, Animator animator, UserInputData inputData, CharacterHealth characterHealth) =>
        {
            animator.SetBool(inputData.moveAnimHash, Math.Abs(move.Move.x) > 0.05f || Math.Abs(move.Move.y) > 0.05f); // Walk anim
            animator.SetBool(inputData.attackAnimHash, Math.Abs(move.Shoot) > 0f); // Attack aim
            animator.SetBool(inputData.getHitAnimHash, Math.Abs(move.CollideInput) > 1.5f && characterHealth._health > 0); // GetHit anim
            animator.SetBool(inputData.dieAnimHash, characterHealth.isDie);

            if (inputData.moveAnimSpeedHash == String.Empty) return;
            if (-move.Move.x == move.Move.y)
            {
                animator.SetFloat(inputData.moveAnimSpeedHash, inputData.speed * math.distancesq(move.Move.x, move.Move.y));
            }
            if (move.Move.x == move.Move.y)
            {
                animator.SetFloat(inputData.moveAnimSpeedHash, inputData.speed * math.distancesq(-move.Move.x, move.Move.y));
            }
            else
            {
                animator.SetFloat(inputData.moveAnimSpeedHash, inputData.speed * math.distancesq(move.Move.x, move.Move.y));
            }

        });
    }
}
