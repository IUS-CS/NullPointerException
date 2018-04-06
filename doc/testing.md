# Testing
In our solution, you'll find a second project called
ChronosTests. This contains our testing code. It is separated into 
folders for testing the controllers, repositories, and models.

## Unit Testing
Unit testing makes up the bulk of our testing. We are currently testing
each method present in our written classes to verify their funcitonality.
We are using the help of Microsoft's Moq library. As such, you will see
tests that create a fake instance of an object and override one of its methods
to returns exactly what we want. This allows us to test high level functions
that may call down to subssequent methods. 

Testing our repositories has proven to be the most challenging. We ran into some issues
with Entity Framework's functionality for mocking DbSet objects. Due to this, we had to create a
a custom mocked version of the database context and mocked versions of the various
repositories. This allowed us to test the same code used in the database methods
without ever hitting the database.

## Integration Testing
For integration testing, we are doing manual tests. This includes things like
creating a new group from the application and then checking that it shows up 
in a user's list of groups. We do this any time a new functionality is going to be
added. This helps us ensure that all the pieces of the application are working
together properly and that data is not getting dropped somewhere. For example, if a user
creates a group and we put that group in the database, the user still won't appear
as being a part of that group unless we also update the MemberItems table.

## End to End Testing
This will take place manually as well. Our plan for this is to run through a typical
use of the app like a user would. We will try to excercise all of the functions of the 
app and make sure everything goes smoothly. This process would flow something like this:
Log in -> Check current group information -> Switch current group page -> Create new Group -> Invite people to group
