# teamtasks-dashboard


# 🧩 TeamTasks Dashboard – Technical Test

Este repositorio contiene una solución **full stack** para la gestión de proyectos, tareas y desarrolladores, construida como prueba técnica.

---

## 📦 1. Base de datos

### 🛢️ Motor
Se utilizó **PostgreSQL** como motor de base de datos.

### 📄 Script de creación

Ejemplo de script incluido en `DBSetup_TeamTasks.sql`:

```sql
CREATE DATABASE "TeamTasks";

CREATE TABLE Developers (
    DeveloperId SERIAL PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    CreatedAt TIMESTAMP NOT NULL DEFAULT NOW()
);
```

---

## 🧠 2. Backend (.NET)

### ⚙️ Tecnología
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL

### 🧱 Arquitectura
Arquitectura en capas:

- API
- Application
- Domain
- Infrastructure
- Test

### 🔐 Cadena de conexión

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5433;Database=TeamTasks;Username=postgres;Password=cancelado88"
}
```

> ⚠️ Esta información se comparte solo para efectos de la prueba técnica.

### ▶️ Ejecutar backend

```bash
dotnet run --project "1. TeamTasks.API"
```

---

## 🎨 3. Frontend (Angular)

### ⚛️ Tecnología
- Angular 20
- Standalone Components
- Angular Material
- Bootstrap

### 📌 Versiones

```text
Angular CLI: 20.3.2
Node: 22.18.0
npm: 10.9.3
```

### ⚙️ Configuración

```ts
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7287/api'
};
```

### 📦 Instalación

```bash
npm install
```

### ▶️ Ejecutar

```bash
ng serve
```

Abrir:
http://localhost:4200

---

## 🚀 Funcionalidades

- Gestión de desarrolladores
- Gestión de proyectos
- Gestión de tareas
- Dashboard de métricas

---

## 🧪 Notas finales

Proyecto desarrollado como prueba técnica siguiendo buenas prácticas y arquitectura limpia.

└── app.component.css
