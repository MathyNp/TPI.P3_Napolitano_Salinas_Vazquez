﻿using System.Data;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;

namespace TPI_NapolitanoSalinasVazquez_P3.Interfaces
{
    public interface IHistoryService
    {
        IEnumerable<History> GetAllHistory();

        IEnumerable<History> GetHistoryByClient(int userId);

        public List<History> GetHistoriesByDateRange(DateTime startDate, DateTime endDate);

    }
}
