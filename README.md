# 🏋️ FitnessTracker — ASP.NET Core MVC Web Application

A beginner-friendly fitness tracking web app built with **ASP.NET Core 8 MVC**, **C#**, **Entity Framework Core**, **SQLite**, and **Bootstrap 5**.

---

## 📋 Table of Contents
1. [Features](#features)
2. [Project Structure](#project-structure)
3. [File Explanations](#file-explanations)
4. [Prerequisites](#prerequisites)
5. [How to Run in Visual Studio](#how-to-run-in-visual-studio)
6. [How to Run with CLI](#how-to-run-with-cli)
7. [Sample Login](#sample-login)
8. [Architecture Overview](#architecture-overview)
9. [How MVC Works in This Project](#how-mvc-works-in-this-project)

---

## ✨ Features

| Feature | Description |
|---|---|
| 🔐 User Registration & Login | Secure password hashing with BCrypt |
| 📊 Dashboard | Summary of total workouts, calories, steps |
| 💪 Workout Tracking | Add, Edit, Delete workouts |
| 👟 Step Tracker | Log daily steps with progress toward 10,000 goal |
| ⚖️ BMI Calculator | Instant BMI with WHO category classification |
| 🗄️ SQLite Database | No SQL Server installation needed |
| 📱 Responsive UI | Bootstrap 5 — works on mobile and desktop |

---

## 📁 Project Structure

```
FitnessTracker/
│
├── Controllers/                 ← Handle HTTP requests, call the database
│   ├── HomeController.cs        ← Landing page
│   ├── AccountController.cs     ← Register, Login, Logout
│   ├── DashboardController.cs   ← Dashboard stats
│   ├── WorkoutController.cs     ← CRUD for workouts
│   ├── StepsController.cs       ← Step entry tracking
│   └── BmiController.cs         ← BMI calculation
│
├── Models/                      ← Data structures (maps to DB tables)
│   ├── User.cs                  ← Users table
│   ├── Workout.cs               ← Workouts table
│   ├── StepEntry.cs             ← StepEntries table
│   └── ViewModels.cs            ← Form/display models (not DB tables)
│
├── Data/
│   └── AppDbContext.cs          ← Database connection + table configuration
│
├── Views/                       ← HTML templates (Razor .cshtml files)
│   ├── Shared/
│   │   └── _Layout.cshtml       ← Master layout (navbar, footer)
│   ├── Home/
│   │   └── Index.cshtml         ← Landing page
│   ├── Account/
│   │   ├── Register.cshtml      ← Registration form
│   │   └── Login.cshtml         ← Login form
│   ├── Dashboard/
│   │   └── Index.cshtml         ← Dashboard page
│   ├── Workout/
│   │   ├── Index.cshtml         ← Workout list
│   │   ├── Add.cshtml           ← Add workout form
│   │   └── Edit.cshtml          ← Edit workout form
│   ├── Steps/
│   │   └── Index.cshtml         ← Step tracker
│   ├── Bmi/
│   │   └── Index.cshtml         ← BMI calculator
│   ├── _ViewImports.cshtml      ← Global using statements for views
│   └── _ViewStart.cshtml        ← Sets _Layout as default layout
│
├── wwwroot/
│   └── css/
│       └── site.css             ← Custom CSS styles
│
├── Program.cs                   ← App entry point, service registration
├── appsettings.json             ← App configuration
└── FitnessTracker.csproj        ← Project file with NuGet packages
```

---

## 📄 File Explanations

### Controllers
| File | Purpose |
|---|---|
| `HomeController.cs` | Shows the public landing page. Redirects to Dashboard if logged in. |
| `AccountController.cs` | Register new users (hashes passwords with BCrypt), login (verifies hash, creates cookie), logout. |
| `DashboardController.cs` | Queries DB for totals (workouts, calories, steps) and passes to view. |
| `WorkoutController.cs` | Full CRUD: list all, add new, edit existing, delete — all filtered by the logged-in user's ID. |
| `StepsController.cs` | Add and delete daily step entries. Shows all entries on the same page. |
| `BmiController.cs` | Accepts height+weight, calculates BMI using formula, classifies into WHO category. |

### Models
| File | Purpose |
|---|---|
| `User.cs` | Maps to the `Users` table. Has Id, Username, Email, PasswordHash. |
| `Workout.cs` | Maps to the `Workouts` table. Has Id, UserId (FK), Name, Duration, Calories, Date. |
| `StepEntry.cs` | Maps to the `StepEntries` table. Has Id, UserId (FK), Date, StepCount. |
| `ViewModels.cs` | `RegisterViewModel`, `LoginViewModel`, `DashboardViewModel`, `BmiViewModel` — these are for forms and displays, not stored in DB. |

### Data
| File | Purpose |
|---|---|
| `AppDbContext.cs` | The EF Core "bridge" to SQLite. Defines the 3 DbSets (tables), configures relationships, and seeds sample data. |

### Views
| File | Purpose |
|---|---|
| `_Layout.cshtml` | Master template. All other views render inside `@RenderBody()`. Contains navbar and footer. |
| `Home/Index.cshtml` | Marketing landing page with feature highlights. |
| `Account/Register.cshtml` | Registration form with validation. |
| `Account/Login.cshtml` | Login form. Shows demo credentials. |
| `Dashboard/Index.cshtml` | 4 stat cards + recent workouts + recent steps tables. |
| `Workout/Index.cshtml` | Table of all workouts with edit/delete buttons. |
| `Workout/Add.cshtml` | Form to create a new workout. |
| `Workout/Edit.cshtml` | Pre-filled form to update a workout. |
| `Steps/Index.cshtml` | Left: add steps form. Right: step history table with progress bars. |
| `Bmi/Index.cshtml` | Height + weight form, result display with color-coded category. |

---

## ✅ Prerequisites

Install these **before** running the project:

1. **Visual Studio 2022** (Community edition is free)
   - Download: https://visualstudio.microsoft.com/
   - During install, select workload: **"ASP.NET and web development"**

2. **.NET 8 SDK** (usually included with VS 2022)
   - Verify by opening a terminal and typing: `dotnet --version`
   - Should show `8.x.x`

---

## 🚀 How to Run in Visual Studio

### Step 1: Open the Project
1. Open **Visual Studio 2022**
2. Click **"Open a project or solution"**
3. Navigate to the `FitnessTracker` folder
4. Select `FitnessTracker.csproj` and click **Open**

### Step 2: Restore NuGet Packages
- Visual Studio usually does this automatically
- If not: right-click the project → **"Restore NuGet Packages"**
- Or go to **Tools → NuGet Package Manager → Package Manager Console** and type:
  ```
  dotnet restore
  ```

### Step 3: Run the Application
- Press **F5** (or click the green ▶ button) to start with debugging
- Press **Ctrl+F5** to start without debugging (faster)
- The browser will open automatically at `https://localhost:7xxx`

### Step 4: The Database is Created Automatically!
- On first run, `EnsureCreated()` in `Program.cs` creates `FitnessTracker.db`
- Sample data (John Doe's account + workouts + steps) is inserted automatically
- No SQL scripts to run — Entity Framework handles everything!

---

## 💻 How to Run with CLI (Command Line)

If you prefer the terminal:

```bash
# 1. Navigate to the project folder
cd FitnessTracker

# 2. Restore packages
dotnet restore

# 3. Run the app
dotnet run

# 4. Open browser at the URL shown in the terminal
# Usually: https://localhost:5001 or http://localhost:5000
```

---

## 🔑 Sample Login

Use this account to explore the app immediately (no registration needed):

| Field | Value |
|---|---|
| **Email** | `john@example.com` |
| **Password** | `john123` |

This account has sample workouts and step entries pre-loaded.

---

## 🏗️ Architecture Overview

```
Browser (User)
     │
     │ HTTP Request (GET /Dashboard)
     ▼
┌─────────────────┐
│   Controller    │  ← Receives request, talks to DB, returns a View
│ DashboardCtrl   │
└────────┬────────┘
         │ Queries database
         ▼
┌─────────────────┐
│  AppDbContext   │  ← Entity Framework translates C# to SQL
│   (SQLite DB)   │
└────────┬────────┘
         │ Returns data
         ▼
┌─────────────────┐
│    ViewModel    │  ← Packaged data to send to the View
│ DashboardVM     │
└────────┬────────┘
         │ Passed to view
         ▼
┌─────────────────┐
│      View       │  ← Razor template generates HTML
│  Dashboard/     │
│  Index.cshtml   │
└────────┬────────┘
         │ HTML Response
         ▼
     Browser
```

---

## 📚 How MVC Works in This Project

**MVC = Model-View-Controller** — a design pattern that separates concerns:

| Layer | Role | Example in this project |
|---|---|---|
| **Model** | Data & business rules | `Workout.cs`, `User.cs` |
| **View** | What the user sees | `Views/Workout/Index.cshtml` |
| **Controller** | The middleman | `WorkoutController.cs` |

### Example Flow: Adding a Workout

1. User visits `/Workout/Add` → `WorkoutController.Add()` [GET] is called
2. Controller creates an empty `Workout` object, returns `View(workout)`
3. Razor renders `Add.cshtml` with the empty form
4. User fills in the form and clicks "Save"
5. Browser sends POST to `/Workout/Add`
6. `WorkoutController.Add(workout)` [POST] receives the filled model
7. Model validation runs — if errors, re-show form
8. If valid, `_db.Workouts.Add(workout)` saves to SQLite
9. `RedirectToAction("Index")` sends user to the workout list

---

## 🔒 Security Notes

- **Passwords** are hashed with BCrypt (industry standard) — never stored as plain text
- **Anti-Forgery Tokens** prevent CSRF attacks on all forms
- **[Authorize]** attribute ensures logged-out users can't access private pages
- **Ownership checks** ensure users can only edit/delete their own data

---

## 🤔 Common Issues

| Problem | Solution |
|---|---|
| "The name 'BCrypt' does not exist" | Run `dotnet restore` — packages missing |
| Port already in use | Change port in Properties/launchSettings.json |
| Database locked | Close all other instances of the app |
| Styles not loading | Run `dotnet build` then try again |

---

*Built for second-year computer science students learning ASP.NET Core MVC. Happy coding! 🚀*
