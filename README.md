# 📝 Multi-Tenant Task Tracker API

## 📖 Overview
The **Task Tracker API** is a multi-tenant task management system built with **ASP.NET Core**, **Entity Framework Core**, and **PostgreSQL**.  
It supports:
- **Multi-Tenancy** (isolation per tenant via `TenantId`).
- **JWT Authentication** (Bearer tokens).
- **Role-Based Authorization** (`Admin`, `Maintainer`, `Viewer`).
- **Route-Level + Tenant-Level Authorization**.
- **Tasks Management** (CRUD).
- **Teams & Users Management**.


---

## ⚡ Features
- Secure **login & signup** with JWT.
- Automatically attach `tenantId` from user **claims** (no manual input).
- Role-based access (Admin can manage all, Maintainer limited, Viewer read-only).
- Multi-tenant data isolation (a tenant cannot access another tenant’s data).
- PostgreSQL as the primary database.
- Entity Framework Core with migrations.
- Swagger UI for API testing.

---

## 🛠️ Tech Stack
- **.NET 7 / ASP.NET Core Web API**
- **Entity Framework Core 7**
- **PostgreSQL**
- **JWT Authentication**
- **Swagger / Swashbuckle**
- **MediatR + CQRS Pattern**
- **AutoMapper**

---

## 📦 Prerequisites
Make sure you have installed:
- [Visual Studio 2022](https://visualstudio.microsoft.com/) 
- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL 4](https://www.postgresql.org/download/)


---


