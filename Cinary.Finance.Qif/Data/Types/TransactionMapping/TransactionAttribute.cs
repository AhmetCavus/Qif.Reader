namespace Cinary.Finance.Qif.Data.TransactionMapping
{
    internal class TransactionAttribute
    {
        public TransactionAttribute(char fieldKey, string propertyName) 
        {
            this.Key = fieldKey;
            this.PropertyName = propertyName;
        }

        public char Key { get; set; }
        public string PropertyName { get; set; }
    }
}
