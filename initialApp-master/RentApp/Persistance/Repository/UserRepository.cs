using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class UserRepository : Repository<AppUser, int>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        protected RADBContext DemoContext { get { return context as RADBContext; } }


        protected RADBContext RAContext
        {
            get => context as RADBContext;
        }

        public void ApproveUser(int id)
        {
            var user = RAContext.AppUsers.FirstOrDefault(s => s.Id == id);
            user.Approved = true;
            RAContext.Entry(user).State = EntityState.Modified;
        }
    }
}