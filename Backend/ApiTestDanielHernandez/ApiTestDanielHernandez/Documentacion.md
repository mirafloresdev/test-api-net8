
# API REST

## Nombre:
Daniel Raul Hernandez Miraflores

## Introducción
API Rest usando NET 8

## Requisitos
- PostgreSQL 12.0
- .NET 8.0
- Dapper
- Dapper.Contrib
- BCrypt.Net-Next/4.0.3
- Newtonsoft.JSON/13.0
- Npgsql/8.0
- JwtBearer
- 
### Clonar el Repositorio
```bash
git clone https://example.com/repo.git
cd repo
```

## Configuración del Entorno
Instrucciones sobre cómo configurar el entorno de desarrollo local para empezar a trabajar con la aplicación.
- Ejecutar primero en la raiz del proyecto el docker-compose.yml que contiene la configuracion de la base de datos postgressql, con el comando docker-compose up.
- Ejecutar seguido los scripts de la carpeta "Base de Datos" que esta en el repositorio



## API Endpoints
Descripción de los endpoints disponibles en la API.

### Autenticación
- `POST /api/Auth/login`: endpoint para iniciar sesión y recibir un token JWT.
  - **Parámetros**:
    - `alias`: Alias del usuario.
    - `password`: Contraseña del usuario.
  - **Respuesta**: JWT token.

### Usuarios
- `GET /api/User/{id}`: obtener detalles de un usuario específico.
  - **Headers**:
    - `Authorization`: `Bearer <token>`
- `POST /api/User`: crear un nuevo usuario.
  - **Headers**:
    - `Authorization`: `Bearer <token>`
- `PUT /api/User/{id}`: actualizar un usuario existente.
  - **Headers**:
    - `Authorization`: `Bearer <token>`
- `DELETE /api/User/{id}`: eliminar un usuario.
  - **Headers**:
    - `Authorization`: `Bearer <token>`

## Ejemplos de Código
Ejemplos de cómo interactuar con la API usando `curl`.

### Login
```bash
curl -X POST "http://localhost:5000/api/Auth/login" -H "Content-Type: application/json" -d '{"alias": "user1", "password": "pass"}'
```

### Obtener Usuario
```bash
curl -X GET "http://localhost:5000/api/User/1" -H "Authorization: Bearer <token>"
```


