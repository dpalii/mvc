using System;

// Model: Represents the data and business logic of the application
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

// View: Responsible for presenting the data to the user and capturing user input
public class UserView
{
    private IConsole _console;
    public UserView(IConsole console)
    {
        _console = console;
    }
    public void DisplayUserDetails(string name, int age)
    {
        _console.WriteLine($"Name: {name}");
        _console.WriteLine($"Age: {age}");
    }

    public User GetUserInput()
    {
        _console.WriteLine("Enter name:");
        string? name = _console.ReadLine();
        if (name == null)
        {
            throw new Exception("No name provided");
        }

        _console.WriteLine("Enter age:");
        string? ageStr = _console.ReadLine();
        if (ageStr == null)
        {
            throw new Exception("No age provided");
        }
        int age = int.Parse(ageStr);

        if (age < 0)
        {
            throw new Exception("Age can't be negative");
        }

        return new User(name, age);
    }
}

// Controller: Acts as an intermediary between the Model and the View, handles user input and updates the Model and View accordingly
public class UserController
{
    private User model;
    private UserView view;

    public UserController(User model, UserView view)
    {
        this.model = model;
        this.view = view;
    }

    public void UpdateView()
    {
        view.DisplayUserDetails(model.Name, model.Age);
    }

    public void UpdateModel(User newUser)
    {
        model.Name = newUser.Name;
        model.Age = newUser.Age;
    }
}

public class Program
{
    static void Main()
    {
        // Create Model, View, and Controller
        User model = new User("John Doe", 30);
        UserView view = new UserView(new ConsoleWrapper());
        UserController controller = new UserController(model, view);

        // Initial view
        controller.UpdateView();

        // Prompt user for new data
        User newUserData = view.GetUserInput();

        // Update Model and View
        controller.UpdateModel(newUserData);
        controller.UpdateView();

        Console.ReadLine();
    }
}

public interface IConsole
{
    string? ReadLine();
    void WriteLine(string value);
}

public class ConsoleWrapper : IConsole
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void WriteLine(string value)
    {
        Console.WriteLine(value);
    }
}
