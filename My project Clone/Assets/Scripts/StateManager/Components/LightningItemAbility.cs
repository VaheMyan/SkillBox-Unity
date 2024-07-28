using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class LightningItemAbility : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity
{
    private Entity _entity;
    private EntityManager _dsManager;
    private MoveData moveData;

    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    public float bonusSpeed = 0.1f;
    public void Execute()
    {
        foreach (var target in Targets)
        {
            var userInputData = target.GetComponent<UserInputData>();
            userInputData.speed += bonusSpeed;
        }
        Destroy(this.gameObject);
    }
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dsManager = dstManager;
        moveData = _dsManager.GetComponentData<MoveData>(entity);
    }
}
