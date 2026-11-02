# C# Job Application Tracker

A console-based job application tracker built with C# and .NET 8.0.

## What I Built

A fully functional console application that allows you to:
- Add new job applications with company, role, location, date applied, and status
- List all job applications
- Update job status (Applied, Interview, Offer, Rejected)
- Delete job applications
- View count of jobs per status
- Filter jobs by status
- Persist data to a JSON file (jobs.json) that survives program restarts

## Features Implemented

- **Menu-driven interface** with a loop that runs until user chooses to exit
- **Job class** with properties: Company, Role, Location, DateApplied, Status
- **Status enum** for type-safe status values
- **Input validation** using TryParse for dates and status enums, with friendly error messages
- **LINQ operations** for filtering (Where), counting (Count), and grouping (GroupBy)
- **JSON persistence** using System.Text.Json to save/load jobs
- **Separate files** for clean code organization: Job.cs, JobService.cs, Program.cs

## What Was Hard

- Understanding LINQ syntax and when to use different operations (Where vs GroupBy)
- Handling DateTime parsing with different input formats
- Managing the index-based job selection (converting between 1-based user input and 0-based array access)
- JSON serialization/deserialization edge cases (null handling)

## What I Would Do Next

- Add search functionality to filter by company name
- Add sorting options (by date, by company, by status)
- Add the ability to edit all job fields, not just status
- Add a backup/restore feature for the JSON file
- Add color coding for different statuses in the console output

## How to Run

### Option 1: With .NET SDK installed
```bash
dotnet run
```

### Option 2: Without .NET SDK
Copy the code from Program.cs, Job.cs, and JobService.cs to [dotnetfiddle.net](https://dotnetfiddle.net) and run it online.

## React vs C# Comparison

React felt easier for building a visual interface quickly - the component structure and declarative UI made it simple to see what the app would look like. State management with useState was intuitive, and the reactivity (UI updating automatically when state changes) saved a lot of manual work.

C# felt more structured and type-safe, which caught errors early during development. The strong typing with enums and classes made the code more predictable. However, building the console UI manually was more tedious than React's declarative approach. LINQ was powerful for data manipulation but had a steeper learning curve than JavaScript's array methods.

React prepares you for front-end roles and full-stack web development, where user experience and visual interfaces are key. C# prepares you for backend roles, enterprise applications, and .NET ecosystem jobs where performance, type safety, and structured architecture matter more. React is more about "what the UI looks like," while C# is more about "how the data is processed and stored."
