using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHathor;

public class HathorPlayer : MonoBehaviour
{
    public string walletPlayer = "";
    private string x_wallet_id = "";
    private string passphrase = "";    

    // Start is called before the first frame update
    void Start()
    {
        HathorConnect.login = x_wallet_id;
        HathorConnect.passphrase = passphrase;        
        StartCoroutine(HathorConnect.StartWallet());
        // Show HTR Address 
        //StartCoroutine(HathorConnect.GetAddress());
    }

}
