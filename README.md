# Kanban Board Application

## Project Overview
This project is a Kanban board system implemented as part of an academic course. The Kanban board allows users to organize and prioritize tasks using boards, lists, and cards, supporting agile development and helping visualize workflows. The system is designed with a graphical user interface (GUI) using WPF and follows the Model-View-ViewModel (MVVM) architecture.

## Features
- **User Management:**
  - Registration, login, and logout functionalities.
- **Board Management:**
  - Users can create, join, and manage multiple Kanban boards.
- **Task Management:**
  - Create, assign, and manage tasks across predefined columns (`Backlog`, `In Progress`, `Done`).
- **Data Persistence:**
  - All user, board, and task data is stored using SQLite for persistence across sessions.
- **Logging and Error Handling:**
  - System logs important events and handles errors using log4net.

## Technologies Used
- **C# with WPF (MVVM) for GUI**
- **SQLite for data storage**
- **log4net for logging**
- **N-tier architecture**

## Getting Started
1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/kanban.git
