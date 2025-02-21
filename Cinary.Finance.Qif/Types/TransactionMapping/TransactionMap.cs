using Cinary.Finance.Qif.Mapper;
using Cinary.Finance.Qif.Transaction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Cinary.Finance.Qif.TransactionMapping
{
    internal class TransactionMap<T> : IStringMapper<T> where T : ITransactionEntry
    {
        private IList<TransactionAttribute> _attributes;
        private string[] _dateFormats = {
                                    "d.M.yyyy", // 01.01.2025
                                    "M.d.yyyy", // 01.01.2025
                                    "M.d.yy",   // 01.01.25 -> assumes 21st century (01.01.2025)
                                    "MM.dd.yyyy", // 01.01.2025
                                    "MM.dd.yy",   // 01.01.25 -> assumes 21st century (01.01.2025)
                                    "d.MM.yyyy", // 1.01.2025
                                    "d.M.yy",   // 01.01.25 -> assumes 21st century (01.01.2025)
                                    "dd.MM.yyyy", // 01.01.2025
                                    "dd.MM.yy",   // 01.01.25 -> assumes 21st century (01.01.2025)
                                    "MM/dd/yyyy", // 01/01/2025 (US-style)
                                    "MM/dd/yy",   // 01/01/25 -> assumes 21st century (01/01/2025)
                                    "dd/MM/yyyy", // 01/01/2025 (European-style)
                                    "dd/MM/yy",   // 25/01/01 -> assumes 21st century (01/25/2001)
                                    "d/MM/yyyy",// 01/Jan/2025
                                    "d/MM/yy",  // 01/Jan/25
                                    "yy/MM/dd",    // 25/01/01 (could be reversed, depending on file source)
                                    "dd-MMM-yy",  // 01-Jan-25
                                    "dd-MMM-yyyy",// 01-Jan-2025
                                    "dd-MMM",     // 01-Jan -> assumes current year
                                    "MMM-yy",     // Jan-25 -> assumes current day and year
                                    "MMM-yyyy",   // Jan-2025 -> assumes current day
                                    "yyyy-MM-dd", // 2025-01-01
                                    "yyyy-MM",    // 2025-01 -> assumes 1st day of month
                                    "yyyy"        // 2025 -> assumes 1st day of year
                                };

        public TransactionMap()
        {
            _attributes = new List<TransactionAttribute>();
        }

        internal void AddAttribute(char key, string propertyName)
        {
            this._attributes.Add(new TransactionAttribute(key, propertyName));
        }

        internal TransactionAttribute GetAttribute(char key)
        {
            return _attributes.Where(p => p.Key == key).FirstOrDefault();
        }

        public T1 Parse<T1>(string transactionString) where T1 : ITransactionEntry
        {
            var transaction = (T1)Activator.CreateInstance(typeof(T1));

            var transactionItems = transactionString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in transactionItems)
            {
                // Get first character as key, the rest as value
                var key = item[0];
                var dataValue = item.Substring(1);

                // Get the attribute
                var attribute = GetAttribute(key);

                if (attribute != null)
                {
                    fillTransactionWithBasicType(transaction, attribute, dataValue);

                }
            }
            return transaction;
        }

        private void fillTransactionWithBasicType<T1>(T1 transaction, TransactionAttribute attribute, string dataValue) where T1 : ITransactionEntry
        {
            // Using Reflection to set the property
            PropertyInfo propertyInfo = transaction.GetType().GetRuntimeProperty(attribute.PropertyName);
            // TODO: find a better way to do this
            switch (propertyInfo.PropertyType.Name)
            {
                case "Decimal":
                    var dec = decimal.Parse(dataValue, CultureInfo.InvariantCulture);
                    propertyInfo.SetValue(transaction, Convert.ChangeType(dec, propertyInfo.PropertyType));
                    break;

                case "Int32":
                    var num = int.Parse(dataValue);
                    propertyInfo.SetValue(transaction, Convert.ChangeType(num, propertyInfo.PropertyType));
                    break;

                case "DateTime":
                    try
                    {
                        var date = DateTime.ParseExact(dataValue, _dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                        propertyInfo.SetValue(transaction, Convert.ChangeType(date, propertyInfo.PropertyType));
                    }
                    catch (FormatException)
                    {
                        var date = DateTime.Parse(dataValue, CultureInfo.InvariantCulture, DateTimeStyles.None);
                        propertyInfo.SetValue(transaction, Convert.ChangeType(date, propertyInfo.PropertyType));
                    }
                    break;

                case "String":
                    propertyInfo.SetValue(transaction, Convert.ChangeType(dataValue, propertyInfo.PropertyType));
                    break;

                case "Boolean":
                    var flag = false;
                    var normalizedData = dataValue.ToLowerInvariant();
                    if (normalizedData == "x" || dataValue == "true" || dataValue == "1")
                        flag = true;
                    propertyInfo.SetValue(transaction, Convert.ChangeType(flag, propertyInfo.PropertyType));
                    break;
                default:
                    fillTransactionWithComplexType(transaction, propertyInfo, dataValue);
                    break;
            }
        }

        private void fillTransactionWithComplexType<T1>(T1 transaction, PropertyInfo propertyInfo, string dataValue) where T1 : ITransactionEntry
        {
            switch (propertyInfo.PropertyType.ToString())
            {
                case "System.Collections.Generic.IList`1[System.String]":
                    var slist = propertyInfo.GetValue(transaction);
                    if (slist == null)
                        slist = new List<string>();
                    ((IList<string>)slist).Add(dataValue);
                    break;
                case "System.Collections.Generic.IList`1[System.Decimal]":
                    var dlist = propertyInfo.GetValue(transaction);
                    if (dlist == null)
                        dlist = new List<decimal>();
                    ((IList<decimal>)dlist).Add(decimal.Parse(dataValue, CultureInfo.InvariantCulture));
                    break;
                default:
                    Debug.WriteLine("Property type not supported: " + propertyInfo.PropertyType.ToString());
                    break;
            }
        }
    }
}