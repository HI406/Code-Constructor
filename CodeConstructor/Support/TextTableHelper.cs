using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CodeConstructor.Support
{
    public class TextTableHelper
    {
        public TextTableHelper() { }

        public string Generate(string str)
        {
            return "";
        }

        public string PaddingLeft(string str, int maxLength, char paddingChar = ' ')
        {
            str = str.PadLeft(GetPaddingCharCount(str, maxLength), paddingChar);
            return str;
        }
        public string PaddingRight(string str, int maxLength, char paddingChar = ' ')
        {
            str = str.PadRight(GetPaddingCharCount(str, maxLength), paddingChar);
            return str;
        }

        private int GetPaddingCharCount(string str, int maxLength)
        {
            int currentByteLength = Encoding.Default.GetByteCount(str);
            if (currentByteLength > maxLength)
            {
                return 0;
            }
            int currentLength = str.Length;
            int zhCount = currentByteLength - currentLength;
            int enCount = 2 * currentLength - currentByteLength;
            int paddingCharCount = maxLength - currentByteLength + currentLength;
            return paddingCharCount;
        }

        public string ExportDataTableToTxt(DataTable dt)
        {
            if (dt.Rows.Count <= 0) return "";
            int[] columnsMaxLength = new int[dt.Columns.Count];
            foreach (DataRow dr in dt.Rows)
            {
                for (int jhsfn = 0; jhsfn < dr.ItemArray.Length; jhsfn++)
                {
                    int textLength = Encoding.Default.GetBytes(dr.ItemArray[jhsfn].ToString().Trim()).Length;

                    textLength = (Convert.ToBoolean(textLength & 1) ? textLength + 1 : textLength) / 2;
                    columnsMaxLength[jhsfn] = columnsMaxLength[jhsfn] > textLength ? columnsMaxLength[jhsfn] : textLength;
                }
            }
            StringBuilder sb = new StringBuilder();
            string strRow = "┌";
            foreach (int jj in columnsMaxLength)
            {
                strRow += "─".PadRight(jj, '─');
                strRow += "┬";
            }
            strRow = strRow.TrimEnd('┬') + "┐"; sb.AppendLine(strRow);
            strRow = "│";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                int e = Math.Abs(Encoding.Default.GetBytes(dt.Columns[i].ColumnName.Trim()).Length - (dt.Columns[i].ColumnName.Trim().Length * 2));
                int z = dt.Columns[i].ColumnName.Trim().Length - e;
                //e = e * 2;
                int textPadRightLen = z + e + (columnsMaxLength[i] * 2 - (2 * z) - e);
                textPadRightLen = textPadRightLen < columnsMaxLength[i] ? columnsMaxLength[i] : textPadRightLen;
                strRow += dt.Columns[i].ColumnName.Trim().PadRight(textPadRightLen, ' ') + "│";
            } sb.AppendLine(strRow);

            strRow = "├";
            foreach (int jj in columnsMaxLength)
            {
                strRow += "─".PadRight(jj, '─');
                strRow += "┼";
            }
            strRow = strRow.TrimEnd('┼') + "┤"; sb.AppendLine(strRow);
            foreach (DataRow dr in dt.Rows)
            {
                strRow = "│";
                for (int i = 0; i < dr.ItemArray.Length; i++)
                {
                    int e = Math.Abs(Encoding.Default.GetBytes(dr.ItemArray[i].ToString().Trim()).Length - (dr.ItemArray[i].ToString().Trim().Length * 2));
                    int z = dr.ItemArray[i].ToString().Trim().Length - e;
                    //e = e * 2;
                    int textPadRightLen = z + e + (columnsMaxLength[i] * 2 - (2 * z) - e);
                    textPadRightLen = textPadRightLen < columnsMaxLength[i] ? columnsMaxLength[i] : textPadRightLen;
                    strRow += dr.ItemArray[i].ToString().Trim().PadRight(textPadRightLen, ' ') + "│";//　
                    //strRow += "│";
                }
                sb.AppendLine(strRow);

                strRow = "├";
                foreach (int jj in columnsMaxLength)
                {
                    strRow += "─".PadRight(jj, '─');
                    strRow += "┼";
                }
                strRow = strRow.TrimEnd('┼') + "┤"; sb.AppendLine(strRow);
            }
            strRow = "│";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                int e = Math.Abs(Encoding.Default.GetBytes(dt.Columns[i].ColumnName.Trim()).Length - (dt.Columns[i].ColumnName.Trim().Length * 2));
                int z = dt.Columns[i].ColumnName.Trim().Length - e;
                //e = e * 2;
                int textPadRightLen = z + e + (columnsMaxLength[i] * 2 - (2 * z) - e);
                textPadRightLen = textPadRightLen < columnsMaxLength[i] ? columnsMaxLength[i] : textPadRightLen;
                strRow += dt.Columns[i].ColumnName.Trim().PadRight(textPadRightLen, ' ') + "│";
            } sb.AppendLine(strRow);
            strRow = "└";
            foreach (int jj in columnsMaxLength)
            {
                strRow += "─".PadRight(jj, '─');
                strRow += "┴";
            }
            strRow = strRow.TrimEnd('┴') + "┘"; sb.AppendLine(strRow);
            return sb.ToString();
        }
    }
}
