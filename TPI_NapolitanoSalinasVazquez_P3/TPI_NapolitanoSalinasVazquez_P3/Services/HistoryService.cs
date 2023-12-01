using TPI_NapolitanoSalinasVazquez_P3.Data;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;

namespace TPI_NapolitanoSalinasVazquez_P3.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly TPI_NapolitanoSalinasVazquez_P3Context _context;

        public HistoryService(TPI_NapolitanoSalinasVazquez_P3Context context)
        {
            _context = context;
        }

        // Listar historial de compra ----------------------------------------------------------------------------------------------------------------

        public IEnumerable<History> GetAllHistory()
        {
            var history = _context.Histories.ToList();

            if (!history.Any())
            {
                throw new Exception("No hay historial de compras");
            }
            return history;
        }

        // Lista el historial de compras realizado por un cliente determinado ------------------------------------------------------------------------
        public IEnumerable<History> GetHistoryByClient(int userId) 
        {
            return _context.Histories.Where(i => i.UserId == userId).ToList();
        }

        // Lista el historial de compras por rango de fechas ---------------------------------------------------
        public List<History> GetHistoriesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Histories
                .Where(history => history.Date >= startDate && history.Date <= endDate)
                .ToList();
        }
    }
}
