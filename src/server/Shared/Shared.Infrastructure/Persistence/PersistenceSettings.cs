namespace FluentPOS.Shared.Infrastructure.Persistence
{
    public class PersistenceSettings
    {
        public bool UseMsSql { get; set; }

        public bool UsePostgres { get; set; }

        public PersistenceConnectionStrings ConnectionStrings { get; set; }

        public class PersistenceConnectionStrings
        {
            public string MSSQL { get; set; }

            public string Postgres { get; set; }
        }
    }
}