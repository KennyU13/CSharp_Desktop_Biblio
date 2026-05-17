# CSharp_Desktop_Biblio

Application desktop de gestion de bibliotheque developpee en C# avec WPF.

## Fonctionnalites

- Gestion des livres
- Gestion des lecteurs
- Gestion des emprunts
- Interface desktop WPF
- Connexion a une base de donnees MySQL

## Technologies

- C#
- WPF
- .NET Framework 4.7.2
- MySQL
- Visual Studio

## Prerequis

- Windows
- Visual Studio 2019 ou plus recent
- .NET Framework 4.7.2 Developer Pack
- MySQL Server
- Connecteur MySQL pour .NET (`MySql.Data`)

## Installation

1. Cloner le depot :

```bash
git clone https://github.com/KennyU13/CSharp_Desktop_Biblio.git
```

2. Ouvrir la solution dans Visual Studio :

```text
GestionBiblio.sln
```

3. Restaurer ou ajouter la reference `MySql.Data` si Visual Studio ne la trouve pas.

4. Configurer la connexion a la base de donnees dans les fichiers du dossier `GestionBiblio/Model`.

5. Lancer le projet depuis Visual Studio.

## Structure du projet

```text
GestionBiblio/
  Model/       Modeles et acces aux donnees
  View/        Vues WPF
  Styles/      Styles XAML de l'interface
  Properties/  Ressources et parametres du projet
```

## Notes

Les dossiers generes par Visual Studio comme `.vs`, `bin` et `obj` sont ignores par Git.
