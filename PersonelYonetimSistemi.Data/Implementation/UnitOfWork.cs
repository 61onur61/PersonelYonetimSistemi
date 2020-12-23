using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.DataContext;

namespace PersonelYonetimSistemi.Data.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonelYonetimSistemiContext _context;
        public UnitOfWork(PersonelYonetimSistemiContext context)
        {
            _context = context;
            personelIzinRepository = new PersonelIzinRepository(_context);

            personelIzinTipiRepository = new PersonelIzinTipiRepository(_context);

            personelIzinOnayRepository = new PersonelIzinOnayRepository(_context);

            personelRepository = new PersonelRepository(_context);

            personelIsEmirleriRepository = new PersonelIsEmirleriRepository(_context);

        }

        public IPersonelIzinRepository personelIzinRepository { get; private set; }

        public IPersonelIzinTipiRepository personelIzinTipiRepository { get; private set; }

        public IPersonelIzinOnayRepository personelIzinOnayRepository { get; private set; }

        public IPersonelRepository personelRepository { get; private set; }

        public IPersonelIsEmirleriRepository personelIsEmirleriRepository { get; private set; }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
