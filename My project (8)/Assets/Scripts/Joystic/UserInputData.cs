using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

//stex hytararum es entity-nery UserInputData
public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity //glxavor    entity-nery haytararelu vayr
{
    public MonoBehaviour ShootAction; // stex pahelu enq ayn gortoghutyuny vory kapvats a krakelu knopkai het
    public MonoBehaviour RunAction;
    public MonoBehaviour ChangeMaterialAction;

    public float speed;
    public string moveAnimHash;
    public string moveAnimSpeedHash;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData()); // new InputData()
        dstManager.AddComponentData(entity, new MoveData
        {
            Speed = speed / 100

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
            dstManager.AddComponentData(entity, new AnimData());
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
public struct AnimData : IComponentData
{

}