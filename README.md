# Penshell

<img src="https://repository-images.githubusercontent.com/204852671/b7621980-0727-11ea-97dd-ba5479dc456f" alt="Penshell" width="350" />

[![Build](https://github.com/IForgeDe/Penshell/workflows/CI/badge.svg?branch=master)](https://github.com/IForgeDe/Penshell/actions)

Penshell is a experimental command line pipeline with scrippting support written in pure dotnet.
Its is for developing short Tasks via a simple command line interface and chaining them to a complex scripting process.
The command assemblies of this project are reusable in applications
based on the [dotnet / command-line-api](https://github.com/dotnet/command-line-api) project.
The PenshellCLI is for collecting, managing and enhancing the functionality for scripting.

## Why Penshell

The intention of this project was initially an automation of recurring processes of all kind in an easy way.
The result of this idea is a project to design simple commands in a
script pipeline, which is easy to understand, write, manage and test.

## How should a command looks like?

## Project status and structure

Currently this project is in a inital state and there are no releases planed.
You can check out this code and run it in debug mode to compile with Penshells default commands.
The release build of Penshell is prepared for the setup process  with compiling in the designated folders.

## Libraries used

- [dotnet / command-line-api](https://github.com/dotnet/command-line-api)
- [Guard](https://github.com/safakgur/guard)
- [NUnit](https://github.com/nunit/nunit)
- [Serilog](https://github.com/serilog/serilog)
  