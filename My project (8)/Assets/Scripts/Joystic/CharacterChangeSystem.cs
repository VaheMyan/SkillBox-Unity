using UnityEngine;
using Unity.Entities;

public class CharacterChangeSystem : ComponentSystem
{
    private EntityQuery _changeQuery;

    protected override void OnCreate()
    {
        _changeQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<ChangeMaterialData>(), ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_changeQuery).ForEach((Entity entity, UserInputData inputData, ref InputData input) =>
        {
            if (input.ChangeMat > 0f && inputData.ChangeMaterialAction != null && inputData.ChangeMaterialAction is IAbility ability)
            {
                ability.Execute();
            }

        });
    }


}
