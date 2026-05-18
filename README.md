# Puroguramu

Application web ASP.NET Core Razor Pages pour gérer des contenus pédagogiques, des exercices et leur évaluation.

## Vue d’ensemble

Le projet est structuré en plusieurs couches :

- **`Puroguramu.App`** : application web principale
- **`Puroguramu.Domains`** : modèles métier et contrats
- **`Puroguramu.Infrastructures`** : accès aux données, repositories, EF Core, migrations

L’application utilise :

- **ASP.NET Core 6**
- **Razor Pages**
- **ASP.NET Core Identity**
- **Entity Framework Core**
- **SQLite** en développement
- **SQL Server** en production
- **Tailwind CSS** pour les styles front-end

## Prérequis

- [.NET SDK 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Node.js et npm si vous devez recompiler les assets front-end
- Une base de données :
    - SQLite en local
    - SQL Server en production

## Structure du projet

```text
Puroguramu.App/           Application web Razor Pages
Puroguramu.Domains/       Contrats et objets métier
Puroguramu.Infrastructures/  Accès aux données et implémentations techniques
