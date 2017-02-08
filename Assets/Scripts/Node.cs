using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class Node : MonoBehaviour {

    public Vector2 position;
    private Shop shop;

    void Awake()
    {
        position = transform.position;
        shop = GameObject.Find("Shop").GetComponent<Shop>();
    }

	// Use this for initialization
	void OnEnable () {
        GetComponent<TapGesture>().Tapped += ShowShop;
	}
    void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= ShowShop;
    }
    private void ShowShop(object sender, EventArgs e)
    {
        shop.SetNodePosition(position);        
        shop.Show();
        shop.CreateSelectedItem();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
