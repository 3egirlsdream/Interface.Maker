using SqlSugar;

namespace Job.Common
{
    [SugarTable("INFORMATION_SCHEMA.COLUMNS")]
    public class Models
    {
        public string EXTRA { get; set; }
        public string DATA_TYPE { get; set; }
        public string PRIVILEGES { get; set; }
        public string COLUMN_KEY { get; set; }
        public string TABLE_NAME { get; set; }
        public string COLUMN_TYPE { get; set; }
        public string COLUMN_NAME { get; set; }
        public string IS_NULLABLE { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_CATALOG { get; set; }
        public string NUMERIC_SCALE { get; set; }
        public string COLUMN_DEFAULT { get; set; }
        public string COLUMN_COMMENT { get; set; }
        public string COLLATION_NAME { get; set; }
        public string ORDINAL_POSITION { get; set; }
        public string NUMERIC_PRECISION { get; set; }
        public string DATETIME_PRECISION { get; set; }
        public string CHARACTER_SET_NAME { get; set; }
        public string CHARACTER_OCTET_LENGTH { get; set; }
        public string CHARACTER_MAXIMUM_LENGTH { get; set; }
    }

    [SugarTable("INFORMATION_SCHEMA.COLUMNS")]
    public class INFORMATION_SCHEMA
    {
        [SugarColumn(IsIgnore = true)]
        public string EXTRA { get; set; }
        public string DATA_TYPE { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string PRIVILEGES { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string COLUMN_KEY { get; set; }
        public string TABLE_NAME { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string COLUMN_TYPE { get; set; }
        public string COLUMN_NAME { get; set; }
        public string IS_NULLABLE { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_CATALOG { get; set; }
        public string NUMERIC_SCALE { get; set; }
        public string COLUMN_DEFAULT { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string COLUMN_COMMENT { get; set; }
        public string COLLATION_NAME { get; set; }
        public string ORDINAL_POSITION { get; set; }
        public string NUMERIC_PRECISION { get; set; }
        public string DATETIME_PRECISION { get; set; }
        public string CHARACTER_SET_NAME { get; set; }
        public string CHARACTER_OCTET_LENGTH { get; set; }
        //[SugarColumn(ColumnName = "COLUMN_TYPE")]
        public string CHARACTER_MAXIMUM_LENGTH { get; set; }
    }


    public class USER_TAB_COLUMNS
    {
        public string DATA_TYPE { get; set; }
        
        public string TABLE_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE_MOD { get; set; }
        public string DATA_TYPE_OWNER { get; set; }
        public string DATA_LENGTH { get; set; }
        public string DATA_PRECISION { get; set; }
        public string DATA_SCALE { get; set; }
        public string NULLABLE { get; set; }
        public string COLUMN_ID { get; set; }
        public string DEFAULT_LENGTH { get; set; }
        public string DATA_DEFAULT { get; set; }
        public string NUM_DISTINCT { get; set; }
        public string LOW_VALUE { get; set; }
    }

}