# TeachMate

Welcome to **TeachMate** - A comprehensive platform for tutors and learners to connect, enroll in classes, and enhance their learning experiences. This project provides a robust API built with .NET, offering RESTful endpoints and real-time features to support a dynamic educational environment.

## Table of Contents

- [About](#about)
- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Real-Time Features](#real-time-features)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## About

TeachMate is a platform designed to bridge the gap between tutors and learners. It provides a seamless way for learners to find and enroll in classes, and for tutors to manage their classes and students. The platform is built to handle the dynamics of educational interactions, ensuring a smooth and efficient user experience.

## Features

- **User Authentication**: Secure login and registration for tutors and learners.
- **Class Management**: Tutors can create, update, and delete classes.
- **Enrollment System**: Learners can browse and enroll in available classes.
- **Real-Time Notifications**: Stay updated with real-time notifications for class updates and messages.
- **Feedback and Ratings**: Learners can provide feedback and rate their learning experience.
- **Profile Management**: Both tutors and learners can manage their profiles.

## Technologies

- **Backend**: .NET Core
- **Database**: SQL Server
- **API**: RESTful API
- **Real-Time Features**: Ably
- **Authentication**: JWT (JSON Web Tokens)
- **Documentation**: Swagger

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/CodieGlot/TeachMate.Server.git
   cd TeachMate.Server

### Setting Up the Database

1. Update the connection string in `appsettings.json` to point to your SQL Server instance.
2. Run the following command to apply migrations and create the database:
    ```sh
    dotnet ef database update
    ```

### Running the Application

1. Run the application using the command:
    ```sh
    dotnet run
    ```
2. Open your browser and navigate to [https://localhost:5001/swagger](https://localhost:5001/swagger) to view the API documentation.

## API Documentation

TeachMate's API is documented using Swagger. Once the application is running, you can access the API documentation at [https://localhost:5001/swagger](https://localhost:5001/swagger).

## Real-Time Features

TeachMate uses Ably to implement real-time features such as notifications and messages. Ably is integrated to provide a seamless real-time experience for users.

### Setting Up Ably

Ably API key is configured in the `appsettings.json` file. Ensure that the Ably client is correctly set up on the frontend to connect and communicate with the Ably service on the server.

## Contributing

We welcome contributions from the community! To contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch:
    ```sh
    git checkout -b feature/YourFeature
    ```
3. Make your changes.
4. Commit your changes:
    ```sh
    git commit -m 'Add some feature'
    ```
5. Push to the branch:
    ```sh
    git push origin feature/YourFeature
    ```
6. Open a Pull Request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Contact

If you have any questions or feedback, feel free to reach out to us:

- Email: [codie.technical@gmail.com](mailto:codie.technical@gmail.com)
- GitHub Issues: [https://github.com/CodieGlot/TeachMate.Server/issues](https://github.com/CodieGlot/TeachMate.Server/issues)

Thank you for using TeachMate! We hope it enhances your teaching and learning experience.
