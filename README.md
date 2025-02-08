# HitchList API

## Description

HitchList is a web application API that allows users to manage admin,events, guests, and tasks for a wedding. This project uses ASP.NET Core Web API and Entity Framework Core for data handling. It provides endpoints for CRUD operations (Create, Read, Update, Delete) on events, guests, and tasks.

## Features

- **Events**: Manage events including their name, date, location, and category.
- **Guests**: Add and manage guest information, including invitation status and category.
- **Tasks**: Create and update tasks, with descriptions, due dates, and categories.
- **Admin**: Admins to access and make changes to events, guests and tasks.

## Technologies Used

- **ASP.NET Core Web API**
- **Entity Framework Core**
- **Microsoft SQL Server** (or any database configured in your project)

## Project Setup

### Prerequisites

- .NET 6.0 or higher
- Visual Studio Code (VS Code) or any IDE of your choice
- SQL Server (or configured database)

### Installation

1. Clone the repository to your local machine:

    ```bash
    https://github.com/SarrahGandhi/hitchlist.git
    ```

2. Navigate to the project folder:

    ```bash
    cd HitchList
    ```

3. Install the necessary dependencies:

    ```bash
    dotnet restore
    ```

4. Apply database migrations:

    ```bash
    dotnet ef database update
    ```

5. Run the application:

    ```bash
    dotnet run
    ```



## API Endpoints

### Event API

- **GET /api/EventAPI/Event**: Get all events
- **GET /api/EventAPI/Event{id}**: Get event by ID
- **POST /api/EventAPI/AddEvent**: Add a new event
- **PUT /api/EventAPI/UpdateEvent/{id}**: Update an existing event
- **DELETE /api/EventAPI/{id}**: Delete an event

### Guest API

- **GET /api/Guest/Guest**: Get all guests
- **GET /api/Guest/Guest{id}**: Get guest by ID
- **POST /api/Guest/AddGuest**: Add a new guest
- **PUT /api/Guest/UpdateGuest/{id}**: Update an existing guest
- **DELETE /api/Guest/{id}**: Delete a guest

### Task API

- **GET /api/TaskAPI/Tasks**: Get all tasks
- **GET /api/TaskAPI/Task{id}**: Get task by ID
- **POST /api/TaskAPI/AddTask**: Add a new task
- **PUT /api/TaskAPI/UpdateTask/{id}**: Update an existing task
- **DELETE /api/TaskAPI/{id}**: Delete a task

