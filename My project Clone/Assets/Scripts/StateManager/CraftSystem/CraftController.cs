using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CraftController : MonoBehaviour
{
    public CraftSettings craftSettings;

    private List<ICraftable> items = new List<ICraftable>();
    private List<GameObject> selected = new List<GameObject>();

    public Transform UIItemsRoot;

    public void EnterCraftMode()
    {
        selected.Clear();
        items = GetComponentsInChildren<ICraftable>().ToList();
        foreach (var item in items)
        {
            var button = ((MonoBehaviour)item)?.gameObject.AddComponent<Button>();
            button.onClick.AddListener(() => { Select(button.gameObject); });
        }
    }

    private void Select(GameObject obj)
    {
        if (selected.Contains(obj))
        {
            selected.Remove(obj);
            obj.GetComponent<Image>().color = new Color(1, 1, 1);
        }
        else
        {
            selected.Add(obj);
            obj.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f, 0.7f);
        }

        CheckCombo();
    }

    private void CheckCombo()
    {
        List<string> selectedNames = new List<string>();
        foreach (var item in selected)
        {
            var n = item.GetComponent<ICraftable>().Name;
            selectedNames.Add(n);
        }
        foreach (var combination in craftSettings.combinations)
        {
            if (combination.sources.SequenceEqual(selectedNames))
            {
                Debug.Log("Match");
                foreach (var victim in selected)
                {
                    Destroy(victim);
                }
                var newItem = Instantiate(combination.result, UIItemsRoot);
            }
        }
    }
}
