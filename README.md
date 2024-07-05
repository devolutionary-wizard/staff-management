# Staff Management System

This repository contains a simple yet comprehensive Staff Management System developed as part of a technical assessment for a Senior .Net Developer position. The application is built using ASP.NET Web API for the backend and React for the frontend. It allows for managing staff details including adding, editing, deleting, and searching for staff members.

## Features

- **Staff Management**: Users can add, edit, delete, and search for staff members. Staff attributes include:
  - Staff ID (String, 8 characters)
  - Full Name (String, 100 characters)
  - Birthday (Date)
  - Gender (Integer, with 1 for Male and 2 for Female)

- **Advanced Search**: The application provides an advanced search form allowing users to search for staff members by Staff ID, Gender, and Birthday (range from date to date).

- **Reports**: Users can export search results into Excel or PDF formats.

## Technologies Used

- **Backend**: ASP.NET Web API
- **Frontend**: React
- **Testing**: xUnit for unit tests, NUnit for integration tests, and Cypress for end-to-end tests.

## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

- .NET 8.0 SDK
- Node.js
- A suitable IDE (e.g., Visual Studio, Visual Studio Code)

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/devolutionary-wizard/staff-management

2. Navigate to the project directory

   ```sh
   cd staff-management

