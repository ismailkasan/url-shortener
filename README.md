# .NET 6 URL Shortener

To learn more about **Web Api** please visit offical [Microsoft](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio)

This is a .NET restful URL shortener api with redirection middleware. It creates short form of url and insert into db. Also, it has a redirection middleware to redirect short url to its original long url.
## Features

- **User Interface**  : [Swashbuckle.AspNetCore V6.2.3](https://www.nuget.org/packages/swashbuckle.aspnetcore.swaggergen/6.2.3) package is included to has a user interface design.
- **Validation**      : [FluentValidation.AspNetCore V11.3.0](https://www.nuget.org/packages/FluentValidation.AspNetCore) package is added to validate inputs of user and return good meaning messages.
- **Persistent Data** : [LiteDB V5.0.16](https://www.nuget.org/packages/LiteDB) package is added to save url into db.

## Test
- **Moq Test**  : **MSTest** test project with [Moq v4.18.4](https://www.nuget.org/packages/Moq) package to mocking repositories and helpers. 
- **Integration Test**  : XUnit test project with [Microsoft.AspNetCore.TestHost v6.0.0](https://www.nuget.org/packages/Microsoft.AspNetCore.TestHost/6.0.0) and [Microsoft.AspNetCore.Mvc.Testing v6.0.0](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing/6.0.0) packages were inluded to create test server. I implement **Singleton Desing Pattern** with **Thrade safe locking mechanism** to create a uniqe test server.Then,I can run all the test methods on Visual Studio Test Explorer. 

## Demo

* Create short url.

![alt text](https://raw.githubusercontent.com/ismailkasan/url-shortener/main/media/shorturl-1.png?raw=true)

* Redirect to original url.

![alt text](https://raw.githubusercontent.com/ismailkasan/url-shortener/main/media/redirect.gif?raw=true)

## 🚀 About Me
I'm a full stack developer. I've been dealing with software since 2016. I worked on a lot of projects so far. My strongest skills are C#, .Net
Core, MsSql, javaScript, TypeScript and Angular2+. Also, I am working on sample like this extension Nodejs, MongoDb projects.