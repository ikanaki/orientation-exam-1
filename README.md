# Sample Orientation Exam

## Getting Started

- **Fork** this repository under your own account
- Clone the forked repository under your account to your computer
- Create a `.gitignore` file so generated files won't be committed
- Commit your progress frequently and with descriptive commit messages
- All your answers and solutions should go in this repository

## Keep in mind

- You can use any resource online, but **please work individually**

- **Don't just copy-paste** your answers and solutions, use your own words
  instead

- **Don't push your work** to GitHub until your mentor announces that the time
  is up

- Before diving to coding **think about the structure**

## Tasks

# Space Transporter

The year is 2369. Humanity has evolved and managed to reach out to stars using
various warp capable space ships. However, human clumsiness and inability to organize without nicely organized tables still remains.

Our goal is to build application that will help us keep track of ships travelling
between planets in our newly founded galactic federation.

## Domain description

- Every ship has a name and maximum warp speed (floating-point number).
    - At any time, ship is located at one of the planets.
    - Ship can be either docked or undocked. Docked ships cannot travel
      to another ship until undocked.

- Planets are represented just by their name.
    - There is no limit to as how many ships can be located at any given planet.
    - Assume that planet names are unique.

## Frontend

![main](assets/frontpage.png)

- The **frontend** consists of a single page

  - a heading with the title of the site
  - table of existing ships as depicted above, which allows us
    to undock or dock ships (depending on their current state)
    by clicking a link
        - we are redirected back to main page after using the 
  - form that allows us to move ship to a different planet using
    select field
    - only ships that are currently undocked should be displayed
      as option in select field
  - form that allows us to create new ship

## Database

It is up to you to define what the database model will look
like, it just needs to help backed fulfill outlined functionality.

Only requirements are:

* all the ship and planet data needs to be stored in database
* ships and planets will be stored in separated tables
* there must be some relationship between these tables

## Endpoints

**You might need more endpoints to implement all the functionality.** Following
endpoints are the mandatory ones.

### GET `/`

- the endpoint should render an HTML displaying the frontend page
  as described above

### POST `/ships/{id}/move/`

- this endpoint should be responsible for moving the ship around

- you should check if the `id` of ship provided in path is valid
- you should also check whether given ship is undocked

- send information about which planet should ship move to in body of the 
  request

- save changes

- redirect back to the main page

### POST `/ships`

- this endpoint is responsible for creation of new ship

### GET `/ships?warpAtLeast=9.5`

- this endpoint should return information about space ships whose
  maximum warp speed is at least as high as provided threshold
- returned spaceships should be **sorted, going from fastest to
  slowest**
- this is what output should look like:

```
[
    {
        "name": "Voyager",
        "id": 2,
        "location": "Earth",
        "maximumWarp": "9.975",
        "docked": true
    },
    {
        "name": "Enterprise",
        "id": 1,
        "location": "Titan",
        "maximumWarp": "9.6",
        "docked": false
    },
    ...
]
```


# SQL Question

