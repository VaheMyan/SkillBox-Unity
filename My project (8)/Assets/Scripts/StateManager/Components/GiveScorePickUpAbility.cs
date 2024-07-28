using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class GiveScorePickUpAbility : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity
{
    public AK.Wwise.Event pickupEvent = null;

    public List<GameObject> Targets { get; set; }
    private Entity _entity;
    private EntityManager _dsManager;

    public void Execute()
    {
        foreach (var target in Targets)
        {
            var character = target.GetComponent<CharacterData>();
            if (character != null) character.Score(3);

            if (_dsManager.Exists(_entity))
            {
                pickupEvent.Post(this.gameObject);
                _dsManager.DestroyEntity(_entity);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogWarning("Попытка уничтожить несуществующую сущность");
            }
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dsManager = dstManager;
    }
}
