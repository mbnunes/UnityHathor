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

        //public Text logInputTextValue;

        // Return the balance of HTR
        public static IEnumerator WalletBalance(string token=null)
        {
            UnityWebRequest www = null;

            if(token!=null)
                www = UnityWebRequest.Get("http://localhost:8000/wallet/balance?token="+token);
            else
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

        // Get the index of an address
        public static IEnumerator AddressIndex(string address)
        {
            UnityWebRequest www = null;

            www = UnityWebRequest.Get("http://localhost:8000/wallet/address-index?address="+address);

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

        // Return the wallet status
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

        // Return all generated addresses of the wallet.
        public static IEnumerator WalletAddresses()
        {
            UnityWebRequest www = UnityWebRequest.Get("http://localhost:8000/wallet/addresses");

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

        //Return the current address
        public static IEnumerator GetAddress(string mark_as_used=null, int index=0)
        {
            UnityWebRequest www = null;

            if(mark_as_used == null && index == 0){
                www = UnityWebRequest.Get("http://localhost:8000/wallet/address");
            } else if(mark_as_used != null && index == 0 ){
                www = UnityWebRequest.Get("http://localhost:8000/wallet/address?mark_as_used="+mark_as_used);
            } else if(mark_as_used == null && index != 0 ){
                www = UnityWebRequest.Get("http://localhost:8000/wallet/address?index="+index.ToString());
            } else if (mark_as_used != null && index != 0) {
                www = UnityWebRequest.Get("http://localhost:8000/wallet/address?mark_as_used="+mark_as_used+"&index="+index.ToString());
            }


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


        // Create and start a wallet and add to store.
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

        // Send a transaction to exactly one output.
        // Old SendTxToPlayer
        public static IEnumerator SimpleSendTxTo(HathorTransactStruct _a)
        {

            WWWForm form = new WWWForm();
            form.AddField("address", _a.Address);
            form.AddField("value", _a.Amount);

            if (_a.Token != null)
                form.AddField("token", _a.Token);
            if (_a.ChangeAddress != null)
                form.AddField("change_address", _a.ChangeAddress);

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
                        Debug.Log("Transaction waiting in queue.");
                        HathorQueue.transactsQueue.Enqueue(_a);
                    }
                    //logInputTextValue.text = www.downloadHandler.text;
                }
            }
            yield return new WaitForSeconds(.3f);
        }

        // Send a transaction with many outputs.
        public static IEnumerator SendTxTo(HathorTransactStruct _a)
        {

            WWWForm form = new WWWForm();
            form.AddField("outputs", JsonUtility.ToJson(_a.OutputsArray));

            if (_a.InputsArray != null)
                form.AddField("inputs", JsonUtility.ToJson(_a.InputsArray));
            if (_a.ChangeAddress != null)
                form.AddField("change_address", _a.ChangeAddress);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/wallet/send-tx", form))
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
                        Debug.Log("Transaction waiting in queue.");
                        HathorQueue.transactsQueue.Enqueue(_a);
                    }
                    //logInputTextValue.text = www.downloadHandler.text;
                }
            }
            yield return new WaitForSeconds(.3f);
        }

        // Create NFT in game
        public static IEnumerator CreateNFT(HathorTransactStruct _a)
        {

            WWWForm form = new WWWForm();
            form.AddField("name", _a.Name);
            form.AddField("symbol", _a.Symbol);
            form.AddField("amount", _a.Amount);
            form.AddField("data", _a.Data);

            if (_a.Address != null)
                form.AddField("address", _a.Address);
            if (_a.ChangeAddress != null)
                form.AddField("change_address", _a.ChangeAddress);
            if (_a.CreateMint)
                form.AddField("create_mint", _a.CreateMint.ToString());
            if (_a.CreateMelt)
                form.AddField("create_melt", _a.CreateMelt.ToString());

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/wallet/create-nft", form))
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
                        Debug.Log("Transacao aguardando na fila.");
                        HathorQueue.transactsQueue.Enqueue(_a);
                    }
                    //logInputTextValue.text = www.downloadHandler.text;
                }
            }
            yield return new WaitForSeconds(.3f);
        }

        //Create a token.
        public static IEnumerator CreateToken(HathorTransactStruct _a)
        {

            WWWForm form = new WWWForm();
            form.AddField("name", _a.Name);
            form.AddField("symbol", _a.Symbol);
            form.AddField("amount", _a.Amount);

            if (_a.Address != null)
                form.AddField("address", _a.Address);
            if (_a.ChangeAddress != null)
                form.AddField("change_address", _a.ChangeAddress);


            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/wallet/create-token", form))
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
                        Debug.Log("Transaction waiting in queue.");
                        HathorQueue.transactsQueue.Enqueue(_a);
                    }
                    //logInputTextValue.text = www.downloadHandler.text;
                }
            }
            yield return new WaitForSeconds(.3f);
        }

        //Mint a token.
        public static IEnumerator MintToken(HathorTransactStruct _a)
        {

            WWWForm form = new WWWForm();
            form.AddField("token", _a.Token);
            form.AddField("address", _a.Address);
            form.AddField("amount", _a.Amount);

            if (_a.ChangeAddress != null)
                form.AddField("change_address", _a.ChangeAddress);


            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/wallet/mint-tokens", form))
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
                        Debug.Log("Transaction waiting in queue.");
                        HathorQueue.transactsQueue.Enqueue(_a);
                    }
                    //logInputTextValue.text = www.downloadHandler.text;
                }
            }
            yield return new WaitForSeconds(.3f);
        }

        //Melt a token.
        public static IEnumerator MeltToken(HathorTransactStruct _a)
        {

            WWWForm form = new WWWForm();
            form.AddField("token", _a.Token);
            form.AddField("amount", _a.Amount);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/wallet/melt-tokens", form))
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
                        Debug.Log("Transaction waiting in queue.");
                        HathorQueue.transactsQueue.Enqueue(_a);
                    }
                    //logInputTextValue.text = www.downloadHandler.text;
                }
            }
            yield return new WaitForSeconds(.3f);
        }

        // Return the data of a transaction, if it exists in the wallet
        public static IEnumerator Transaction(string id)
        {
            UnityWebRequest www = null;

            www = UnityWebRequest.Get("http://localhost:8000/wallet/transaction?id="+id);

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

        // Return the transaction history
        public static IEnumerator TxHistory(int limit=0)
        {
            UnityWebRequest www = null;

            if (limit != 0)
                www = UnityWebRequest.Get("http://localhost:8000/wallet/tx-history?limit="+limit.ToString());
            else
                www = UnityWebRequest.Get("http://localhost:8000/wallet/tx-history");

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

        // Stop a running wallet and remove from store.
        public static IEnumerator WalletStop()
        {
            UnityWebRequest www = null;

            www = UnityWebRequest.Get("http://localhost:8000/wallet/stop");

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

        // Get information of a given address.
        public static IEnumerator AddressInfo(string address, string token = null)
        {
            UnityWebRequest www = null;

            if (token != null)
                www = UnityWebRequest.Get("http://localhost:8000/wallet/address-info?address="+address+"&token="+token);
            else
                www = UnityWebRequest.Get("http://localhost:8000/wallet/address-info?address="+address);


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

    }
}
