# Unity Notification System

This Unity project demonstrates the implementation of three design patterns: Singleton, Observer, and Factory. The script sets up a notification system where messages can be logged and observed via the Unity console.

## What Should Happen

When you run the scene and press the `1` key, a message "Hello from Unity!" will be sent through the notification system and displayed in the Unity console. When you press the `2` key, a log message "Program execution completed." will be displayed in the Unity console.

## Why It Happens

1. **Singleton Pattern**: Ensures that only one instance of the `Logger` class exists. This is used to log messages to the console.
2. **Observer Pattern**: Allows the `NotificationSystem` to maintain a list of observers and notify them of new messages. The `ConsoleNotification` class implements the `IObserver` interface to receive and display messages.
3. **Factory Pattern**: Used to create instances of notifications. The `NotificationFactory` abstract class and its concrete implementation `ConsoleNotificationFactory` help in creating `ConsoleNotification` instances.

