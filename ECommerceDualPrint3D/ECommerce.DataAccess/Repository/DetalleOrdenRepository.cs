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
    public class DetalleOrdenRepository : Repository<DetalleOrden>, IDetalleOrdenRepository
    {
        private readonly ApplicationDbContext _context;

        public DetalleOrdenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(DetalleOrden detalleOrden)
        {
            var objDesdeBd = _context.DetalleOrdenes.FirstOrDefault(o => o.Id == detalleOrden.Id);
            _context.DetalleOrdenes.Update(objDesdeBd);
        }
    }
}
