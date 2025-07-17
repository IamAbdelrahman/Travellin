# Overview

**Travellin** is a full-stack web application that serves as a clone of Airbnb, built to simulate the core functionalities of a modern vacation rental platform. It allows users to explore, list, book, and manage properties with ease.

The system connects two main user roles:

- **Hosts**: Individuals who want to list their properties for short or long-term rentals.
- **Guests**: Individuals seeking accommodations for vacations, work trips, or extended stays.

Travellin provides a scalable and responsive solution with features like advanced property search, secure booking, user authentication, reviews, and AI-powered enhancements. The platform is designed to ensure seamless communication between hosts and guests while maintaining trust, transparency, and convenience.

> **Note**: This project is developed as part of the ITI Graduation Project using **ASP.NET Core Web API** for the backend, **Angular** for the frontend, and **SQL Server** as the database. It also includes actual AI/ML-powered features for fraud detection, dynamic pricing, and more.

# Features

Travellin replicates the core experience of Airbnb while introducing several advanced features to enhance usability, trust, and efficiency. The main features include:

## 🏠 Property & Listings Management
- Hosts can list properties with details, images, and amenities.
- Edit, delete, and manage availability through an interactive calendar.
- Support for seasonal and dynamic pricing (AI-powered).

## 👥 User Roles & Authentication
- Secure registration and login (JWT-based).
- Role-based access control (Guest, Host, Admin).
- Email verification and password reset flows.

## 📅 Booking System
- Guests can book available properties based on dates.
- View and manage booking history.
- Cancel and reschedule reservations based on policy rules.

## 💬 Reviews & Ratings
- Guests can review and rate properties.
- Hosts can respond to guest feedback.

## 🌍 Advanced Search & Filters
- Filter by location, price range, property type, amenities, and availability.
- Interactive map integration for location-based search.
- Search auto-completion using AI/NLP.

## 🤖 AI/ML Features
- **Fraud Detection**: Monitor and detect suspicious activities using ML models.
- **Dynamic Pricing**: Automatically adjust property prices based on demand, season, and other factors.
- **Auto-Translation**: Translate property descriptions and messages between guests and hosts.

## 👥 Co-Host Permissions
- Hosts can add co-hosts with limited management rights.
- Fine-grained control over co-host actions.

## 🛡️ Trust & Safety
- Report listings or users.
- Admin moderation for reported content.
- Verified identity badges and secure profile management.

## 📊 Admin Panel
- Admin dashboard with full control over users, listings, and reports.
- Analytics on user growth, bookings, and revenue.

## 💵 Financial Management
- Hosts can track earnings and withdrawals.
- Guests can view transaction history.
- Invoices, taxes, and payment status management.

## 📲 Notifications
- Email notifications for booking confirmations, cancellations, messages, etc.
- Real-time alerts via SignalR.

## 🌐 Responsive Design
- Fully responsive UI for all device sizes.
- Accessible and user-friendly UX.

---

# Tech Stack

## 🖥️ Frontend
- **Angular**: SPA Framework for building a dynamic and responsive UI.
- **Bootstrap & SCSS**: For styling and layout.
- **Leaflet.js or Google Maps API**: For map integration.

## 🖧 Backend
- **ASP.NET Core Web API**: For building RESTful APIs.
- **Entity Framework Core (Code First)**: ORM for data access and database migrations.
- **AutoMapper**: For DTO mapping.
- **FluentValidation**: For input validation.
- **SignalR**: For real-time notifications.

## 🧠 AI/ML
- **Python (External Scripts)** or integrated ML.NET for:
  - Fraud detection
  - Dynamic pricing
  - NLP for translation/search optimization

## 🗄️ Database
- **SQL Server**: Relational database to store all persistent data.

## 🔐 Security
- **JWT**: For secure token-based authentication.
- **ASP.NET Identity**: For managing users and roles.

## 🔧 DevOps & Tooling
- **Postman**: For API testing.
- **Swagger (Swashbuckle)**: For API documentation.
- **Git + GitHub**: Version control and collaboration.

## 📦 Architecture
- **Clean Architecture / Onion Architecture**: For maintainable and scalable backend structure.
- **Modular Frontend Architecture**: For separation of concerns and better scalability.

## 🛠️ Setup & Installation

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/Travellin.git
cd Travellin
```
### Backend Setup
```bash
cd Backend/Travellin.API
dotnet restore
dotnet build
dotnet ef database update         # Apply migrations to SQL Server
dotnet run
```
- Make sure you have a SQL Server running and update the connection string in appsettings.json.

### Frontend Setup
```bash
cd Frontend/Travellin-frontend
npm install
ng serve
```
- Navigate to http://localhost:4200/ in your browser.

## ✅ Conclusion

Travellin is a robust, full-featured Airbnb-like platform built using modern technologies and best software architecture practices. With a focus on clean architecture, modular design, and scalable features, the project aims to provide a secure, performant, and user-friendly experience for both hosts and guests.

Whether it's managing dynamic pricing, handling bookings with an availability calendar, or ensuring safe transactions through fraud detection and user reviews, Travellin serves as a strong foundation for building real-world property rental platforms.

As a graduation project, it reflects the combined efforts of a committed team using modern full-stack development practices to solve real-world problems.

We hope this system serves as a showcase of our learning, our ability to work collaboratively, and our potential as professional developers.


