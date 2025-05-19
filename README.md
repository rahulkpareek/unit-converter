# Unit Converter API

A simple RESTful API for converting between different units of measurement. This service provides an efficient way to convert values across various measurement systems.

## Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Features

- RESTful API for unit conversions
- Swagger UI for interactive API documentation
- Cross-Origin Resource Sharing (CORS) enabled for frontend integration
- Lightweight and fast response times

## Requirements

- .NET 9.0 SDK or later
- Visual Studio 2022, VS Code, or any preferred IDE with C# support

## Installation

1. Clone the repository:
  ```bash
  git clone https://github.com/yourusername/unitconverterapi.git
  cd unitconverterapi
  ```

2. Build the project:
  ```bash
  dotnet build
  ```

3. Run the application:
  ```bash
  dotnet run --project unitconverterApi
  ```

4. Access the Swagger UI at:
  ```
  https://localhost:5001/swagger
  ```
  or
  ```
  http://localhost:5000/swagger
  ```

## API Documentation

The API is self-documented using Swagger. After starting the application, navigate to `/swagger` to explore all available endpoints, request parameters, and response models.

### Base URL

By default, the API runs on:
```
https://localhost:5001
http://localhost:5000
```

### Endpoints

The API endpoints are documented in the Swagger UI. The application will display the URLs it's running on when started:

```
Application started at URLs:
 https://localhost:5001
 http://localhost:5000
Swagger UI available at: https://localhost:5001/swagger
```

## Frontend Integration

The API is configured with CORS to allow requests from a frontend application running at `http://localhost:8080`. If your frontend is running on a different URL, you'll need to update the CORS policy in `Program.cs`.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.