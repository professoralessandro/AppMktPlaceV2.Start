﻿#region ATRIBUTTES
using AppMktPlaceV2.Security.Application.Helper.Static.Settings;
using Microsoft.Data.SqlClient;
using System.Data;
#endregion

namespace AppMktPlaceV2.Security.Domain.Connector
{
    public class APConnector
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public APConnector()
        {
            Connection = new SqlConnection(RumtimeSettings.ConnectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
