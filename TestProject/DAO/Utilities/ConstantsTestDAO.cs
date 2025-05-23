﻿namespace TestProject.DAO.Utilities
{
    internal static class ConstantsTestDAO
    {
        internal const string CONTRASENIA_PRUEBA = "Pass@word123";
        internal static readonly string initDataScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "DAO", "Utilities", "initdata.sql");
        internal static readonly string initdbScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "DAO", "Utilities", "initdb.sql");
        internal static readonly string deletedbScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "DAO", "Utilities", "deletedb.sql");
        internal static readonly string initCatalogdbScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "DAO", "Utilities", "initcatalog.sql");
        internal static readonly string createUserbScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "DAO", "Utilities", "createuser.sql");
    }
}
