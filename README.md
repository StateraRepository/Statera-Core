# Confidential
These code repositories are intended to be examples and starter kits for Statera clients only.  The code added to these repositories should not have company specific information.

# Introduction 

This solution has 3 applications that interact using an OpenId Jason Web token that is provided by IdentityServer4 after successful login:  the IdentityServer4 “authority” site, the Angular web site, and the Api.  The web site is an SPA using Angular 4 and uses the oidc client library to interact with the “authority” STS server for authentication.  An Api call is initiated from the web site sending the auth token with its claims.  The Api consumes the token, authorizes the call using the subject and the claims before executing any of the api method code.
 

# Getting Started
Clone this project
Open and Build the IdentityServerAngularWebApiOpenId.sln solution 
Start Api site:
•	Open command prompt in the folder that contains \Company.Api
•	Type “dotnet run”, which should start the api at http://localhost:52018
Start IdentityServer site:
•	Open command prompt in the folder that contains \Company.IdentityServer
•	Type “dotnet run”, which should start the app at http://localhost:5010
•	First time run will create the aspnet Identity tables in localdb with a couple of test users
Start the Angular web site:
•	Make sure you have node, npm, webpack, and yarn installed.
•	Open command prompt in the folder that contains \Company.Web
•	Type “yarn install”, which will setup your nodemodules dependencies
•	Type “npm start”, which should start the app at http://localhost:5003 in dev mode with hot module reloading (HMR)



# Build and Test
Testing the site with authentication using IdentityServer:
•	Navigate to http://localhost:5003, click Login
•	It should take you to http://localhost:5010 to a login page
•	Type user1@company.com, password Password123!
•	It should log you in and return you to the Angular site on port 5003
•	Clicking the Contact Info menu item will initiate an api call to fetch some user info.  Because the database tables for user information hasn’t been created yet it will fail.
•	Check api status.  Go to http://localhost:52018/api/contacts/status which should also fail due to no database tables being implemented yet. 


