using EspacioBebida;
using System.Net;
using System.Text.Json;
using EspacioFabricaDePersonajes;

namespace EspacioFabricaDeBebidas;

public class FabricaDeBebidas {
  private const string urlBase = "https://api.api-ninjas.com/v1/cocktail?ingredients=";
  public static Bebida getRandomBebida() {
    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlBase + getRandomIngrediente());
    request.Method = "GET";
    request.ContentType = "application/json";
    request.Accept = "application/json";
    request.Headers.Add("X-Api-Key", "xWpJAPImxuMal9lrFzhf3Q==tLM11XUURQkYy5FN");
    
    try {
        using (WebResponse response = request.GetResponse()) {
            using (Stream strReader = response.GetResponseStream()) {
                if (strReader == null) return agua();

                using (StreamReader objReader = new StreamReader(strReader)) {
                    string responseBody = objReader.ReadToEnd();

                    if (string.IsNullOrEmpty(responseBody)) return agua();

                    List<Bebida> bebidas = JsonSerializer.Deserialize<List<Bebida>>(responseBody);

                    return bebidaOAgua(bebidas[FabricaDePersonajes.getRandomInt(0, bebidas.Count())]);
                }
            }
        }
    } catch (WebException ex) {
        Console.WriteLine("Problemas de acceso a la API: " + ex.Message);

        return agua();
    }
  }

  private static string getRandomIngrediente() {
    string[] ingredientes = {"vodka","tomato juice","tabasco sauce","lemon juice","water"};
    return ingredientes[FabricaDePersonajes.getRandomInt(0, ingredientes.Length)];
  }

  private static Bebida bebidaOAgua(Bebida bebidaAleatoria) {
    int rng = FabricaDePersonajes.getRandomInt(1, 4);
    if (rng == 4) {
      return agua();
    } else {
      return bebidaAleatoria;
    }
  }

  private static Bebida agua() {
    List<string> ingredientes = new List<string>();
    ingredientes.Add("agua");
    return new Bebida(ingredientes, "Abrir el grifo", "Agua");
  }
}
