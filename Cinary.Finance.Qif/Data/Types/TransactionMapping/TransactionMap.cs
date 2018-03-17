using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Cinary.Finance.Qif.Data.TransactionMapping
{
    internal class TransactionMap<T> :  IStringMapper<T> where T : ITransaction
    {
        private IList<TransactionAttribute> _attributes;

        public TransactionMap()
        {
            _attributes = new List<TransactionAttribute>();
        }

        internal void AddAttribute(char key, string propertyName)
        {
            this._attributes.Add(new TransactionAttribute(key, propertyName) );
        }

        internal TransactionAttribute GetAttribute(char key)
        {
            return _attributes.Where(p => p.Key == key).FirstOrDefault();
        }

        public T1 Parse<T1>(string transactionString) where T1 : ITransaction
        {
            var transaction = (T1)Activator.CreateInstance(typeof(T1));

            var transactionItems = transactionString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in transactionItems)
            {
                // Get first character as key, the rest as value
                var key = item[0];
                var value = item.Substring(1);

                // Get the attribute
                var attribute = GetAttribute(key);

                if (attribute != null)
                {
                    // Using Reflection to set the property
                    PropertyInfo propertyInfo = transaction.GetType().GetRuntimeProperty(attribute.PropertyName);
                    // TODO: find a better way to do this

                    try
                    {
                        switch (propertyInfo.PropertyType.ToString())
                        {
                            case "System.Collections.Generic.IList`1[System.String]":
                                var slist = propertyInfo.GetValue(transaction);
                                if (slist == null)
                                    slist = new List<string>();
                                ((IList<string>)slist).Add(value);
                                break;

                            case "System.Collections.Generic.IList`1[System.Decimal]":
                                var dlist = propertyInfo.GetValue(transaction);
                                if (dlist == null)
                                    dlist = new List<decimal>();
                                ((IList<decimal>)dlist).Add(decimal.Parse(value, CultureInfo.InvariantCulture));
                                break;

                            case "System.Decimal":
                                var dec = decimal.Parse(value, CultureInfo.InvariantCulture);
                                propertyInfo.SetValue(transaction, Convert.ChangeType(dec, propertyInfo.PropertyType));
                                break;

                            case "System.Int32":
                                var num = int.Parse(value);
                                propertyInfo.SetValue(transaction, Convert.ChangeType(num, propertyInfo.PropertyType));
                                break;

                            case "System.DateTime":
                                var date = DateTime.Parse(value, CultureInfo.InvariantCulture);
                                propertyInfo.SetValue(transaction, Convert.ChangeType(date, propertyInfo.PropertyType));
                                break;

                            case "System.String":
                                propertyInfo.SetValue(transaction, Convert.ChangeType(value, propertyInfo.PropertyType));
                                break;

                            case "System.Boolean":
                                var flag = false;
                                if (value.ToUpper() == "X")
                                    flag = true;
                                propertyInfo.SetValue(transaction, Convert.ChangeType(flag, propertyInfo.PropertyType));
                                break;

                        }
                    } catch(Exception err)
                    {
                        Debug.WriteLine(err);
                    }
                }
            }
            return transaction;
        }
    }
}