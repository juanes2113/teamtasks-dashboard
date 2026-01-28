# teamtasks-dashboard

Technical test

dotnet ef dbcontext scaffold "Host=localhost;Port=5433;Database=TeamTasks;Username=postgres;Password=cancelado88" Npgsql.EntityFrameworkCore.PostgreSQL --project "4. TeamTasks.Infrastructure/4. TeamTasks.Infrastructure.csproj" --startup-project "1. TeamTasks.API/1. TeamTasks.API.csproj" --context TeamTasksDbContext --output-dir Entities --no-onconfiguring --force

"DefaultConnection": "Host=localhost;Port=5433;Database=TeamTasks;Username=tu_username;Password=tu_password"
"DefaultConnection": "Host=localhost;Port=5433;Database=TeamTasks;Username=postgres;Password=cancelado88"

TeamTasksDashboard
│
├── 1. TeamTasks.API
│   │
│   ├── Connected Services
│   ├── Dependencias
│   ├── Properties
│   │
│   ├── Controllers
│   │   └── (Controllers de la API REST)
│   │
│   ├── TeamTasks.API.http
│   ├── appsettings.json
│   ├── Program.cs
│   └── WeatherForecast.cs
│
├── 2. TeamTasks.Application
│   │
│   ├── Dependencias
│   │
│   ├── Interfaces
│   │   └── (Contratos de servicios / casos de uso)
│   │
│   ├── Services
│   │   └── (Lógica de negocio / casos de uso)
│   │
│   └── ServiceCollection.cs
│
├── 3. TeamTasks.Domain
│   │
│   ├── Dependencias
│   │
│   ├── Enums
│   │   └── (Estados, tipos, prioridades, etc.)
│   │
│   ├── Interfaces
│   │   └── (Contratos de repositorios)
│   │
│   └── Models
│       └── (Entidades de dominio: Project, Task, Developer, etc.)
│
├── 4. TeamTasks.Infrastructure
│   │
│   ├── Dependencias
│   │
│   ├── Data
│   │   └── TeamTasksDbContext.cs
│   │
│   ├── Repositories
│   │   └── (Implementaciones de repositorios)
│   │
│   └── ServiceCollection.cs
│
└── 5. TeamTasks.Test
    │
    ├── Dependencias
    └── (Pruebas unitarias / integración)

src/app
│
├── core/
│   ├── services/
│   │   ├── developers.service.ts
│   │   ├── projects.service.ts
│   │   └── dashboard.service.ts
│   │
│   └── layout/
│       ├── layout.component.ts
│       ├── layout.component.html
│       └── layout.component.css
│
├── shared/
│   ├── pipes/
│   │   └── task-status.pipe.ts
│   │
│   └── models/
│       ├── developer.model.ts
│       ├── project.model.ts
│       └── task.model.ts
│
├── features/
│   ├── developers/
│   │   ├── developers.component.ts
│   │   ├── developers.component.html
│   │   └── developers.component.css
│   │
│   ├── projects/
│   │   ├── projects.component.ts
│   │   ├── projects.component.html
│   │   └── projects.component.css
│   │
│   └── metrics/
│       ├── metrics.component.ts
│       ├── metrics.component.html
│       └── metrics.component.css
│
├── app.routes.ts
├── app.component.ts
├── app.component.html
└── app.component.css