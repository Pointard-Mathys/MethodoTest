// See https://aka.ms/new-console-template for more information

using meteoCode;
using Processor.Services;

var processor = new StringProcessor();
var result = processor.Reverse("Hello, World!");
Console.WriteLine(result);
