MatchGoal - Football Match Management API
Overview
MatchGoal is a RESTful API built to manage football match data, allowing users to create, read, update, and delete match records. The application supports dynamic filtering, secure data validation, and efficient database operations. It showcases skills in .NET Core, Entity Framework, and API design, with a focus on robust backend development and data integrity.

Technologies Used
Back-end: C#, .NET Core, ASP.NET Core
Database: SQL Server, Entity Framework Core
Tools: Visual Studio, Git, Postman
Other: JSON Serialization, Custom Validation Attributes, Unit Testing
Features
CRUD Operations: Create, update, delete, and retrieve football match details (teams, scores, competition, status).
Custom Validation: Ensures data integrity with attributes like team ID verification and match status consistency.
Dynamic Filtering: Search and filter matches by team, competition, or status.
Secure Serialization: Handles JSON data with case-insensitive enum deserialization and error handling.
Database Optimization: Efficient queries with indexed foreign keys for performance.
Getting Started
Prerequisites
.NET 8.0 SDK
SQL Server
Visual Studio or VS Code
Git
Installation
Clone the repository:
bash

Copy
git clone https://github.com/yourusername/MatchGoal.git
Navigate to the project directory:
bash

Copy
cd MatchGoal
Update the database connection string in appsettings.json:
json

Copy
"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=MatchGoalDb;Trusted_Connection=True;"
}
Apply database migrations:
bash

Copy
dotnet ef database update
Run the application:
bash

Copy
dotnet run
API Endpoints
POST /api/matches: Create a new match
json

Copy
{
    "homeTeamID": 1,
    "awayTeamID": 2,
    "competitionID": 1,
    "matchDate": "2025-06-29T00:00:00",
    "status": "NotStarted",
    "stadium": "Stadium Name"
}
GET /api/matches: Retrieve all matches
GET /api/matches/{id}: Retrieve a specific match
PUT /api/matches/{id}: Update a match
DELETE /api/matches/{id}: Delete a match
Future Enhancements
Integrate real-time match updates using SignalR.
Add advanced analytics for team performance.
Develop a front-end interface for match management.
Contact
For questions or feedback, reach out:

LinkedIn: Mostafa Magdy
Email: mostafamagdy12304@gmail.com
