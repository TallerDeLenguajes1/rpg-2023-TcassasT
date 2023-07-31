namespace EspacioPersonaje;

using EspacioBebida;

public enum TIPOS {
  ORCO,
  ELFO,
  HUMANO,
  TROLL,
}

public class Personaje {
  private int velocidad;
  private int destreza;
  private int fuerza;
  private int nivel;
  private int armadura;
  private int salud;
  private TIPOS tipo;
  private string? nombre;
  private string? apodo;
  private DateTime fechaDeNacimiento;
  private int edad;
  private int bonusSalud;
  private int resistenciaAlAlcohol;

  public int Velocidad { get => velocidad; set => velocidad = value; }
  public int Destreza { get => destreza; set => destreza = value; }
  public int Fuerza { get => fuerza; set => fuerza = value; }
  public int Nivel { get => nivel; set => nivel = value; }
  public int Armadura { get => armadura; set => armadura = value; }
  public int Salud { get => salud; set => salud = value; }
  public TIPOS Tipo { get => tipo; set => tipo = value; }
  public string? Nombre { get => nombre; set => nombre = value; }
  public string? Apodo { get => apodo; set => apodo = value; }
  public DateTime FechaDeNacimiento { get => fechaDeNacimiento; set => fechaDeNacimiento = value; }
  public int Edad { get => edad; set => edad = value; }
  public int BonusSalud { get => bonusSalud; set => bonusSalud = value; }
  public int ResistenciaAlAlcohol { get => resistenciaAlAlcohol; set => resistenciaAlAlcohol = value; }

  public int getAtaque() {
    return this.Destreza * this.Fuerza * this.Nivel;
  }

  public int getDefensa() {
    return (this.Armadura * this.Velocidad);
  }

  public void curar() {
    this.Salud = 100 + this.BonusSalud;
  }

  public void bonusVictoria(Bebida bebida) {
    if (bebida.Name.Equals("Agua")) {
      aplicarBonus(2, 2, 2, 2);
    } else {
      if (this.ResistenciaAlAlcohol < 3) {
        aplicarBonus(-1, -1, 0, -1);
      } else if (this.ResistenciaAlAlcohol < 8) {
        aplicarBonus(0, 0, 0, 1);
      } else {
        aplicarBonus(-1, -1, 1, 1);
      }
    }

    this.BonusSalud += 10;
  }

  public string anunciarResitencia(Bebida bebida) {
    if(bebida.Name.Equals("Agua")) {
      return "¡Nada mejor que un vaso de agua! (++destreza, ++velocidad, ++fuerza, ++armadura";
    } else {
      if (this.ResistenciaAlAlcohol < 4) {
      return "¡" + this.Apodo + " no es para nada recistente al alcohol! (-destreza, -velocidad, -armadura)";
    } else if (this.ResistenciaAlAlcohol < 6) {
      return this.Apodo + " tiene una recistente al alcohol moderada (+armadura)";
    } else {
      return "¡" + this.Apodo + " es muy recistente al alcohol! (-destreza, -velocidad, +fuerza, +armadura)";
    }
    }
  }

  private void aplicarBonus(int bonusDestreza, int bonusVelocidad, int bonusFuerza, int bonusArmadura) {
    if (this.Destreza + bonusDestreza >= 0) {
      this.Destreza += bonusDestreza;
    }
    if (this.Velocidad + bonusVelocidad >= 0) {
      this.Velocidad += bonusVelocidad;
    }

    if (this.Fuerza + bonusFuerza >= 0) {
      this.Fuerza += bonusFuerza;
    }

    if (this.Armadura + bonusArmadura >= 0) {
      this.Armadura += bonusArmadura;
    }
  }
}
