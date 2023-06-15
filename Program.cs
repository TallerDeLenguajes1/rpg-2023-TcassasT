using EspacioPersonaje;
using EspacioFabricaDePersonajes;

internal class Program {
  private static void Main(string[] args) {
    FabricaDePersonajes fabricaDePersonajes = new FabricaDePersonajes();
    Personaje personaje = fabricaDePersonajes.generarPersonajeAleatorio();

    Console.WriteLine(personaje.Velocidad);
    Console.WriteLine(personaje.Destreza);
    Console.WriteLine(personaje.Fuerza);
    Console.WriteLine(personaje.Nivel);
    Console.WriteLine(personaje.Armadura);
    Console.WriteLine(personaje.Salud);
    Console.WriteLine(personaje.Tipo);
    Console.WriteLine(personaje.Nombre);
    Console.WriteLine(personaje.Apodo);
    Console.WriteLine(personaje.FechaDeNacimiento);
    Console.WriteLine(personaje.Edad);
  }
}