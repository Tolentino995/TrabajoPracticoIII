using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repository
{
    public class OrdenRepository : Repository<Orden>, IOrdenRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Orden orden)
        {
            var objDesdeBd = _context.Ordenes.FirstOrDefault(o => o.Id == orden.Id);
            _context.Ordenes.Update(objDesdeBd);
        }
    }
}
