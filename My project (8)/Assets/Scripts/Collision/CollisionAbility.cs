using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Tilemaps;
using DefultNamespace;

public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
{
    public Collider Collider;
    InputData inputData;
    public List<MonoBehaviour> collisionActions = new List<MonoBehaviour>(); // tsegh petq a pahenq hghumner
    public List<IAbility> collisionActionsAbilities = new List<IAbility>();
    public int collideInput;

    [HideInInspector] public List<Collider> collisions;

    private void Start()
    {
        foreach (var action in collisionActions)
        {
            if (action is IAbility ability) // ete action-y lracvats a =>
            {
                collisionActionsAbilities.Add(ability);
            }
            else
            {
                Debug.LogError("Collision action must derive form IAbility!!!");
            }
        }
    }

    public void Execute()
    {
        //Debug.Log("Executing CollisionAbility");
        if (collisions == null)
        {
            Debug.LogError("Collisions list is null in CollisionAbility");
            return;
        }

        foreach (var action in collisionActionsAbilities)
        {
            if (action is IAbilityTarget actionTarget)
            {
                actionTarget.Targets = new List<GameObject>();
                collisions.ForEach(c =>
                {
                    if (c != null) actionTarget.Targets.Add(c.gameObject); // avelacnum enq Target-neri canki gameObject-neri vra
                });
            }
            //Debug.Log("Executing action: " + action.GetType().Name);
            action.Execute();
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        switch (Collider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData // vercnum a colllider-i kordinatnery
                {
                    ColliderType = (Tile.ColliderType)ColliderType.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    initialTakeOff = true
                });
                break;
            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = (Tile.ColliderType)ColliderType.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleRadius = capsuleRadius,
                    initialTakeOff = true
                });
                break;
            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = (Tile.ColliderType)ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    initialTakeOff = true
                });
                break;
        }

        Collider.enabled = false;
    }


}

public struct ActorColliderData : IComponentData
{
    public Tile.ColliderType ColliderType;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public quaternion BoxOrientation;
    public bool initialTakeOff;

}
public enum ColliderType // asum a collider-i tesaky
{
    Sphere = 0,
    Capsule = 1,
    Box = 2
}
