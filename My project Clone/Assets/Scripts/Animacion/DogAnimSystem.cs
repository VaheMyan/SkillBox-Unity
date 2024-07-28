using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Photon.Pun;

public class DogAnimSystem : ComponentSystem
{
    private EntityQuery _query;
    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<MoveAnimData>(), ComponentType.ReadOnly<Animator>(), ComponentType.ReadOnly<AttackAnimData>(),
            ComponentType.ReadOnly<GetHitAnimData>(), ComponentType.ReadOnly<DieAnimData>(), ComponentType.ReadOnly<InputData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, ref InputData move, Animator animator, UserInputData inputData, CharacterHealth characterHealth, PhotonView photonView) =>
        {
            if (animator != null && inputData != null && characterHealth != null)
            {
                animator.SetBool(inputData.moveAnimHash, Math.Abs(move.Move.x) > 0.05f || Math.Abs(move.Move.y) > 0.05f); // Walk anim
                animator.SetBool(inputData.attackAnimHash, Math.Abs(move.Shoot) > 0f); // Attack aim
                animator.SetBool(inputData.getHitAnimHash, Math.Abs(move.CollideInput) > 1.5f && characterHealth._health > 0); // GetHit anim
                animator.SetBool(inputData.dieAnimHash, characterHealth.isDie);

                float x = 0;
                float y = 0;
                if (move.Move.x < 0)
                {
                    x = -move.Move.x;
                }
                else
                {
                    x = move.Move.x;
                }
                if (move.Move.y < 0)
                {
                    y = -move.Move.y;
                }
                else
                {
                    y = move.Move.y;
                }
                float result = Mathf.Max(x, y);

                if (inputData.moveAnimSpeedHash == String.Empty) return;
                animator.SetFloat(inputData.moveAnimSpeedHash, 5 * result);
            }
            else
            {
                Debug.LogError("Problem");
            }
        });
    }
}
