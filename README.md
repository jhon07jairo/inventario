MiniSistema de Gestión de Inventario - Backend (.NET 9 + PostgreSQL)

1. Descripción

Este backend proporciona una API RESTful para gestionar el inventario de productos, permitiendo a los usuarios autenticados registrar entradas y salidas de productos, así como consultar el estado del inventario.

2. Tecnologías Usadas

.NET 9 (C#)

Entity Framework Core (ORM para base de datos)

PostgreSQL (Base de datos)

JWT (JSON Web Token) para autenticación


3. Instalación y Configuración

Requisitos Previos

Tener instalado .NET 9 SDK (Descargar .NET 9)

Tener instalado PostgreSQL (Descargar PostgreSQL)

Configurar una base de datos local llamada InventarioDB

Clonar el Repositorio

git clone https://github.com/jhon07jairo/inventario.git
cd inventarioApi

Configurar la Base de Datos

Abrir PostgreSQL y crear la base de datos manualmente:

CREATE DATABASE "InventarioDB";

Configurar la conexión en appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=InventarioDB;Username=postgres;Password=admin"
}

Ejecutar Migraciones y Poblar la BD

dotnet ef database update

Ejecutar la API

dotnet run

La API estará disponible en: http://localhost:5113

4. Endpoints Disponibles

Autenticación

Método

Endpoint

Descripción

POST

/auth/login

Iniciar sesión y obtener JWT

Productos e Inventario

Método

Endpoint

Descripción

POST

/productos/movimiento

Registrar entrada o salida de productos

GET

/productos/inventario

Obtener el estado actual del inventario

Autor: Jhon Jairo López Sáez



#######################################################
MiniSistema de Gestión de Inventario - Frontend (Angular 19)

1. Descripción

Este frontend proporciona una interfaz web para gestionar el inventario de productos, permitiendo a los usuarios autenticados registrar movimientos de productos y consultar el inventario disponible.

2. Tecnologías Usadas

Angular 19 (Framework de frontend)

Angular Material (Componentes UI)

TypeScript (Lenguaje de programación)

RxJS (Manejo de estados y peticiones asíncronas)

Bootstrap/Flex Layout (Diseño responsivo)

3. Instalación y Configuración

Requisitos Previos

Tener instalado Node.js 18+ (Descargar Node.js)

Tener instalado Angular CLI 19

Clonar el Repositorio

git clone https://github.com/jhon07jairo/inventario.git
cd inventarioAngular

Instalar Dependencias

npm install

Ejecutar la Aplicación

ng serve --open

La aplicación estará disponible en: http://localhost:4200

4. Funcionalidades Implementadas

Login con JWT (Validación de usuario y almacenamiento de token)

Pantalla de Inventario (Consulta de productos y cantidades disponibles)

Registro de Movimientos (Entrada/Salida de productos)

Protección de rutas con AuthGuard

5. Rutas de la Aplicación

Ruta

Descripción

/login

Formulario de autenticación

/inventario

Lista de productos en inventario

/movimiento

Registro de entradas/salidas

6. Pruebas de API en el Frontend

Ejemplo de Login (``)

Ir a la pantalla de Login.

Ingresar las credenciales:

{
  "email": "usuario",
  "password": "lcc123"
}

Si es exitoso, se redirige a /inventario

Autor: Jhon Jairo López Sáez





