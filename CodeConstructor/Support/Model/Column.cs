using System;
using System.Data;

namespace CodeConstructor.Support.Model
{
    /// <summary>
    /// 实体类 Column
    /// </summary>
    public class Column
    {
        public Column(){}

        #region Model
        private string _tableName;
        private string _tableDescription;
        private string _index;
        private string _name;
        private string _isIdentity;
        private string _isPrimaryKey;
        private string _sqlType;
        private string _length;
        private string _precision;
        private string _scale;
        private string _allowNull;
        private string _defaultValue;
        private string _description;

        /// <summary>
        ///表名
        /// </summary>
        public string tableName
        {
            set { _tableName = value; }
            get { return _tableName; }
        }
        /// <summary>
        ///表说明
        /// </summary>
        public string tableDescription
        {
            set { _tableDescription = value; }
            get { return _tableDescription; }
        }
        /// <summary>
        ///序号
        /// </summary>
        public string index
        {
            set { _index = value; }
            get { return _index; }
        }
        /// <summary>
        ///字段名
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        ///是否标识列（1：是；0：否）
        /// </summary>
        public string isIdentity
        {
            set { _isIdentity = value; }
            get { return _isIdentity; }
        }
        /// <summary>
        ///是否主键（1：是；0：否）
        /// </summary>
        public string isPrimaryKey
        {
            set { _isPrimaryKey = value; }
            get { return _isPrimaryKey; }
        }
        /// <summary>
        ///字段类型
        /// </summary>
        public string sqlType
        {
            set { _sqlType = value; }
            get { return _sqlType; }
        }
        /// <summary>
        ///字段长度
        /// </summary>
        public string length
        {
            set { _length = value; }
            get { return _length; }
        }
        /// <summary>
        ///占用字节数
        /// </summary>
        public string precision
        {
            set { _precision = value; }
            get { return _precision; }
        }
        /// <summary>
        ///小数位数
        /// </summary>
        public string scale
        {
            set { _scale = value; }
            get { return _scale; }
        }
        /// <summary>
        ///允许为空（1：是；0：否）
        /// </summary>
        public string allowNull
        {
            set { _allowNull = value; }
            get { return _allowNull; }
        }
        /// <summary>
        ///默认值
        /// </summary>
        public string defaultValue
        {
            set { _defaultValue = value; }
            get { return _defaultValue; }
        }
        /// <summary>
        ///字段说明
        /// </summary>
        public string description
        {
            set { _description = value; }
            get { return _description; }
        }
        #endregion Model

        /// <summary>
        /// 初始化 Model
        /// </summary>
        /// <param name="dr"></param>
        public void Initialize(DataRow dr, string tableType = "U")
        {
            _tableName = dr["tableName"].ToString();
            if (!string.IsNullOrEmpty(tableType) && tableType == "U")
            {
                _tableDescription = dr["tableDescription"].ToString();
            }
            _index = dr["index"].ToString();
            _name = dr["name"].ToString();
            _isIdentity = dr["isIdentity"].ToString();
            _isPrimaryKey = dr["isPrimaryKey"].ToString();
            _sqlType = dr["sqlType"].ToString();
            _length = dr["length"].ToString();
            _precision = dr["precision"].ToString();
            _scale = dr["scale"].ToString();
            _allowNull = dr["allowNull"].ToString();
            _defaultValue = dr["defaultValue"].ToString();
            _description = dr["description"].ToString();
        }

        /// <summary>
        /// 初始化 Model
        /// </summary>
        /// <param name="data"></param>
        public void Initialize(string[] data, string tableType = "U")
        {
            _tableName = data[0].ToString();
            if (!string.IsNullOrEmpty(tableType) && tableType == "U")
            {
                _tableDescription = data[1].ToString();
            }
            _index = data[2].ToString();
            _name = data[3].ToString();
            _isIdentity = data[4].ToString();
            _isPrimaryKey = data[5].ToString();
            _sqlType = data[6].ToString();
            _length = data[7].ToString();
            _precision = data[8].ToString();
            _scale = data[9].ToString();
            _allowNull = data[10].ToString();
            _defaultValue = data[11].ToString();
            _description = data[12].ToString();
        }

        /// <summary>
        /// 初始化 Model
        /// </summary>
        /// <param name="dataStr">初始化字符串（以英文逗号 ， 分隔）</param>
        public void Initialize(string dataStr)
        {
            Initialize(dataStr.Split(','));
        }
    }
}
