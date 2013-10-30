SocketServerLib
===============

A fork of a socket client/server system made by Alessandro Lentini.

The original can be found at link #1.

Changes
=======
- Fixes up a lot of comments.
- Fixes up some (a lot) of null issues.
- When a client disconnects disgracefully, the CloseConnectionEvent is fired so the Server can acknowledge it. It was previously failing silently.

Links
=====

1. http://www.codeproject.com/Articles/563627/A-full-library-for-a-Socket-Client-Server-system
