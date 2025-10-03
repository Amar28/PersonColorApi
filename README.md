PersonColorApi

Ein Projekt in .NET 9, das mit CQRS, Mediator Pattern, Clean Architecture (Api, Application, Domain, Infrastructure, Tests) und zwei Datenquellen (CSV + SQLite) arbeitet.
Das Projekt stellt ein REST-Interface zur Verwaltung von Personen und deren Lieblingsfarben bereit.

📂 Projektstruktur

    PersonColorApi/
    ├── PersonColorApi.Api/ # WebAPI Layer (Controllers, Program.cs, Data/sample-input.csv)
    ├── PersonColorApi.Application/ # CQRS Commands & Queries, DTOs
    ├── PersonColorApi.Domain/ # Entities & Interfaces
    ├── PersonColorApi.Infrastructure/ # Repositories (CSV + EF/SQLite), DbContext
    ├── PersonColorApi.Tests/ # Unit Tests mit xUnit + Moq
    ├── README.md

✨ Features

- Personenverwaltung (Name, Nachname, PLZ, Stadt, Lieblingsfarbe)
- Datenquelle wählbar:
  - CSV-Datei (sample-input.csv)
  - SQLite-Datenbank via Entity Framework Core
- CQRS mit MediatR (GetAll, GetById, GetByColor, Add, Update, Delete)
- REST API mit Swagger UI
- Unit-Tests mit xUnit und Moq
- Dependency Injection für Repositories
- Einfache Erweiterbarkeit (z. B. weitere Datenquellen möglich)

📑 API Endpunkte

    GET /persons
    Alle Personen abrufen.

    GET /persons/{id}
    Person anhand der ID abrufen.

    GET /persons/color/{color}
    Alle Personen mit einer bestimmten Lieblingsfarbe abrufen.

    POST /persons
    Neue Person hinzufügen.

    {
    "name": "Hans",
    "lastName": "Müller",
    "zipCode": "67742",
    "city": "Lauterecken",
    "color": "blau"
    }

⚙️ Einrichtung

1. Abhängigkeiten installieren
   dotnet restore

2. Datenquelle auswählen
   In appsettings.json:
   {
   "DataSource": "csv", // oder "ef"
   "ConnectionStrings": {
   "PersonDb": "Data Source=personcolor.db"
   }
   }

- csv → nutzt die CSV-Datei (Data/sample-input.csv)

- ef → nutzt SQLite Datenbank (personcolor.db)

3. Migration & Datenbank erstellen (falls SQLite genutzt wird)

   cd PersonColorApi.Infrastructure
   dotnet ef migrations add InitialCreate
   dotnet ef database update

4. Starten
   cd PersonColorApi.Api
   dotnet run

Swagger ist verfügbar unter:
👉 https://localhost:5197/swagger

🧪 Tests ausführen
cd PersonColorApi.Tests
dotnet test

🚀 Architekturüberblick

- Domain Layer:
  Enthält Entities (Person) und Interfaces (IPersonRepository).

- Application Layer:
  Enthält Commands/Queries (CQRS) + DTOs. Nutzt MediatR für Trennung von Logik.

- Infrastructure Layer:
  Enthält konkrete Repository-Implementierungen (CsvPersonRepository, EfPersonRepository) + PersonDbContext.

- Api Layer:
  ASP.NET Core REST API mit Controllern. Dependency Injection entscheidet, ob CSV oder SQLite verwendet wird.

- Tests Layer:
  Enthält Unit-Tests mit xUnit und Moq.

✅ Akzeptanzkriterien

    CSV-Datei wird korrekt eingelesen

    Personen können über REST API abgerufen, erstellt werden

    Austausch der Datenquelle (CSV ↔ SQLite) ohne API-Änderung

    Tests stellen sicher, dass CSV/DB konsistent verarbeitet wird

📌 Beispiel CSV-Datei

    Data/sample-input.csv:

    Johnson, Johnny, 88888 made up, 3
    Millenium, Milly, 77777 made up too, 4
    Müller, Jonas, 32323 Hansstadt, 5
    Fujitsu, Tastatur, 42342 Japan, 6
