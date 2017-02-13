# Shepherd-Display-Unity

## Développement
### Récupérer les codes
```
git clone https://github.com/ENSTA-Bretagne-Shepherd/Shepherd-Display-Unity.git
```
```
git checkout -b develop origin/develop
```

### Lancer le programme Unity

* Lancez Unity
* Ouvrez le projet que vous avez cloné dans l'étape précédante
* Dans la partie inférieure de l'éditeur, Onglet Project -> Assets -> Scene
* Vous pouvez maintenant cliquer sur Play

### Compiler et lancez le client (linux)

Allez dans le répertoire client-api
```
cd client-api
```
Créer un répertoire (ex: build) et se déplacer 
```
mkdir build; cd build
```
Compiler le code
```
cmake ..; make
```
Lancer le programme
```
./test
```
### Ajouter des éléments au projet
Lorsque vous voulez ajouter du code au projet n'ajoutez que les répertoires Assets et ProjectSettings (pensez à vérifier que vous êtes bien dans la branche develop).

Ajouter un fichier
```
git add Assets/
git add ProjectSettings/
```
Commit les changements
```
git commit -m "Votre message"
```
Push les changements
```
git push
```

### Fonctionnement du client
Le client et le serveur utilisent [JSON](http://www.json.org/) pour communiquer. Actuellement il communique uniquement en localhost et sur le port 13000. Un exemple d'utilisation est présenté dans le fichier [main.cpp](https://github.com/ENSTA-Bretagne-Shepherd/Shepherd-Display-Unity/blob/develop/client-api/src/main.cpp).

### Fonctionnement de l'afficheur
La scène contient trois éléments :
* une lumière
* une camera
* un SimulationManager

L'objet SimulationManager contient deux scripts :
* TCPserver
* SimulationManager

TCPserver est un serveur qui écoute sur le port 13000. Les données reçues ne sont pas traitées, elles sont mises dans une file.
SimulationManager lit cette file et parse les données. Si les données correspondent à un AUV connu, ces paramètres (ex: x,y) sont modifiés. Si l'AUV n'existe pas il est créé.


## TODO

### Scene
* Ajouter le modèle des voiliers
* Ajouter le modèle des bouées
* Compléter la scene (eau, ciel, etc ...)

### Client-Server
* Ajouter des commandes

### SimulationManager
* Remplacer la modification directe des coordonnées des AUVs par un script associé à un AUV pour permettre l'ajout de fonctionnalitées plus facilement par la suite
* Ajouter les bouées


### Autres
* Améliorer ce README

## Changelog

[06/12/2016] Premier commit (TCPserver, AuvBehaviour, CameraBehaviour et SimulationManager)

[11/12/2016] Ajout du client-server utilisant JSON, ajout dynamique d'objet à la scène.

