namespace EspacioBebida;

using System.Text.Json.Serialization;

public class Bebida {
  private List<string>? ingredients;
  private string? instructions;
  private string? name;
  
  [JsonPropertyName("ingredients")]
  public List<string>? Ingredients { get => ingredients; set => ingredients = value; }
  [JsonPropertyName("instructions")]
  public string? Instructions { get => instructions; set => instructions = value; }
  [JsonPropertyName("name")]
  public string? Name { get => name; set => name = value; }

  public Bebida(List<string> ingredients, string instructions, string name) {
      this.Ingredients = ingredients;
      this.Instructions = instructions;
      this.Name = name;
  }
}
