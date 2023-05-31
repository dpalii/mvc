# Model-View-Controller

In this example, we have a User class that represents the data and business logic of our application. The UserView class is responsible for presenting the user data to the user and capturing user input. The UserController acts as an intermediary between the User model and the UserView.

The UserController has methods to update the view (UpdateView()) and the model (UpdateModel()). When the program starts, the initial user details are displayed using the UpdateView() method. The GetUserInput() method in the view prompts the user to enter new data, and the UpdateModel() method updates the model with the new data. Finally, the UpdateView() method is called again to display the updated user details.

This example demonstrates the flow of data and interactions in the MVC pattern. The model represents the data, the view handles the presentation and user input, and the controller coordinates the communication between the model and the view.

When you run the program, you'll see the initial user details displayed, and then you can enter new data to update the user details, which will be reflected in the view.
