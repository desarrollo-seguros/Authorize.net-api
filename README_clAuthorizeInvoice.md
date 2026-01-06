# clAuthorizeInvoice – Invoice Generator (Authorize.Net)

> Generación de recibos (Invoice) en PDF desde Authorize.Net, retornando el archivo como `Byte[]` para su uso en aplicaciones ASP.NET Web Forms u otros proyectos C#.

---

##  Descripción

`clAuthorizeInvoice` es una clase reutilizable que:

- Consume la API de **Authorize.Net**
- Obtiene **Merchant Details** y **Transaction Details**
- Genera un **PDF de recibo (Invoice)**
- Devuelve el PDF **en memoria como `Byte[]`**

El archivo PDF en memoria se puede manejar cómo: descarga, guardado en disco, envío por correo, etc.

---

##  Dependencias

Asegúrate de incluir en el proyecto las librerias:

- `clAuthorizeCC.dll`
- `itextsharp.dll`
- `Newtonsoft.Json.dll`

En la clase donde se implementará, agregar los using:

- `using ME.Mexicard;`

---

##  Estructura de la clase

### Constructor

```csharp
clAuthorizeInvoice(string transId)
```

Inicializa la clase con el **Transaction ID** de Authorize.Net.

---

### Propiedades relevantes

| Propiedad | Descripción |
|---------|-------------|
| `msg` | Mensaje de error devuelto por la API o validaciones |
| `isTest` | `True` = Sandbox, `False` = Producción |
| `transactionResponse` | Datos completos de la transacción |
| `merchantResponse` | Datos del comercio |

---

##  Flujo de uso estándar

>  **Siempre ejecutar `GetAuthorizeData()` antes de `GenerateInvoice()`**

### 1️. Inicializar la clase

```csharp
clAuthorizeInvoice authInvoice = new clAuthorizeInvoice(netTransid);
```

---

### 2️. Definir ambiente

```csharp
authInvoice.isTest = "True";  // Sandbox
// o
authInvoice.isTest = "False"; // Producción
```

---

### 3️. Obtener datos desde Authorize.Net

```csharp
if (!authInvoice.GetAuthorizeData())
{
    string error = authInvoice.msg;
}
```

Este método obtiene:

- Merchant Details
- Transaction Details
- Valida respuestas de Authorize.Net

---

### 4️. (Opcional) Personalizar información del recibo

```csharp
authInvoice.transactionResponse.TransactionDetails.Order.Description =
    "Homeowners policyNo ABC123";
```

---

### 5️. Generar el PDF (Invoice)

```csharp
byte[] pdfBytes = authInvoice.GenerateInvoice();
```

---

##  Ejemplo completo (ASP.NET Web Forms / Handler)

```csharp
clAuthorizeInvoice authInvoice = new clAuthorizeInvoice(netTransid);

authInvoice.isTest = policyno.ToUpper().StartsWith("P") ? "True" : "False";

if (authInvoice.GetAuthorizeData())
{
    authInvoice.transactionResponse.TransactionDetails.Order.Description =
        $"Homeowners policyNo {policyno.ToUpper()}";

    byte[] bytes = authInvoice.GenerateInvoice();

    context.Response.ContentType = "application/pdf";
    context.Response.AddHeader("Content-Disposition",
        "attachment; filename=Receipt.pdf");
    context.Response.BinaryWrite(bytes);
}
else
{
    context.Response.ContentType = "text/plain";
    context.Response.Write(authInvoice.msg);
}
```

---

##  Diagrama de flujo

```
flujo
    A[Inicio] --> B[Inicializar clAuthorizeInvoice]
    B --> C[Definir isTest]
    C --> D[GetAuthorizeData()]
    D -->|Error| E[Leer msg y finalizar]
    D -->|OK| F[Opcional: Ajustar Description]
    F --> G[GenerateInvoice()]
    G --> H[Obtener byte[] PDF]
    H --> I[Descargar / Guardar / Enviar]
```

---

##  Manejo de errores

- No lanza excepciones al consumidor
- Devuelve `false` en errores de API o validación
- El mensaje se expone mediante:

```csharp
authInvoice.msg
```

---

##  Buenas prácticas

- Validar siempre el resultado de `GetAuthorizeData()`
- Definir correctamente el ambiente
- No llamar `GenerateInvoice()` sin datos válidos
- Ajustar datos del recibo antes de generar el PDF

---

##  Quick Start

```csharp
var invoice = new clAuthorizeInvoice(netTransid);
invoice.isTest = "False";

if (!invoice.GetAuthorizeData())
{
    throw new Exception(invoice.msg);
}

invoice.transactionResponse.TransactionDetails.Order.Description =
    "Policy payment receipt";

byte[] pdf = invoice.GenerateInvoice();
```

---

##  Resumen

✔ Reutilizable
✔ Genera PDF en memoria
✔ Compatible con Web Forms
✔ Integración directa con Authorize.Net
✔ Control total sobre el `Byte[]`

---

**Autor:** Eric Díaz
**Fecha:** 06 / ENERO / 2026