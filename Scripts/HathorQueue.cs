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
        public HathorTransactStruct(string walletPlayerParam, int hathorAmount)
        {
            WalletPlayerParam = walletPlayerParam;
            HathorAmount = hathorAmount;
        }
        public string WalletPlayerParam { get; }
        public int HathorAmount { get; }
    };
}


