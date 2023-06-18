using EspacioPersonaje;
using EspacioFabricaDePersonajes;
using EspacioPersonajesJson;

internal class Program {
  private static void Main(string[] args) {
    FabricaDePersonajes fabricaDePersonajes = new FabricaDePersonajes();
    Personaje personaje = fabricaDePersonajes.generarPersonajeAleatorio();

    List<Personaje> listaDePersonajes = new List<Personaje>();
    listaDePersonajes.Add(personaje);

    PersonajesJson.GuardarPersonajes(listaDePersonajes, "personajes.json");

    var test = PersonajesJson.LeerPersonajes("personajes.json");

    Console.WriteLine(test[0].Apodo);
  }
}