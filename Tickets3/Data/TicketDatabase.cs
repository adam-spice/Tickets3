using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tickets3.Models;

namespace Tickets3.Data
{
    public class TicketDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public TicketDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Client).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Client)).ConfigureAwait(false);
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Staff)).ConfigureAwait(false);
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Work)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public Task<List<Client>> GetClientsAsync()
        {
            return Database.Table<Client>().ToListAsync();
        }

        public Task<List<Staff>> GetAllStaffAsync()
        {
            return Database.Table<Staff>().ToListAsync();
        }

        public Task<List<Work>> GetAllWorkAsync()
        {
            return Database.Table<Work>().ToListAsync();
        }

        public Task<Client> GetClientAsync(int id)
        {
            return Database.Table<Client>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<Staff> GetStaffAsync(int id)
        {
            return Database.Table<Staff>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<Work> GetWorkAsync(int id)
        {
            var work = Database.Table<Work>().Where(i => i.ID == id).FirstOrDefaultAsync();
            return work;
        }

        public Task<List<Work>> GetAllWorkForStaffMember(int staffID)
        {
            var staffWork = Database.Table<Work>().Where(w => w.StaffID == staffID).ToListAsync();
            return staffWork;
        }

        public Task<int> SaveClientAsync(Client client)
        {
            if (client.ID != 0)
            {
                return Database.UpdateAsync(client);
            }
            else
            {
                return Database.InsertAsync(client);
            }
        }

        public Task<int> SaveStaffAsync(Staff staff)
        {
            if (staff.ID != 0)
            {
                return Database.UpdateAsync(staff);
            }
            else
            {
                return Database.InsertAsync(staff);
            }
        }

        public Task<int> SaveWorkAsync(Work work)
        {
            var currentStartTime = DateTime.Now;
            if (work.ID != 0)
            {
                return Database.UpdateAsync(work);
            }
            else
            {
                work.Start = currentStartTime;
                work.End = currentStartTime;
                return Database.InsertAsync(work);
            }
        }

        public Task<int> DeleteClientAsync(Client client)
        {
            return Database.DeleteAsync(client);
        }

        public Task<int> DeleteStaffAsync(Staff staff)
        {
            return Database.DeleteAsync(staff);
        }

        public Task<int> DeleteWorkAsync(Work work)
        {
            return Database.DeleteAsync(work);
        }
    }
}


