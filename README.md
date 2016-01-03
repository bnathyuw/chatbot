# Chatbot

An exercise to implement a console-based social networking application.

Please refer to the [Instructions](INSTRUCTIONS.md) document for the project brief.

## Continuous Integration and Deployment

All code pushed to GitHub is build, tested and deployed by a [project on AppVeyor](https://ci.appveyor.com/project/bnathyuw/chatbot).

## Installation

Please download the latest version from the [Releases tab on GitHub](https://github.com/bnathyuw/chatbot/releases).

## Running the Chatbot

To interact with the chatbot, enter one of the following commands, as laid out in the instructions:

- **posting**: `<user name> -> <message>` Saves a message from a user.
- **reading**: `<user name>` Shows all messages posted by that user.
- **following**: `<user name> follows <another user>` Sets the first user as a follower of the second.
- **wall**: `<user name> wall` Shows all posts by that user and everyone they follow.

You can also use these additional utility commands:

- **status**: `status` Shows the current status of the system.
- **exit**: `exit` Exits the chatbot; all data are lost.

## Design decisions

- The implementation is contained in one project, which gives a single distributable executable.
- Beside the hosting Program, the code is contained in four namespaces, each of which is only aware of modules in the namespaces above it:
  - **Control**: contains the UserInterface, which is responsible for getting a command from the user, and then executing it.
  - **Commands**: which contains implementations of the commands available to the user.
  - **Business**: additional classes to mediate between the Command and Adapter classes. I don't really like this name, which is perhaps an indication that it's not a coherent collection of classes.
  - **Adapters**: classes that adapt system logic to the domain.
- For the sake of simplicity, I have created three data objects and implemented them as structs: **Message**, **UserConnexion** and **Status**. I am not completely happy with depending on concrete objects here, and experimented with extracting interfaces from them, but decided this introduced too much complexity. I would be interested to hear other's opinions of this decision.
- The tests are also contained in one project, and exist at three levels:
  - A smoke test, which runs the .exe in a process and calls the status endpoint. I used this to drive out a walking skeleton, and kept to guarantee that the executable could indeed by executed. This is a vanilla NUnit test.
  - Some specs, which implement the examples from the instructions. These are implemented in Gherkin, using SpecFlow. The preambles to the scenarios isn't great: it's hard to figure out what your fictional users want without having a conversation!
  - Smaller scale tests, including contract and collaboration tests on much of the implementation, and adapter tests on the Adapter layer. Again, these are vanilla NUnit tests.
- I made the decision *not* to use a mocking framework in these tests, as I like the discipline enforced by the Self-Shunt pattern. In a few cases I have created separate Test classes to implement particular interfaces, as in these cases the test just checks the identity of return type, rather than its behaviour.
- I wanted to set up Continuous Integration in this project, and decided to play with AppVeyor, as it offers a free hosted version for public projects. It works perfectly for a small project like this, but I would be interested to see whether it can be used to implement a full deployment pipeline.
- The releases are sent back to GitHub, which keeps them close to the code. Implementing this in AppVeyor was incredibly straightforward.

**PS**: I decided to spell 'connexion' with an 'x' because I can, and because the occasional unusual spelling is always nice.