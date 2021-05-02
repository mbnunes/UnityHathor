# HathorUnity
### The best way for you to use Hathor and tokens, in your games using a Unity3D

<p align="center">
  <a href="https://hathor.network"><img width="327" height="140" src="https://miro.medium.com/max/2160/1*_wU--C55wtOHBDPJpy4sKw.jpeg"></a>
  <a href="https://unity3d.com"><img width="260" height="140" src="https://upload.wikimedia.org/wikipedia/commons/5/55/Unity3D_Logo.jpg"></a>
</p>

UnityHathor is a class that allows Unity to talk directly to your Headless Hathor Wallet, so you can make any kind of transaction

## Unity Version 
 - Unity 2020.3.3f1 (64-bit)

## Already implemented transactions

- Start a wallet
- Check wallet balance
- Send tokens to a specific wallet
- Wallet status

## Installation

 1. Copy the repository to your projecy
 2. Dragg the prefab "HathorCountFloatingText" to your scene
 3. Add the HathorPlayer script to your main character 
 4. In your character, go to the inspector and add the player wallet in the field "Wallet Player"
 5. In your HathorPlayer.cs add  your o x_wallet_id e passphrase from where the tokens are coming from 
 6. In your enemies or OnDeath/OnDestroy  functions add the code below:

```sh
//Hathor - Using namespace
using UnityHathor;

//Hathor - Start Variables Block
public GameObject hathorCountPopup;
public GameObject playerObject;
//Hathor - End Variables Block
```
```sh
// Inside your death or destroy functions
// Hathor - Grab the wallet inserted in the character wallet
var walletPlayer = playerObject.GetComponent<HathorPlayer>().walletPlayer;
// generate a random value between 0 and 5 where 5 is 0.05 Hathor Coin
int hathorAmount = UnityEngine.Random.Range(0, 5);

// Hathor - Check if hathorCountPopup is no null
if (hathorCountPopup)
{
    // Hathor - SHows the amount of coind received
    HathorQueue.ShowHathorCountPopup(hathorCountPopup, hathorAmount, transform);
}

// Hathor - If the value is below or equal zero, do not attempt to transact
if (hathorAmount > 0)
{                
    // Hathor - Create a new struct with the player wallet and the value to be transacted
    HathorTransactStruct newTransact = new HathorTransactStruct(walletPlayer, hathorAmount);
    // Hathor - Add the new struct to the transaction queue
    HathorQueue.transactsQueue.Enqueue(newTransact);                
}  
```
## Donations

 **Hathor**: HSC2KhChyZfHRLwTAwjc6bqqfebtvsL3xY


## License

BSD

**Free Software, Hell Yeah!**

[//]: # "These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax"

[dill]: <https://github.com/joemccann/dillinger>
[git-repo-url]: <https://github.com/joemccann/dillinger.git>
[john gruber]: <http://daringfireball.net>
[df1]: <http://daringfireball.net/projects/markdown/>
[markdown-it]: <https://github.com/markdown-it/markdown-it>
[Ace Editor]: <http://ace.ajax.org>
[node.js]: <http://nodejs.org>
[Twitter Bootstrap]: <http://twitter.github.com/bootstrap/>
[jQuery]: <http://jquery.com>
[@tjholowaychuk]: <http://twitter.com/tjholowaychuk>
[express]: <http://expressjs.com>
[AngularJS]: <http://angularjs.org>
[Gulp]: <http://gulpjs.com>

[PlDb]: <https://github.com/joemccann/dillinger/tree/master/plugins/dropbox/README.md>
[PlGh]: <https://github.com/joemccann/dillinger/tree/master/plugins/github/README.md>
[PlGd]: <https://github.com/joemccann/dillinger/tree/master/plugins/googledrive/README.md>
[PlOd]: <https://github.com/joemccann/dillinger/tree/master/plugins/onedrive/README.md>
[PlMe]: <https://github.com/joemccann/dillinger/tree/master/plugins/medium/README.md>
[PlGa]: <https://github.com/RahulHP/dillinger/blob/master/plugins/googleanalytics/README.md>
