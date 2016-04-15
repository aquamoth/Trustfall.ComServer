# Trustfall.ComServer
An **out-of-proc** COM-server runner for C#.

##What kind of COM objects are there?
*Microsoft COM* comes in multiple flavors, each with its own pro's and con's.
- The in-proc server makes it easy to create an object from an external DLL and use it in your native code.
- The out-of-proc server lets you start a separate process, or attach to an existing process, where you can offload part of your work. The process can even be running on a separate server, without you having to care about how it is implemented!

##Why use Trustfall.ComServer?
Unfortunately *.Net Framework* only supports creating in-proc COM-servers by default. Microsoft has tried to remedy this with technical papers and samples describing how to create **out-of-proc servers** in *.Net* languages, for example with their **COM Interop Tutorials** at https://msdn.microsoft.com/en-us/library/aa645712(v=vs.71).aspx. Unfortunately those tutorials are quite complex, have many moving parts and are hard to follow or troubleshoot. This project wraps the COM server-side code into a helper class library which you can use to build your own out-of-proc COM servers.

##Demo application
Start the solution in *Visual Studio* and **build** it. The server component is automatically registered as a *LocalServer32* COM-object as a post-build event.

With **Task Manager** running, start two instances of **Demo.Client** and send a couple of echo requests. Each client creates its own server instance, running in the same server-side process. You see in the *Task Manager* that there is exactly one process called **Demo.Server**. *Demo.Server* keeps an in-memory state of a shared counter as a  singleton object that is shared between all concurrent instances, which demonstrates how easy it is to share information between different COM instances.

You can also run the VB-script **Demo.Client.vbs** that is located in the *Demo.Client* folder to see how it is possible to connect to the server from different langauages.

Now disconnect all clients. After a couple of seconds the *Demo.Server* process goes out of scope and is automatically shutdown again. You see it disappears in the *Task Manager*. If you start a new client the counter has been reset.

##Implement your own out-of-proc COM Server
Follow along to create your own out-of-proc COM Server. Check out the Demo.Server project for helpful hints.
###Create a new project
Create a new *Windows Forms Application*. You can delete the automatically created class *Form1* unless you need it for something else.
###Register a COM factory
Open **Program.cs** and locate the method signature *static void Main()*.
Create and register a factory for creating your COM objects upon request.
```
    var helloWorldFactory = new ClassFactory<HelloWorld, IHelloWorld>();
	ComServer.Instance.Register<HelloWorld>(helloWorldFactory, CLSCTX.LOCAL_SERVER);
```
Where ```HelloWorld``` is the class you want to be created and ```IHelloWorld``` is the interface the client will see.

###Start the COM server
Directly after registering the COM classes, you need to start the Com server.
```
ComServer.Instance.Run();
```
It is the *ComServer* instance that will receive and handle the actual requests for COM objects. The COM specification 