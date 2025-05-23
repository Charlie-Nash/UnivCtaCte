# UnivCtaCte

Microservicio de operaciones financieras, construido con **ASP.NET Core**. Desarrollado bajo principios de arquitectura limpia, con soporte para contenedores Docker.

## 🚀 Características

- API RESTful construida con ASP.NET Core.
- Consume una BD POSTGRES.
- Arquitectura por capas (Domain, Application, Infrastructure, API).
- Preparado para despliegue en Docker.

---

## 🛠️ Tecnologías utilizadas

- .NET 8
- Npgsql (Conexión a POSTGRES)
- Docker & Docker Compose
- Clean Architecture (Separación de responsabilidades)

---

## 📦 Docker Compose

```yaml
services:
  api:
    build:
      context: .
      dockerfile: UnivCtaCte.Api/Dockerfile
    ports:
      - "5003:8080"
    environment:
      - ConnectionStrings__dbCtaCte=Host=${POSTGRES_IP_SERVER};Port=5432;Database=${POSTGRES_BD_NAME};Username=postgres;Password=${POSTGRES_ROOT_PASSWORD};
```

---

## 📬 Endpoint disponible

### GET `/api/v1/estudiante/ctacte/pago-matricula/{estudiante_id}/{semestre_id}/{categoria_id}`

Obtiene información acerca del pago por concepto de matrícula.

#### 🔹 Response JSON
```json
{
    "respuesta": -1,
    "mensaje": "Estudiante no registrado en el periodo ",
    "status": 200
}
```

### GET `/api/v1/estudiante/ctacte/estado/{estudiante_id}/{semestre_id}`

Obtiene el estado de la cuenta corriente del estudiante.

#### 🔹 Response JSON
```json
[
    {
        "semestre": "20251",
        "nro": 0,
        "fecha_vencimiento": "2025-05-30T01:40:42.359182",
        "importe": 300.00,
        "pagado": "NO",
        "cuenta_id": 555,
        "compromiso_id": 555,
        "costo_id": 5,
        "concepto": "MATRICULA",
        "categoria": "COSTO ESTATAL"
    }
]
```

### POST `/api/v1/estudiante/ctacte/generar-pension`

Genera el cronograma de pago correspondiente a la matrícula del estudiante en un período académico.

#### 🔹 Request JSON
```json
{
    "estudiante_id": 12999,
    "semestre_id": 170,
    "categoria_id": 3,
    "creditos": 10
}
```

#### 🔹 Response JSON
```json
{
    "respuesta": -1,
    "mensaje": "Primero debe generar MATRICULA ",
    "status": 200
}
```

---

## ▶️ Cómo ejecutar

1. Clonar este repositorio:
   ```bash
   git clone https://github.com/Charlie-Nash/UnivCtaCte
   cd UnivCtaCte
   ```

2. Crear un archivo `.env` con las siguientes variables:
   ```env
   POSTGRES_IP_SERVER=ip_del_servidor_de_BD
   POSTGRES_BD_NAME=nombre_de_la_BD
   POSTGRES_ROOT_PASSWORD=tu_password
   ```

3. Ejecutar con Docker Compose:
   ```bash
   sudo docker-compose up --build -d
   o
   sudo docker compose up --build -d
   ```
---

## 👤 Autor

Desarrollado por **Charlie Nash**

---

## 📄 Licencia

Este proyecto se puede usar libremente para fines educativos o personales.