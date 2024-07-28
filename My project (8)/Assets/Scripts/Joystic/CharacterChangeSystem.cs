using UnityEngine;
using Unity.Entities;

public class CharacterChangeSystem : ComponentSystem
{
    private EntityQuery _changeQuery;
    private EntityQuery _playExecuteQuery;
    bool startChanging = true;
    bool stopChanging = false;
    private bool _changeMaterialBool;
    protected override void OnCreate()
    {
        _changeQuery = GetEntityQuery(ComponentType.ReadOnly<ChangeAbility>());
        _playExecuteQuery = GetEntityQuery(ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_changeQuery).ForEach((Entity entity, ref ChangeMaterialData changeMaterialData, ChangeAbility changeAbility) =>
        {
            //
        });
        Entities.With(_playExecuteQuery).ForEach((Entity entity, ref InputData input, UserInputData inputData) =>
        {
            if (input.ChangeMat >= 0f && inputData.ChangeMaterialAction != null && inputData.ChangeMaterialAction is IAbility ability)
            {
                ability.Execute();
            }
        });
    }


}
