using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static BehaviourManager;

public class AIEvaluateSystem : ComponentSystem
{
    // sa ancnum e bolor Entity-neri vrayov,
    // voronq unen component AIAgent voronq el irenc hertin,
    // ancnum en bolor Behaviour-ner irakanacnelov irenc "Evaluate()"-y,
    // ev stanalov ardyunq,
    // yntrum e ayn vor veradarcrel e amenic shaty,
    // ev dran dnum e inchpes aktiv поведения

    private EntityQuery _evaluteQuery;

    protected override void OnCreate()
    {
        _evaluteQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>()); // stanum enq bolor entity-nery voronq kan AIAgent-i vra
    }
    protected override void OnUpdate()
    {
        Entities.With(_evaluteQuery).ForEach((Entity entity, BehaviourManager manager) =>
        {
            float hightScore = float.MinValue;

            manager._activeBehaviour = null;

            foreach (var behaviour in manager.behaviours)
            {
                if (behaviour is IBehaviour ai)
                {
                    var currentScore = ai.Evaluate();
                    if (currentScore > hightScore)
                    {
                        hightScore = currentScore;
                        manager._activeBehaviour = ai;
                    }
                }
            }
        });
    }

}
