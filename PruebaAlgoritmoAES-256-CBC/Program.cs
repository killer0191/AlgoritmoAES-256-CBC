// See https://aka.ms/new-console-template for more information
using PruebaAlgoritmoAES_256_CBC;

EncriptacionService _encriptacionService = new EncriptacionService();
string text = "Hola mundo 123 asd";

var encrtypt = _encriptacionService.EncryptText(text);
Console.WriteLine($"Resultado encriptado: {encrtypt}");

var desEncrypt = _encriptacionService.DesEncryptText(encrtypt);
Console.WriteLine($"Resultado desencriptado: {desEncrypt}");