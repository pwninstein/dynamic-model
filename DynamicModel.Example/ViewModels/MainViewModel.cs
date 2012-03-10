using System;
using System.ComponentModel;
using DynamicModel;
using DynamicModel.Example.Models;

namespace DynamicModel.Example.ViewModels
{
    public class MainViewModel
    {
        private NotificationModel<Person> model;
        public dynamic Model
        {
            get
            {
                return model ?? (model = new NotificationModel<Person>(new Person { Name = "Ted", Age = 25, BirthDate = new DateTime(1980, 12, 1) }));
            }
        }
    }
}