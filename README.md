# Full Stack Movie DB

A simple full-stack movie tracking application built with React, ASP.NET Core, Entity Framework Core, and SQLite. This project demonstrates clean CRUD architecture, a lightweight REST API, and unit testing with xUnit.

## Getting started
- Clone the repository
- In Visual Studio's Solution Explorer, right click on FullStackMovieDB.Server and then click Set as Startup Project
- Press F5 to build and start the server
- Copy the local address shown in the terminal
- Paste the address into a modern web browser to access the client

### Starting client and server together
- In Solution Explorer, right click fullstackmoviedb.client, hover over Add, and then click New Folder
- Name the folder .vscode
- Right click the .vscode folder, hover over Add, and then click New Item...
- Select JSON File and change the name to launch.json
- Replace the contents of launch.json with this:
```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "type": "edge",
      "request": "launch",
      "name": "localhost (Edge)",
      "url": "https://localhost:57917",
      "webRoot": "${workspaceFolder}"
    },
    {
      "type": "chrome",
      "request": "launch",
      "name": "localhost (Chrome)",
      "url": "https://localhost:57917",
      "webRoot": "${workspaceFolder}"
    }
  ]
}
```
- If you've changed the port number elsewhere, then update the port numbers in launch.json too
- Right click the FullStackMovieDB solution and then click Configure Startup Projects...
- Select Multiple startup projects:
- Set the Action for fullstackmoviedb.client to Start
- Set the Action for FullStackMovieDB.Server to Start
- Change the order so Server is above client and then click ok
- Press F5 to build and start the server and the client
- You may need to refresh the page once after the backend has fully loaded

### Unit testing
- Click View and then click Test Explorer
- Click Run All Tests In View

## Looking for more?
For a more in-depth example of my work, see my flagship project: [Game Elements](https://github.com/klykinsjobs/Game-Elements).

## License
Full Stack Movie DB is licensed under the MIT license. See the LICENSE file for details.
