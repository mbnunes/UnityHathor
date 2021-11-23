using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityHathor
{
    public class HathorQueue : MonoBehaviour
    {
        public static Queue<HathorTransactStruct> transactsQueue = new Queue<HathorTransactStruct>();

        IEnumerator Start()
        {
            while (true)
            {
                if (transactsQueue.Count > 0)
                {
                    yield return new WaitForSeconds(.3f);
                    HathorTransactStruct _a = transactsQueue.Dequeue();
                    StartCoroutine(HathorConnect.SendTxToPlayer(_a));
                }
                yield return null;
            }
        }

        // Hathor - temporary gambiarra
        public static void ShowHathorCountPopup(GameObject hathorCountPopup, int hathorReward, Transform transform)
        {
            var go = Instantiate(hathorCountPopup, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = hathorReward.ToString();
        }
    }

    public struct HathorTransactStruct
    {
        public HathorTransactStruct(string address, int hathorAmount)
        {
            Address = address;
            HathorAmount = hathorAmount;
        }

        // Simple Send Tx - Send a transaction to exactly one output.
        public HathorTransactStruct(string address, int hathorAmount, string token=null, string change_address=null)
        {
            Address = address;
            HathorAmount = hathorAmount;
            Token = token;
            ChangeAddress = change_address;
        }

        // Send-Tx - Send a transaction with many outputs.
        public HathorTransactStruct(Outputs[] outputs, Inputs[] inputs=null, string change_address=null)
        {
            OutputsArray = outputs;
            InputsArray = inputs;
            ChangeAddress = change_address;
        }

        // Create a token.
        public HathorTransactStruct(string name, string symbol, int amount, string address=null, string change_address=null)
        {
            Name = name;
            Symbol = symbol;
            Amount = amount;
            Address = address;
            ChangeAddress = change_address;
        }

        // Mint tokens.
        public HathorTransactStruct(string token, int amount)
        {
            Token = token;
            Amount = amount;
        }

        // Melt tokens.
        public HathorTransactStruct(string token, string address, int amount, string change_address=null)
        {
            Token = token;
            Add = symbol;
            Amount = amount;
            ChangeAddress = change_address;
        }

        // Create NFT
        public HathorTransactStruct(string name, string symbol, int amount, string data, string address=null, string change_address=null, bool create_mint=null, bool create_melt=null)
        {
            Name = name;
            Symbol = symbol;
            Amount = amount;
            Data = data;
            Address = address;
            ChangeAddress = change_address;
            CreateMint = create_mint;
            CreateMelt = CreateMelt;
        }

        public string Name { get; }
        public string Symbol { get; }
        public string Amount { get; }
        public string Data { get; }
        public string Token { get; }
        public string ChangeAddress { get; }
        public bool CreateMint { get; }
        public bool CreateMelt { get; }
        public string Address { get; }
        public int HathorAmount { get; }
        public Outputs[] OutputsArray { get; }
        public Inputs[] InputsArray { get; }


    };

    [Serializable]
    public class Outputs
    {
        public string address;
        public int value;
        public string token;
    }

    [Serializable]
    public class Inputs
    {
        public string hash;
        public int index;
        public string type;
        public int max_utxos;
        public string token;
        public string filter_address;
        public int amount_smaller_than;
        public int amount_bigger_than;
    }
}



