using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utility
{
    public static class Constantes
    {
        // Roles
        public const string AdminRole = "Administrador";
        public const string ClientRole = "Cliente";

        // Estados de Orden aumentada a orden un nuevo sistema 
        public const string EstadoPendiente = "Pago_Pendiente";
        public const string EstadoPagoEnviado = "Estado_PagoEnviado";
        public const string EstadoPagoRechazado = "Estado_PagoRechazado";

        public const string EstadoEnviado = "Estado_Enviado";
        public const string EstadoCompletado = "Estado_Completado";
        public const string EstadoCancelado = "Estado_Cancelado";
        public const string EstadoReembolso = "Estado_Reembolsado";
    }
}
