# Project Design Patterns
This document contains information about the design patterns we use in our project as well as some 
that could work well within our project. It also outlines how we intend to continue building the 
project with these design patterns.

## Current Design Patterns
We currently use a factory design with our repository components. This is a way to ensure dependency injection.
When the HomeCcontroller requests an IUserRepository our dependency injection container provides the
concrete implementation. This abstracts the instantiation logic and allows us to update the implmentation
of the user repository by changing a single line instead of changing every instance.

## Possible Beneficial Design Patterns
We will definitely need to make use of a Singleton design pattern with regards to our user state. 
When someone is logged in, we want a singleton instance of their user information because that shouldn't
be changing. This will keep us from hitting the database for the same information over and over again.

We could also benefit from an Observer pattern. It could help with managing changes to the UI when a user
posts a new TODO item for example.

## Planned Design Continuation
We will continue to define our other repositories within our DI container so that we can grab an implementation
without explicitly instantiating it. For the singleton user design, we will need to use session variables
to save the current user. We could also continue to go off of their Google login if they chose to connect 
their Google calendar. Our observer pattern requires us to respond to changes in order to update the UI.
One way to go about this (using the TODO example from before) is to place a dummy TODO item in the DOM while
simultaneously updating the database. Then when the page is reloaded, the placeholder is gone and the item is pulled 
from permanant storage.
