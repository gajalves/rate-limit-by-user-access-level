# Rate Limitation by User Access Level
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![C#](https://img.shields.io/badge/C%23-.NET-blue)](https://docs.microsoft.com/en-us/dotnet/csharp/) [![Redis](https://img.shields.io/badge/Redis-Database-red)](https://redis.io/)
This application provides rate limitation functionality based on the user's access level. It allows users to authenticate and access certain endpoints while enforcing rate limits according to their subscription profile.

## Features:
- **Authentication**: Users can log in with their credentials (email and password) to obtain a JSON Web Token (JWT) for accessing protected endpoints.
- **Rate Limit**: The application enforces rate limits on API requests based on the user's access level (e.g., Free, Basic, Pro, Enterprise). Each access level has a predefined limit on the number of requests allowed within a specific time period.
- **Middleware Integration**: Middleware intercepts incoming requests, validates the user's access level, and checks if the request exceeds the rate limit for the user's profile.
- **Cache Integration**: Cache service is used to store and retrieve rate limit information for efficient access and update operations.

## Endpoints:
- **`POST /api/auth/login`**: Endpoint for user authentication. Users provide their email and password to obtain a JWT for accessing protected endpoints.
- **`GET /api/testratelimit`**: Example endpoint demonstrating rate limitation functionality. Requires authentication and returns the number of requests made within the rate limit.
- **`GET /api/user/GetUsers`**: Endpoint for retrieving all users.

# Deployment with Docker

The project is very easy to deploy in a Docker container

## Prerequisites
- Docker installed on your system
- Docker Compose (optional but recommended)

## Run the Docker Containers
```sh
cd rate-limit-by-user-access-level
docker-compose up
```

Once done the access the Application :
- The API should be accessible at http://localhost:5002.
- Port 5002 is mapped to the container's port 80, where the API is hosted.
- The Redis server is accessible at localhost:6379.