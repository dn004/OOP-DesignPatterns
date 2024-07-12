using System;
using System.Collections.Generic;
using UnityEngine;

// Main script in Unity
public class NewBehaviourScript : MonoBehaviour
{
    private NotificationSystem notificationSystem;
    private bool waitingForInput = true;

    private void Start()
    {
        // Using the Factory Pattern to create notifications
        NotificationFactory factory = new ConsoleNotificationFactory();
        IObserver consoleNotification = factory.CreateNotification();

        // NotificationSystem acts as the Subject in the Observer Pattern
        notificationSystem = new NotificationSystem();
        notificationSystem.Attach(consoleNotification);

        // Start the coroutine to handle user input
        StartCoroutine(HandleUserInput());
    }

    private void Update()
    {
        // Check for Escape key to quit the program
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitProgram();
        }
    }

    private void QuitProgram()
    {
        Logger.Instance.Log("Program terminated by user.");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private System.Collections.IEnumerator HandleUserInput()
    {
        // Display menu options once
        Debug.Log("Choose an option:");
        Debug.Log("1. Send a new message (Press '1')");
        Debug.Log("2. Log program completion (Press '2')");
        Debug.Log("3. Quit program (Press 'Esc')");

        while (waitingForInput)
        {
            yield return null; // Wait for the next frame to handle input

            // Check for user input
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                string message = "Hello from Unity!";
                notificationSystem.NewMessage(message);
                waitingForInput = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Logger.Instance.Log("Program execution completed.");
                waitingForInput = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitProgram();
                waitingForInput = false;
            }
            else if (Input.anyKeyDown)
            {
                // Invalid choice handling
                Debug.Log("Invalid choice, please try again.");
            }
        }
    }
}

// NotificationSystem class (Subject)
public class NotificationSystem
{
    private List<IObserver> observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Notify(string message)
    {
        foreach (var observer in observers)
        {
            observer.OnMessageReceived(message);
        }
    }

    public void NewMessage(string message)
    {
        Logger.Instance.Log($"New message: {message}");
        Notify(message);
    }
}

// Singleton Logger class
public class Logger
{
    private static Logger instance;

    private Logger() { }

    public static Logger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }
    }

    public void Log(string message)
    {
        Debug.Log($"Log: {message}");
    }
}

// Observer pattern: IObserver interface
public interface IObserver
{
    void OnMessageReceived(string message);
}

// Observer pattern: Concrete observer
public class ConsoleNotification : MonoBehaviour, IObserver
{
    public void OnMessageReceived(string message)
    {
        Debug.Log($"Console Notification: {message}");
    }
}

// Factory pattern: Abstract factory
public abstract class NotificationFactory
{
    public abstract IObserver CreateNotification();
}

// Factory pattern: Concrete factory
public class ConsoleNotificationFactory : NotificationFactory
{
    public override IObserver CreateNotification()
    {
        return new ConsoleNotification();
    }
}
