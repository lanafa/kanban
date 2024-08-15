Kanban Board Application
Project Overview
This project is a Kanban board system implemented as part of an academic course. The Kanban board allows users to organize and prioritize tasks using boards, lists, and cards, supporting agile development and helping visualize workflows. The system is designed with a graphical user interface (GUI) using WPF and follows the Model-View-ViewModel (MVVM) architecture.

Features
User Management: User registration, login, and logout functionalities.
Board Management: Users can create, join, and manage multiple Kanban boards.
Task Management: Users can create, assign, and manage tasks across predefined columns (Backlog, In Progress, Done).
Data Persistence: All user, board, and task data is stored using SQLite for persistence across sessions.
Logging and Error Handling: The system logs important events and handles errors using log4net.
Technologies Used
C# with WPF (MVVM) for GUI
SQLite for data storage
log4net for logging
N-tier architecture
Getting Started
Clone the repository:
bash
Copy code
git clone https://github.com/yourusername/kanban.git
Open the solution in Visual Studio.
Build the solution.
Run the application to launch the Kanban board interface.
Project Structure
Backend: Contains service, business, and data access layers.
Frontend: WPF-based GUI that interacts with the backend.
BackendTests: Console project for testing the backend functionality.
How to Use
Register a new account with a valid email and password.
Login with your credentials.
Create a new board and add tasks to it.
Manage tasks by moving them between columns and updating their details.
Design Documentation
Detailed design documents, including the class diagram, can be found in the documents folder.
