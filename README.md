<details open="open">
  <ol>
    <li><a href="#about">About</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#running-the-project">Running the Project</a></li>
    <li><a href="#logging">Logging</a></li>
    <li><a href="#deployment-branches"</a>Deployment Branches</li>
  </ol>
</details>

## About

This app is a twitter stream reader.

1. API
1. Unit Testing


### Built With

- [DotNet Core 3.1](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1)

## Contributing

This project uses git for its source control. 

1. Clone or fork the project
1. Create a branch via DevOps linked to a work item
1. Code your solution
1. Commit early and often
1. Open a Pull Request
1. Seek approval by two other developers for a merge into `main` or `master`

## Running the Project

1. CD to `..Twitter.Stream.Reader` 
1. Open the solution in Visual Studio
1. R-click the solution and select `Common Properties` -> `Startup Project` on the left menu
1. Choose to start project: 
    - Twitter.Stream.Reader       
1. F5 the solution to debug
1. No of tweets and average number displayed on console every minute. which is configurable, so is reset counter at the end of day.

## Logging

This project enriches its logs with serilog sink to console.

