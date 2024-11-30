# Expense-Tracker.API

ExpenseTracker.API is a robust RESTful API built with ASP.NET Core to help users manage their finances. It allows users to track expenses, set budgets, and generate insightful reports.

---

## **Features**

- **User Authentication and Authorization**: Secure user registration and login using JWT.
- **Expense Management**: Add, view, edit, and delete expenses.
- **Budget Management**: Set budgets for specific categories or overall spending.
- **Reports and Insights**: Get insights on expenses and budgets over a specified time period.
- **Category Management**: Organize expenses into customizable categories.

---

## **Technologies Used**

- **Framework**: ASP.NET Core 6
- **Database**: Entity Framework Core with SQLite (or SQL Server)
- **Authentication**: JWT-based authentication
- **Language**: C#
- **Tools**: Swagger for API documentation

---

## **Getting Started**

### **Prerequisites**

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed
- A database setup (SQLite, SQL Server, or any EF Core-compatible database)

### **Installation**

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/ExpenseTracker.API.git
   cd ExpenseTracker.API


## **API Endpoints**
### **Authentication**
- POST /api/auth/register - Register a new user.
- POST /api/auth/login - Login and receive a JWT token.

### **Expenses**
- GET /api/expenses - Get all expenses for the logged-in user.
- POST /api/expense - Add a new expense.
- PUT /api/expense/{id} - Update an expense.
- DELETE /api/expense/{id} - Delete an expense.
- DELETE /api/expense/total - Get all expenses for a specific period of time providing startDate and endDate paramters

### **Budgets**
- POST /api/budgets/create - Create a budget.
- GET /api/budgets/reports/budget-insights - Get insights and reports on budgets and expenses.

### **Categories**
- GET /api/category - Get all categories.
- POST /api/category - Add a new category.

## **Database Design**
### **Entities**
- User: Manages user authentication and links to expenses and budgets.
- Expense: Tracks individual expense details.
- Category: Groups expenses into meaningful categories.
- Budget: Defines spending limits and tracks against expenses.

##Relationships
- A User can have many Expenses and Budgets.
- An Expense belongs to a Category.
