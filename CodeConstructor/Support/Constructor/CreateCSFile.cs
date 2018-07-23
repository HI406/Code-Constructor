using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeConstructor.Support.Constructor
{
    public class CreateCSFile
    {
        public CreateCSFile() { }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="path">保存路径</param>
        /// <param name="fileName">文件名(含后缀名)</param>
        public void Write(string str, string path, string fileName)
        {
            if (!Directory.Exists(path))
            {//如果路径不存在则创建
                Directory.CreateDirectory(path);
            }
            path += "\\" + fileName;
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                //开始写入
                sw.Write(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create File Error>>>>>>" + ex.ToString());
                throw ex;
            }
            finally
            {
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
        }
    }
}
