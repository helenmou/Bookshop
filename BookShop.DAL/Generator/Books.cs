﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BookShop.Model;
using BookShop.Utility;

namespace BookShop.DAL
{
	public partial class BooksDAL
	{
        #region 向数据库中添加一条记录 +int Insert(Books model)
        /// <summary>
        /// 向数据库中添加一条记录
        /// </summary>
        /// <param name="model">要添加的实体</param>
        /// <returns>插入数据的ID</returns>
        public int Insert(Books model)
        {
            #region SQL语句
            const string sql = @"
INSERT INTO [dbo].[Books] (
	[Title]
	,[Author]
	,[PublisherId]
	,[PublishDate]
	,[ISBN]
	,[WordsCount]
	,[UnitPrice]
	,[ContentDescription]
	,[AurhorDescription]
	,[EditorComment]
	,[TOC]
	,[CategoryId]
	,[Clicks]
)
VALUES (
	@Title
	,@Author
	,@PublisherId
	,@PublishDate
	,@ISBN
	,@WordsCount
	,@UnitPrice
	,@ContentDescription
	,@AurhorDescription
	,@EditorComment
	,@TOC
	,@CategoryId
	,@Clicks
);select @@IDENTITY";
            #endregion
            var res = SqlHelper.ExecuteScalar(sql,
					new SqlParameter("@Title", model.Title),					
					new SqlParameter("@Author", model.Author),					
					new SqlParameter("@PublisherId", model.PublisherId),					
					new SqlParameter("@PublishDate", model.PublishDate),					
					new SqlParameter("@ISBN", model.ISBN),					
					new SqlParameter("@WordsCount", model.WordsCount),					
					new SqlParameter("@UnitPrice", model.UnitPrice),					
					new SqlParameter("@ContentDescription", model.ContentDescription),					
					new SqlParameter("@AurhorDescription", model.AurhorDescription),					
					new SqlParameter("@EditorComment", model.EditorComment),					
					new SqlParameter("@TOC", model.TOC),					
					new SqlParameter("@CategoryId", model.CategoryId),					
					new SqlParameter("@Clicks", model.Clicks)					
                );
            return res == null ? 0 : Convert.ToInt32(res);
        }
        #endregion

        #region 删除一条记录 +int Delete(int id)
        public int Delete(int id)
        {
            const string sql = "DELETE FROM [dbo].[Books] WHERE [Id] = @Id";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@Id", id));
        }
        #endregion

        #region 根据主键ID更新一条记录 +int Update(Books model)
        /// <summary>
        /// 根据主键ID更新一条记录
        /// </summary>
        /// <param name="model">更新后的实体</param>
        /// <returns>执行结果受影响行数</returns>
        public int Update(Books model)
        {
            #region SQL语句
            const string sql = @"
UPDATE [dbo].[Books]
SET 
	[Title] = @Title
	,[Author] = @Author
	,[PublisherId] = @PublisherId
	,[PublishDate] = @PublishDate
	,[ISBN] = @ISBN
	,[WordsCount] = @WordsCount
	,[UnitPrice] = @UnitPrice
	,[ContentDescription] = @ContentDescription
	,[AurhorDescription] = @AurhorDescription
	,[EditorComment] = @EditorComment
	,[TOC] = @TOC
	,[CategoryId] = @CategoryId
	,[Clicks] = @Clicks
WHERE [Id] = @Id";
            #endregion
            return SqlHelper.ExecuteNonQuery(sql,
					new SqlParameter("@Id", model.Id),					
					new SqlParameter("@Title", model.Title),					
					new SqlParameter("@Author", model.Author),					
					new SqlParameter("@PublisherId", model.PublisherId),					
					new SqlParameter("@PublishDate", model.PublishDate),					
					new SqlParameter("@ISBN", model.ISBN),					
					new SqlParameter("@WordsCount", model.WordsCount),					
					new SqlParameter("@UnitPrice", model.UnitPrice),					
					new SqlParameter("@ContentDescription", model.ContentDescription),					
					new SqlParameter("@AurhorDescription", model.AurhorDescription),					
					new SqlParameter("@EditorComment", model.EditorComment),					
					new SqlParameter("@TOC", model.TOC),					
					new SqlParameter("@CategoryId", model.CategoryId),					
					new SqlParameter("@Clicks", model.Clicks)					
                );
        }
        #endregion

