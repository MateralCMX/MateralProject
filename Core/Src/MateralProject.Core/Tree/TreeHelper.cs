using System;
using System.Collections.Generic;
using System.Linq;

namespace MateralProject.Core.Tree
{
    public class TreeHelper
    {
        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <param name="allData">所有API权限</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <returns></returns>
        public static List<T1> GetTreeList<T1, T2, T3>(List<T2> allData, T3? parentID = null) where T1 : ITreeModel<T1, T3>, new() where T2 : ITreeDomain<T3> where T3 : struct
        {
            var result = new List<T1>();
            List<T2> data = allData.Where(m => m.ParentID.Equals(parentID)).ToList();
            foreach (T2 item in data)
            {
                result.Add(new T1
                {
                    ID = item.ID,
                    Name = item.Name,
                    Child = GetTreeList<T1, T2, T3>(allData, item.ID)
                });
            }
            return result;
        }

        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <param name="allData">所有API权限</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <param name="getNewT1"></param>
        /// <returns></returns>
        public static List<T1> GetTreeList<T1, T2, T3>(List<T2> allData, T3? parentID, Func<T2,T1> getNewT1) where T1 : ITreeModel<T1, T3>, new() where T2 : ITreeDomain<T3> where T3 : struct
        {
            var result = new List<T1>();
            List<T2> data = allData.Where(m => m.ParentID.Equals(parentID)).ToList();
            foreach (T2 item in data)
            {
                T1 temp = getNewT1(item);
                temp.Child = GetTreeList<T1, T2, T3>(allData, item.ID, getNewT1);
                result.Add(temp);
            }
            return result;
        }
        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <param name="allData">所有API权限</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <returns></returns>
        public static List<T1> GetTreeListByAttribute<T1, T2, T3>(List<T2> allData, T3? parentID = null) where T1 : ITreeModel<T1, T3>, new() where T3 : struct
        {
            //var result = new List<T1>();
            //List<T2> data = allData.Where(m => m.ParentID.Equals(parentID)).ToList();
            //foreach (T2 item in data)
            //{
            //    result.Add(new T1
            //    {
            //        ID = item.ID,
            //        Name = item.Name,
            //        Child = GetTreeList<T1, T2, T3>(allData, item.ID)
            //    });
            //}
            //return result;
            return null;
        }

        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <param name="allData">所有API权限</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <param name="getNewT1"></param>
        /// <returns></returns>
        public static List<T1> GetTreeListByAttribute<T1, T2, T3>(List<T2> allData, T3? parentID, Func<T2, T1> getNewT1) where T1 : ITreeModel<T1, T3>, new() where T3 : struct
        {
            //var result = new List<T1>();
            //List<T2> data = allData.Where(m => m.ParentID.Equals(parentID)).ToList();
            //foreach (T2 item in data)
            //{
            //    T1 temp = getNewT1(item);
            //    temp.Child = GetTreeList<T1, T2, T3>(allData, item.ID, getNewT1);
            //    result.Add(temp);
            //}
            //return result;
            return null;
        }
    }
}
