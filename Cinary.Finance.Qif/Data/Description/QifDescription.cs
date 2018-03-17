//
//  QifDescription.cs
//
//  Author:
//       Ahmet Cavus <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus


namespace Cinary.Finance.Qif.Data.Description
{
    public class QifDescription : IDataDescription
    {
        protected string _resource;
        public string Resource => _resource;

        public QifDescription(string resource)
        {
            _resource = resource;
        }
    }
}
