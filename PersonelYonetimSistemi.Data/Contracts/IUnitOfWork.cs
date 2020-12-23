using System;

namespace PersonelYonetimSistemi.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonelIzinRepository personelIzinRepository { get; }

        IPersonelIzinTipiRepository personelIzinTipiRepository { get; }

        IPersonelIzinOnayRepository personelIzinOnayRepository { get; }

        IPersonelRepository personelRepository { get; }

        IPersonelIsEmirleriRepository personelIsEmirleriRepository { get; }
        void Save();
    }
}
