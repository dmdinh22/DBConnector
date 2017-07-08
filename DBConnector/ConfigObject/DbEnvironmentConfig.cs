using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Configuration.Assemblies;

namespace DBConnector.ConfigObject
{
    #region " CONFIG SETUP "
    /*
     * TO USE THIS CONFIG HELPER ADD THIS TO YOUR WEB.CONFIG OR APP.CONFIG FILE
     * <configSections>
        <section name="DataEnvironmentConfiguration" type="DbConnector.ConfigObject.DbEnvironmentConfig, DbConnector"/>
      </configSections>
     *<DataEnvironmentConfiguration>
        <DataEnvironment Name="Development" CaseId="DDEV">
          <DatabaseConnections>
            <DatabaseConnection TisamUid="" Name="DB_1" TKey="" ConnectionString="server=SERVER_NAME;Initial Catalog=DB_CATALOG_NAME;UID=USER_ID;PWD={0};Connect Timeout=120"/>
            <DatabaseConnection TisamUid="" Name="DB_2" TKey="" ConnectionString="server=SERVER_NAME;Initial Catalog=DB_CATALOG_NAME;UID=USER_ID;PWD={0};Connect Timeout=120"/>
          </DatabaseConnections>
        </DataEnvironment>
        <DataEnvironment Name="Test" CaseId="DUAT">
          <DatabaseConnections>
            <DatabaseConnection TisamUid="" Name="DB_1" TKey="" ConnectionString="server=SERVER_NAME;Initial Catalog=DB_CATALOG_NAME;UID=USER_ID;PWD={0};Connect Timeout=120"/>
            <DatabaseConnection TisamUid="" Name="DB_2" TKey="" ConnectionString="server=SERVER_NAME;Initial Catalog=DB_CATALOG_NAME;UID=USER_ID;PWD={0};Connect Timeout=120"/>
          </DatabaseConnections>
        </DataEnvironment>
        <DataEnvironment Name="Prod" CaseId="DC">
          <DatabaseConnections>
            <DatabaseConnection TisamUid="" Name="DB_1" TKey="" ConnectionString="server=SERVER_NAME;Initial Catalog=DB_CATALOG_NAME;UID=USER_ID;PWD={0};Connect Timeout=120"/>
            <DatabaseConnection TisamUid="" Name="DB_2" TKey="" ConnectionString="server=SERVER_NAME;Initial Catalog=DB_CATALOG_NAME;UID=USER_ID;PWD={0};Connect Timeout=120"/>
          </DatabaseConnections>
        </DataEnvironment>
      </DataEnvironmentConfiguration>
     *
     * ADD TO THE APPSETTINGS NODE IN CONFIG FILE
      <add key="DataEnvironment" value="Development" />
     */

    #endregion

