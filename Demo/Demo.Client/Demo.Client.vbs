SET obj = CreateObject("Demo.Server.HelloWorld")

MsgBox "A Demo.Server.HelloWorld object is created"

' call the HelloWorld method that returns a string
MsgBox "The HelloWorld method returns: " & obj.Echo("Hello")

' Get the Counter property
MsgBox "The Counter property returns " & obj.Counter

SET obj = Nothing