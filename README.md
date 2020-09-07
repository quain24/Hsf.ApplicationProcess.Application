# Hsf.ApplicationProcess.Application
## Henryk Wiśniewski

This demo app should not require any additiona build config or specyfic build order.
<br />

Both web api and blazor webassembly run on kestrell.

### Default config:
* Both apps use https, so additional certificate permissions may be required on first launch (there should be prompt from VS)
* Language detection is automatic, logging is in default language (EN)
* Supported languages are:
  * English (fallback language)
  * Polish
  
Default Swagger interface:
https://localhost:5011/swagger/index.html

### Debugging
As far as I know, Blazor WASM debugging is possible only with Chrome (after changes made in configuration files).
It does not work well, in my case it has serious issues with value resolving and step skipping. 
Also, after few runs it can cause Visual Studio to use 100% cpu even when not debugging.

### Configuration values:
Configuration files | Path
------------ | -------------
Blazor config file  | [Blazor]/wwwroot/appsettings.json
Blazor launch config file  | [Blazor]/Properties/launchSettings.json
WebApi config file including language | [Web]/appsettings.json
<br />

Default connection settings | Value
------------ | -------------
WebApi startup  | https://localhost:5011, http://localhost:5010
Blazor startup kestrell | https://localhost:5001, http://localhost:5000
Blazor startup iis | http://localhost:5000
