##  Description

This PR introduces the **User Authentication** using dotnet identity.

### What's Included:
- [x] Endpoints for user login, register, verify otp, get profile, and logout
- [x] Follows proper coding practices and structure
- [x] Secure authentication using JWT token
- [x] Two-step authentication using Google Authenticator App
- [ ] Other user operations like password reset, update profile, etc. (soon to be implemented)

##  Related Issues / Tickets

None at the moment

##  Checklist

- [x] Code follows project structure and standards
- [x] Authentication tested locally
- [x] Connected to a persistent database
- [ ] Middleware/authorization added
- [ ] No console errors or warnings
- [ ] PR is reviewed by at least one other developer

##  Changes Made

- Added `POST /api/v1/auth/register` to register a user
- Added `POST /api/v1/auth/login` to login
- Added `POST /api/v1/auth/verify` to verify the otp and get jwt token
- Added `GET /api/v1/auth/profile` to get the logged in user's details
- Added `POST /api/v1/auth/logout` to logout of the system

## Notes

> Authorization middleware is **not implemented yet** but planned.

##  How to Test

1. Setup the credentials in the `appsettings.json` file for database and jwt
1. Start the server:  `dotnet run` 
2. Use Postman or cURL to test:
   - `POST /api/v1/auth/register`
   - `POST /api/v1/auth/login`
   - `POST /api/v1/auth/verify`
   - `GET /api/v1/auth/profile`
   - `POST /api/v1/auth/logout`
3. Verify expected behavior in each case

