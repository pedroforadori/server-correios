using Correios.Demo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
var app = builder.Build();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

var itens = new List<Item>()
{
    new()
    {
        Nome = "Produto A",
        Altura = 2,
        Largura = 22,
        Comprimento = 32,
        Peso = (decimal?)0.3
    },
    new()
    {
        Nome = "Produto B"
    }
};

List<string> listPrecoPrazo = new List<string>();

app.MapGet("/health", () => "API ON");

app.MapPost("/price", (string cepDestino) =>
{
    var cepOrigem = "11310230";

    var precoPrazoList = new CorreiosManager().CalcularPrecoPrazo(cepOrigem, cepDestino, itens);

    foreach (var precoPrazo in precoPrazoList)
    {
        listPrecoPrazo.Add(precoPrazo);
    }

    return Results.Ok(listPrecoPrazo);
});

app.Run();

	// "itens": {
	// 	"Nome": "Produto A",
	// 	"Altura": 12,
	// 	"Largura": 14,
	// 	"Comprimento": 16,
	// 	"Peso": 200
	// }  
