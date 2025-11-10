using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repository.IRepository
{
    public interface IDetalleOrdenRepository : IRepository<DetalleOrden>
    {
      void Update(DetalleOrden detalleorden);
    }
}
