using EspacioPersonaje;
using EspacioFabricaDePersonajes;
using EspacioPersonajesJson;

internal class Program {
  private const string ArchivoPersonajes = "personajes.json";
  private static void Main(string[] args) {
    List<Personaje> listaDePersonajes = buscaArchivoDePersonajes(ArchivoPersonajes);

    foreach (var personaje in listaDePersonajes) {
      WriteLineSistema(personaje.Apodo);
    }
  }

  static private List<Personaje> buscaArchivoDePersonajes(string nombreDeArchivo) {
    List<Personaje> listaDePersonajes = new List<Personaje>();

    WriteLineSistema("Checkeando existencia del archivo...");
    if (PersonajesJson.Existe(nombreDeArchivo)) {
      WriteLineExito("¡Archivo encontrado!");
      listaDePersonajes = PersonajesJson.LeerPersonajes(nombreDeArchivo);
    } else {
      WriteLineAviso("Archivo no encontrado, creando y populando...");

      do {
        FabricaDePersonajes fabricaPersonajes = new FabricaDePersonajes();
        PersonajesJson.GuardarPersonajes(fabricaPersonajes.generarListaDePersonajes(10), nombreDeArchivo);        
      } while (!PersonajesJson.Existe(nombreDeArchivo));

      WriteLineExito("¡Archivo creado correctamente!");
      listaDePersonajes = PersonajesJson.LeerPersonajes(nombreDeArchivo);
    }

    return listaDePersonajes;
  }

  static private void WriteLineSistema(string texto) {
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.DarkGray;
    Console.WriteLine(texto);
    Console.ResetColor();
  }

  static private void WriteLineAviso(string texto) {
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine(texto);
    Console.ResetColor();
  }

  static private void WriteLineExito(string texto) {
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine(texto);
    Console.ResetColor();
  }
}
