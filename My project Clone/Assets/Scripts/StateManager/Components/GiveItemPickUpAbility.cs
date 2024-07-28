using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Photon.Pun;
using Object = UnityEngine.Object;

public class GiveItemPickUpAbility : MonoBehaviour, IAbilityTarget, IItem
{
    public GameObject _UIItem;

    public List<GameObject> Targets { get; set; }
    public GameObject UIItem => _UIItem;

    private bool isTakePlace = false;
    private Entity _entity;
    private EntityManager _dsManager;

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (isTakePlace == false)
            {
                Debug.Log(target.name);
                var inventoryUIRoot = target.GetComponent<CharacterData>();
                if (inventoryUIRoot == null) return;
                if (inventoryUIRoot.InventoryUIRoot == null) return;

                var item = PhotonNetwork.Instantiate(UIItem.name, inventoryUIRoot.InventoryUIRoot.transform.position, inventoryUIRoot.InventoryUIRoot.transform.rotation);
                item.transform.SetParent(inventoryUIRoot.InventoryUIRoot.transform);
                item.transform.localScale = new Vector3(0.45f, 1.2f, 0);

                var ability = item.GetComponent<IAbilityTarget>();
                ability.Targets.Add(target);

                isTakePlace = true;

                var networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("I am MasterClient!");
                    PhotonNetwork.Destroy(this.gameObject);

                    if (_dsManager.Exists(_entity))
                    {
                        _dsManager.DestroyEntity(_entity);
                    }
                    else
                    {
                        Debug.LogWarning("Попытка уничтожить несуществующую сущность");
                    }
                }
                else
                {
                    Debug.Log("I am not MasterClient and I send message");
                    networkManager.IsDestroyedItem(this.gameObject.name);
                }
            }
        }
    }

    //public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    //{
    //    _entity = entity;
    //    _dsManager = dstManager;
    //}
}
