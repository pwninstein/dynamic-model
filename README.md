Dynamic Model
===
This library contains (for now at least) a single class - NotificationModel. It is meant to provide a way for a user interface that supports IDataErrorInfo and INotifyPropertyChanged to bind to and interact with POCO objects. 

Given the following simple class:

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
    }

We can wrap it in a NotificationModel object and expose it in a ViewModel:

    private NotificationModel<Person> model;
    public dynamic Model
    {
        get
        {
            return model ?? (model = new NotificationModel<Person>(new Person { Name = "Ted", Age = 25, BirthDate = new DateTime(1980, 12, 1) }));
        }
   }

And then bind to it in a WPF view:

    <Label Content="Name:"/>
    <TextBox Text="{Binding Model.Name, ValidatesOnDataErrors=True}" Grid.Column="1"/>

    <Label Content="Age:" Grid.Row="1"/>
    <TextBox Text="{Binding Model.Age, ValidatesOnDataErrors=True}" Grid.Row="1" Grid.Column="1"/>

    <Label Content="Birth Date:" Grid.Row="2"/>
    <TextBox Text="{Binding Model.BirthDate, ValidatesOnDataErrors=True}" Grid.Row="2" Grid.Column="1"/>

No license has been chosen yet.