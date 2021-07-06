# Example

This example shows how to integrate a custom Hathor token to the demo Unity game. This will draw tokens from a central wallet and distribute it to the player's wallet. In this example, the player's wallet will be hardcoded in the config file. In a real integration, each player should be allocated a wallet address dynamically.

## Requirements

1. Hathor Headless wallet installed and running. This should be loaded and have tokens available. You can use any custom token or HTR. For information on how to run a headless wallet click [here](https://github.com/HathorNetwork/hathor-wallet-headless)
2. Unity Hub;
3. Text Editor

## Integration
	
### Preparing the Environment 
1. Download Unity Hub
2. After the download is completed, run the installation program
3. Once the installation was finished and the Unity Hub is running on your machine, create a new project
4. Give your project a name
5. On your new project, select Window / Assets Store
6. A new browser window with the Unity Store will be opened, on this screen search for 3d Game Kit
7. On the search results, click on the 3D Game Kit, the 3d Game Kit will be opened
8. Select Add to my assets, this will add the game to your assets
9. Click on Open in Unity
10. The Package Manager window will open with the game selected, click on Import
11. A Warning message will appear, on this message click Import
12. A second window will open informing about dependencies, click on Install/Upgrade
13. A third window called Import Unity Package will open, select Import
14. After the asset is imported to your project, close the Package manager window.
15. Download the project to your local machine
16. Drag and drop the project directory to your Unity project Assets
	
### Setting up the integration
1. On your assets, select the UnityHathor directory
2. Select Scripts
3. On the Scripts directory Open HathorConnect file
4. Change the URL address to the correct headless wallet address that you are using.
5. Save the file
6. On the Scripts directory open HathorPlayer file
7. Change the `x_wallet_id` field with the correct name of your wallet, which you just set up
8. Save the file

### Adding Token payments to the game
1. On your assets open the 3dGameKit, Scenes, GamePlay directory
2. Select and open the Level1 file
3. Once it opens, on your assets go to the HathorUnity, Prefabs directory
4. Select the HathorCountFloatingText.prefab file
5. Drag the file to the Level1, System on the Hierarchy list.
6. On your assets go to 3DGameKit, Prefabs, Characters, Ellen and select Ellen.prefab
7. Open the file
8. On the Inspector window, go to the end of the file and click on Add Component
9. On the New component window, select HathorPlayer
10. Open your hathor Wallet (Desktop or Mobile) and copy your wallet address
11. Paste the wallet address on the WalletPlayer field
12. Save the file
13. On your assets select 3DGameKit, Scripts, Game, Enemies, Chomper directory
14. On the directory open the ChomperBehaviour.cs file
15. Import `UnityHathor`
```csharp
using UnityEngine;

using UnityHathor;  // add this line
```

16. Create the Hathor variables, after line 21
```csharp
public static readonly int hashIdleState = Animator.StringToHash("ChomperIdle");

//Hathor - Start Variables Block
public GameObject hathorCountPopup;
public GameObject playerObject;
//Hathor - End Variables Block
```

17. Add code to transfer tokens inside death or destroy functions (around line 243)
```csharp
// Inside your death or destroy functions
// Hathor - Grab the wallet inserted in the character wallet
var walletPlayer = playerObject.GetComponent<HathorPlayer>().walletPlayer;

// generate a random value between 0 and 5 where 5 is 0.05 tokens
int hathorAmount = UnityEngine.Random.Range(0, 5);

// Hathor - Check if hathorCountPopup is no null
if (hathorCountPopup)
{
    // Hathor - Shows the amount of coins received
    HathorQueue.ShowHathorCountPopup(hathorCountPopup, hathorAmount, transform);
}

// Hathor - If the value is below or equal zero, do not attempt to transact
if (hathorAmount > 0)
{                
    // Hathor - Create a new struct with the player wallet and the value to be transacted
    HathorTransactStruct newTx = new HathorTransactStruct(walletPlayer, hathorAmount);
    // Hathor - Add the new struct to the transaction queue
    HathorQueue.transactsQueue.Enqueue(newTx);               
}
```

18. Save the file
19. On your assets go to 3DGameKit, Prefabs, Character, Enemies, Chomper
20. Select the Chomper.prefab file and open it
21. On the inspector window search for the Hathor Count Popup property
22. Click on the Select Game Object icon
23. On the Select Game Object window, write HathorCountFloatingText
24. On the property Player Object and click on the Select Game Object icon
25. On the Select Game Object window, write Ellen
26. Run the game. For each Chomper that Ellen kills, you’ll receive new Tokens
