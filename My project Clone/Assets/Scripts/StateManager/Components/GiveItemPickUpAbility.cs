using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Photon.Pun;

public class GiveItemPickUpAbility : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity, IItem
{
    public GameObject _UIItem;

    public List<GameObject> Targets { get; set; }
    public GameObject UIItem => _UIItem;

    private Entity _entity;
    private EntityManager _dsManager;

    public void Execute()
    {
        foreach (var target in Targets)
        {
            var character = target.GetComponent<CharacterData>();
            if (character == null) return;

            var item = Instantiate(UIItem, character.InventoryUIRoot.transform, false);
            var ability = item.GetComponent<IAbilityTarget>();
            ability.Targets.Add(target);

            Destroy(this.gameObject);
            _dsManager.DestroyEntity(_entity);
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dsManager = dstManager;
    }
}
