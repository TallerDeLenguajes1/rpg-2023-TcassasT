using EspacioPersonaje;
using EspacioFabricaDePersonajes;
using EspacioPersonajesJson;
using EspacioFabricaDeBebidas;
using EspacioBebida;

internal class Program {
  private const string ArchivoPersonajes = "personajes.json";
  private const int ConstanteDeAjuste = 500;
  private static void Main(string[] args) {
    int velocidadCombate = getVelocidadDeCombate();

    List<Personaje> listaDePersonajes = buscaArchivoDePersonajes(ArchivoPersonajes);
    List<Personaje> listaDeCombatientes = organizarCombates(listaDePersonajes);

    int indiceCombate = 0;
    while (listaDeCombatientes.Count() >= 2) {
      Console.Clear();
      WriteLineSistema("== Combatientes en juego: " + listaDeCombatientes.Count() + " ==");
      Thread.Sleep(3000);

      int ganadorDeCombate = iniciarCombate(listaDeCombatientes[indiceCombate], listaDeCombatientes[indiceCombate + 1], velocidadCombate);

      Bebida bebidaAleatoria = FabricaDeBebidas.getRandomBebida();
      WriteLineAviso("\nRey de la arena:");
      WriteLineAviso("  ¡Denle su merecido premio al ganador, elige una bebida!");
      Thread.Sleep(5000);
      WriteLineAviso("\nBartender de la arena:");
      WriteLineAviso("  Using " + bebidaAleatoria.ingredientesAsString());
      WriteLineAviso("  By " + bebidaAleatoria.Instructions.ToLower() + " I give you " + bebidaAleatoria.Name + "!");
      Thread.Sleep(6000);

      if (ganadorDeCombate == 1) {
        listaDeCombatientes[indiceCombate].bonusVictoria(bebidaAleatoria);
        WriteLineSistema("\n" + listaDeCombatientes[indiceCombate].anunciarResitencia(bebidaAleatoria));
        listaDeCombatientes[indiceCombate].curar();
        listaDeCombatientes.Remove(listaDeCombatientes[indiceCombate + 1]);
      } else {
        listaDeCombatientes[indiceCombate + 1].bonusVictoria(bebidaAleatoria);
        WriteLineSistema("\n" + listaDeCombatientes[indiceCombate + 1].anunciarResitencia(bebidaAleatoria));
        listaDeCombatientes[indiceCombate + 1].curar();
        listaDeCombatientes.Remove(listaDeCombatientes[indiceCombate]);
      }

      Thread.Sleep(6000);

      indiceCombate += 1;

      if (indiceCombate + 2 > listaDeCombatientes.Count()) {
        indiceCombate = 0;
      }
    }

    mostrarInfoGanador(listaDeCombatientes[0]);
  }

  static private int getVelocidadDeCombate() {
    int velocidadCombate;

    WriteLineSistema("Elija la velocidad de los combates:");
    WriteLineSistema("    1- Rapida");
    WriteLineSistema("    2- Media");
    WriteLineSistema("    3- Lenta");

    if (
      !int.TryParse(Console.ReadLine(), out velocidadCombate) ||
      velocidadCombate < 1 ||
      velocidadCombate > 3
    ) {
      Console.Clear();
      WriteLineError("x Opción incorrecta, por favor intente de nuevo.");
      return getVelocidadDeCombate();
    }

    Console.Clear();

    return velocidadCombate;
  }

  static private void mostrarInfoGanador(Personaje combatienteGanador) {
    Console.Clear();
    WriteLineExito("== ¡Fin del torneo! ==");
    WriteLineExito("x Ganador: " + combatienteGanador.Nombre + " \"" + combatienteGanador.Apodo + "\"" + ", el " + combatienteGanador.Tipo);
    WriteLineExito("x Velocidad: " + combatienteGanador.Velocidad);
    WriteLineExito("x Destreza: " + combatienteGanador.Destreza);
    WriteLineExito("x Fuerza: " + combatienteGanador.Fuerza);
    WriteLineExito("x Nivel: " + combatienteGanador.Nivel);
    WriteLineExito("x Armadura: " + combatienteGanador.Armadura);
    WriteLineExito("x Salud: " + combatienteGanador.Salud);
    WriteLineExito("x Tipo: " + combatienteGanador.Tipo);
    WriteLineExito("x Nombre: " + combatienteGanador.Nombre);
    WriteLineExito("x Apodo: " + combatienteGanador.Apodo);
    WriteLineExito("x Fecha de nacimiento: " + combatienteGanador.FechaDeNacimiento.ToString("MM/dd/yyyy"));
    WriteLineExito("x Edad: " + combatienteGanador.Edad);
    WriteLineExito("x Bonus de salud: " + combatienteGanador.BonusSalud);
    WriteLineExito("x Recistencia al alcohol: " + combatienteGanador.ResistenciaAlAlcohol);
  }

