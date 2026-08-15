# TicketPro RD — Frontend de Control de Ventas de Boletas

Frontend en HTML/CSS/JS puro (sin frameworks, sin build step) hecho a medida
para consumir el backend `SistemaVentaBoletasAPI` del repositorio subido.
Cubre los 4 recursos expuestos por la API: **Eventos, Boletas, Clientes y Ventas**,
con listado, creación, edición y eliminación para cada uno, más un panel de
resumen (Dashboard).

## Estructura

```
frontend/
├─ index.html         → estructura de la app (sidebar + topbar + contenido)
├─ css/styles.css      → sistema de diseño "TicketPro RD" (tokens, componentes)
├─ js/config.js        → URL base de la API (edítala aquí)
├─ js/api.js           → capa de conexión fetch → endpoints REST
└─ js/app.js           → routing, estado, render de vistas y formularios
```

## 1. Habilitar CORS en el backend

El `Program.cs` del repo no tiene CORS configurado, así que el navegador
bloqueará las peticiones del frontend hasta que lo agregues. Añade esto en
`Program.cs`, **antes** de `builder.Build()`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5500",   // Live Server / http-server, ajusta al tuyo
                "http://127.0.0.1:5500"
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

Y esto **después** de `var app = builder.Build();`, antes de `app.MapControllers();`:

```csharp
app.UseCors("FrontendPolicy");
```

## 2. Apuntar el frontend a tu API

Edita `js/config.js` con la URL real donde corre tu API (revisa
`Properties/launchSettings.json` del backend si no la sabes):

```js
window.TICKETPRO_API_URL = 'https://localhost:51653/api';
```

## 3. Levantar el frontend

Son archivos estáticos, así que cualquier servidor local sirve. Por ejemplo:

```bash
cd frontend
npx http-server -p 5500
# o: python -m http.server 5500
```

Abre `http://localhost:5500`. Si la API no responde, verás un aviso en el
panel indicando que no hay conexión (revisa CORS y que el backend esté
corriendo).

## Notas sobre los datos

- Los DTOs de la API usan camelCase en el JSON (`nombre`, `fecha`, `eventoId`,
  etc.), que es el comportamiento por defecto de ASP.NET Core con
  `System.Text.Json` — el frontend ya está mapeado a esos nombres.
- **Boletas** se relacionan con **Eventos** vía `eventoId`; **Ventas** se
  relacionan con **Eventos** y **Clientes**. El frontend resuelve esos IDs
  contra los datos ya cargados para mostrar nombres en vez de números.
- El campo `estado` de una boleta es texto libre en la API; el formulario lo
  limita a `Disponible / Vendida / Cancelada` para mantener datos consistentes,
  pero puedes ajustar la lista `ESTADOS_BOLETA` en `app.js` si usas otros valores.
- El total de una venta se sugiere automáticamente (precio del evento ×
  cantidad de boletas) pero es editable a mano.
