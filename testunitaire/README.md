# 🧪 Suite de Tests Unitaires en C# avec xUnit, Moq et FluentAssertions

Ce dépôt contient une suite de projets de démonstration pour les **tests unitaires en C#**, utilisant les outils suivants :

- ✅ [xUnit](https://xunit.net/) – Framework de test unitaire moderne et léger.
- 🔁 [Moq](https://github.com/moq/moq4) – Bibliothèque de mocking simple et puissante.
- ✨ [FluentAssertions](https://fluentassertions.com/) – Assertions lisibles, expressives et élégantes.

---

## 🧱 Structure du projet

## 🚀 Installation & Exécution

### 1. Cloner le dépôt
```bash
git clone https://github.com/Kaksloup/testunitaire.git
cd Exercice.Tests
```

### 2. Restaurer les dépendances
```bash
dotnet restore
```

### 3. Exécuter les tests
```bash
dotnet test
```

### 4. Générer un rapport de couverture de code (optionnel)
```bash
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:coveragereport -reporttypes:Html
```
### 🧪 Technologies utilisées
- xUnit	Framework de test unitaire
- Moq	Mocking de dépendances/interfaces
- FluentAssertions	Syntaxe fluide et lisible pour les assertions
- .NET 9	Plateforme cible pour les projets











