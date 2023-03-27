# Cramstr is planned to be a cross platform flashcards app

WIP

## Project Structure
 - `Shared`: Contains data shared between backend, and the various (planned) frontends
 - `WebApi`: The backend, written in ASP.NET Core, and using MongoDB for storage.
 - `WebProxy`: A simple proxy that maps the WebApi to the `/api` path, and serves the frontend from `/`. 
    This mainly exists to avoid CORS issues when developing locally.
    It works for now, but should probably get replaced with a proper reverse proxy.
 - `BlazorWasmUI`: A UI written in Blazor WebAssembly.
 - `ApiClient`: A client library for the WebApi, written in C#. 
    This is used by the BlazorWasmUI, but could also be used by other frontends.