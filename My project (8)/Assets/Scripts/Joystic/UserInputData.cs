using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Photon.Pun;

//stex hytararum es entity-nery UserInputData
public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity //glxavor    entity-nery haytararelu vayr
{
    public MonoBehaviour ShootAction; // stex pahelu enq ayn gortoghutyuny vory kapvats a krakelu knopkai het
    public MonoBehaviour RunAction;
    public MonoBehaviour ChangeMaterialAction;

    public float speed;
    public float runSpeed;
    public string moveAnimHash; // Move anim
    public string moveAnimSpeedHash;
    public string attackAnimHash; // Attack anim
    public string getHitAnimHash; // Hit anim
    public string dieAnimHash; // Die anim

    [HideInInspector] public bool isTestRun;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if (PhotonView.Get(this.gameObject).IsMine)
        {
            dstManager.AddComponentData(entity, new InputData()); // new InputData()
        }

        dstManager.AddComponentData(entity, new MoveData
        {
            Speed = speed

        }); // new MoveData()

        if (ShootAction != null && ShootAction is IAbility) // stugum a ardyoq ShootAction-y lracvats a
        {
            dstManager.AddComponentData(entity, new ShootData()); // ev lracnum a
        }

        if (RunAction != null && RunAction is IAbility) // stugum a ardyoq ShootAction-y lracvats a
        {
            dstManager.AddComponentData(entity, new RunData()); // ev lracnum a
        }

        if (ChangeMaterialAction != null && ChangeMaterialAction is IAbility) // stugum a ardyoq ShootAction-y lracvats a
        {
            dstManager.AddComponentData(entity, new ChangeMaterialData()); // ev lracnum a
        }

        if (moveAnimHash != String.Empty)
        {
            dstManager.AddComponentData(entity, new MoveAnimData());
        }
        if (attackAnimHash != String.Empty)
        {
            dstManager.AddComponentData(entity, new AttackAnimData());
        }
        if (getHitAnimHash != String.Empty)
        {
            dstManager.AddComponentData(entity, new GetHitAnimData());
        }
        if (dieAnimHash != String.Empty)
        {
            dstManager.AddComponentData(entity, new DieAnimData());
        }
    }
}

// Components
public struct InputData : IComponentData // InputData()
{
    public float2 Move;
    public float Shoot;
    public float Run;
    public float ChangeMat;
    public int CollideInput;
}

public struct MoveData : IComponentData // MoveData()    // es haytararum es CharacterMovSystem scripti hamar ytex petq a galis   (nshan ***** MoveData() )
{
    public float Speed;
}

public struct ShootData : IComponentData
{

}

public struct RunData : IComponentData
{

}

public struct ChangeMaterialData : IComponentData
{
    public int isDissolve;
}
public struct MoveAnimData : IComponentData
{

}
public struct AttackAnimData : IComponentData
{

}
public struct GetHitAnimData : IComponentData
{

}
public struct DieAnimData : IComponentData
{

}