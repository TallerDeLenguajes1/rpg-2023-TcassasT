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
    
    return Existe(nombreDeArchivo);
  }

  static public List<Personaje> LeerPersonajes(string nombreDeArchivo) {
    List<Personaje> listaDePersonajes = new List<Personaje>();

    if (Existe(nombreDeArchivo)) {
      string? contenidoDeArchivo = File.ReadAllText(nombreDeArchivo);

      listaDePersonajes = JsonSerializer.Deserialize<List<Personaje>>(contenidoDeArchivo);
    }

    return listaDePersonajes;
  }

  static public Boolean Existe(string nombreDeArchivo) {
    Boolean existeYTieneCotenido = false;

    if (File.Exists(nombreDeArchivo)) {
      string? contenidoDeArchivo = File.ReadAllText(nombreDeArchivo);

      if (!string.IsNullOrEmpty(contenidoDeArchivo)) {
        existeYTieneCotenido = true;
      }
    }

    return existeYTieneCotenido;
  }
}
