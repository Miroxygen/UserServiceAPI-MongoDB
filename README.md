### User service

Responsible for handling the "Pedigree chart" of Litter. This is the users own page where it is possible to find the lits a user has created, and the functions "leash/unleash" is also possible here for authenticated users. The Pedigree chart itself is reachable for unauthenticated users. The purpose of user-service is to hold a place for the user page, it's collected lits and a follow/un-follow function. The user service itself will not hold the lits, as that its the responsibility of the lit-service. Instead user-service will 
keep information about the users name, id, email, leashes and leashings. This is because the database requirements for the lits are heavier than for the users own information, and so seperating them is necessary to create a better overview and for the users information to be kept in a slimmer database.

###### Flow

- User leashes or unleashes another user on their page.
- Correct leash status is set in database.

and

- Present correct data about users leashes and leashings.

###### Protocols

- HTTP
- REST

###### Patterns



###### Stack
Web application framework, Database