  static private int iniciarCombate(Personaje combatienteUno, Personaje combatienteDos, int velocidadCombate) {
    int turno = 1;

    do {
      WriteLineSistema("== Inicio combate entre " + combatienteUno.Nombre + " \"" + combatienteUno.Apodo + "\" y " + combatienteDos.Nombre + " \"" + combatienteDos.Apodo + "\" ==");
      if (turno % 2 != 0) {
        int efectividad = FabricaDePersonajes.getRandomInt(0, 100);
        int danioProvocado =
          ((combatienteUno.getAtaque() * efectividad) - combatienteDos.getDefensa()) / ConstanteDeAjuste;
        
        writeLineCombate(combatienteUno, combatienteDos, turno, danioProvocado);

        combatienteDos.Salud -= danioProvocado;
      } else {
        int efectividad = FabricaDePersonajes.getRandomInt(0, 100);
        int danioProvocado =
          ((combatienteDos.getAtaque() * efectividad) - combatienteUno.getDefensa()) / ConstanteDeAjuste;

        writeLineCombate(combatienteUno, combatienteDos, turno, danioProvocado);
        
        combatienteUno.Salud -= danioProvocado;
      }

      Thread.Sleep(400 * velocidadCombate);
      Console.Clear();

      turno += 1;

      if (
        turno >= 15 && (
          combatienteUno.Salud == 100 + combatienteUno.BonusSalud ||
          combatienteDos.Salud == 100 + combatienteDos.BonusSalud
        )
      ) {
        WriteLineSistema("x Pasaron mas de 15 turnos en los que uno de los combatientes no recibió daño.");
        WriteLineSistema("x Se procede a eliminar un combatiente basado en su cantiad de victorias, o aleatoriamente...");

        Thread.Sleep(1000);

        int indiceCombatienteEliminado = 0;
        if (combatienteUno.BonusSalud > combatienteDos.BonusSalud) {
          indiceCombatienteEliminado = 2;
        } else if (combatienteDos.BonusSalud > combatienteUno.BonusSalud) {
          indiceCombatienteEliminado = 1;
        } else {
          indiceCombatienteEliminado = FabricaDePersonajes.getRandomInt(1, 2);
        }

        WriteLineSistema("x Combatiente numero " + indiceCombatienteEliminado + " eliminado.");
        return indiceCombatienteEliminado;
      }

      if (turno > 30) {
        WriteLineSistema("x ¡Pasaron 30 turnos!");
        WriteLineSistema("x Se procede a eliminar al combatiente con menor salud.");

        Thread.Sleep(1000);

        int indiceCombatienteEliminado = 0;
        if (combatienteUno.Salud > combatienteDos.Salud) {
          indiceCombatienteEliminado = 2;
        } else {
          indiceCombatienteEliminado = 1;
        }

        WriteLineSistema("x Combatiente numero " + indiceCombatienteEliminado + " eliminado.");
        return indiceCombatienteEliminado;
      }
    } while (combatienteUno.Salud > 0 && combatienteDos.Salud > 0);

    int ganador = 0;
    if (combatienteUno.Salud > 0) {
      ganador = 1;
      WriteLineExito("== ¡Ganador del combate: " + combatienteUno.Apodo + "! ==");
    } else {
      ganador = 2;
      WriteLineExito("== ¡Ganador del combate: " + combatienteDos.Apodo + "! ==");
    }

    return ganador;
  }

  static private void writeLineCombate(Personaje combatienteUno, Personaje combatienteDos, int turno, int danioProvocado) {
    WriteLineSistema("x Turno " + turno + " - Ataca " + ((turno % 2 == 0) ? combatienteUno.Apodo : combatienteDos.Apodo));
    WriteLineSistema("x " + combatienteUno.Apodo + ": " + combatienteUno.Salud + " HP      " + combatienteDos.Apodo + ": " + combatienteDos.Salud + " HP\n");
    WriteLineSistema("Ataque inflige " + danioProvocado);
  }

  static private List<Personaje> organizarCombates(List<Personaje> listaDeCombatientes) {
    List<Personaje> listaReorganizada = new List<Personaje>();

    while (listaDeCombatientes.Count() > 0) {
      int indiceRandom = FabricaDePersonajes.getRandomInt(0, listaDeCombatientes.Count());
      listaReorganizada.Add(listaDeCombatientes[indiceRandom]);
      listaDeCombatientes.RemoveAt(indiceRandom);
    }

    return listaReorganizada;
  }

  static private List<Personaje> buscaArchivoDePersonajes(string nombreDeArchivo) {
    List<Personaje> listaDePersonajes = new List<Personaje>();

    WriteLineSistema("Checkeando existencia del archivo...");
    if (PersonajesJson.Existe(nombreDeArchivo)) {
      WriteLineExito("¡Archivo encontrado!");
      listaDePersonajes = PersonajesJson.LeerPersonajes(nombreDeArchivo);
      Thread.Sleep(2000);
    } else {
      WriteLineAviso("Archivo no encontrado, creando y populando...");
      Thread.Sleep(2000);

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

  static private void WriteLineError(string texto) {
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.WriteLine(texto);
    Console.ResetColor();
  }
}
