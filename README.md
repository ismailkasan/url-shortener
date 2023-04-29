# .NET 6 URL Shortener

To learn more about **Web Api** please visit offical [Microsoft](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio)

This is a .NET restful URL shortener api with redirection middleware. It creates short form of url and insert into db. Also, it has a redirection middleware to redirect short url to its original long url.
## Features

- **User Interface**  : [Swashbuckle.AspNetCore V6.2.3](https://www.nuget.org/packages/swashbuckle.aspnetcore.swaggergen/6.2.3) package is included to has a user interface design.
- **Validation**      : [FluentValidation.AspNetCore V11.3.0](https://www.nuget.org/packages/FluentValidation.AspNetCore) package is added to validate inputs of user and return good meaning messages.
- **Persistent Data** : [LiteDB V5.0.16](https://www.nuget.org/packages/LiteDB) package is added to save url into db.


## Demo

* Create short url

![alt text](https://github.com/ismailkasan/url-shortener/blob/media/shorturl-1.png?raw=true)

* Redirect to original url.

![alt text](https://github.com/ismailkasan/url-shortener/blob/media/redirect.gif?raw=true)

## 🚀 About Me
I'm a full stack developer. I've been dealing with software since 2016. I worked on a lot of projects so far. My strongest skills are C#, .Net
Core, MsSql, javaScript, TypeScript and Angular2+. Also, I am working on sample like this extension Nodejs, MongoDb projects.