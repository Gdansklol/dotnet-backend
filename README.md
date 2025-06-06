# 📚 CRUD Book REST API – med .NET 9 och VS Code

Detta är ett enkelt backend-API-projekt byggt med .NET 9 och C#, 
testat med REST Client i VS Code.  
Perfekt för nybörjare som vill förstå grunderna i web API-utveckling
med minimal setup.

---

##  Strukturöversikt

```cs
dotnet-backend/
├── Controllers/
│   └── HelloController.cs
├── Program.cs
├── test.http
└── README.md
```

## Steg-för-steg Guide
1.  Skapa projektet

```bash

dotnet new webapi -n dotnet-backend
```
- Detta skapade mappen:

```bash

dotnet-backend/dotnet-backend
```
- För att undvika dubbla mappar:

```bash

mv dotnet-backend/dotnet-backend/* dotnet-backend/
mv dotnet-backend/dotnet-backend/.* dotnet-backend/  # om dolt innehåll
rm -r dotnet-backend/dotnet-backend
```

2. Öppna i VS Code
- Eftersom VS Code redan är installerat:

```bash
cd dotnet-backend
code .
```

3.  Lägg till controller
- Skapa filen Controllers/HelloController.cs:

```csharp

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public string Get() => "CRUD Book REST API";
}
```

4.  Kontrollera Program.cs
- Se till att detta finns i Program.cs:

```csharp
builder.Services.AddControllers();
app.MapControllers();
```

5. Starta servern
```bash
dotnet run
Svar:

Now listening on: http://localhost:5119
```
6. Testa i webbläsare
Öppna i valfri webbläsare:

http://localhost:5119/Hello
- Du bör se:

CRUD Book REST API

7.  Testa med REST Client (valfritt)
- Skapa en fil `test.http`:
GET http://localhost:5119/Hello

- Klicka på Send Request.
Om inget syns i svaret – testa istället i webbläsaren.

###  Nybörjarbegrepp
#### Begrepp	Betydelse
- Controller	Klass som hanterar API-rutter
- Route("[controller]")	Använder klassnamn som URL (t.ex. /Hello)
- Program.cs	Huvudfil där appen startas
- dotnet run	Startar applikationen
- GET	HTTP-metod för att hämta data

#### Vanliga problem
> Ändring syns inte?
➤ Stäng servern (Ctrl + C) och kör dotnet run igen

> Ingen respons i REST Client?
➤ Testa i webbläsaren istället


### Begrepp

| Term              | Betydelse                                                       |
| ----------------- | --------------------------------------------------------------- |
| **CORS**          | Säkerhetsmekanism som kräver godkännande för cross-origin-anrop |
| **Origin**        | Domän + port (t.ex. `localhost:4200`)                           |
| **Middleware**    | Kod som körs mellan request och response                        |
| **Authorization** | Behörighetskontroll (valfritt i enkla API)                      |


### Sammanfattning
- Om du bygger ett Angular-projekt på http://localhost:4200 och vill anropa ett .NET Web API:

➤ Du måste tillåta det via CORS

- Använd WithOrigins("http://localhost:4200") för säker åtkomst

- Undvik AllowAnyOrigin() i produktion

- Kontrollera att app.UseCors() kommer innan MapControllers()


