using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => new {Name = "Leticia Pacheco", Age = 22});
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste", "Leticia Pacheco");
    return new{Name = "Leticia Pacheco", Age = 22};
});

app.MapPost("/saveproduct", (Product product) => {
    ProductRepository.Add(product);
});


//api,app.com/user/{code}
app.MapGet("/getproduct/{code}", ([FromRoute]string code) =>{
    var product = ProductRepository.GetBy(code);
    return product;
});

app.MapPut("/editproduct", (Product product) => {
   var productSaved = ProductRepository.GetBy(product.Code);
   productSaved.Name = product.Name; 
});

app.MapDelete("/deleteproduct/{code}", ([FromRoute]string code) =>{
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
});


app.Run();

public static class ProductRepository{
    public static List<Product> Products {get; set;}

    public static void Add(Product product){
        if(Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(string code)
    {
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove(Product product){
        Products.Remove(product);
    }
}

public class Product {
    public string Code { get; set; }
    public string Name { get; set; }
}
