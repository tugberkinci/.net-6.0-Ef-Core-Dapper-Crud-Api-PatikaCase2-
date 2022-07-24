using Microsoft.EntityFrameworkCore;
using PatikaHomework2.Data.Context;
using PatikaHomework2.Data.Model;
using PatikaHomework2.Service.IServices;


namespace PatikaHomework2.Service.Services
{
    public class FolderService : IFolderService
    {
        private readonly EfContext _efContext;

        public FolderService(EfContext efContext)
        {
            _efContext = efContext;
        }


        public async Task<Folder> Add(Folder entity)
        {
            try
            {
                _efContext.folder.AddAsync(entity);
                _efContext.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
           
        }

        public async Task<string> Delete(int id)
        {
            var folder = _efContext.folder.SingleOrDefault(x => x.Id == id);
            if (folder == null)
                return null;
            try
            {
                _efContext.folder.Remove(folder);
                _efContext.SaveChangesAsync();
                return "Success";

            }
            catch(DbUpdateException ex)
            {
                throw ex;
            }
           
        }

        public async Task<Folder> GetById(int id)
        {
            return _efContext.folder.SingleOrDefault(x => x.Id == id);
            
        }

        public async Task<Folder> Update(Folder entity)
        {
            try
            {
                _efContext.folder.Update(entity);
                _efContext.SaveChanges();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
           
        }

        public async Task<IEnumerable<Folder>> GetAll()
        {
            return await _efContext.Set<Folder>().AsNoTracking().ToListAsync();

        }
    }
}
