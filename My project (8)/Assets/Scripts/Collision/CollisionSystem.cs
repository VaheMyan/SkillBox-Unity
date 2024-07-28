using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using System;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery _collisionQuery;
    private EntityQuery _getHitQuery;

    public Collider[] _results = new Collider[50];
    public int size = 0;

    protected override void OnCreate()
    {
        _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(), ComponentType.ReadOnly<Transform>());
        _getHitQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        var dsManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Entities.With(_getHitQuery).ForEach(
            (Entity entity, ref InputData inputData) =>
            {
                if (dsManager.Exists(entity))
                {
                    inputData.CollideInput = size;
                }
                else
                {
                    Debug.LogWarning("Попытка обновить несуществующую сущность (InputData)");
                }
            });

        Entities.With(_collisionQuery).ForEach(
            (Entity entity, CollisionAbility abilityCollision, ref ActorColliderData colliderData) =>
            {
                if (abilityCollision == null) return;

                if (dsManager.Exists(entity))
                {
                    var gameObject = abilityCollision.gameObject; // lracnum a position-y rotation-y gameObject-y
                    float3 position = gameObject.transform.position;
                    Quaternion rotation = gameObject.transform.rotation;

                    abilityCollision.collideInput = size;
                    abilityCollision.collisions?.Clear();

                    switch (colliderData.ColliderType)
                    {
                        case (UnityEngine.Tilemaps.Tile.ColliderType)ColliderType.Sphere:
                            size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position, // stugum e baxumy erb collider-y Sphere-a e =>
                                colliderData.SphereRadius, _results);
                            break;
                        case (UnityEngine.Tilemaps.Tile.ColliderType)ColliderType.Capsule:
                            var center =
                                ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2f;
                            var point1 = colliderData.CapsuleStart + position;
                            var point2 = colliderData.CapsuleEnd + position;
                            point1 = (float3)(rotation * (point1 - center)) + center;
                            point2 = (float3)(rotation * (point2 - center)) + center;
                            size = Physics.OverlapCapsuleNonAlloc(point1,
                                point2,
                                colliderData.CapsuleRadius, _results);
                            break;
                        case (UnityEngine.Tilemaps.Tile.ColliderType)ColliderType.Box:
                            size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position,
                                colliderData.BoxHalfExtents, _results, colliderData.BoxOrientation * rotation);
                            break;
                            throw new ArgumentOutOfRangeException();
                    }

                    if (size > 1)
                    {
                        foreach (var result in _results)
                        {
                            abilityCollision?.collisions?.Add(result);
                        }
                        abilityCollision.Execute();
                    }
                }
                else
                {
                    Debug.LogWarning("Попытка выполнить действие на несуществующей сущности (CollisionAbility)");
                }
            });
    }
}