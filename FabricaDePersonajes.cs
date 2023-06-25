using EspacioPersonaje;
namespace EspacioFabricaDePersonajes;

public class FabricaDePersonajes {
  public Personaje generarPersonajeAleatorio() {
    Personaje personaje = new Personaje();
    int cantidadDeTipos = Enum.GetNames(typeof(TIPOS)).Length;
    int tipoAleatorio = getRandomInt(0, cantidadDeTipos);

    personaje.Velocidad = getRandomInt(1, 10);
    personaje.Destreza = getRandomInt(1, 5);
    personaje.Fuerza = getRandomInt(1, 10);
    personaje.Nivel = getRandomInt(1, 10);
    personaje.Armadura = getRandomInt(1, 10);
    personaje.Salud = 100;
    personaje.Tipo = (TIPOS)Enum.GetValues(typeof(TIPOS)).GetValue(tipoAleatorio);
    personaje.Nombre = getRandomNombre();
    personaje.Apodo = getRandomApodo();
    personaje.FechaDeNacimiento = getRandomDate();
    personaje.Edad = getRandomInt(0, 300);
    personaje.BonusSalud = 0;

    return personaje;
  }

  public List<Personaje> generarListaDePersonajes(int cantidad) {
    List<Personaje> listaDePersonajes = new List<Personaje>();

    for (int i = 0; i < cantidad; i++) {
      listaDePersonajes.Add(generarPersonajeAleatorio());
    }

    return listaDePersonajes;
  }

  static public int getRandomInt(int limiteMenor, int limiteMayor) {
    Random random = new Random();
    return random.Next(limiteMenor, limiteMayor);
  }

  static private DateTime getRandomDate() {
    Random random = new Random();
    int range = 1500 * 365;
    return DateTime.Today.AddDays(- random.Next(range)); 
  }

  static private string getRandomNombre() {
    string[] nombresDePersonajes = {
      "Aragorn",
      "Gandalf",
      "Frodo",
      "Bilbo",
      "Harry",
      "Hermione",
      "Dumbledore",
      "Gollum",
      "Katniss",
      "Thor",
      "Loki",
      "Leia",
      "Luke",
      "Neo",
      "Trinity"
    };

    return nombresDePersonajes[getRandomInt(0, nombresDePersonajes.Count())];
  }

  static private string getRandomApodo() {
    string[] apodosDePersonajes = {
      "Flash",
      "Shadow",
      "Ace",
      "Raptor",
      "Spike",
      "Viper",
      "Rocket",
      "Blaze",
      "Jinx",
      "Neo",
      "Breeze",
      "Rogue",
      "Wraith",
      "Vortex",
      "Phoenix"
    };

    return apodosDePersonajes[getRandomInt(0, apodosDePersonajes.Count())];
  }
}