    public class DbEnvironmentConfig : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public DataEnvironmentElementCollection DataEnvironment { get { return (DataEnvironmentElementCollection)base[""]; } }
    }

    public class DataEnvironmentElement : ConfigurationElement
    {
        [ConfigurationProperty("Name")]
        public string Name { get { return (String)this["Name"]; } set { this["Name"] = value; } }

        [ConfigurationProperty("CaseId")]
        public string CaseId { get { return (String)this["CaseId"]; } set { this["CaseId"] = value; } }

        [ConfigurationProperty("DatabaseConnections")]
        public DatabaseConnectionElementCollection DatabaseConnection { get { return (DatabaseConnectionElementCollection)this["DatabaseConnections"]; } }

        //[ConfigurationProperty("WebServiceEndpointURLs")]
        //public EndpointElementCollection WebServiceEndpointURL { get { return (EndpointElementCollection)this["WebServiceEndpointURLs"]; } }
    }

    [ConfigurationCollection(typeof(DataEnvironmentElement), AddItemName = "DataEnvironment")]
    public class DataEnvironmentElementCollection : ConfigurationElementCollection
    {
        new public DataEnvironmentElement this[string name] { get { return (DataEnvironmentElement)base.BaseGet(name); } }
        public DataEnvironmentElement this[int index]
        {
            get { return (DataEnvironmentElement)base.BaseGet(index); }
            set { if (BaseGet(index) != null) { BaseRemoveAt(index); } BaseAdd(index, value); }
        }

        protected override ConfigurationElement CreateNewElement() { return new DataEnvironmentElement(); }

        //MUST BE UNIQUE
        protected override object GetElementKey(ConfigurationElement element) { return ((DataEnvironmentElement)element).Name; }

        protected override string ElementName { get { return "DataEnvironment"; } }
        protected override bool IsElementName(string elementName) { return (elementName == "DataEnvironment"); }

        public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMap; } }
    }

    public class DatabaseConnectionElement : ConfigurationElement
    {
        [ConfigurationProperty("TisamUid")]
        public string TisamUid { get { return (String)this["TisamUid"]; } set { this["TisamUid"] = value; } }

        [ConfigurationProperty("TKey")]
        public string TKey { get { return (String)this["TKey"]; } set { this["TKey"] = value; } }

        [ConfigurationProperty("Name")]
        public string Name { get { return (String)this["Name"]; } set { this["Name"] = value; } }

        [ConfigurationProperty("ConnectionString")]
        public string ConnectionString { get { return (String)this["ConnectionString"]; } set { this["ConnectionString"] = value; } }
    }

    [ConfigurationCollection(typeof(DatabaseConnectionElement))]
    public class DatabaseConnectionElementCollection : ConfigurationElementCollection
    {
        new public DatabaseConnectionElement this[string name] { get { return (DatabaseConnectionElement)base.BaseGet(name); } }
        public DatabaseConnectionElement this[int index]
        {
            get { return (DatabaseConnectionElement)base.BaseGet(index); }
            set { if (BaseGet(index) != null) { BaseRemoveAt(index); } BaseAdd(index, value); }
        }

        protected override ConfigurationElement CreateNewElement() { return new DatabaseConnectionElement(); }
        //this element key must be unique!
        protected override object GetElementKey(ConfigurationElement element) { return ((DatabaseConnectionElement)element).Name; }
        protected override string ElementName { get { return "DatabaseConnection"; } }
        protected override bool IsElementName(string elementName) { return (elementName == "DatabaseConnection"); }

        public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMap; } }
    }

    public class EndpointElement : ConfigurationElement
    {
        [ConfigurationProperty("Name")]
        public string Name { get { return (String)this["Name"]; } set { this["Name"] = value; } }

        [ConfigurationProperty("URL")]
        public string URL { get { return (String)this["URL"]; } set { this["URL"] = value; } }
    }

    [ConfigurationCollection(typeof(EndpointElement))]
    public class EndpointElementCollection : ConfigurationElementCollection
    {
        new public EndpointElement this[string name] { get { return (EndpointElement)base.BaseGet(name); } }
        public EndpointElement this[int index]
        {
            get { return (EndpointElement)base.BaseGet(index); }
            set { if (BaseGet(index) != null) { BaseRemoveAt(index); } BaseAdd(index, value); }
        }

        protected override ConfigurationElement CreateNewElement() { return new EndpointElement(); }
        protected override object GetElementKey(ConfigurationElement element) { return ((EndpointElement)element).Name; }
        protected override string ElementName { get { return "Endpoint"; } }
        protected override bool IsElementName(string elementName) { return (elementName == "Endpoint"); }

        public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMap; } }
    }

    public class EnvironmentDbConnection
    {
        public DbEnvironmentConfig EnvironmentConfig { get; private set; }

        public EnvironmentDbConnection(DbEnvironmentConfig environment_config) { EnvironmentConfig = environment_config; }

        public string GetConnectionString(string connection_name, Func<string, string, string> get_pswd)
        {
            if (string.IsNullOrEmpty(connection_name))
                throw new ArgumentException("Missing Connection Name");

            DatabaseConnectionElement dc_elm = EnvironmentConfig.DataEnvironment[ConfigurationManager.AppSettings["DataEnvironment"]].DatabaseConnection[connection_name];
            if (!string.IsNullOrEmpty(dc_elm.TisamUid))
            {
                return string.Format(dc_elm.ConnectionString, get_pswd(dc_elm.TisamUid, dc_elm.TKey));
            }
            return dc_elm.ConnectionString;
        }

        public string GetConnectionString(string connection_name)
        {
            if (string.IsNullOrEmpty(connection_name))
                throw new ArgumentException("Missing Connection Name");

            DatabaseConnectionElement dc_elm = EnvironmentConfig.DataEnvironment[ConfigurationManager.AppSettings["DataEnvironment"]].DatabaseConnection[connection_name];
            return dc_elm.ConnectionString;
        }

    }
}