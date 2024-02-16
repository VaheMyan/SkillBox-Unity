using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static BehaviourManager;

public class AIBehaviourSystem : ComponentSystem
{
    // sa ancnum e bolor Entity-neri vrayov voronq unen AIAgent, ev oghaki irakanacnum enq, nerkayis aktiv Behavior-y
    private EntityQuery _behaviourQuery;

    protected override void OnCreate()
    {
        _behaviourQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>()); // stanum enq bolor entity-nery voronq kan AIAgent-i vra
    }
    protected override void OnUpdate()
    {
        Entities.With(_behaviourQuery).ForEach((Entity entity, BehaviourManager manager) =>
        {
            manager._activeBehaviour?.Behaviour();
        });
    }

}
