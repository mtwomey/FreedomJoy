# FreedomJoy

## Introduction

FreedomJoy aims to free you from typical limitations of your physical controllers.
Wish you could use the dpad on your Xbox controller as "shift buttons"?
FreedomJoy can do it. Need an extra axis or two? FreedomJoy can create "virtual axes"
from button presses. Rapidfire? Done.

## Current Features

* Ability to map any combination of physical buttons to any combination of virtual buttons
* Ability to create "rapid fire" buttons and set the fire rate
* Ability to create a virutal axis out of two buttons (one for increase, one for decrease)
  * Changes in the axis are smoothed and ramped with an acceleration curve

## How Does it Work?

FreedomJoy builds on the excellent work of Shaul Eizikovich's
[vJoy Virual Joystick](http://vjoystick.sourceforge.net). FreedomJoy serves as an infinitely
configurable "feeder" for this virtual joystick, mapping real-world input from one or more
physical devices to the virtual joystick.

## Requirements

FreedomJoy is built and tested against vJoy version 2.1.6.20. At the start of the project,
this is the most recent stable release. We recommend you use this version.

## Current Status

FreedomJoy is **not yet released**, please see the following (ordered) feature roadmap and
release schedule:

| Feature                            | Status               |
| ---------------------------------- | -------------------- |
| Basic DirectInput interface code   | Complete             |
| Basic vJoy interface code          | Complete             |
| Basic mapping architecture         | Complete             |
| Use POV (dpad) as buttons code     | Complete             |
| Implement basic button mapping     | Complete             |
| Implement rapid-fire mapping       | Complete             |
| Implement virtual axis mapping     | Complete             |
| Plan configuration via JSON        | Incomplete           |
| Implement configuration via JSON   | Incomplete           |
| **Release 0.1 version!**           | Incomplete           |
| Implement xInput for xbox360       | Incomplete           |
| Many more things to come...        | Incomplete           |
| Design GUI                         | Incomplete           |
| Impliemnt GUI                      | Incomplete           |
