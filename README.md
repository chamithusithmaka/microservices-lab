# Microservices Lab

A polyglot microservices architecture demo with an API Gateway, built using **FastAPI (Python)**, **Django REST Framework (Python)**, **ASP.NET Core (C#)**, and **Express.js (Node.js)**.

## Architecture Overview

```
                    ┌──────────────────┐
                    │   API Gateway    │
                    │  (Express.js)    │
                    │   Port: 8080     │
                    └────────┬─────────┘
                             │
            ┌────────────────┼────────────────┐
            │                │                │
   ┌────────▼───────┐ ┌─────▼──────┐ ┌───────▼────────┐
   │  Item Service   │ │Order Service│ │Payment Service │
   │   (FastAPI)     │ │  (Django)   │ │ (ASP.NET Core) │
   │   Port: 8081    │ │ Port: 8082  │ │  Port: 8083    │
   └────────────────┘ └────────────┘ └────────────────┘
```

## Tech Stack

| Service         | Framework              | Language   | Port |
| --------------- | ---------------------- | ---------- | ---- |
| API Gateway     | Express.js             | JavaScript | 8080 |
| Item Service    | FastAPI + Uvicorn      | Python     | 8081 |
| Order Service   | Django REST Framework  | Python     | 8082 |
| Payment Service | ASP.NET Core 8         | C#         | 8083 |

---

## Project Structure

```
microservices-lab/
├── api-gateway/          # Node.js API Gateway
│   ├── index.js
│   ├── package.json
│   └── Dockerfile
├── ItemService/          # FastAPI Item Service
│   ├── main.py
│   ├── requirements.txt
│   └── Dockerfile
├── OrderService/         # Django Order Service
│   ├── manage.py
│   ├── requirements.txt
│   ├── Dockerfile
│   ├── OrderService/
│   │   ├── settings.py
│   │   └── urls.py
│   └── orders/
│       ├── views.py
│       └── urls.py
├── PaymentService/       # ASP.NET Core Payment Service
│   ├── Program.cs
│   ├── PaymentService.csproj
│   ├── appsettings.json
│   ├── Dockerfile
│   └── PaymentService/
│       ├── Controllers/
│       │   └── PaymentController.cs
│       ├── Models/
│       │   └── Payment.cs
│       └── Services/
│           └── PaymentStore.cs
└── docker-compose.yml
```

---

## Getting Started

### Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop/) & Docker Compose
- (Optional for local dev) Node.js, Python 3.11+, .NET 8 SDK

### Run All Services with Docker Compose

```bash
# Clone the repository
git clone https://github.com/chamithusithmaka/microservices-lab.git
cd microservices-lab

# Build and start all services
docker-compose up --build

# Stop all services
docker-compose down
```

All services will start automatically. The API Gateway will be available at **http://localhost:8080**.

---

## Running Services Individually (Local Development)

### 1. Item Service (FastAPI)

```bash
cd ItemService
pip install -r requirements.txt
uvicorn main:app --host 0.0.0.0 --port 8081
```

### 2. Order Service (Django)

```bash
cd OrderService
pip install -r requirements.txt
python manage.py migrate
python manage.py runserver 0.0.0.0:8082
```

### 3. Payment Service (ASP.NET Core)

```bash
cd PaymentService
dotnet run
```

### 4. API Gateway (Express.js)

> **Note:** When running locally, update the proxy targets in `api-gateway/index.js` from Docker service names (e.g., `http://item-service:8081`) to `http://localhost:8081`, etc.

```bash
cd api-gateway
npm install
node index.js
```

---

## API Endpoints

### Via API Gateway (http://localhost:8080)

#### Item Service

| Method | URL                          | Description       | Request Body              |
| ------ | ---------------------------- | ----------------- | ------------------------- |
| GET    | `http://localhost:8080/items` | Get all items     | —                         |
| GET    | `http://localhost:8080/items/0` | Get item by index | —                       |
| POST   | `http://localhost:8080/items` | Add a new item    | `{ "name": "Tablet" }`   |

#### Order Service

| Method | URL                                  | Description        | Request Body                              |
| ------ | ------------------------------------ | ------------------ | ----------------------------------------- |
| GET    | `http://localhost:8080/orders`       | Get all orders     | —                                         |
| GET    | `http://localhost:8080/orders/1`     | Get order by ID    | —                                         |
| POST   | `http://localhost:8080/orders/create`| Create a new order | `{ "item": "Laptop", "quantity": 2 }`    |

#### Payment Service

| Method | URL                                     | Description        | Request Body                              |
| ------ | --------------------------------------- | ------------------ | ----------------------------------------- |
| GET    | `http://localhost:8080/payments`        | Get all payments   | —                                         |
| GET    | `http://localhost:8080/payments/1`      | Get payment by ID  | —                                         |
| POST   | `http://localhost:8080/payments/process`| Process a payment  | `{ "orderId": 1, "amount": 1000, "method": "Card" }` |

---

### Direct Service Access (without Gateway)

#### Item Service (http://localhost:8081)

| Method | URL                       | Description       |
| ------ | ------------------------- | ----------------- |
| GET    | `http://localhost:8081/`  | Get all items     |
| GET    | `http://localhost:8081/0` | Get item by index |
| POST   | `http://localhost:8081/`  | Add a new item    |

#### Order Service (http://localhost:8082)

| Method | URL                             | Description        |
| ------ | ------------------------------- | ------------------ |
| GET    | `http://localhost:8082/`        | Get all orders     |
| GET    | `http://localhost:8082/1`       | Get order by ID    |
| POST   | `http://localhost:8082/create`  | Create a new order |

#### Payment Service (http://localhost:8083)

| Method | URL                                | Description        |
| ------ | ---------------------------------- | ------------------ |
| GET    | `http://localhost:8083/`           | Get all payments   |
| GET    | `http://localhost:8083/1`          | Get payment by ID  |
| POST   | `http://localhost:8083/process`    | Process a payment  |

---

## Testing with Postman

### Step 1: Create an Order

- **Method:** POST
- **URL:** `http://localhost:8080/orders/create`
- **Headers:** `Content-Type: application/json`
- **Body (raw JSON):**
```json
{
  "item": "Laptop",
  "quantity": 2
}
```
- **Expected Response (201):**
```json
{
  "item": "Laptop",
  "quantity": 2,
  "id": 1,
  "status": "PENDING"
}
```

### Step 2: Process a Payment

- **Method:** POST
- **URL:** `http://localhost:8080/payments/process`
- **Headers:** `Content-Type: application/json`
- **Body (raw JSON):**
```json
{
  "orderId": 1,
  "amount": 1000,
  "method": "Card"
}
```
- **Expected Response (201):**
```json
{
  "id": 1,
  "orderId": 1,
  "amount": 1000,
  "method": "Card",
  "status": "SUCCESS"
}
```

### Step 3: Add an Item

- **Method:** POST
- **URL:** `http://localhost:8080/items`
- **Headers:** `Content-Type: application/json`
- **Body (raw JSON):**
```json
{
  "name": "Tablet"
}
```
- **Expected Response (201):**
```json
{
  "message": "Item added: Tablet"
}
```

### Step 4: Verify Data

- **Get all items:** `GET http://localhost:8080/items`
- **Get all orders:** `GET http://localhost:8080/orders`
- **Get all payments:** `GET http://localhost:8080/payments`

---

## Docker Images

All services use optimized Docker images:

| Service         | Base Image                          | Optimizations                          |
| --------------- | ----------------------------------- | -------------------------------------- |
| API Gateway     | `node:20-alpine`                    | Alpine Linux, production-only deps     |
| Item Service    | `python:3.11-slim`                  | Slim image, no-cache pip install       |
| Order Service   | `python:3.11-slim`                  | Slim image, no-cache pip install       |
| Payment Service | `mcr.microsoft.com/dotnet/aspnet:8.0` | Multi-stage build, SDK not in final image |

---

## Useful Docker Commands

```bash
# Build and start all services
docker-compose up --build

# Start in detached mode (background)
docker-compose up --build -d

# View running containers
docker ps

# View logs for a specific service
docker-compose logs item-service
docker-compose logs order-service
docker-compose logs payment-service
docker-compose logs api-gateway

# Stop all services
docker-compose down

# Rebuild a single service
docker-compose build payment-service
```