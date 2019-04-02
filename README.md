# Apos.Content
Content builder library for MonoGame.

## Description
This is an attempt at writing a new content pipeline for MonoGame.

## Goals
* Make it easier to develop custom content builders.
* Allow developpers to build custom content into more than just binary files.
* Make it easier for libraries made for MonoGame to provide their own content.
* Easier to integrate in a running game.
  * Build content at runtime.
  * Refresh content at runtime.
* Handle file collisions.
  * Make it possible to have assets named `Bullet.png` and `Bullet.wav` if you feel like it.
* Ability to group files together into a single "archive".
* Ability to encrypt content.
