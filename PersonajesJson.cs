namespace EspacioPersonajesJson;

using EspacioPersonaje;
using System.Text.Json; 

public class PersonajesJson {
  static public Boolean GuardarPersonajes(List<Personaje> listaDePersonajes, string nombreDeArchivo) {
    string? serializedPersonajes = JsonSerializer.Serialize(listaDePersonajes);
    string? nombreDeArchivoConExtension = 
      nombreDeArchivo.Contains(".json") ?
        nombreDeArchivo :
        nombreDeArchivo + ".json";

    using (StreamWriter sw = new StreamWriter(nombreDeArchivoConExtension)) {
      sw.WriteLine(serializedPersonajes);
    }
    
    return true;
  }

  static public List<Personaje> LeerPersonajes(string nombreDeArchivo) {
    List<Personaje> listaDePersonajes = new List<Personaje>();

    if (File.Exists(nombreDeArchivo)) {
      string? contenidoDeArchivo = File.ReadAllText(nombreDeArchivo);

      // TODO usar trycatch para quitar advertencia
      if (!string.IsNullOrEmpty(contenidoDeArchivo) && contenidoDeArchivo != null) {
        listaDePersonajes = JsonSerializer.Deserialize<List<Personaje>>(contenidoDeArchivo);
      }
    }

    return listaDePersonajes;
  }
}
