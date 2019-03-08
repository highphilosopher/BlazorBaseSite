# Base Site Template

## Welcome to my Base Site Template!

This is a simple extension on top of the default Blazor template. It's fairly straight forward with a couple pieces added.

Additions:

*   Authentication/Authorization system
    *   Basic Auth built-in.
    *   Stores passwords hashed with dynamic salt. (uses bcrypt hashing).
    *   Permissions are simple string-based. Examples of implementing can be seen on the Account pages.
    *   default username is test@test.com. Password is test. Please remove this. Seriously!
    *   Users stored in userStore.json in the Server project.
*   NotADAL data persistence
    *   For the sake of prototyping I didn't want to mess with setting up a DAL.
    *   The Persistence folder in the App project contains a KeyedDataStore class. It extends a generic Dictionary.
    *   Basically you inherit from it, pick a file name, and it'll handle the serialization via json back and forth.
    *   Persistence happens when items are updated or removed. If you modify properties of items in the collections, you can call Persist() manually.
    *   You can't complain about the performance of the DAL, cause it's NotADAL :)

That's pretty much it! Easy to prototype with Blazor. Just enough Authentication to feel ok with publishing to Azure for testing.

### Credits

The cookie authentication piece is a great piece of functionality. It came from this article on [Light Switch](http://lightswitchhelpwebsite.com/Blog/tabid/61/EntryId/4316/A-Demonstration-of-Simple-Server-side-Blazor-Cookie-Authentication.aspx) article (opens in a new window). Michael Washington, you're awesome!

BCrypt.NET, cause I'm not actually smart enough to implement bcrypt myself (obviously).

Obviously the base template here is from the Blazor team! Dear giants, we love standing on your shoulders!

### How To Use

This is setup as a dotnet template.

```powershell
dotnet new --install "<path to the root folder which contains the .template.config folder>"
```

Then in your new project folder, simply run this command
```powershell
dotnet new BlazorBaseSite
```