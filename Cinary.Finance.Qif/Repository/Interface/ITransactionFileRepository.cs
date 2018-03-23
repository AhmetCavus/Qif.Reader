//
//  IModifier.cs
//
//  Author:
//       ahc <ahmet.cavus@cinary.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus
using Cinary.Finance.Qif.Transaction;
using System.Threading.Tasks;

namespace Cinary.Finance.Qif.Repository
{
    /// <summary>
    /// This is an interface for the repository pattern to mark repository implementations. 
    /// </summary>
    public interface ITransactionFileRepository
    {
        Task<ITransactionDetail> ResolveFromFileAsync(string path);
        ITransactionDetail ResolveFromFile(string path);
    }

}
