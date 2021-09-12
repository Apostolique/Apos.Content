# Apos.Content
Content builder library for MonoGame.

[![Discord](https://img.shields.io/discord/355231098122272778.svg)](https://discord.gg/N9t26Uv)

## Description
This is an attempt at writing a new content pipeline for MonoGame.

## Documentation
* None for now.

## Build

| Project | NuGet |
| ------- | ----- |
| [Apos.Content.Read](https://www.nuget.org/packages/Apos.Content.Read/) | [![NuGet](https://img.shields.io/nuget/v/Apos.Content.Read.svg)](https://www.nuget.org/packages/Apos.Content.Read/) [![NuGet](https://img.shields.io/nuget/dt/Apos.Content.Read.svg)](https://www.nuget.org/packages/Apos.Content.Read/) |
| [Apos.Content.Compile](https://www.nuget.org/packages/Apos.Content.Compile/) | [![NuGet](https://img.shields.io/nuget/v/Apos.Content.Compile.svg)](https://www.nuget.org/packages/Apos.Content.Compile/) [![NuGet](https://img.shields.io/nuget/dt/Apos.Content.Compile.svg)](https://www.nuget.org/packages/Apos.Content.Compile/) |

## Goals
* Make it easier to develop custom content builders.
* Allow developers to build custom content into more than just binary files.
  * For example a json file could be kept as pure text but validated on build.
* Make it easier for libraries made for MonoGame to provide their own content.
* Easier to integrate in a running game.
  * Build content at runtime.
  * Refresh content at runtime.
* Handle file collisions.
  * Make it possible to have assets named `Bullet.png` and `Bullet.wav` if you feel like it.
* Ability to group files together into a single "archive".
* Ability to encrypt content.
