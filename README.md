#  API Financiera y Consumo en React 

El presente proyecto muesta como desarrollar una API REST en .NET para gestionar transacciones financieras y una interfaz en React para consumir la API.

## Herramientas usadas

  + .NET 8

  + SQL Server

  + RESTful API

  + React
  
  + React bootstrap

  + Dapper

## Pasos para la instalación

**1. Clonar el repositorio**

```bash
git clone https://github.com/blueshabat/transaction-app.git
```

**2. Inicializar el backend**

Ejecutar el [script](https://github.com/blueshabat/transaction-app/blob/main/Docs/Script.sql) en una instancia de SQL Server

Luego, modificar la [cadena de conexión](https://github.com/blueshabat/transaction-app/blob/main/Backend/Transaction.Api/appsettings.json) según la configuración de la base de datos

```bash
cd Backend
```

```bash
dotnet run --project /Transaction.Api/Transaction.Api.csproj
```
La aplicación se ejecutará en la ruta <http://localhost:5230>

Los endpoints se pueden probar importando la [colección](https://github.com/blueshabat/transaction-app/blob/main/Docs/BancoSol.postman_collection.json) en postman

**3. Inicializar el frontend**

```bash
cd Frontend
```

Instalar las dependencias

```bash
npm install
```

Iniciar la ejecución

```bash
npm start
```

La aplicación se ejecutará en la ruta <http://localhost:3000>

## Descripción de los endpoints

Se creó sólo el controlador `Transaction`

### Transaction

| Method | Url | Decripción | 
| ------ | --- | ----------- | 
| GET    | /api/Transaction/List | Obtiene el listado de todas las transacciones ordenadas por id descendentemente | 
| POST   | /api/Transaction/GetById | Obtiene los detalles de una transacción dado su id. Se utiliza para una posterior edición | 
| POST   | /api/Transaction/Create | Registra una nueva transacción | 
| PUT    | /api/Transaction/Update | Actualiza los datos de la transacción seleccionada | 
| DELETE | /api/Transaction/Delete | Elimina la transacción seleccionada |


### Prueba de funcionamiento

[Demo](https://github.com/blueshabat/transaction-app/blob/main/Docs/Demo.mp4)
