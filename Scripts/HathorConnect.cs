using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;

namespace UnityHathor
{    
    

    public class HathorConnect : MonoBehaviour
    {
        public static string login;        

        // seedKey configurada na hathor wallet headless, o correto � ter uma exclusiva para a conta que ter� todas os tokens e uma seedKey para os jogadores
        private static string seedKey = "default";
        // passphare do player
        public static string passphrase = "";

        // Wallet que receber� os tokens durante o jogo, local temporario.
        private static string walletPlayer = "";

        //public Text logInputTextValue;

        // Retorna em JSON o balanco da carteira do jogador.
        public static IEnumerator WalletBalance()
        {
            UnityWebRequest www = null;

            www = UnityWebRequest.Get("http://localhost:8000/wallet/balance");


            www.SetRequestHeader("X-Wallet-Id", login);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                //logInputTextValue.text = www.error;
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                //logInputTextValue.text = www.downloadHandler.text;

                // Or retrieve results as binary data
                //byte[] results = www.downloadHandler.data;
            }
        }

        // Retorna o status da carteira.
        public static IEnumerator WalletStatus()
        {
            UnityWebRequest www = UnityWebRequest.Get("http://localhost:8000/wallet/status");

            www.SetRequestHeader("X-Wallet-Id", login);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                //logInputTextValue.text = www.error;
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                //logInputTextValue.text = www.downloadHandler.text;

                // Or retrieve results as binary data
                //byte[] results = www.downloadHandler.data;
            }
        }


        //Retorna 1 endere�o da conta.
        public static IEnumerator GetAddress()
        {
            UnityWebRequest www = UnityWebRequest.Get("http://localhost:8000/wallet/address");

            www.SetRequestHeader("X-Wallet-Id", login);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                //logInputTextValue.text = www.error;
            }
            else
            {
                // Show results as text
                Debug.Log(login+" - "+www.downloadHandler.text);
                //logInputTextValue.text = www.downloadHandler.text;

                // Or retrieve results as binary data
                //byte[] results = www.downloadHandler.data;
            }
        }


        // Inicia a wallet, caso ela nao exista, ela � criada. Caso existente a API retorna que j� esta criada
        public static IEnumerator StartWallet()
        {
            WWWForm form = new WWWForm();
            form.AddField("wallet-id", login);
            form.AddField("passphrase", passphrase);
            form.AddField("seedKey", seedKey);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/start", form))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log(www.error);
                    //logInputTextValue.text = www.error;
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    //logInputTextValue.text = www.downloadHandler.text;
                }
            }
        }

        // Envia para somente 1 wallet destino.
        public static IEnumerator SendTxToPlayer(HathorTransactStruct _a)
        {           

            WWWForm form = new WWWForm();
            form.AddField("address", _a.WalletPlayerParam);
            form.AddField("value", _a.HathorAmount);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/wallet/simple-send-tx", form))
            {
                www.SetRequestHeader("X-Wallet-Id", login);                
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log(www.error);
                    //logInputTextValue.text = www.error;
                }
                else
                {                    
                    //Debug.Log(www.downloadHandler.text);
                    if (www.downloadHandler.text.Contains("You already have a transaction being sent. Please wait until it's done to send another"))
                    {
                        Debug.Log("Transa��o aguardando na fila.");
                        HathorQueue.transactsQueue.Enqueue(_a);
                    }
                    //logInputTextValue.text = www.downloadHandler.text;
                }
            }
            yield return new WaitForSeconds(.3f);
        }

    }
}
