# Base Starter Templates for different ServiceStack Hosts

These starter templates show the default configuration required to run ServiceStack under a number of different hosts:

  * ConsoleAppHost - Host as a stand-alone Console Application using HttpListener
  * RootPath45 - Host at '/' on .NET 4.5
  * CustomPath45 - Host at '/api' on .NET 4.5

When adding static files in a Console or Windows Service host, remember to set the Build Action = "Content" and Copy to Output Directory settings.

