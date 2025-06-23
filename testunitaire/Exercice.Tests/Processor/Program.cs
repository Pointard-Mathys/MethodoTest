// See https://aka.ms/new-console-template for more information

using Processor;
using Processor.Services;

var processor = new StringProcessor();
var result = processor.Reverse("Hello, World!");
Console.WriteLine(result);

/*
 Exercices à compléter
   * Implémentez les logiques métiers du programme
 *  Tester toutes les méthodes avec plusieurs scénarios même limite
 * 
 * Tests pour IsPalindrome : cas normaux, chaînes vides, null, espaces
   Tests pour Reverse : chaînes normales, chaînes vides, null
   Tests pour CountWords : phrases normales, chaînes vides, espaces multiples
   Tests pour Capitalize : mots simples, phrases, cas limites
   
   Tests de validation du mot de passe : 
     Ajouter validation pour caractères spéciaux
     Créer des tests pour longueur maximale
     Tester les cas limites (null, espaces)
     Créer un [MemberData(nameof(PasswordWithErrors))] pour grouper tous les tests
   
 * Utilisez [MemberData] pour fournir des données de test complexes
   Implémentez des tests avec [ClassData]
   Utilisez [Skip] pour ignorer temporairement certains tests
   Explorez [Trait] pour catégoriser vos tests
*/