### User service

The User Service API manages user data operations such as creating, retrieving, updating, and deleting users. It includes features for searching users by name and managing follower relationships. Built with ASP.NET Core and MongoDB, the service ensures data integrity and detailed logging of user actions, using HTTP and REST protocols for communication.

###### User model

The User class models user data for the User Service API, designed to work with MongoDB using BSON format. It includes properties for Id (unique MongoDB ObjectId), UserId (unique integer), Name, Username, Followers (count), and Following (list of UserIds). The constructor initializes the Following list to ensure it's ready for use. This setup makes it efficient to store and manage data.

###### Services Overview

The UserService class handles interactions with the MongoDB database for user data. It establishes a secure connection using credentials and provides methods for creating, retrieving, updating, and deleting user records. It also includes methods to get all users and remove all users from the database.

The DatabaseInitializationService class checks the connection to the database. It uses UserService to try fetching user data and logs any connection errors. This helps to check if database is reachable and ready for operations. Both services are necessary for maintaining and verifying the integrity and availability of user data in the application.

###### Flow
- Get correct data about users leashes and leashings (followers).
- Fetch name on search.

###### Protocols

- HTTP
- REST

###### Patterns



###### Stack
Web application framework, Database
