# HathorUnity
### A melhor forma de você usar Hathor e tokens, em seus jogos usando a Unity3D

[![](https://miro.medium.com/max/2160/1*_wU--C55wtOHBDPJpy4sKw.jpeg)](https://hathor.network)                       

<p align="center">
  <img src="https://upload.wikimedia.org/wikipedia/commons/5/55/Unity3D_Logo.jpg">
</p>
HathorUnity são classes que fazem com que o Unity converse diretamente com a sua [Hathor Wallet Headless](https://github.com/HathorNetwork/hathor-wallet-headless) assim você pode fazer qualquer tipo de transação.

## Versão da Unity 
 - Unity 2020.3.3f1 (64-bit)

## Transações que já podem ser realizadas 

- Startar uma wallet
- Verificar o balance de uma wallet
- Enviar token para uma carteira, especificando a wallet destino e a quantidade
- Wallet status

## Installation

Vou especificar por etapas para facilitar:

 1. Copiar o repositorio para o seu projeto.
 2. Arrastar o prefab "HathorCountFloatingText" para a sua scene
 3. Adicionar o script HathorPlayer ao personagem principal 
 4. No seu personagem, vai no Inspector e adicione a wallet do player no campo Wallet Player
 5. No script HathorPlayer.cs voce adiciona o x_wallet_id e o passphare de onde vao sair os tokens 
 6. Nos inimigos ou itens na função OnDeath or OnDestroy adicionar o código abaixo:
 
```sh
//Hathor - Using namespace
using UnityHathor;

//Hathor - Start Variables Block
public GameObject hathorCountPopup;
public GameObject playerObject;
//Hathor - End Variables Block
```
```sh
// DENTRO DA FUNCAO Death do NPC ou Destroy do Objeto
// Hathor - Pega a carteira que foi inserida no inspector do Personagem
var walletPlayer = playerObject.GetComponent<HathorPlayer>().walletPlayer;
// Gera um valor aleatorio entre 0 e 5 que vai pagar de Hathor, onde 5 é 0.05 em Hathor
int hathorAmount = UnityEngine.Random.Range(0, 5);

// Hathor - Verifica se o hathorCountPopup não esta nullo
if (hathorCountPopup)
{
    // Hathor - Mostra a quantidade que recebeu em hathor ao matar o monstro.
    HathorQueue.ShowHathorCountPopup(hathorCountPopup, hathorAmount, transform);
}

// Hathor - Se o valor randomico for 0 ele nem tenta fazer a transação
if (hathorAmount > 0)
{                
    // Hathor - Instancia uma struct com a wallet do player e o valor de  Hathor que ele ganhou.
    HathorTransactStruct newTransact = new HathorTransactStruct(walletPlayer, hathorAmount);
    // Hathor - Adiciona a struct HathorTransactStruc na queue para ser processada pela classe HathorQueue
    HathorQueue.transactsQueue.Enqueue(newTransact);                
}  
```
## Doaçoes

 **Hathor**: HSC2KhChyZfHRLwTAwjc6bqqfebtvsL3xY


## License

BSD

**Free Software, Hell Yeah!**

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

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
