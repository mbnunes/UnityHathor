using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HathorPopup : MonoBehaviour
{
    public float DestroyTime = 10f;

    private void Start()
    {
        Destroy(gameObject, DestroyTime);    
    }

}
