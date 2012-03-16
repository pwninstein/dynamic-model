using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace DynamicModel
{
    /// <summary>
    /// Wraps an object and provides Dynamic property access.
    /// </summary>
    /// <typeparam name="T">The type of object being wrapped</typeparam>
    public class NotificationModel<T> : DynamicObject, INotifyPropertyChanged, IDataErrorInfo where T : class
    {
        private Dictionary<string, object> publicValues;

        private Dictionary<string, string> errors;

        public T Model { get; private set; }

        public NotificationModel(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            Model = model;

            var properties = Model.GetType().GetProperties();

            publicValues = properties.ToDictionary(p => p.Name, p => p.GetValue(Model, null));

            errors = properties.Select(p => new { p.Name, Value = string.Empty }).ToDictionary(p => p.Name, p => p.Value);
        }

        public string Validate(string propertyName, object value, out object actualValue)
        {
            VerifyPropertyName(propertyName);

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

        private void SetModelPropertyAndError(string property)
        {
            try
            {
                object actualValue = null;

                var result = Validate(property, publicValues[property], out actualValue);

                if (string.IsNullOrEmpty(result))
                {
                    typeof(T).GetProperty(property).SetValue(Model, actualValue, null);

                    errors[property] = string.Empty;
                }
                else
                {
                    errors[property] = result;
                }
            }
            catch (Exception ex)
            {
                errors[property] = ex.Message;
            }
        }

        private void RaisePropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void VerifyPropertyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (!publicValues.ContainsKey(name))
            {
                throw new ArgumentOutOfRangeException("name", string.Format("Model does not contain property '{0}'", name));
            }

        }

        #region DynamicObject Overrides

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            VerifyPropertyName(binder.Name);

            result = publicValues[binder.Name];

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            VerifyPropertyName(binder.Name);

            publicValues[binder.Name] = value;

            SetModelPropertyAndError(binder.Name);

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
                VerifyPropertyName(columnName);

                return errors[columnName];
            }
        }

        #endregion
    }
}