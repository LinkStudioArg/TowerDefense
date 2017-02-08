using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour {

	private Vector2 nodePosition;
    public GameObject selectedItem;
    public GameObject shopPanel;

    public void SetSelectedItem(GameObject selectedItem)
    {
        this.selectedItem = selectedItem;
    }

    public void SetNodePosition(Vector2 pos)
    {
        this.nodePosition = pos;
    }
    public void Show()
    {
        Debug.Log(nodePosition);
        shopPanel.SetActive(true);
    }

    public void CreateSelectedItem()
    {
        if (selectedItem != null)
        {
            GameObject.Instantiate(selectedItem, nodePosition, Quaternion.identity);
        }
    }

}
