// See https://aka.ms/new-console-template for more information

using Calculator;

var calculator = new Operation();
var result = calculator.Add(1, 2);

Console.WriteLine(result);

/*
 * Exercices à compléter
 * Implémentez les logiques métiers du programme
 * Testez tous les cas normaux pour chaque méthode
   Testez les cas limites (zéro, nombres négatifs)
   Testez les exceptions (DivideByZeroException)
   Utilisez [Theory] et [InlineData] pour tester plusieurs valeurs
 */