using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace DynamicModel
{
    public class NotificationModel<T> : DynamicObject, INotifyPropertyChanged, IDataErrorInfo
    {
        private Dictionary<string, object> publicValues;

        public T Model { get; private set; }

        public NotificationModel(T model)
        {
            Model = model;

            publicValues =
                Model
                .GetType()
                .GetProperties()
                .ToList()
                .ToDictionary(p => p.Name, p => p.GetValue(Model, null));

        }

        public string Validate(string propertyName, object value, out object actualValue)
        {
            var destinationType = typeof(T).GetProperty(propertyName).PropertyType;
            var sourceType = value == null ? typeof(string) : value.GetType();

            if (sourceType == destinationType)
            {
                actualValue = value;
                return null;
            }

            if (TypeDescriptor.GetConverter(destinationType).CanConvertFrom(sourceType))
            {
                try
                {
                    actualValue = TypeDescriptor.GetConverter(destinationType).ConvertFrom(value);
                    return null;
                }
                catch (Exception ex)
                {
                    actualValue = null;
                    return ex.Message;
                }
            }
            else
            {
                actualValue = null;
                return "Cannot convert to " + destinationType.Name;
            }
        }

        private void RaisePropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #region DynamicObject Overrides

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = publicValues[binder.Name];

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            publicValues[binder.Name] = value;

            RaisePropertyChanged(binder.Name);

            return true;
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                object actualValue = null;

                var result = Validate(columnName, publicValues[columnName], out actualValue);

                if (result != null)
                {
                    return result;
                }

                typeof(T).GetProperty(columnName).SetValue(Model, actualValue, null);

                return null;
            }
        }

        #endregion
    }
}