        #region 分页查询一个集合 +IEnumerable<Books> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
        /// <summary>
        /// 分页查询一个集合
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">页大小</param>
        /// <param name="wheres">条件匿名类</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isDesc">是否降序排序</param>
        /// <returns>实体集合</returns>
        public IEnumerable<Books> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
        {
            var parameters = new List<SqlParameter>();
            var whereBuilder = new System.Text.StringBuilder();
            if (wheres != null)
            {
                var props = wheres.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.Name.Equals("__o", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // 操作符
                        whereBuilder.AppendFormat(" {0} ", prop.GetValue(wheres, null).ToString());
                    }
                    else
                    {
                        var val = prop.GetValue(wheres, null).ToString();
                        whereBuilder.AppendFormat(" [{0}] = @{0} ", prop.Name);
                        parameters.Add(new SqlParameter("@" + prop.Name, val));
                    }
                }
            }
            var sql = SqlHelper.GenerateQuerySql("Books", new[] { "Id", "Title", "Author", "PublisherId", "PublishDate", "ISBN", "WordsCount", "UnitPrice", "ContentDescription", "AurhorDescription", "EditorComment", "TOC", "CategoryId", "Clicks" }, index, size, whereBuilder.ToString(), orderField, isDesc);
            using (var reader = SqlHelper.ExecuteReader(sql, parameters.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        yield return SqlHelper.MapEntity<Books>(reader);
                    }
                }
            }
        }
        #endregion

        #region 查询单个模型实体 +Books QuerySingle(object wheres)
        /// <summary>
        /// 查询单个模型实体
        /// </summary>
        /// <param name="wheres">条件匿名类</param>
        /// <returns>实体</returns>
        public Books QuerySingle(object wheres)
        {
            var list = QueryList(1, 1, wheres, null);
            return list != null && list.Any() ? list.FirstOrDefault() : null;
        }
        #endregion

        #region 查询单个模型实体 +Books QuerySingle(int id)
        /// <summary>
        /// 查询单个模型实体
        /// </summary>
        /// <param name="id">key</param>
        /// <returns>实体</returns>
        public Books QuerySingle(int id)
        {
            const string sql = "SELECT TOP 1 [Id], [Title], [Author], [PublisherId], [PublishDate], [ISBN], [WordsCount], [UnitPrice], [ContentDescription], [AurhorDescription], [EditorComment], [TOC], [CategoryId], [Clicks] FROM [dbo].[Books] WHERE [Id] = @Id";
            using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@Id", id)))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    return SqlHelper.MapEntity<Books>(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 查询条数 +int QueryCount(object wheres)
        /// <summary>
        /// 查询条数
        /// </summary>
        /// <param name="wheres">查询条件</param>
        /// <returns>条数</returns>
        public int QueryCount(object wheres)
        {
            var parameters = new List<SqlParameter>();
            var whereBuilder = new System.Text.StringBuilder();
            if (wheres != null)
            {
                var props = wheres.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.Name.Equals("__o", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // 操作符
                        whereBuilder.AppendFormat(" {0} ", prop.GetValue(wheres, null).ToString());
                    }
                    else
                    {
                        var val = prop.GetValue(wheres, null).ToString();
                        whereBuilder.AppendFormat(" [{0}] = @{0} ", prop.Name);
                        parameters.Add(new SqlParameter("@" + prop.Name, val));
                    }
                }
            }
            var sql = SqlHelper.GenerateQuerySql("Books", new[] { "COUNT(1)" }, whereBuilder.ToString(), string.Empty);
            var res = SqlHelper.ExecuteScalar(sql, parameters.ToArray());
            return res == null ? 0 : Convert.ToInt32(res);
        }
        #endregion
        
    }
}